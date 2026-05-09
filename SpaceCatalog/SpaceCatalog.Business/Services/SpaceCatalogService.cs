using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Business.Factories;
using SpaceCatalog.Business.Models;
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

        public List<StarSystemListItem> SearchStarSystems(string query)
        {
            var normalizedQuery = query.Trim().ToLower();

            using (var context = contextFactory())
            {
                return context.StarSystems
                    .AsNoTracking()
                    .Where(starSystem => starSystem.Name.ToLower().Contains(normalizedQuery))
                    .OrderBy(starSystem => starSystem.Name)
                    .Select(starSystem => new StarSystemListItem
                    {
                        Id = starSystem.Id,
                        Name = starSystem.Name
                    })
                    .ToList();
            }
        }

        public StarSystemDetail? GetStarSystemDetail(int starSystemId)
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

                return new StarSystemDetail
                {
                    Id = starSystem.Id,
                    Name = starSystem.Name,
                    DistanceLy = starSystem.DistanceLy,
                    Rektascenze = starSystem.Coordinates.Rectascension,
                    Deklinace = starSystem.Coordinates.Declination,
                    Stars = starSystem.Stars
                        .OrderBy(star => star.Name)
                        .Select(star => new StarListItem
                        {
                            Id = star.Id,
                            Name = star.Name,
                            SpectralClass = star.SpectralClass
                        })
                        .ToList(),
                    Exoplanets = starSystem.Exoplanets
                        .OrderBy(exoplanet => exoplanet.Name)
                        .Select(exoplanet => new ExoplanetListItem
                        {
                            Id = exoplanet.Id,
                            Name = exoplanet.Name,
                            Type = exoplanet.Type
                        })
                        .ToList()
                };
            }
        }

        public OperationResult CreateStarSystemWithMainStar(CreateStarSystemRequest request)
        {
            var systemName = request.SystemName.Trim();

            if (string.IsNullOrWhiteSpace(systemName))
            {
                return OperationResult.Fail("Nazev systemu je povinny.");
            }

            try
            {
                using (var context = contextFactory())
                {
                    var nameExists = context.StarSystems
                        .Any(starSystem => starSystem.Name.ToLower() == systemName.ToLower());

                    if (nameExists)
                    {
                        return OperationResult.Fail("System s timto nazvem jiz existuje.");
                    }

                    var starSystem = spaceObjectFactory.CreateStarSystemWithMainStar(request);

                    context.StarSystems.Add(starSystem);
                    context.SaveChanges();

                    var mainStar = starSystem.Stars.First();

                    return OperationResult.Ok($"System '{starSystem.Name}' byl zalozen. ID systemu: {starSystem.Id}, ID hvezdy: {mainStar.Id}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResult.Fail("System se nepodarilo ulozit.");
            }
        }

        public OperationResult CreateExoplanetForStar(int starId, CreateExoplanetRequest request)
        {
            try
            {
                using (var context = contextFactory())
                {
                    var star = context.Stars.FirstOrDefault(existingStar => existingStar.Id == starId);

                    if (star == null)
                    {
                        return OperationResult.Fail("Hvezda neexistuje.");
                    }

                    if (star.StarSystemId == null)
                    {
                        return OperationResult.Fail("Hvezda neni prirazena k hvezdnemu systemu.");
                    }

                    var exoplanet = spaceObjectFactory.CreateExoplanet(request, star.StarSystemId.Value);
                    exoplanet.StarExoplanets.Add(new StarExoplanet
                    {
                        StarId = star.Id,
                        Exoplanet = exoplanet
                    });

                    context.Exoplanets.Add(exoplanet);
                    context.SaveChanges();

                    return OperationResult.Ok($"Exoplaneta '{exoplanet.Name}' byla ulozena. ID exoplanety: {exoplanet.Id}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResult.Fail("Exoplanetu se nepodarilo ulozit.");
            }
        }

        public ExoplanetEditModel? GetExoplanetForEdit(int exoplanetId)
        {
            using (var context = contextFactory())
            {
                var exoplanet = context.Exoplanets
                    .AsNoTracking()
                    .Include(existingExoplanet => existingExoplanet.StarExoplanets)
                    .FirstOrDefault(existingExoplanet => existingExoplanet.Id == exoplanetId);

                if (exoplanet == null)
                {
                    return null;
                }

                return new ExoplanetEditModel
                {
                    Id = exoplanet.Id,
                    Name = exoplanet.Name,
                    Type = exoplanet.Type,
                    CurrentStarId = exoplanet.StarExoplanets.FirstOrDefault()?.StarId
                };
            }
        }

        public OperationResult UpdateExoplanet(UpdateExoplanetRequest request)
        {
            try
            {
                using (var context = contextFactory())
                {
                    var exoplanet = context.Exoplanets
                        .Include(existingExoplanet => existingExoplanet.StarExoplanets)
                        .FirstOrDefault(existingExoplanet => existingExoplanet.Id == request.ExoplanetId);

                    if (exoplanet == null)
                    {
                        return OperationResult.Fail("Exoplaneta nenalezena.");
                    }

                    Star? newStar = null;
                    int? newStarSystemId = null;

                    if (request.NewStarId != null)
                    {
                        newStar = context.Stars.FirstOrDefault(star => star.Id == request.NewStarId.Value);

                        if (newStar == null)
                        {
                            return OperationResult.Fail("Nova hvezda neexistuje. Puvodni vazba zustala beze zmeny.");
                        }

                        if (newStar.StarSystemId == null)
                        {
                            return OperationResult.Fail("Nova hvezda neni prirazena k hvezdnemu systemu.");
                        }

                        newStarSystemId = newStar.StarSystemId.Value;
                    }

                    exoplanet.Name = request.Name.Trim();
                    exoplanet.Type = request.Type;

                    if (newStar != null)
                    {
                        context.StarExoplanets.RemoveRange(exoplanet.StarExoplanets);
                        context.StarExoplanets.Add(new StarExoplanet
                        {
                            StarId = newStar.Id,
                            ExoplanetId = exoplanet.Id
                        });
                        exoplanet.StarSystemId = newStarSystemId.GetValueOrDefault();
                    }

                    context.SaveChanges();

                    return OperationResult.Ok("Exoplaneta byla aktualizovana.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while saving entity to the database. Error type: {ex}; message: {ex.Message}");
                return OperationResult.Fail("Exoplanetu se nepodarilo aktualizovat.");
            }
        }
    }
}
