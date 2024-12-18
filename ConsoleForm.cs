using System.Diagnostics;
using System.Drawing;

namespace ConsoleForms
{
    public class ConsoleForm : Form
    {
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public event KeyEventHandler KeyPress;
        public event ConsoleResizeEventHandler ConsoleResize;

        public FormStartPosition StartPosition { get; set; }
        public Size ClientSize = new(30, 20);
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool Visible { get; set; } = false;
        public bool Focused { get; set; } = false;
        public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
        public FormBorderStyle BorderStyle { get; set; } = FormBorderStyle.Sizable;

        public ConsoleColor TitlebarBackColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor TitlebarForeColor { get; set; } = ConsoleColor.Black;

        public ConsoleColor BackColor { get; set; } = ConsoleColor.DarkBlue;
        public ConsoleColor ForeColor { get; set; } = ConsoleColor.White;

        private Size OriginalClientSize;
        private Point OriginalLocation;

        private bool isDragging = false;
        private Point dragStartPoint = Point.Empty;
        private Point formStartPoint = Point.Empty;
        private object paintLock = new object();

        public ConsoleForm()
        {
            this.FormClosed += new FormClosedEventHandler(ConsoleForm_FormClosed);
            this.MouseClick += HandleMouseClick;
            this.MouseMove += HandleMouseMove;
            this.MouseDown += HandleMouseDown;
            this.MouseUp += HandleMouseUp;
            this.KeyPress += HandleKeyPress;
            this.ConsoleResize += HandleConsoleResize;
        }
        public void OnMouseClick(MouseButtons button, int x, int y)
        {
            MouseClick?.Invoke(this, new MouseEventArgs(button, new Point(x, y)));
        }

        public void OnMouseDown(MouseButtons button, int x, int y)
        {
            MouseDown?.Invoke(this, new MouseEventArgs(button, new Point(x, y)));
        }

        public void OnMouseMove(MouseButtons button, int x, int y)
        {
            MouseMove?.Invoke(this, new MouseEventArgs(button, new Point(x, y)));
        }

        public void OnMouseUp(MouseButtons button, int x, int y)
        {
            MouseUp?.Invoke(this, new MouseEventArgs(button, new Point(x, y)));
        }

        public void OnKeyPress(ConsoleKey key)
        {
            KeyPress?.Invoke(this, new KeyEventArgs(key));
        }

        public void OnConsoleResize(int width, int height)
        {
            ConsoleResize?.Invoke(this, new ConsoleResizeEventArgs(width, height));
        }

        private void ResetView()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        // Example method to handle mouse click
        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Location.X == Location.X + ClientSize.Width - 1 && e.Location.Y == Location.Y)
            {
                Visible = false;
                OnFormClosed(new FormClosedEventArgs(CloseReason.UserClosing));
            }
            else if (e.Location.X == Location.X + ClientSize.Width - 3 && e.Location.Y == Location.Y) // Maximize/Restore button
            {
                ResetView();
                if (WindowState == FormWindowState.Normal)
                {
                    WindowState = FormWindowState.Maximized;
                    OriginalClientSize = ClientSize;
                    OriginalLocation = Location;
                    Location = new Point(0, 0);
                    ClientSize = new Size(Console.WindowWidth, Console.WindowHeight);
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    Location = OriginalLocation;
                    ClientSize = OriginalClientSize;
                }
                OnPaint();
            }
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("HandleMouseDown");
            if (e.Button == MouseButtons.Left)
            {
                if (e.Location.X >= Location.X && e.Location.X < Location.X + ClientSize.Width - 3 && e.Location.Y == Location.Y)
                {
                    if (WindowState == FormWindowState.Normal)
                    {
                        Debug.WriteLine("Dragg start");
                        isDragging = true;
                        dragStartPoint = e.Location;
                        formStartPoint = Location;
                    }
                }
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("Mouse move");
            if (isDragging)
            {
                Debug.WriteLine("Dragging");
                Point newLocation = new Point(
                    formStartPoint.X + e.Location.X - dragStartPoint.X,
                    formStartPoint.Y + e.Location.Y - dragStartPoint.Y);
                // Only update the location if it's within the bounds of the console window
                if (newLocation.X >= 0
                    && newLocation.X + ClientSize.Width < Console.WindowWidth
                    && newLocation.Y >= 0
                    && newLocation.Y + ClientSize.Height < Console.WindowHeight)
                {
                    Location = newLocation;
                }

                ResetView();
                OnPaint();
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void HandleConsoleResize(object sender, ConsoleResizeEventArgs e)
        {
            ResetView();
            if (WindowState == FormWindowState.Maximized)
            {
                ClientSize = new Size(e.Width, e.Height);
            }
            OnPaint();
        }

        // Example method to handle key press
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            //Console.WriteLine($"Key pressed: {e.Key}");
        }

        public void Show()
        {
            Visible = true;
            OnPaint();
        }

        public void OnPaint()
        {
            lock (paintLock)
            {
                ConsoleState.Save();
                Console.CursorVisible = false;
                Console.CursorLeft = Location.X;
                Console.CursorTop = Location.Y;
                int titleWidth = Text.Length;
                var titleBarLeftWidth = (ClientSize.Width - titleWidth) / 2;
                var titleBarRightWidth = ClientSize.Width - titleWidth - titleBarLeftWidth - 3;
                Console.BackgroundColor = TitlebarBackColor;
                Console.ForegroundColor = TitlebarForeColor;
                Console.Write(new string(' ', titleBarLeftWidth));
                Console.Write(Text);
                Console.Write(new string(' ', titleBarRightWidth));
                Console.Write("■ X");
                Console.BackgroundColor = BackColor;
                Console.ForegroundColor = ForeColor;
                for (int y = 1; y < ClientSize.Height - 1; y++)
                {
                    int newCursorTop = Location.Y + y;
                    if (newCursorTop >= 0 && newCursorTop < Console.BufferHeight)
                    {
                        Console.CursorLeft = Location.X;
                        Console.CursorTop = newCursorTop;
                        Console.Write(new string(' ', ClientSize.Width));
                    }
                }

                foreach (var control in Controls)
                {
                    control.OnPaint();
                }

                ConsoleState.Restore();
            }
        }

        private void ConsoleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResetView();
        }
    }
}
