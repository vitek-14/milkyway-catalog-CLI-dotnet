namespace SpaceCatalog.Domain.Base
{
    abstract public class EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
