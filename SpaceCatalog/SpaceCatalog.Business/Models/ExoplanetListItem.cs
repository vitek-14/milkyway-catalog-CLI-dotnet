using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Models
{
    public class ExoplanetListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
    }
}
