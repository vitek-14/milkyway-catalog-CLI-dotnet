using SpaceCatalog.Business.Services;
using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class ShowStarSystemDetailCommand : ICommand
    {
        private readonly ISpaceCatalogService spaceCatalogService;

        public ShowStarSystemDetailCommand(ISpaceCatalogService spaceCatalogService)
        {
            this.spaceCatalogService = spaceCatalogService;
        }

        public string Name => "Zobrazit detail hvezdneho systemu";

        public void Execute()
        {
            ConsoleRenderer.PrintHeader("DETAIL HVEZDNEHO SYSTEMU");

            while (true)
            {
                var starSystemId = ConsoleInput.ReadIntOrCancel("Zadejte ID systemu (0 = zpet do hlavniho menu): ");
                if (starSystemId == null)
                {
                    return;
                }

                var detail = spaceCatalogService.GetStarSystemDetail(starSystemId.Value);

                if (detail == null)
                {
                    Console.WriteLine("[CHYBA]: Neplatne ID systemu. Zadejte jine ID, nebo 0 pro navrat do hlavniho menu.");
                    continue;
                }

                Console.WriteLine($"ID: {detail.Id}");
                Console.WriteLine($"Nazev: {detail.Name}");
                Console.WriteLine($"Vzdalenost: {detail.DistanceLy} ly");
                Console.WriteLine($"Souradnice: {detail.Rectascension} / {detail.Declination}");
                Console.WriteLine($"Pocet exoplanet: {detail.Exoplanets.Count}");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Hvezdy:");

                foreach (var star in detail.Stars)
                {
                    Console.WriteLine($"{star.Id}: {star.Name} | spektralni trida: {star.SpectralClass}");
                }

                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Exoplanety:");

                foreach (var exoplanet in detail.Exoplanets)
                {
                    Console.WriteLine($"{exoplanet.Id}: {exoplanet.Name} | typ: {exoplanet.Type}");
                }

                ConsoleInput.WaitForEnter();
                return;
            }
        }
    }
}
