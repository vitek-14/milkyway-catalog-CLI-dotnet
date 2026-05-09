using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    public class NotImplementedCommand : ICommand
    {
        private readonly string commandName;

        public NotImplementedCommand(string commandName)
        {
            this.commandName = commandName;
        }

        public string Name => commandName;

        public void Execute()
        {
            Console.WriteLine("[INFO]: Tato akce neni v ramci vybranych pripadu uziti implementovana.");
            ConsoleInput.WaitForEnter();
        }
    }
}
