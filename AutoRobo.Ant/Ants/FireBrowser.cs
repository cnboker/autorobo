using System;
using System.Windows.Forms;
using AutoRobo.Core.Plugins;
using csExWB;
using IfacesEnumsStructsClasses;
using AutoRobo.ClientLib.Properties;
using Util;
using AutoRobo.Core;

namespace AutoRobo.ClientLib.Ants
{
    public partial class FireBrowser : Form, IPluginHost, IUILog
    {    
        protected ICoreBrowser webBrowser;
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HostBase));

        public event SendKeysEvent SendKeyHandler;
        private PluginManger pluginManger = null;
        //private Microsoft.Win32.MouseHook hook = new Microsoft.Win32.MouseHook();


        /// <summary>
        /// 验证成功后新的导航链接
        /// </summary>
        public string RedirectUrl { get; set; }

        public ICoreBrowser Browser
        {
            get { return webBrowser; }
        }

        public ToolStrip Strip {
            get { return browserToolbar.Strip; }
        }

        public FireBrowser()
        {
            InitializeComponent();
           
            pluginManger = new PluginManger(this);
            //hook.MouseMove += new Microsoft.Win32.MouseHookEventHandler(hook_MouseMove);
            //hook.Install();
        }

        
        void hook_MouseMove(object sender, Microsoft.Win32.MouseHookEventArgs e)
        {
            xyLabel.Text = e.X + "," + e.Y;
        }

        /// <summary>
        /// 支持多浏览器切换
        /// </summary>
        /// <param name="webBrowser">激活的浏览器</param>
        protected virtual void AttachBrowser(ICoreBrowser activeBrowser)
        {
            if (activeBrowser == webBrowser) return;
            browserToolbar.WebBrowser = activeBrowser;
            if (webBrowser == null)
            {
                activeBrowser.Navigate(StringHelper.Domain + "/page/start");
                browserToolbar.Host = this;
                browserToolbar.Initialize();               
            }
            else {
                DetachBrowser(webBrowser);
            }
            MyBrowser myBrowser = activeBrowser as MyBrowser;
            if (myBrowser != null)
            {
                //加载配置
                myBrowser.RefreshConfig();
                myBrowser.ProgressChange += new ProgressChangeEventHandler(Browser_ProgressChange);
                myBrowser.StatusTextChange += new StatusTextChangeEventHandler(webBrowser_StatusTextChange);
                myBrowser.DocumentFullComplete += new EventHandler(browser_DocumentFullComplete);
                myBrowser.NavigateComplete2 += new NavigateComplete2EventHandler(myBrowser_NavigateComplete2);
            }
            webBrowser = activeBrowser;
        }

        private void DetachBrowser(ICoreBrowser browser) { 
             MyBrowser myBrowser = browser as MyBrowser;
             if (myBrowser != null)
             {
                 myBrowser.ProgressChange -= new ProgressChangeEventHandler(Browser_ProgressChange);
                 myBrowser.StatusTextChange -= new StatusTextChangeEventHandler(webBrowser_StatusTextChange);
                 myBrowser.DocumentFullComplete -= new EventHandler(browser_DocumentFullComplete);
                 myBrowser.NavigateComplete2 -= new NavigateComplete2EventHandler(myBrowser_NavigateComplete2);
             }
        }
        /// <summary>
        /// 更新地址栏和标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void myBrowser_NavigateComplete2(object sender, NavigateComplete2EventArgs e)
        {
            ICoreBrowser browser = (ICoreBrowser)sender;
            UpdateToolbarAddressAndTitle(browser, browser.LocationUrl);
        }
 
        protected void UpdateToolbarAddressAndTitle(ICoreBrowser browser, string addressUrl) {
            string title = browser.DocumentTitle;
            FindForm().Text = title.Length > 12 ? title.Substring(0, 12) : title;
            browserToolbar.UrlChange(addressUrl);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                Browser.Navigate(Util.StringHelper.Domain + "/host");
            }
            
            if (SendKeyHandler != null) {
                SendKeyHandler(keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        } 

        protected void webBrowser_StatusTextChange(object sender, StatusTextChangeEventArgs e)
        {
            if (sender != webBrowser)
                return;
            tsStatus.Text = e.text;
        }

        protected void Browser_ProgressChange(object sender, ProgressChangeEventArgs e)
        {
            if (sender != webBrowser)
                return;
            ProgressChange(e.progressmax, e.progress);
        }

        protected void ProgressChange(int maxProgress, int curProgress)
        {
            if ((curProgress == -1) || (maxProgress <= curProgress))
            {
                tsProgress.Value = 0; // 100;
                tsProgress.Maximum = maxProgress;
                return;
            }
            if ((maxProgress > 0) && (curProgress > 0) && (curProgress < maxProgress))
            {
                tsProgress.Maximum = maxProgress;
                tsProgress.Value = curProgress; // tsProgress.Maximum;
            }
        }


        void browser_DocumentFullComplete(object sender, EventArgs e)
        {
            OnDocumentComplete(sender, e);
        }
  
        protected virtual void OnDocumentComplete(object sender, EventArgs e)
        { 
            
        }

        ICoreBrowser IPluginHost.Browser
        {
            get
            {
                return webBrowser;
            }
            set
            {
                webBrowser = value;
            }
        }

        public void Write(string message)
        {
            stripStatusLMessage.Text = message;
        }

        public ToolStripStatusLabel Writer
        {
            get { return stripStatusLMessage; }
        }


        public void Register(string pluginName)
        {
             pluginManger.Register(pluginName);
        }

        //public void Show(IPlugin ipi)
        //{
        //    MyBrowser browser = webBrowser as MyBrowser;
        //   // if (browser.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE) return ;
        //    Type t = ipi.GetType();
        //    if (t.Name == "DebugerUI")
        //    {
        //        Control debuger = ipi as Control;
        //        debuger.Dock = DockStyle.Fill;

        //        if (splitContainer1.Panel2.Controls.Contains(debuger))
        //        {
        //            this.splitContainer1.Panel2Collapsed = true;
        //            splitContainer1.Panel2.Controls.Remove(debuger);
        //            browser.Selector.Restore();
        //        }
        //        else
        //        {
        //            this.splitContainer1.Panel2.Controls.Add(debuger);
        //            this.splitContainer1.Panel2Collapsed = false;
        //        }

        //    }
        //}



        public void Show(IPlugin ipi)
        {
            throw new NotImplementedException();
        }
    }
}
