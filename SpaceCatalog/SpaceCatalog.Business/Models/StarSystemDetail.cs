namespace SpaceCatalog.Business.Models
{
    public class StarSystemDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rektascenze { get; set; } = string.Empty;
        public string Deklinace { get; set; } = string.Empty;
        public List<StarListItem> Stars { get; set; } = new();
        public List<ExoplanetListItem> Exoplanets { get; set; } = new();
    }
}
