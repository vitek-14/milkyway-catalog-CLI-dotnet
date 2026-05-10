using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Business.Factories;
using SpaceCatalog.Business.Dto;
using SpaceCatalog.Data;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Business.Services
{
    /// <summary>
    /// Provides catalog operations for space objects.
    /// </summary>
    public class SpaceCatalogService : ISpaceCatalogService
    {
        private readonly Func<MyDbContext> contextFactory;
        private readonly ISpaceObjectFactory spaceObjectFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceCatalogService"/> class.
        /// </summary>
        /// <param name="contextFactory">The database context factory.</param>
        /// <param name="spaceObjectFactory">The space object factory.</param>
        public SpaceCatalogService(Func<MyDbContext> contextFactory, ISpaceObjectFactory spaceObjectFactory)
        {
            this.contextFactory = contextFactory;
            this.spaceObjectFactory = spaceObjectFactory;
        }

        /// <summary>
        /// Searches star systems by name.
        /// </summary>
        /// <param name="query">The search text.</param>
        /// <returns>Matching star systems.</returns>
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

        /// <summary>
        /// Gets detailed data for a star system.
        /// </summary>
        /// <param name="starSystemId">The star system identifier.</param>
        /// <returns>The star system detail, or null when not found.</returns>
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

        /// <summary>
        /// Creates a star system with its main star.
        /// </summary>
        /// <param name="request">The creation request.</param>
        /// <returns>The operation result.</returns>
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

        /// <summary>
        /// Creates an exoplanet for a star.
        /// </summary>
        /// <param name="starId">The star identifier.</param>
        /// <param name="request">The creation request.</param>
        /// <returns>The operation result.</returns>
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

        /// <summary>
        /// Gets exoplanet data for editing.
        /// </summary>
        /// <param name="exoplanetId">The exoplanet identifier.</param>
        /// <returns>The edit model, or null when not found.</returns>
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

        /// <summary>
        /// Updates an exoplanet.
        /// </summary>
        /// <param name="request">The update request.</param>
        /// <returns>The operation result.</returns>
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
