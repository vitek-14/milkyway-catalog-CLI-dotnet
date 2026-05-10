using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    /// <summary>
    /// Represents an exoplanet item in list output.
    /// </summary>
    public class ExoplanetListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
    }
}
