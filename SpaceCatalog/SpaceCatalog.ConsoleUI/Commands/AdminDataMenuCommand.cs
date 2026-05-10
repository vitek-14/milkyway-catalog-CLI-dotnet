using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Displays the administrative data menu.
    /// </summary>
    public class AdminDataMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminDataMenuCommand"/> class.
        /// </summary>
        /// <param name="addDataMenuCommand">The add data menu command.</param>
        /// <param name="updateDataMenuCommand">The update data menu command.</param>
        public AdminDataMenuCommand(AddDataMenuCommand addDataMenuCommand, UpdateDataMenuCommand updateDataMenuCommand)
        {
            menuInvoker.Register("1", addDataMenuCommand);
            menuInvoker.Register("2", updateDataMenuCommand);
            menuInvoker.Register("3", new NotImplementedCommand("Smazat"));
        }

        public string Name => "Sprava dat";

        /// <summary>
        /// Executes the administrative data menu.
        /// </summary>
        public void Execute()
        {
            while (true)
            {
                ConsoleRenderer.PrintHeader("MENU SPRAVY DAT");
                Console.WriteLine("1. Pridat");
                Console.WriteLine("2. Upravit");
                Console.WriteLine("3. Smazat");
                Console.WriteLine("4. Zpet do hlavniho menu");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Vase volba: ");

                var choice = Console.ReadLine()?.Trim();

                if (choice == "4")
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
