using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.UserControls.Wizard;

namespace AutoRobo.Core.UserControls.DTS
{
    public partial class Welcome : WizardPage
    {
        public Welcome()
        {
            InitializeComponent();
            SetActive += new CancelEventHandler(Welcome_SetActive);
        }
        /// <summary>
        /// 初始化导航按钮状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Welcome_SetActive(object sender, CancelEventArgs e)
        {
            SetWizardButtons(WizardButtons.Next);
        }

        public override void Prepare(){
            WizardContext context = null;
            if (csvRadioBtn.Checked)
            {
                context = new CsvContext();                
            }
            else if (sqlserverRadioBtn.Checked)
            {
                context = new MsSqlContext();
            }
            else if (excelRadioBtn.Checked) {
                context = new ExcelContext();
            }
            context.InitailizeWizardPages();
            context.Pages.Insert(0, this);
            GetWizard().Context = context;
            DTSWizardBook wizardBook = GetWizard() as DTSWizardBook;
            context.Data = wizardBook.Session.DataSource;
        }
     
    }
}
