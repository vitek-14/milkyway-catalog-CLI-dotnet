using SpaceCatalog.Business.Dto;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Business.Factories
{
    /// <summary>
    /// Defines factory methods for space domain objects.
    /// </summary>
    public interface ISpaceObjectFactory
    {
        /// <summary>
        /// Creates a star system with its main star.
        /// </summary>
        /// <param name="request">The creation request.</param>
        /// <returns>The created star system.</returns>
        StarSystem CreateStarSystemWithMainStar(CreateStarSystemRequestDto request);

        /// <summary>
        /// Creates an exoplanet for a star system.
        /// </summary>
        /// <param name="request">The creation request.</param>
        /// <param name="starSystemId">The star system identifier.</param>
        /// <returns>The created exoplanet.</returns>
        Exoplanet CreateExoplanet(CreateExoplanetRequestDto request, int starSystemId);
    }
}
