using System;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.ClientLib.PageHooks;
using AutoRobo.ClientLib.Properties;
using AutoRobo.Core;
using AutoRobo.Core.Plugins;
using AutoRobo.Protocol;
using AutoRobo.ClientLib.PageHooks.Models;

namespace AutoRobo.ClientLib.Ants
{
    /// <summary>
    /// 这里引进winform内置浏览器主要是用于打开主页，使用mybrowser打开主页后， 启动录制器后会导致IHTMLDocument2.body无法访问，
    /// 抛出对于方法找不到异常，对于cEXWeb.cs, 3054行
    /// </summary>
    public partial class AutoBrowser : FireBrowser
    {
     
        private FakeHttpContext fakeHttpContext = null;
   

        public AutoBrowser(){           
            InnerInit();
        }

  
        private void InnerInit()
        {
            webBrowser = new MyBrowser();
            //注册插件
            //Register("TaskPlugin");                         
            //Register("DebugerUI");
            Register("CutPaper");
            Register("Reply");
            Register("BackForm");
            Control c = webBrowser as Control;
            c.Dock = DockStyle.Fill;
            this.Controls.Add(c);
            c.BringToFront();
        }

      
    
        protected override void OnLoad(EventArgs e)
        {
            AttachBrowser(webBrowser);
            if (RedirectUrl != "about:blank")
            {
                Thread t = new Thread(() =>
                {
                    bool result = fakeHttpContext.Authenticate(RedirectUrl);
                    if (result)
                    {
                        webBrowser.Navigate(RedirectUrl);
                    }
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            base.OnLoad(e);
        }

        protected override void AttachBrowser(ICoreBrowser browser)
        {
            fakeHttpContext = new FakeHttpContext(webBrowser);
            fakeHttpContext.Initialize();
            MyBrowser mybrower = (MyBrowser)webBrowser;
            mybrower.ObjectForScripting = fakeHttpContext;
            mybrower.DownloadComplete += new csExWB.DownloadCompleteEventHandler(mybrower_DownloadComplete);                
   
            base.AttachBrowser(browser);
        }

        void wb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            UpdateToolbarAddressAndTitle((ICoreBrowser)sender, e.Url.ToString());  
        }

        void mybrower_DownloadComplete(object sender)
        {
            if (DesignMode) return;
            
            MyBrowser mybrower = (MyBrowser)webBrowser;
            if (mybrower == null) return;            
            if (string.IsNullOrEmpty(mybrower.LocationUrl) || mybrower.LocationUrl == "about:blank") return;
            if (mybrower.IsMySite()) return;
            if (!this.fakeHttpContext.EnableTracing) return;
            logger.Info("exescript:downolad");
            Thread t = new Thread(() => {
                AutoRobo.Core.App.Wait(() => {                   
                    try
                    {                        
                        mybrower.execScript(true, Resources.domReady, "");
                    }
                    catch (Exception ex) {
                        logger.Fatal(ex);
                        return false;
                    }
                    logger.Info("inject Resources.domReady sucess!" + mybrower.LocationUrl);
                    return true;
                });
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        void webBrowser_StatusTextChanged(object sender, EventArgs e)
        {
            WinformBrowser browser = webBrowser as WinformBrowser;
            tsStatus.Text = browser.StatusText;
        }

        void webBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            ProgressChange(Convert.ToInt32(e.MaximumProgress), Convert.ToInt32(e.CurrentProgress));  
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AutoBrowser
            // 
            this.ClientSize = new System.Drawing.Size(525, 348);
            this.Name = "AutoBrowser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
