using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class UpdateDataMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        public UpdateDataMenuCommand(UpdateExoplanetCommand updateExoplanetCommand)
        {
            menuInvoker.Register("1", new NotImplementedCommand("Upravit hvezdny system"));
            menuInvoker.Register("2", new NotImplementedCommand("Upravit hvezdu"));
            menuInvoker.Register("3", updateExoplanetCommand);
            menuInvoker.Register("4", new NotImplementedCommand("Upravit mlhovinu"));
        }

        public string Name => "Upravit";

        public void Execute()
        {
            while (true)
            {
                ConsoleRenderer.PrintHeader("UPRAVIT");
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
