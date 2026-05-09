using SpaceCatalog.Business.Models;
using SpaceCatalog.Domain;

namespace SpaceCatalog.Business.Factories
{
    public interface ISpaceObjectFactory
    {
        StarSystem CreateStarSystemWithMainStar(CreateStarSystemRequest request);
        Exoplanet CreateExoplanet(CreateExoplanetRequest request, int starSystemId);
    }
}
