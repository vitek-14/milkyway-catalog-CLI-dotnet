using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    /// <summary>
    /// Represents a nebula and its related stars.
    /// </summary>
    public class Nebula : EntityBase
    {
        public NebulaType Type { get; set; }
        public double DistanceLy { get; set; }
        public Coordinates Coordinates { get; set; } = new();
        public ICollection<Star> Stars { get; set; } = new List<Star>();
    }
}
