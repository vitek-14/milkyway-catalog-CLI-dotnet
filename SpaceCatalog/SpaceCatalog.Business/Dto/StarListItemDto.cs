using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    public class StarListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SpectralClass SpectralClass { get; set; }
    }
}
