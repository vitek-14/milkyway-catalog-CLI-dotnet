namespace SpaceCatalog.Domain
{
    public class StarExoplanet
    {
        public int StarId { get; set; }
        public Star Star { get; set; } = null!;
        public int ExoplanetId { get; set; }
        public Exoplanet Exoplanet { get; set; } = null!;
    }
}
