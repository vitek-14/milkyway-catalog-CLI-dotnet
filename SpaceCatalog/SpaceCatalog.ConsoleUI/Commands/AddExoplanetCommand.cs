using SpaceCatalog.Business.Dto;
using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;
using SpaceCatalog.Domain.DataTypes;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Handles adding a new exoplanet.
    /// </summary>
    public class AddExoplanetCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddExoplanetCommand"/> class.
        /// </summary>
        /// <param name="spaceCatalogService">The space catalog service.</param>
        public AddExoplanetCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Evidovat novou exoplanetu";

        /// <summary>
        /// Executes the add exoplanet workflow.
        /// </summary>
        public void Execute()
        {
            ConsoleRenderer.PrintHeader("PRIDAT EXOPLANETU");

            var starId = ConsoleInput.ReadIntOrCancel("Zadejte ID cilove hvezdy (0 = zpet): ");
            if (starId == null)
            {
                return;
            }

            var name = ConsoleInput.ReadRequiredStringOrCancel("Nazev exoplanety (0 = zpet): ");
            if (name == null)
            {
                return;
            }

            var type = ConsoleInput.ReadEnumOrCancel<ExoplanetType>("Typ exoplanety (0 = zpet): ");
            if (type == null)
            {
                return;
            }

            var mass = ConsoleInput.ReadDoubleOrCancel("Hmotnost (0 = zpet): ");
            if (mass == null)
            {
                return;
            }

            var orbitTime = ConsoleInput.ReadDoubleOrCancel("Doba obehu (0 = zpet): ");
            if (orbitTime == null)
            {
                return;
            }

            var request = new CreateExoplanetRequestDto
            {
                Name = name,
                Type = type.Value,
                Mass = mass.Value,
                OrbitTime = orbitTime.Value
            };

            var result = spaceCatalogService.CreateExoplanetForStar(starId.Value, request);
            Console.WriteLine(result.Success ? "[INFO]: " + result.Message : "[CHYBA]: " + result.Message);
            ConsoleInput.WaitForEnter();
        }
    }
}
