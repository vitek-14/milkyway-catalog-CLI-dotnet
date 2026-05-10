using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    /// <summary>
    /// Carries data required to create a star.
    /// </summary>
    public class CreateStarRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public SpectralClass SpectralClass { get; set; }
        public double Mass { get; set; }
        public double Age { get; set; }
    }
}
