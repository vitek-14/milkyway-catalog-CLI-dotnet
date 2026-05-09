using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class SearchMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        public SearchMenuCommand(SearchStarSystemCommand searchStarSystemCommand)
        {
            menuInvoker.Register("1", searchStarSystemCommand);
            menuInvoker.Register("2", new NotImplementedCommand("Vyhledat hvezdu"));
            menuInvoker.Register("3", new NotImplementedCommand("Vyhledat exoplanetu"));
            menuInvoker.Register("4", new NotImplementedCommand("Vyhledat mlhovinu"));
        }

        public string Name => "Vyhledat";

        public void Execute()
        {
            while (true)
            {
                ConsoleRenderer.PrintHeader("VYHLEDAT");
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
