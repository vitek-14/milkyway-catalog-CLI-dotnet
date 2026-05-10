using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    /// <summary>
    /// Represents a star system with stars and exoplanets.
    /// </summary>
    public class StarSystem : EntityBase
    {
        public double DistanceLy { get; set; }
        public Coordinates Coordinates { get; set; } = new();
        public ICollection<Star> Stars { get; set; } = new List<Star>();
        public ICollection<Exoplanet> Exoplanets { get; set; } = new List<Exoplanet>();
    }
}
