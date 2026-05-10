namespace SpaceCatalog.Domain.DataTypes
{
    /// <summary>
    /// Stores celestial coordinates.
    /// </summary>
    public sealed class Coordinates
    {
        public string Rectascension { get; set; } = string.Empty;
        public string Declination { get; set; } = string.Empty;
    }
}
