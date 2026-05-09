using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class BrowseMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        public BrowseMenuCommand(ShowStarSystemDetailCommand showStarSystemDetailCommand)
        {
            menuInvoker.Register("1", showStarSystemDetailCommand);
            menuInvoker.Register("2", new NotImplementedCommand("Prohlizet hvezdu"));
            menuInvoker.Register("3", new NotImplementedCommand("Prohlizet exoplanetu"));
            menuInvoker.Register("4", new NotImplementedCommand("Prohlizet mlhovinu"));
        }

        public string Name => "Prohlizet";

        public void Execute()
        {
            while (true)
            {
                ConsoleRenderer.PrintHeader("PROHLIZET");
                Console.WriteLine("1. Hvezdny system");
                Console.WriteLine("2. Hvezdu");
                Console.WriteLine("3. Exoplanetu");
                Console.WriteLine("4. Mlhovinu");
                Console.WriteLine("5. Zpet do hlavniho menu");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Vase volba: ");

                var choice = Console.ReadLine()?.Trim();

                if (choice == "5")
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
