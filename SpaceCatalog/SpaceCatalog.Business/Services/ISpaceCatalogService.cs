using SpaceCatalog.Business.Dto;

namespace SpaceCatalog.Business.Services
{
    /// <summary>
    /// Defines catalog operations for space objects.
    /// </summary>
    public interface ISpaceCatalogService
    {
        /// <summary>
        /// Searches star systems by name.
        /// </summary>
        /// <param name="query">The search text.</param>
        /// <returns>Matching star systems.</returns>
        List<StarSystemListItemDto> SearchStarSystems(string query);

        /// <summary>
        /// Gets detailed data for a star system.
        /// </summary>
        /// <param name="starSystemId">The star system identifier.</param>
        /// <returns>The star system detail, or null when not found.</returns>
        StarSystemDetailDto? GetStarSystemDetail(int starSystemId);

        /// <summary>
        /// Creates a star system with its main star.
        /// </summary>
        /// <param name="request">The creation request.</param>
        /// <returns>The operation result.</returns>
        OperationResultDto CreateStarSystemWithMainStar(CreateStarSystemRequestDto request);

        /// <summary>
        /// Creates an exoplanet for a star.
        /// </summary>
        /// <param name="starId">The star identifier.</param>
        /// <param name="request">The creation request.</param>
        /// <returns>The operation result.</returns>
        OperationResultDto CreateExoplanetForStar(int starId, CreateExoplanetRequestDto request);

        /// <summary>
        /// Gets exoplanet data for editing.
        /// </summary>
        /// <param name="exoplanetId">The exoplanet identifier.</param>
        /// <returns>The edit model, or null when not found.</returns>
        ExoplanetEditModelDto? GetExoplanetForEdit(int exoplanetId);

        /// <summary>
        /// Updates an exoplanet.
        /// </summary>
        /// <param name="request">The update request.</param>
        /// <returns>The operation result.</returns>
        OperationResultDto UpdateExoplanet(UpdateExoplanetRequestDto request);
    }
}
