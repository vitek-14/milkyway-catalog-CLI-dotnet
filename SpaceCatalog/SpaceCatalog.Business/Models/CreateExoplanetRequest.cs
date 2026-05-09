using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Models
{
    public class CreateExoplanetRequest
    {
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public double Mass { get; set; }
        public double OrbitTime { get; set; }
    }
}
