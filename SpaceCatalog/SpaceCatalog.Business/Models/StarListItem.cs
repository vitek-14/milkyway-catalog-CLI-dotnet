using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Models
{
    public class StarListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SpectralClass SpectralClass { get; set; }
    }
}
