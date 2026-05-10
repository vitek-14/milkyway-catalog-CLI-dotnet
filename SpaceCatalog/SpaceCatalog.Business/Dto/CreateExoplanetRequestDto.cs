using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    /// <summary>
    /// Carries data required to create an exoplanet.
    /// </summary>
    public class CreateExoplanetRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public double Mass { get; set; }
        public double OrbitTime { get; set; }
    }
}
