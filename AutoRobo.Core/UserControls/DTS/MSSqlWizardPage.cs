using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.UserControls.Wizard;

namespace AutoRobo.Core.UserControls.DTS
{
    public partial class MSSqlWizardPage : WizardPage
    {
        public MSSqlWizardPage()
        {
            InitializeComponent();
            SetActive += new CancelEventHandler(ImportMSSql_SetActive);
        }

        void ImportMSSql_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Back | WizardButtons.Next);
        }
    }
}
