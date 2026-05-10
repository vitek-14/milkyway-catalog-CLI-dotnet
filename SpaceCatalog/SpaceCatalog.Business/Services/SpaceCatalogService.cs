using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Business.Factories;
using SpaceCatalog.Business.Dto;
using SpaceCatalog.Data;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Business.Services
{
    public class SpaceCatalogService : ISpaceCatalogService
    {
        private readonly Func<MyDbContext> contextFactory;
        private readonly ISpaceObjectFactory spaceObjectFactory;

        public SpaceCatalogService(Func<MyDbContext> contextFactory, ISpaceObjectFactory spaceObjectFactory)
        {
            this.contextFactory = contextFactory;
            this.spaceObjectFactory = spaceObjectFactory;
        }

        public List<StarSystemListItemDto> SearchStarSystems(string query)
        {
            var normalizedQuery = query.Trim().ToLower();

            using (var context = contextFactory())
            {
                return context.StarSystems
                    .AsNoTracking()
                    .Where(starSystem => starSystem.Name.ToLower().Contains(normalizedQuery))
                    .OrderBy(starSystem => starSystem.Name)
                    .Select(starSystem => new StarSystemListItemDto
                    {
                        Id = starSystem.Id,
                        Name = starSystem.Name
                    })
                    .ToList();
            }
        }

        public StarSystemDetailDto? GetStarSystemDetail(int starSystemId)
        {
            using (var context = contextFactory())
            {
                var starSystem = context.StarSystems
                    .AsNoTracking()
                    .Include(system => system.Stars)
                    .Include(system => system.Exoplanets)
                    .FirstOrDefault(system => system.Id == starSystemId);

                if (starSystem == null)
                {
                    return null;
                }

                return new StarSystemDetailDto
                {
                    Id = starSystem.Id,
                    Name = starSystem.Name,
                    DistanceLy = starSystem.DistanceLy,
                    Rectascension = starSystem.Coordinates.Rectascension,
                    Declination = starSystem.Coordinates.Declination,
                    Stars = starSystem.Stars
                        .OrderBy(star => star.Name)
                        .Select(star => new StarListItemDto
                        {
                            Id = star.Id,
                            Name = star.Name,
                            SpectralClass = star.SpectralClass
                        })
                        .ToList(),
                    Exoplanets = starSystem.Exoplanets
                        .OrderBy(exoplanet => exoplanet.Name)
                        .Select(exoplanet => new ExoplanetListItemDto
                        {
                            Id = exoplanet.Id,
                            Name = exoplanet.Name,
                            Type = exoplanet.Type
                        })
                        .ToList()
                };
            }
        }

        public OperationResultDto CreateStarSystemWithMainStar(CreateStarSystemRequestDto request)
        {
            var systemName = request.SystemName.Trim();

            if (string.IsNullOrWhiteSpace(systemName))
            {
                return OperationResultDto.Fail("Nazev systemu je povinny.");
            }

            try
            {
                using (var context = contextFactory())
                {
                    var nameExists = context.StarSystems
                        .Any(starSystem => starSystem.Name.ToLower() == systemName.ToLower());

                    if (nameExists)
                    {
                        return OperationResultDto.Fail("System s timto nazvem jiz existuje.");
                    }

                    var starSystem = spaceObjectFactory.CreateStarSystemWithMainStar(request);

                    context.StarSystems.Add(starSystem);
                    context.SaveChanges();

                    var mainStar = starSystem.Stars.First();

                    return OperationResultDto.Ok($"System '{starSystem.Name}' byl zalozen. ID systemu: {starSystem.Id}, ID hvezdy: {mainStar.Id}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResultDto.Fail("System se nepodarilo ulozit.");
            }
        }

        public OperationResultDto CreateExoplanetForStar(int starId, CreateExoplanetRequestDto request)
        {
            try
            {
                using (var context = contextFactory())
                {
                    var star = context.Stars.FirstOrDefault(existingStar => existingStar.Id == starId);

                    if (star == null)
                    {
                        return OperationResultDto.Fail("Hvezda neexistuje.");
                    }

                    if (star.StarSystemId == null)
                    {
                        return OperationResultDto.Fail("Hvezda neni prirazena k hvezdnemu systemu.");
                    }

                    var exoplanet = spaceObjectFactory.CreateExoplanet(request, star.StarSystemId.Value);
                    exoplanet.Stars.Add(star);

                    context.Exoplanets.Add(exoplanet);
                    context.SaveChanges();

                    return OperationResultDto.Ok($"Exoplaneta '{exoplanet.Name}' byla ulozena. ID exoplanety: {exoplanet.Id}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResultDto.Fail("Exoplanetu se nepodarilo ulozit.");
            }
        }

        public ExoplanetEditModelDto? GetExoplanetForEdit(int exoplanetId)
        {
            using (var context = contextFactory())
            {
                var exoplanet = context.Exoplanets
                    .AsNoTracking()
                    .Include(existingExoplanet => existingExoplanet.Stars)
                    .FirstOrDefault(existingExoplanet => existingExoplanet.Id == exoplanetId);

                if (exoplanet == null)
                {
                    return null;
                }

                return new ExoplanetEditModelDto
                {
                    Id = exoplanet.Id,
                    Name = exoplanet.Name,
                    Type = exoplanet.Type,
                    CurrentStarId = exoplanet.Stars.FirstOrDefault()?.Id
                };
            }
        }

        public OperationResultDto UpdateExoplanet(UpdateExoplanetRequestDto request)
        {
            try
            {
                using (var context = contextFactory())
                {
                    var exoplanet = context.Exoplanets
                        .Include(existingExoplanet => existingExoplanet.Stars)
                        .FirstOrDefault(existingExoplanet => existingExoplanet.Id == request.ExoplanetId);

                    if (exoplanet == null)
                    {
                        return OperationResultDto.Fail("Exoplaneta nenalezena.");
                    }

                    Star? newStar = null;
                    int? newStarSystemId = null;

                    if (request.NewStarId != null)
                    {
                        newStar = context.Stars.FirstOrDefault(star => star.Id == request.NewStarId.Value);

                        if (newStar == null)
                        {
                            return OperationResultDto.Fail("Nova hvezda neexistuje. Puvodni vazba zustala beze zmeny.");
                        }

                        if (newStar.StarSystemId == null)
                        {
                            return OperationResultDto.Fail("Nova hvezda neni prirazena k hvezdnemu systemu.");
                        }

                        newStarSystemId = newStar.StarSystemId.Value;
                    }

                    exoplanet.Name = request.Name.Trim();
                    exoplanet.Type = request.Type;

                    if (newStar != null)
                    {
                        exoplanet.Stars.Clear();
                        exoplanet.Stars.Add(newStar);
                        exoplanet.StarSystemId = newStarSystemId.GetValueOrDefault();
                    }

                    context.SaveChanges();

                    return OperationResultDto.Ok("Exoplaneta byla aktualizovana.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResultDto.Fail("Exoplanetu se nepodarilo aktualizovat.");
            }
        }
    }
}
