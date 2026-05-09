using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    public class CreateStarRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public SpectralClass SpectralClass { get; set; }
        public double Mass { get; set; }
        public double Age { get; set; }
    }
}
