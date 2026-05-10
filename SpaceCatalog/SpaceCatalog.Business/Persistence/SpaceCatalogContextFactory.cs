using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Data;

namespace SpaceCatalog.Business.Persistence
{
    /// <summary>
    /// Creates database contexts for the space catalog.
    /// </summary>
    public static class SpaceCatalogContextFactory
    {
        /// <summary>
        /// Creates a factory for database contexts.
        /// </summary>
        /// <returns>A database context factory.</returns>
        public static Func<MyDbContext> CreateContextFactory()
        {
            return CreateContext;
        }

        /// <summary>
        /// Creates a configured database context.
        /// </summary>
        /// <returns>A configured database context.</returns>
        public static MyDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            var dbPath = GetDatabasePath();

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new MyDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Gets the database file path.
        /// </summary>
        /// <returns>The database file path.</returns>
        private static string GetDatabasePath()
        {
            return Path.Combine(AppContext.BaseDirectory, "spacecatalog.db");
        }
    }
}
