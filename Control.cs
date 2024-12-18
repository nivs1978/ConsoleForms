using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ConsoleForms
{
    public class Control : Component
    {
        public string? Text { get; set; }
        public Control Parent { get; set; }
        public Point Location { get; set; } = Point.Empty;
        public Size Size { get; set; } = Size.Empty;
        public ControlCollection Controls { get; }

        public Control()
        {
            Controls = new ControlCollection(this);
        }

        public Point PointToScreen(Point clientPoint)
        {
            int globalX = clientPoint.X;
            int globalY = clientPoint.Y;
            Control current = this;

            while (current.Parent != null)
            {
                globalX += current.Parent.Location.X;
                globalY += current.Parent.Location.Y;
                current = current.Parent;
            }

            return new Point(globalX, globalY);
        }

        public virtual void OnPaint()
        {
            foreach (var control in Controls)
            {
                control.OnPaint();
            }
        }
    }
}
