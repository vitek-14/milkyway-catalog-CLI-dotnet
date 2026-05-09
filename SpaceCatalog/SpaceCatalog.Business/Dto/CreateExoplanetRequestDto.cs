using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    public class CreateExoplanetRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public double Mass { get; set; }
        public double OrbitTime { get; set; }
    }
}
