using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Models
{
    public class UpdateExoplanetRequest
    {
        public int ExoplanetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public int? NewStarId { get; set; }
    }
}
