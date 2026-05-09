using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class AdminDataMenuCommand : ICommand
    {
        private readonly MenuInvoker menuInvoker = new();

        public AdminDataMenuCommand(AddDataMenuCommand addDataMenuCommand, UpdateDataMenuCommand updateDataMenuCommand)
        {
            menuInvoker.Register("1", addDataMenuCommand);
            menuInvoker.Register("2", updateDataMenuCommand);
            menuInvoker.Register("3", new NotImplementedCommand("Smazat"));
        }

        public string Name => "Sprava dat";

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
