using SpaceCatalog.Domain.Base;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Domain
{
    public class Nebula : EntityBase
    {
        public NebulaType Type { get; set; }
        public double DistanceLy { get; set; }
        public Coordinates Coordinates { get; set; } = new();
        public ICollection<Star> Stars { get; set; } = new List<Star>();
    }
}
