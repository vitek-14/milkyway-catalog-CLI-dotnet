namespace SpaceCatalog.ConsoleUI.Commands
{
    public class MenuInvoker
    {
        private readonly Dictionary<string, ICommand> commands = new();

        public void Register(string menuChoice, ICommand command)
        {
            commands[menuChoice] = command;
        }

        public bool ExecuteCommand(string menuChoice)
        {
            if (commands.TryGetValue(menuChoice, out var command))
            {
                command.Execute();
                return true;
            }

            return false;
        }
    }
}
