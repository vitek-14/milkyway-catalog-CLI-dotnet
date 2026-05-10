using Microsoft.EntityFrameworkCore;
using SpaceCatalog.Data;

namespace SpaceCatalog.Business.Persistence
{
    /// <summary>
    /// Initializes the space catalog database.
    /// </summary>
    public static class SpaceCatalogDatabaseInitializer
    {
        private const string PopulateDatabaseScriptResourceName = "SpaceCatalog.Data.Sql.populate_db.sql";

        /// <summary>
        /// Ensures that the database exists and populates it when it has just been created.
        /// </summary>
        /// <param name="contextFactory">The database context factory.</param>
        public static void EnsureCreated(Func<MyDbContext> contextFactory)
        {
            using (var context = contextFactory())
            {
                var databaseCreated = context.Database.EnsureCreated();

                if (databaseCreated)
                {
                    ExecutePopulateDatabaseScript(context);
                }
            }
        }

        private static void ExecutePopulateDatabaseScript(MyDbContext context)
        {
            var assembly = typeof(MyDbContext).Assembly;

            using (var stream = assembly.GetManifestResourceStream(PopulateDatabaseScriptResourceName))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Database seed script resource '{PopulateDatabaseScriptResourceName}' was not found.");
                }

                using (var reader = new StreamReader(stream))
                {
                    var sql = reader.ReadToEnd();
                    context.Database.ExecuteSqlRaw(sql);
                }
            }
        }
    }
}
