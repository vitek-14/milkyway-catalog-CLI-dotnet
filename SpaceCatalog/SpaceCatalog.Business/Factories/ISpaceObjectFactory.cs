using SpaceCatalog.Business.Dto;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Business.Factories
{
    public interface ISpaceObjectFactory
    {
        StarSystem CreateStarSystemWithMainStar(CreateStarSystemRequestDto request);
        Exoplanet CreateExoplanet(CreateExoplanetRequestDto request, int starSystemId);
    }
}
