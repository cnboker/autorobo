using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace AutoRobo.Core.Plugins
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class PopIE : Form
    {
        protected MyBrowser browser = null;
     
        public PopIE()
        {
            InitializeComponent();
            browser = new MyBrowser();
            browser.Dock = DockStyle.Fill;
            browser.AllowAlert = true;
            
            this.Controls.Add(browser);
        }

        public void Goto(string url) {
            url = url + (url.IndexOf("?") > 0 ? "&" : "?") + "master=empty";
            browser.Navigate(url);
        }

        protected override void OnLoad(EventArgs e)
        {            
            browser.DocumentCompleteEX += new csExWB.DocumentCompleteExEventHandler(browser_DocumentCompleteEX);          
            base.OnLoad(e);
        }

      

        void browser_DocumentCompleteEX(object sender, csExWB.DocumentCompleteExEventArgs e)
        {
            if (string.IsNullOrEmpty(browser.LocationUrl) || browser.LocationUrl == "about:blank") return;
            if (e.istoplevel)
            {
                OnDocumentComplete(sender, EventArgs.Empty);
            }
        }

        protected virtual void OnDocumentComplete(object sender, EventArgs args){
            
        }

       
        protected override void OnClosed(EventArgs e)
        {
            browser.Dispose();
            base.OnClosed(e);
        }

      
    }
}
