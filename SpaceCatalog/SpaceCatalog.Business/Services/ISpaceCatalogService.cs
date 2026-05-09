using SpaceCatalog.Business.Dto;

namespace SpaceCatalog.Business.Services
{
    public interface ISpaceCatalogService
    {
        List<StarSystemListItemDto> SearchStarSystems(string query);
        StarSystemDetailDto? GetStarSystemDetail(int starSystemId);
        OperationResultDto CreateStarSystemWithMainStar(CreateStarSystemRequestDto request);
        OperationResultDto CreateExoplanetForStar(int starId, CreateExoplanetRequestDto request);
        ExoplanetEditModelDto? GetExoplanetForEdit(int exoplanetId);
        OperationResultDto UpdateExoplanet(UpdateExoplanetRequestDto request);
    }
}
