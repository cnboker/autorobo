using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.UserControls.Wizard
{
    public partial class WizardPage : UserControl
    {
        
        public WizardPage()
        {
            InitializeComponent();            
        }
  
        public WizardContext GetContext() {
            return GetWizard().Context;
        }
      
        [Category("Wizard")]
        public event CancelEventHandler SetActive;

        public virtual void OnSetActive(CancelEventArgs e)
        {
            Focus();            
            if (SetActive != null)
                SetActive(this, e);
        }
        protected WizardBook GetWizard()
        {
            WizardBook wizard = (WizardBook)ParentForm;
            return wizard;
        }

        protected void SetWizardButtons(WizardButtons buttons)
        {
            GetWizard().SetWizardButtons(buttons);
        }

        protected void EnableCancelButton(bool enableCancelButton)
        {
            GetWizard().EnableCancelButton(enableCancelButton);
        }

        protected void PressButton(WizardButtons buttons)
        {
            GetWizard().PressButton(buttons);
        }

        [Category("Wizard")]
        public event WizardPageEventHandler WizardNext;

        public virtual void OnWizardNext(WizardPageEventArgs e)
        {
            if (WizardNext != null)
                WizardNext(this, e);
        }

        [Category("Wizard")]
        public event WizardPageEventHandler WizardBack;

        public virtual void OnWizardBack(WizardPageEventArgs e)
        {
            if (WizardBack != null)
                WizardBack(this, e);
        }

        [Category("Wizard")]
        public event CancelEventHandler WizardFinish;

        public virtual void OnWizardFinish(CancelEventArgs e)
        {
            if (WizardFinish != null)
                WizardFinish(this, e);
        }

        [Category("Wizard")]
        public event CancelEventHandler QueryCancel;

        public virtual void OnQueryCancel(CancelEventArgs e)
        {
            if (QueryCancel != null)
                QueryCancel(this, e);
        }
        /// <summary>
        /// 当前页面处理
        /// </summary>
        public virtual void Prepare()
        {
            
        }
    }
}
