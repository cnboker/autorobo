using AutoRobo.Core;
using AutoRobo.PlulgIn.Debuger;
using System;
using System.Windows.Forms;

namespace AutoRobo.Makers.Views
{
    public partial class HtmlView : ViewBase
    {
        private HtmlTree htmlTree = null;
        private MakerView top = null;

        public HtmlView(MakerView top)
        {
            this.top = top;
            
            InitializeComponent();
            TabText = "HTML";
            htmlTree = new HtmlTree() { Dock = DockStyle.Fill };
           
            this.DockStateChanged += new EventHandler(Reload);
            this.Controls.Add(htmlTree);
            top.InsertStripButton(-4, htmlTree.SelectorStripButton);
            top.InsertStripButton(-4, htmlTree.SelectorAreaStripButton);
            htmlTree.SelectedEventHandler += htmlTree_SelectedEventHandler;
            CloseButtonVisible = false;
        }

        void htmlTree_SelectedEventHandler(object sender, csExWB.HTMLMouseEventArgs e)
        {
            //top.ShowContextMenu();
            this.Show();
        }

        public void BrowserAttach() {
            MyBrowser browser = AppContext.Current.Browser as MyBrowser;
            htmlTree.Attach(browser);
            browser.DocumentFullComplete -= new EventHandler(Reload);
            browser.DocumentFullComplete += new EventHandler(Reload);
            
        }

        void Reload(object sender, EventArgs e)
        {
            if (this.DockState == WeifenLuo.WinFormsUI.Docking.DockState.DockBottom)
            {
                htmlTree.RefreshTreeView();
            }
        }
      
    }
}
