namespace SpaceCatalog.Business.Models
{
    public class CreateStarSystemRequest
    {
        public string SystemName { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rektascenze { get; set; } = string.Empty;
        public string Deklinace { get; set; } = string.Empty;
        public CreateStarRequest MainStar { get; set; } = new();
    }
}
