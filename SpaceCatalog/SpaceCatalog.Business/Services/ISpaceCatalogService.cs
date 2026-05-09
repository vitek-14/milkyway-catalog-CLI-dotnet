using SpaceCatalog.Business.Models;

namespace SpaceCatalog.Business.Services
{
    public interface ISpaceCatalogService
    {
        List<StarSystemListItem> SearchStarSystems(string query);
        StarSystemDetail? GetStarSystemDetail(int starSystemId);
        OperationResult CreateStarSystemWithMainStar(CreateStarSystemRequest request);
        OperationResult CreateExoplanetForStar(int starId, CreateExoplanetRequest request);
        ExoplanetEditModel? GetExoplanetForEdit(int exoplanetId);
        OperationResult UpdateExoplanet(UpdateExoplanetRequest request);
    }
}
