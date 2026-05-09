using SpaceCatalog.Data;

namespace SpaceCatalog.Business.Persistence
{
    public static class SpaceCatalogDatabaseInitializer
    {
        public static void EnsureCreated(Func<MyDbContext> contextFactory)
        {
            using (var context = contextFactory())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
