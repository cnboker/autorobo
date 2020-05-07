using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AutoRobo.Core.UserControls.Wizard
{
    public class WizardPageEventArgs : CancelEventArgs
    {
        public string NewPage { get; set; }
    }

    public delegate void WizardPageEventHandler(object sender, WizardPageEventArgs e);
}