using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    public class Star : EntityBase
    {
        public SpectralClass SpectralClass { get; set; }
        public double Mass { get; set; }
        public double Age { get; set; }
        public int? StarSystemId { get; set; }
        public StarSystem? StarSystem { get; set; }
        public int? NebulaId { get; set; }
        public Nebula? Nebula { get; set; }
        public ICollection<StarExoplanet> StarExoplanets { get; set; } = new List<StarExoplanet>();
    }
}
