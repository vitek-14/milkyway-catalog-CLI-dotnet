using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    public class UpdateExoplanetRequestDto
    {
        public int ExoplanetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public int? NewStarId { get; set; }
    }
}
