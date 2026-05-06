using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    public class StarSystem : EntityBase
    {
        public double DistanceLy { get; set; }
        public Coordinates Coordinates { get; set; } = new();
        public ICollection<Star> Stars { get; set; } = new List<Star>();
        public ICollection<Exoplanet> Exoplanets { get; set; } = new List<Exoplanet>();
    }
}
