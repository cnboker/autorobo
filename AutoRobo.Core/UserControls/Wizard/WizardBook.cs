using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoRobo.Core.UserControls.Wizard
{
    [Flags]
    public enum WizardButtons
    {
        None = 0x0000,
        Back = 0x0001,
        Next = 0x0002,
        Finish = 0x0004,
    }

    public partial class WizardBook : Form
    {
        public WizardContext Context { get; set; }
        private WizardPage _activePage;

        public WizardBook()
        {
            InitializeComponent();            
        }

        private void SetActivePage(string newPageName)
        {
            WizardPage newPage = FindPage(newPageName);

            if (newPage == null)
                throw new Exception(string.Format("Can't find page named {0}", newPageName));

            SetActivePage(newPage);
        }

        private WizardPage FindPage(string pageName)
        {
            foreach (WizardPage page in Context.Pages)
            {
                if (page.Name == pageName)
                    return page;
            }

            return null;
        }

        private TabPage FindTagPage(WizardPage page)
        {
            foreach (TabPage p in wizardPages1.TabPages)
            {
                if (p.Controls.Count > 0)
                {
                    if (p.Controls[0] == page) return p;
                }
            }
            return null;
        }

        private int GetActiveIndex()
        {
            for (int i = 0; i < Context.Pages.Count; ++i)
            {
                if (_activePage == Context.Pages[i])
                    return i;
            }

            return -1;
        }

        public void SetActivePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= Context.Pages.Count)
                throw new ArgumentOutOfRangeException("pageIndex");

            WizardPage page = Context.Pages[pageIndex];
            SetActivePage(page);
        }


        protected void SetActivePage(WizardPage newPage)
        {
            //WizardPage oldActivePage = _activePage;
            TabPage page = FindTagPage(newPage);
            if (page == null)
            {
                page = new TabPage();
                page.Controls.Add(newPage);
                wizardPages1.Controls.Add(page);
                newPage.Dock = DockStyle.Fill;
            }       
            _activePage = newPage;
            wizardPages1.SelectedTab = page;
            CancelEventArgs e = new CancelEventArgs();
            newPage.OnSetActive(e);
        }

        internal void SetWizardButtons(WizardButtons buttons)
        {
            // The Back button is simple.
            backButton.Enabled = ((buttons & WizardButtons.Back) != 0);

            // The Next button is a bit more complicated. If we've got a Finish button, then it's disabled and hidden.
            if ((buttons & WizardButtons.Finish) != 0)
            {
                finishButton.Visible = true;
                finishButton.Enabled = true;

                nextButton.Visible = false;
                nextButton.Enabled = false;

                AcceptButton = finishButton;
            }
            else
            {
                finishButton.Visible = false;
                finishButton.Enabled = false;

                nextButton.Visible = true;
                nextButton.Enabled = ((buttons & WizardButtons.Next) != 0);

                AcceptButton = nextButton;
            }
        }
        /// <summary>
        /// 换页预处理
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        private WizardPageEventArgs PreChangePage(int delta)
        {
            // Figure out which page is next.
            int activeIndex = GetActiveIndex();
            int nextIndex = activeIndex + delta;

            if (nextIndex < 0 || nextIndex >= Context.Pages.Count)
                nextIndex = activeIndex;

            // Fill in the event args.
            WizardPage newPage = Context.Pages[nextIndex];

            WizardPageEventArgs e = new WizardPageEventArgs { NewPage = newPage.Name, Cancel = false };

            return e;
        }

        private void PostChangePage(WizardPageEventArgs e)
        {
            if (!e.Cancel)
                SetActivePage(e.NewPage);
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            _activePage.Prepare();
            WizardPageEventArgs wpea = PreChangePage(+1);
            _activePage.OnWizardNext(wpea);
            PostChangePage(wpea);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            WizardPageEventArgs wpea = PreChangePage(-1);
            _activePage.OnWizardBack(wpea);
            PostChangePage(wpea);
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            CancelEventArgs cea = new CancelEventArgs();
            _activePage.OnWizardFinish(cea);
            if (cea.Cancel)
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        internal void PressButton(WizardButtons buttons)
        {
            if ((buttons & WizardButtons.Finish) == WizardButtons.Finish)
                finishButton.PerformClick();
            else if ((buttons & WizardButtons.Next) == WizardButtons.Next)
                nextButton.PerformClick();
            else if ((buttons & WizardButtons.Back) == WizardButtons.Back)
                backButton.PerformClick();
        }

        internal void EnableCancelButton(bool enableCancelButton)
        {
            cancelButton.Enabled = enableCancelButton;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WizardSheet_Closing(object sender, CancelEventArgs e)
        {
            if (!cancelButton.Enabled)
                e.Cancel = true;
            else if (!finishButton.Enabled)
                OnQueryCancel(e);
        }

        protected virtual void OnQueryCancel(CancelEventArgs e)
        {
            _activePage.OnQueryCancel(e);
        }

    
    }
}
