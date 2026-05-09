namespace SpaceCatalog.Business.Dto
{
    public class CreateStarSystemRequestDto
    {
        public string SystemName { get; set; } = string.Empty;
        public double DistanceLy { get; set; }
        public string Rektascenze { get; set; } = string.Empty;
        public string Deklinace { get; set; } = string.Empty;
        public CreateStarRequestDto MainStar { get; set; } = new();
    }
}
