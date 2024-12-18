using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleForms
{
    public class GroupBox : Control
    {
        public ConsoleColor BorderColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        public override void OnPaint()
        {
            ConsoleState.Save();
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = BorderColor;

            char topLeft = '┌';
            char topRight = '┐';
            char bottomLeft = '└';
            char bottomRight = '┘';
            char horizontal = '─';
            char vertical = '│';

            // Get the global location
            var globalLocation = PointToScreen(this.Location);

            // Draw top border
            Console.SetCursorPosition(globalLocation.X, globalLocation.Y);
            Console.Write(topLeft);

            // Draw title with spaces and crop if necessary
            string title = " " + Text + " ";
            if (title.Length > Size.Width - 2)
            {
                title = title.Substring(0, Size.Width - 2);
            }

            Console.Write(title);
            Console.Write(new string(horizontal, Size.Width - 2 - title.Length));
            Console.Write(topRight);

            // Draw sides and content
            for (int y = 1; y < Size.Height - 1; y++)
            {
                Console.SetCursorPosition(globalLocation.X, globalLocation.Y + y);
                Console.Write(vertical + new string(' ', Size.Width - 2) + vertical);
            }

            // Draw bottom border
            Console.SetCursorPosition(globalLocation.X, globalLocation.Y + Size.Height - 1);
            Console.Write(bottomLeft + new string(horizontal, Size.Width - 2) + bottomRight);

            ConsoleState.Restore();

            base.OnPaint();
        }
    }
}
