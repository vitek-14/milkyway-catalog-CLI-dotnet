using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.Business.Dto
{
    public class ExoplanetEditModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ExoplanetType Type { get; set; }
        public int? CurrentStarId { get; set; }
    }
}
