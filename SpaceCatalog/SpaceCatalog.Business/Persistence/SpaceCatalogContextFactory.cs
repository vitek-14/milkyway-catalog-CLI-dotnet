using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Data;

namespace SpaceCatalog.Business.Persistence
{
    public static class SpaceCatalogContextFactory
    {
        public static Func<MyDbContext> CreateContextFactory()
        {
            return CreateContext;
        }

        public static MyDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            var dbPath = GetDatabasePath();

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new MyDbContext(optionsBuilder.Options);
        }

        private static string GetDatabasePath()
        {
            return Path.Combine(AppContext.BaseDirectory, "spacecatalog.db");
        }
    }
}
