using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core
{
    public class RunningActionEventArgs : EventArgs
    {
        public ActionBase Action { get; set; }
        public int RunningRowIndex { get; set; }
    }
}
