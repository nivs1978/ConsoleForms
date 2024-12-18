using System.Diagnostics;

namespace ConsoleForms
{
    class Application
    {
        private static ConsoleForm? mainForm;
        private static bool mouseDown = false;
        private static int mouseDownX = 0;
        private static int mouseDownY = 0;
        private static int mouseX = 0;
        private static int mouseY = 0;
        private static DateTime mouseDownTime = DateTime.MinValue;
        private static int previousButtonState = 0;

        public static void Run(ConsoleForm mainConsoleForm)
        {
            //ConsoleListener.Initialize();
            ConsoleListener.MouseEvent += ConsoleListener_MouseEvent;
            ConsoleListener.KeyEvent += ConsoleListener_KeyEvent;
            ConsoleListener.ConsoleResizeEvent += (e) => mainForm.OnConsoleResize(e.Width, e.Height);
            mainForm = mainConsoleForm;
            mainForm.FormClosed += (sender, e) => Exit();
            mainForm.Show();
            ConsoleListener.Start();
        }

        private static void ConsoleListener_MouseEvent(NativeMethods.MOUSE_EVENT_RECORD r)
        {
            MouseButtons button = MouseButtons.None;

            switch (r.dwButtonState)
            {
                case 1:
                    button = MouseButtons.Left;
                    break;
                case 2:
                    button = MouseButtons.Right;
                    break;
                default:
                    break;
            }

            if (r.dwButtonState != previousButtonState)
            {
                if (r.dwButtonState == 1)
                {
                    Debug.WriteLine("Mouse Down");
                    mouseDown = true;
                    mouseDownTime = DateTime.UtcNow;
                    mainForm?.OnMouseDown(button, r.dwMousePosition.X, r.dwMousePosition.Y);
                }
                else if (previousButtonState == 1)
                {
                    Debug.WriteLine("Mouse Up");
                    mouseDown = false;
                    mainForm?.OnMouseUp(button, r.dwMousePosition.X, r.dwMousePosition.Y);
                    var duration = DateTime.UtcNow - mouseDownTime;
                    if (duration.TotalMilliseconds < 200)
                    {
                        mainForm?.OnMouseClick(button, r.dwMousePosition.X, r.dwMousePosition.Y);
                    }
                }

                previousButtonState = (int)r.dwButtonState;
            }

            if (mouseX != r.dwMousePosition.X || mouseY != r.dwMousePosition.Y)
            {
                mouseX = r.dwMousePosition.X;
                mouseY = r.dwMousePosition.Y;
                mainForm?.OnMouseMove(button, r.dwMousePosition.X, r.dwMousePosition.Y);
            }

        }

        private static void ConsoleListener_KeyEvent(NativeMethods.KEY_EVENT_RECORD r)
        {
            if (r.bKeyDown)
            {
                mainForm?.OnKeyPress((ConsoleKey)r.wVirtualKeyCode);
            }
        }

        public static void Exit(int exitCode = 0)
        {
            ConsoleListener.Stop();
            Environment.Exit(exitCode);
        }
    }
}
