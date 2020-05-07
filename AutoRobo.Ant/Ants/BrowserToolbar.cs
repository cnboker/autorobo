using System;
using System.Windows.Forms;
using AutoRobo.Core.Plugins;
using IfacesEnumsStructsClasses;
using Util;
using AutoRobo.ClientLib.PageHooks;
using System.Threading;
using SHDocVw;
using AutoRobo.Core;


namespace AutoRobo.ClientLib.Ants
{
    public partial class BrowserToolbar : UserControl
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(BrowserToolbar));
        public ICoreBrowser WebBrowser
        {
            get;
            set;
        }

        public IPluginHost Host
        {
            get;
            set;
        }

        public ToolStrip Strip {
            get {
                return toolStrip1;
            }
        }

        public event EventHandler CommandClick = null;

        public BrowserToolbar()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            this.Height = this.toolStrip1.Height + 2;
            urlTextbox.AutoSize = true;
            urlTextbox.DoubleClick += new EventHandler(urlTextbox_DoubleClick);
            urlTextbox.Click +=new EventHandler(urlTextbox_Click);
            MyBrowser browser = WebBrowser as MyBrowser;
            if (browser != null) {               
                backButton.Enabled = browser.CanGoBack;
                forwardButton.Enabled = browser.CanGoForward;
                if (Host is AutoBrowser)
                {
                    TaskToolStrip trip = new TaskToolStrip(browser);
                    trip.RenderMode = ToolStripRenderMode.ManagerRenderMode;
                    trip.Dock = DockStyle.Top;
                    this.Controls.Add(trip);
                    trip.BringToFront();
                    trip.NavBind();
                    this.Height = this.toolStrip1.Height + trip.Height + 2;
                }
                browser.CommandStateChange += new csExWB.CommandStateChangeEventHandler(browser_CommandStateChange);
            }
            WinformBrowser wb = WebBrowser as WinformBrowser;
            if (wb != null) {                        
                //configButton.Visible = false;
                backButton.Enabled = wb.CanGoBack;
                forwardButton.Enabled = wb.CanGoForward;
                wb.CanGoBackChanged += new EventHandler(wb_CanGoBackChanged);
                wb.CanGoForwardChanged += new EventHandler(wb_CanGoForwardChanged);
            }           
           
        }

        void urlTextbox_DoubleClick(object sender, EventArgs e)
        {
            urlTextbox.SelectAll();
        }

        void wb_CanGoForwardChanged(object sender, EventArgs e)
        {
             WinformBrowser wb = WebBrowser as WinformBrowser;
             forwardButton.Enabled = wb.CanGoForward;
        }

        void wb_CanGoBackChanged(object sender, EventArgs e)
        {
            WinformBrowser wb = WebBrowser as WinformBrowser;
            backButton.Enabled = wb.CanGoBack;
        }

        void browser_CommandStateChange(object sender, csExWB.CommandStateChangeEventArgs e)
        {
            if (e.command == CommandStateChangeConstants.CSC_NAVIGATEBACK)
                backButton.Enabled = e.enable;
            else if (e.command == CommandStateChangeConstants.CSC_NAVIGATEFORWARD)
                forwardButton.Enabled = e.enable;
        }

        public void UrlChange(string url)
        {
            if (url != "about:blank")
            {
                urlTextbox.Text = url;
            }
            else {
                urlTextbox.Focus();
            }           
        }

        private void Go(string text) {
            string url = (text.IndexOf("http://") == 0 || text.IndexOf("https://") == 0) ? text : "http://" + text;
            WebBrowser.Navigate(text);
        }

        private void GoSearchToolStripButtonClickHandler(object sender, EventArgs e)
        {
            try
            {
                if (sender == homeBtn)
                {
                    Go(StringHelper.Domain);
                }
                else if (sender == this.urlTextbox)
                {
                    string text = urlTextbox.Text;
                    Go(text);                 
                }
                else if (sender == this.backButton)
                {
                  WebBrowser.GoBack();
                }
                else if (sender == this.forwardButton)
                {
                    WebBrowser.GoForward();      
                }
                else if (sender == this.refreshButton)
                {
                    WebBrowser.Refresh();
                }            
                else if (sender == configButton)
                {
                    BrowserSettings settings = new BrowserSettings();
                    settings.ClearCache += new EventHandler(settings_ClearCache); 
                    if (settings.ShowDialog() == DialogResult.OK)
                    {                       
                        MyBrowser webBrowser = WebBrowser as MyBrowser;
                        if (webBrowser == null) return;
                        webBrowser.RefreshConfig();
                    }
                }               
                if (CommandClick != null) {
                    CommandClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }

        void settings_ClearCache(object sender, EventArgs e)
        {
            new System.Threading.Thread(() =>
            {
                MyBrowser browser = WebBrowser as MyBrowser;
                browser.ClearHistory();
                browser.ClearSessionCookies();
            }).Start();
           
        }


        private void KeyClickHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GoSearchToolStripButtonClickHandler(sender, EventArgs.Empty);
            }
        }

       
        private void urlTextbox_Click(object sender, EventArgs e)
        {
            //urlTextbox.SelectedText = "";
            if (urlTextbox.Text == "about:blank") {
                urlTextbox.Text = "";
            }
        }

    
    }
}
