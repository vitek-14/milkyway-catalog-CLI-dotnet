namespace SpaceCatalog.ConsoleUI.Commands
{
    /// <summary>
    /// Defines an executable console command.
    /// </summary>
    public interface ICommand
    {
        string Name { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();
    }
}
