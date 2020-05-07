using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.UserControls.Wizard;
using System.Data;
using System.Windows.Forms;

namespace AutoRobo.Core.UserControls.DTS
{

    public class DTSWizardBook : WizardBook
    {
        public DTSSession Session { get; set; }

        public DTSWizardBook() {            
            SetActivePage(new Welcome());
        }

        public DataTable Import() {
            Session = new DTSSession() { Direction = DTSDirection.Import};
            ShowDialog();
            if (Context != null)
            {
                return Context.Data;
            }
            return null;
        }

        public void Export(DataTable dt) {
            Session = new DTSSession() { Direction = DTSDirection.Export, DataSource = dt };
            ShowDialog();
        }
    }
}
