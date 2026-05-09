using SpaceCatalog.Business.Factories;
using SpaceCatalog.Business.Persistence;
using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Commands;

namespace SpaceCatalog.ConsoleUI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var contextFactory = SpaceCatalogContextFactory.CreateContextFactory();
            SpaceCatalogDatabaseInitializer.EnsureCreated(contextFactory);

            var factory = new SpaceObjectFactory();
            var service = new SpaceCatalogService(contextFactory, factory);

            var searchStarSystemCommand = new SearchStarSystemCommand(service);
            var showStarSystemDetailCommand = new ShowStarSystemDetailCommand(service);
            var addStarSystemCommand = new AddStarSystemCommand(service);
            var addExoplanetCommand = new AddExoplanetCommand(service);
            var updateExoplanetCommand = new UpdateExoplanetCommand(service);
            var searchMenuCommand = new SearchMenuCommand(searchStarSystemCommand);
            var browseMenuCommand = new BrowseMenuCommand(showStarSystemDetailCommand);
            var addDataMenuCommand = new AddDataMenuCommand(addStarSystemCommand, addExoplanetCommand);
            var updateDataMenuCommand = new UpdateDataMenuCommand(updateExoplanetCommand);
            var adminDataMenuCommand = new AdminDataMenuCommand(addDataMenuCommand, updateDataMenuCommand);

            var mainMenuInvoker = new MenuInvoker();
            mainMenuInvoker.Register("1", searchMenuCommand);
            mainMenuInvoker.Register("2", browseMenuCommand);
            mainMenuInvoker.Register("3", adminDataMenuCommand);

            RunMainMenu(mainMenuInvoker);
        }

        private static void RunMainMenu(MenuInvoker menuInvoker)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("    KATALOG OBJEKTU MLECNE DRAHY (v1.0)");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. Vyhledat");
                Console.WriteLine("2. Prohlizet");
                Console.WriteLine("3. Sprava dat (ADMIN)");
                Console.WriteLine("0. Konec");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Vase volba: ");

                var choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                {
                    return;
                }

                if (!menuInvoker.ExecuteCommand(choice ?? string.Empty))
                {
                    Console.WriteLine("[CHYBA]: Neplatna volba.");
                }
            }
        }
    }
}
