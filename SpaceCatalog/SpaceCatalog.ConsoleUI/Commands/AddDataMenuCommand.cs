using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Displays the add data menu.
    /// </summary>
    public class AddDataMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDataMenuCommand"/> class.
        /// </summary>
        /// <param name="addStarSystemCommand">The add star system command.</param>
        /// <param name="addExoplanetCommand">The add exoplanet command.</param>
        public AddDataMenuCommand(AddStarSystemCommand addStarSystemCommand, AddExoplanetCommand addExoplanetCommand)
        {
            menuInvoker.Register("1", addStarSystemCommand);
            menuInvoker.Register("2", new NotImplementedCommand("Pridat hvezdu"));
            menuInvoker.Register("3", addExoplanetCommand);
            menuInvoker.Register("4", new NotImplementedCommand("Pridat mlhovinu"));
        }

        public string Name => "Pridat";

        /// <summary>
        /// Executes the add data menu.
        /// </summary>
        public void Execute()
        {
            while (true)
            {
                ConsoleRenderer.PrintHeader("PRIDAT");
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
