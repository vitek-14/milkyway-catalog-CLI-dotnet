using SpaceCatalog.ConsoleUI.Infrastructure;

namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Represents a placeholder command.
    /// </summary>
    public class NotImplementedCommand : ICommand
    {
        private readonly string commandName;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotImplementedCommand"/> class.
        /// </summary>
        /// <param name="commandName">The command name.</param>
        public NotImplementedCommand(string commandName)
        {
            this.commandName = commandName;
        }

        public string Name => commandName;

        /// <summary>
        /// Executes the placeholder command.
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("[INFO]: Tato akce neni v ramci vybranych pripadu uziti implementovana.");
            ConsoleInput.WaitForEnter();
        }
    }
}
