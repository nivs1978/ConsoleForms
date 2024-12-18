namespace ConsoleForms
{
    internal class ConsoleState
    {
        private static int cursorLeft;
        private static int cursorTop;
        private static bool cursorVisible;
        private static ConsoleColor backgroundColor;
        private static ConsoleColor foregroundColor;

        public static void Save()
        {
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            cursorVisible = Console.CursorVisible;
            backgroundColor = Console.BackgroundColor;
            foregroundColor = Console.ForegroundColor;
        }

        public static void Restore()
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop;
            Console.CursorVisible = cursorVisible;
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

    }
}
