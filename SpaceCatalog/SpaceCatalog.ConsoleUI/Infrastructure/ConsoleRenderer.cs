namespace SpaceCatalog.ConsoleUI.Infrastructure
{
    /// <summary>
    /// Renders common console output.
    /// </summary>
    public static class ConsoleRenderer
    {
        /// <summary>
        /// Prints a formatted header.
        /// </summary>
        /// <param name="title">The header title.</param>
        public static void PrintHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("-------------------------------------------");
        }
    }
}
