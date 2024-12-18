using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForms
{
    public partial class Form : Control
    {
        private static readonly object EVENT_FORMCLOSED = new();

        public event FormClosedEventHandler FormClosed
        {
            add => Events.AddHandler(EVENT_FORMCLOSED, value);
            remove => Events.RemoveHandler(EVENT_FORMCLOSED, value);
        }

        protected virtual void OnFormClosed(FormClosedEventArgs e)
        {
            var handler = (FormClosedEventHandler?)Events[EVENT_FORMCLOSED];
            handler?.Invoke(this, e);
        }
    }
}
