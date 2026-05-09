namespace SpaceCatalog.Business.Dto
{
    public class StarSystemDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rektascenze { get; set; } = string.Empty;
        public string Deklinace { get; set; } = string.Empty;
        public List<StarListItemDto> Stars { get; set; } = new();
        public List<ExoplanetListItemDto> Exoplanets { get; set; } = new();
    }
}
