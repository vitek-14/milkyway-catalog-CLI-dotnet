using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Handles searching star systems.
    /// </summary>
    public class SearchStarSystemCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchStarSystemCommand"/> class.
        /// </summary>
        /// <param name="spaceCatalogService">The space catalog service.</param>
        public SearchStarSystemCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Vyhledat hvezdny system";

        /// <summary>
        /// Executes the star system search workflow.
        /// </summary>
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
