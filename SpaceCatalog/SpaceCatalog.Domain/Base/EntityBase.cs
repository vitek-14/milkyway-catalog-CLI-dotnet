namespace SpaceCatalog.Domain.Base
{
    /// <summary>
    /// Provides common identity and name fields for domain entities.
    /// </summary>
    abstract public class EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
