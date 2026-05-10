using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Displays the browse menu.
    /// </summary>
    public class BrowseMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseMenuCommand"/> class.
        /// </summary>
        /// <param name="showStarSystemDetailCommand">The show star system detail command.</param>
        public BrowseMenuCommand(ShowStarSystemDetailCommand showStarSystemDetailCommand)
        {
            menuInvoker.Register("1", showStarSystemDetailCommand);
            menuInvoker.Register("2", new NotImplementedCommand("Prohlizet hvezdu"));
            menuInvoker.Register("3", new NotImplementedCommand("Prohlizet exoplanetu"));
            menuInvoker.Register("4", new NotImplementedCommand("Prohlizet mlhovinu"));
        }

        public string Name => "Prohlizet";

        /// <summary>
        /// Executes the browse menu.
        /// </summary>
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
