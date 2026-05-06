using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    public class Exoplanet : EntityBase
    {
        public double Mass { get; set; }
        public double OrbitTime { get; set; }
        public ExoplanetType Type { get; set; }
        public int StarSystemId { get; set; }
        public StarSystem StarSystem { get; set; } = null!;
        public ICollection<StarExoplanet> StarExoplanets { get; set; } = new List<StarExoplanet>();
    }
}
