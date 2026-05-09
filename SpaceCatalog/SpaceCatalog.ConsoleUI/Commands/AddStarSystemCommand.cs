using SpaceCatalog.Business.Dto;
using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class AddStarSystemCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        public AddStarSystemCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Zalozit novy hvezdny system";

        public void Execute()
        {
            ConsoleRenderer.PrintHeader("ZAKLADANI NOVEHO SYSTEMU");

            var systemName = ConsoleInput.ReadRequiredStringOrCancel("Zadejte nazev systemu (0 = zpet): ");
            if (systemName == null)
            {
                return;
            }

            var rektascenze = ConsoleInput.ReadRequiredStringOrCancel("Zadejte rektascenzi (0 = zpet): ");
            if (rektascenze == null)
            {
                return;
            }

            var deklinace = ConsoleInput.ReadRequiredStringOrCancel("Zadejte deklinaci (0 = zpet): ");
            if (deklinace == null)
            {
                return;
            }

            var distanceLy = ConsoleInput.ReadDoubleOrCancel("Zadejte vzdalenost (ly, 0 = zpet): ");
            if (distanceLy == null)
            {
                return;
            }

            var request = new CreateStarSystemRequestDto
            {
                SystemName = systemName,
                Rektascenze = rektascenze,
                Deklinace = deklinace,
                DistanceLy = distanceLy.Value
            };

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("[INFO]: Udaje hvezdneho systemu nacteny.");
            ConsoleRenderer.PrintHeader("ZAKLADANI NOVE HVEZDY");

            var starName = ConsoleInput.ReadRequiredStringOrCancel("Nazev hvezdy (0 = zpet): ");
            if (starName == null)
            {
                return;
            }

            var starMass = ConsoleInput.ReadDoubleOrCancel("Hmotnost (v hmot. Slunce, 0 = zpet): ");
            if (starMass == null)
            {
                return;
            }

            var starAge = ConsoleInput.ReadDoubleOrCancel("Stari (0 = zpet): ");
            if (starAge == null)
            {
                return;
            }

            var spectralClass = ConsoleInput.ReadSpectralClassOrCancel("Spektralni trida (O,B,A,F,G,K,M, 0 = zpet): ");
            if (spectralClass == null)
            {
                return;
            }

            request.MainStar = new CreateStarRequestDto
            {
                Name = starName,
                Mass = starMass.Value,
                Age = starAge.Value,
                SpectralClass = spectralClass.Value
            };

            var result = spaceCatalogService.CreateStarSystemWithMainStar(request);
            Console.WriteLine(result.Success ? "[INFO]: " + result.Message : "[CHYBA]: " + result.Message);
            ConsoleInput.WaitForEnter();
        }
    }
}
