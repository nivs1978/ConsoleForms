using System.Drawing;
using ConsoleForms;

public class MouseEventArgs : EventArgs
{
    public MouseButtons Button { get; }
    public Point Location { get; }

    public MouseEventArgs(MouseButtons button, Point location)
    {
        Button = button;
        Location = location;
    }
}

public class KeyEventArgs : EventArgs
{
    public ConsoleKey Key { get; }

    public KeyEventArgs(ConsoleKey key)
    {
        Key = key;
    }
}

public class ConsoleResizeEventArgs : EventArgs
{
    public int Width { get; }
    public int Height { get; }

    public ConsoleResizeEventArgs(int width, int height)
    {
        Width = width;
        Height = height;
    }
}

public delegate void MouseEventHandler(object sender, MouseEventArgs e);
public delegate void KeyEventHandler(object sender, KeyEventArgs e);
public delegate void ConsoleResizeEventHandler(object sender, ConsoleResizeEventArgs e);
