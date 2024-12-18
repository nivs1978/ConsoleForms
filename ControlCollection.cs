using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForms
{
    public class ControlCollection : List<Control>
    {
        private readonly Control _owner;

        public ControlCollection(Control owner)
        {
            _owner = owner;
        }

        public new void Add(Control control)
        {
            control.Parent = _owner;
            base.Add(control);
        }
    }
}
