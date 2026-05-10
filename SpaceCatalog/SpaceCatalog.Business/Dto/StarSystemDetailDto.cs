namespace SpaceCatalog.Business.Dto
{
    public class StarSystemDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rectascension { get; set; } = string.Empty;
        public string Declination { get; set; } = string.Empty;
        public List<StarListItemDto> Stars { get; set; } = new();
        public List<ExoplanetListItemDto> Exoplanets { get; set; } = new();
    }
}
