namespace SpaceCatalog.Business.Dto
{
    public class CreateStarSystemRequestDto
    {
        public string SystemName { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rectascension { get; set; } = string.Empty;
        public string Declination { get; set; } = string.Empty;
        public CreateStarRequestDto MainStar { get; set; } = new();
    }
}
