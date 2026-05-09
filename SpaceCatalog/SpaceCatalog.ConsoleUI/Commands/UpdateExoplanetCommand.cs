using SpaceCatalog.Business.Models;
using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class UpdateExoplanetCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        public UpdateExoplanetCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Upravit exoplanetu";

        public void Execute()
        {
            ConsoleRenderer.PrintHeader("UPRAVIT EXOPLANETU");

            var exoplanetId = ConsoleInput.ReadIntOrCancel("Zadejte ID exoplanety (0 = zpet): ");
            if (exoplanetId == null)
            {
                return;
            }

            var current = spaceCatalogService.GetExoplanetForEdit(exoplanetId.Value);

            if (current == null)
            {
                Console.WriteLine("[CHYBA]: Exoplaneta nenalezena.");
                ConsoleInput.WaitForEnter();
                return;
            }

            Console.WriteLine($"Aktualni nazev: {current.Name}");
            Console.WriteLine($"Aktualni typ: {current.Type}");
            Console.WriteLine($"Aktualni ID hvezdy: {current.CurrentStarId?.ToString() ?? "neni nastaveno"}");
            Console.WriteLine("-------------------------------------------");

            var newName = ConsoleInput.ReadOptionalString("Novy nazev (ENTER ponecha stavajici): ");
            var newType = ConsoleInput.ReadOptionalEnum("Novy typ (ENTER ponecha stavajici): ", current.Type);
            var newStarId = ConsoleInput.ReadNullableInt("Nove ID materske hvezdy (ENTER ponecha stavajici): ");

            var request = new UpdateExoplanetRequest
            {
                ExoplanetId = current.Id,
                Name = string.IsNullOrWhiteSpace(newName) ? current.Name : newName,
                Type = newType,
                NewStarId = newStarId
            };

            var result = spaceCatalogService.UpdateExoplanet(request);
            Console.WriteLine(result.Success ? "[INFO]: " + result.Message : "[CHYBA]: " + result.Message);
            ConsoleInput.WaitForEnter();
        }
    }
}
