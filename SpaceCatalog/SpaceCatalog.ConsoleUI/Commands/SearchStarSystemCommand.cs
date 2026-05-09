using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class SearchStarSystemCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        public SearchStarSystemCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Vyhledat hvezdny system";

        public void Execute()
        {
            ConsoleRenderer.PrintHeader("VYHLEDAT HVEZDNY SYSTEM");
            var query = ConsoleInput.ReadRequiredStringOrCancel("Zadejte nazev nebo jeho cast (0 = zpet): ");
            if (query == null)
            {
                return;
            }

            var systems = spaceCatalogService.SearchStarSystems(query);

            if (systems.Count == 0)
            {
                Console.WriteLine("[INFO]: System nebyl nalezen.");
                ConsoleInput.WaitForEnter();
                return;
            }

            Console.WriteLine("Nalezene systemy:");
            foreach (var system in systems)
            {
                Console.WriteLine($"{system.Id}: {system.Name}");
            }

            ConsoleInput.WaitForEnter();
        }
    }
}
