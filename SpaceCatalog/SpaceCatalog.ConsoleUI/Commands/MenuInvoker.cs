namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Registers and executes menu commands.
    /// </summary>
    public class MenuInvoker
    {
        private readonly Dictionary<string, ICommand> commands = new();

        /// <summary>
        /// Registers a command for a menu choice.
        /// </summary>
        /// <param name="menuChoice">The menu choice key.</param>
        /// <param name="command">The command to register.</param>
        public void Register(string menuChoice, ICommand command)
        {
            commands[menuChoice] = command;
        }

        /// <summary>
        /// Executes a registered command.
        /// </summary>
        /// <param name="menuChoice">The menu choice key.</param>
        /// <returns>True when a command was executed; otherwise false.</returns>
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
