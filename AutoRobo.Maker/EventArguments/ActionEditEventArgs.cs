using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;
using System.Windows.Forms;

namespace AutoRobo.Makers.EventArguments
{
    public class ActionEditEventArgs : EventArgs
    {
        public DialogResult Result { get; set; }
        public ActionBase EditAction { get; set; }
    }
}
