namespace SpaceCatalog.ConsoleUI.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
    }
}
