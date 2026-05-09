namespace SpaceCatalog.ConsoleUI.Infrastructure
{
    public static class ConsoleRenderer
    {
        public static void PrintHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("-------------------------------------------");
        }
    }
}
