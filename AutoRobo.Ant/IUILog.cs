using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.ClientLib
{
    public interface IUILog
    {
        ToolStripStatusLabel Writer { get;  }
        void Write(string message);
    }
}
