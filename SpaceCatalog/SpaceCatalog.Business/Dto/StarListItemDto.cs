using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    /// <summary>
    /// Represents a star item in list output.
    /// </summary>
    public class StarListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SpectralClass SpectralClass { get; set; }
    }
}
