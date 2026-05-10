using SpaceCatalog.Business.Dto;
using SpaceCatalog.Domain;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Factories
{
    public class SpaceObjectFactory : ISpaceObjectFactory
    {
        public StarSystem CreateStarSystemWithMainStar(CreateStarSystemRequestDto request)
        {
            var starSystem = new StarSystem
            {
                Name = request.SystemName.Trim(),
                DistanceLy = request.DistanceLy,
                Coordinates = new Coordinates
                {
                    Rectascension = request.Rectascension.Trim(),
                    Declination = request.Declination.Trim()
                }
            };

            var mainStar = new Star
            {
                Name = request.MainStar.Name.Trim(),
                Mass = request.MainStar.Mass,
                Age = request.MainStar.Age,
                SpectralClass = request.MainStar.SpectralClass,
                StarSystem = starSystem
            };

            starSystem.Stars.Add(mainStar);

            return starSystem;
        }

        public Exoplanet CreateExoplanet(CreateExoplanetRequestDto request, int starSystemId)
        {
            return new Exoplanet
            {
                Name = request.Name.Trim(),
                Mass = request.Mass,
                OrbitTime = request.OrbitTime,
                Type = request.Type,
                StarSystemId = starSystemId
            };
        }
    }
}
