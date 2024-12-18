using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleForms
{
    public class FormClosedEventArgs : EventArgs
    {
        public FormClosedEventArgs(CloseReason closeReason)
        {
            CloseReason = closeReason;
        }

        /// <summary>
        ///  Provides the reason for the Form Close.
        /// </summary>
        public CloseReason CloseReason { get; }
    }
}
