using SpaceCatalog.Data;

namespace SpaceCatalog.Business.Persistence
{
    /// <summary>
    /// Initializes the space catalog database.
    /// </summary>
    public static class SpaceCatalogDatabaseInitializer
    {
        /// <summary>
        /// Ensures that the database exists.
        /// </summary>
        /// <param name="contextFactory">The database context factory.</param>
        public static void EnsureCreated(Func<MyDbContext> contextFactory)
        {
            using (var context = contextFactory())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
