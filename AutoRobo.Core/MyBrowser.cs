using System;
using System.Collections.Generic;
using System.Text;
using csExWB;
using AutoRobo.Core;
using mshtml;
using AutoRobo.Core.Actions;
using System.Drawing;
using WatiN.Core;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using WatiN.Core.Constraints;
using System.Linq;

namespace AutoRobo
{

    public class MyBrowser : DesignTimeBrowser, ICoreBrowser
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger("MyBrowser");

        public INewWindow NewWin { get; set; }
           
        /// <summary>
        /// 文档全部加载完成
        /// </summary>
        public event EventHandler DocumentFullComplete;
        
        public BrowserWindow WatinBrowser
        {
            get
            {
                return new BrowserWindow(this); 
            }
        }

        public MyBrowser() {            
            WatiN.Core.Settings.AutoStartDialogWatcher = false;
            SendSourceOnDocumentCompleteWBEx = true;
            //解决跨域iframe不能读取cookie问题
            AddUrlToTrustedZone(System.Configuration.ConfigurationManager.AppSettings["root"]);
            WBDocHostShowUIShowMessage += new DocHostShowUIShowMessageEventHandler(cb_WBDocHostShowUIShowMessage);
            this.WBEvaluteNewWindow += new csExWB.EvaluateNewWindowEventHandler(RoboBrowser_WBEvaluteNewWindow);
            this.DocumentCompleteEX += new DocumentCompleteExEventHandler(MyBrowser_DocumentCompleteEX);
            this.NewWindow2 += new csExWB.NewWindow2EventHandler(this.cEXWB1_NewWindow2);
            this.NewWindow3 += new csExWB.NewWindow3EventHandler(this.cEXWB1_NewWindow3);
           
        }

      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) {
                Selector.Restore();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        void MyBrowser_RefreshEnd(object sender)
        {
            if (DocumentFullComplete != null)
            {
                DocumentFullComplete(this, EventArgs.Empty);
            }
        }

        public void  EnableScriptError(bool enable){
            if (enable)
            {
                ScriptError += new ScriptErrorEventHandler(MyBrowser_ScriptError);
            }
            else {
                ScriptError -= new ScriptErrorEventHandler(MyBrowser_ScriptError);
            }
        }
       
        void MyBrowser_ScriptError(object sender, ScriptErrorEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e.errorMessage);
            sb.AppendLine("error code:" + e.errorCode);
            sb.AppendLine("line:" + e.lineNumber);
            sb.AppendLine(e.url);
            MessageBox.Show(sb.ToString());
        }

        public void GoToBlank() {
            Navigate("about:blank");
        }

        public new bool GoBack()
        {
            if (CanGoBack) {
                mshtml.IOmHistory history = GetHistory();
                history.back(null);
            }
            return false;
        }

        public new bool GoForward()
        {
            if (CanGoForward)
            {
                mshtml.IOmHistory history = GetHistory();
                history.forward(null);
            }
            return false;
        }

        public new void Refresh() {
            Navigate2(LocationUrl);
        } 

        private mshtml.IOmHistory GetHistory()
        {
            if (InvokeRequired) {
                return (mshtml.IOmHistory)Invoke(new Func<mshtml.IOmHistory>(GetHistory));
            }
            IHTMLDocument2 doc2 = GetActiveDocument() as mshtml.IHTMLDocument2;
            mshtml.IHTMLWindow2 win = doc2.parentWindow as mshtml.IHTMLWindow2;
            mshtml.IOmHistory history = win.history as mshtml.IOmHistory;
            return history;
        }

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();
     
        /// <summary>
        /// 页面加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyBrowser_DocumentCompleteEX(object sender, DocumentCompleteExEventArgs e)
        {
            if (!e.istoplevel) return;
            if (string.IsNullOrEmpty(LocationUrl) || LocationUrl == "about:blank") return;
            //if (IsMySite()) return;
            //浏览器的执行周期， uninitialized->loading->interactive->loaded->interactive->complete, 在包含frame的页面， 
            //只有frame的ReadyState全部变成complete后， browser.ReadyState状态才会变成complete， 所以当browser.readyState=complete
            //的时候表示页面加载全部完成
            if (this.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            {
                try
                {
                    FindForm().Text = DocumentTitle;
                }
                catch { }                
                OnDocumentComplete(sender, e);
                if (DocumentFullComplete != null)
                {
                    DocumentFullComplete(this, EventArgs.Empty);
                }
               
            }
      
            //解决IE内存泄漏问题
            System.Diagnostics.Process loProcess = System.Diagnostics.Process.GetCurrentProcess();
            try
            {
                loProcess.MaxWorkingSet = (IntPtr)((int)loProcess.MaxWorkingSet - 1);
                loProcess.MinWorkingSet = (IntPtr)((int)loProcess.MinWorkingSet - 1);
            }
            catch (System.Exception)
            {
                loProcess.MaxWorkingSet = (IntPtr)((int)1413120);
                loProcess.MinWorkingSet = (IntPtr)((int)204800);
            }

            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);
        }

        /// <summary>
        /// 阻止显示javascript提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cb_WBDocHostShowUIShowMessage(object sender, DocHostShowUIShowMessageEventArgs e)
        {
            if (!IsMySite())
            {
                e.handled = !AllowAlert;
            }
            OnWBDocHostShowUIShowMessage(sender, e);
        }

        protected virtual void OnWBDocHostShowUIShowMessage(object sender, DocHostShowUIShowMessageEventArgs e)
        {
            
        }

        public bool IsMySite() {
            return this.LocationUrl.IndexOf("ebotop.com") >= 0 || this.LocationUrl.IndexOf("localhost") >= 0;
        }

        protected virtual void RoboBrowser_WBEvaluteNewWindow(object sender, EvaluateNewWindowEventArgs e)
        {
            // modeless/modal flags=38
            if ((e.flags & IfacesEnumsStructsClasses.NWMF.HTMLDIALOG) == IfacesEnumsStructsClasses.NWMF.HTMLDIALOG )
            {
                e.Cancel = true;
            }
            else
            {
                OnEvaluteNewWindowHander(sender, e);
                if (NewWin != null)
                {
                    //NewWin.Goto(e.url, "_blank");
                }
            }
        }

        /// <summary>
        /// 是否允许窗口弹出
        /// </summary>
        /// <param name="allow"></param>
        public void PopWindow(bool allow)
        {
            this.NewWindow2 -= new csExWB.NewWindow2EventHandler(this.cEXWB1_NewWindow2);
            this.NewWindow3 -= new csExWB.NewWindow3EventHandler(this.cEXWB1_NewWindow3);
            if (!allow)
            {
                this.NewWindow2 += new csExWB.NewWindow2EventHandler(this.cEXWB1_NewWindow2);
                this.NewWindow3 += new csExWB.NewWindow3EventHandler(this.cEXWB1_NewWindow3);
            }
        }

        protected void cEXWB1_NewWindow2(object sender, csExWB.NewWindow2EventArgs e)
        {
            e.browser = WebbrowserObject;
        }

        protected void cEXWB1_NewWindow3(object sender, csExWB.NewWindow3EventArgs e)
        {
            //if (NewWin != null)
            //{
            //    NewWin.AssignBrowserObject(ref e.browser);
            //}
            //else
            {
                Navigate(e.url);
                e.browser = WebbrowserObject;
            }
            
        }


        protected virtual void OnDocumentComplete(object sender, DocumentCompleteExEventArgs e)
        {

        }
     

        public bool AllowAlert
        {
            get;
            set;
        }


        protected virtual void OnEvaluteNewWindowHander(object sender, EvaluateNewWindowEventArgs arg) { 
            
        }

        public void NavigateTo(Uri url)
        {
            throw new NotImplementedException();
        }

        public void NavigateToNoWait(Uri url)
        {
            throw new NotImplementedException();
        }

        public void Reopen()
        {
            throw new NotImplementedException();
        }

        public IntPtr hWnd
        {
            get { return base.Handle; }
        }

        public bool DownloadFlash
        {            
            set {
                if (value)
                {
                    ProcessUrlAction += new csExWB.ProcessUrlActionEventHandler(webBrowser_ProcessUrlAction);
                }
                else
                {
                    ProcessUrlAction -= new csExWB.ProcessUrlActionEventHandler(webBrowser_ProcessUrlAction);
                }
            }
        }

        public void RefreshConfig() {
            DownloadImages = AppSettings.Instance.DownloadImages;
            DownloadSounds = AppSettings.Instance.DownloadSounds;
            DownloadVideo = AppSettings.Instance.DownloadVideo;
            DownloadActiveX = AppSettings.Instance.DownloadActiveX;
            AllowAlert = AppSettings.Instance.AllowAlert;
            bool DownloadFlash = AppSettings.Instance.DownloadFlash;
            if (!DownloadFlash)
            {
                ProcessUrlAction -= new csExWB.ProcessUrlActionEventHandler(webBrowser_ProcessUrlAction);
            }
            else
            {
                ProcessUrlAction += new csExWB.ProcessUrlActionEventHandler(webBrowser_ProcessUrlAction);
            }
            EnableScriptError(AppSettings.Instance.AllowScriptError);
        }

        /// <summary>
        /// 禁止显示flash
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void webBrowser_ProcessUrlAction(object sender, csExWB.ProcessUrlActionEventArgs e)
        {
            if (e.urlAction == IfacesEnumsStructsClasses.URLACTION.ACTIVEX_RUN)
            {
                Guid flash = new Guid("D27CDB6E-AE6D-11cf-96B8-444553540000");
                if (e.context == flash)
                {
                    e.handled = true;
                    e.urlPolicy = IfacesEnumsStructsClasses.URLPOLICY.DISALLOW;
                }
            }
        }
        /// <summary>
        /// 获取元素绝对位置
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public Point FindPos(IHTMLElement obj)
        {
            int curleft = 0;
            int curtop = 0;
            if (obj.offsetParent != null)
            {
                do
                {
                    curleft += obj.offsetLeft;
                    curtop += obj.offsetTop;
                } while ((obj = obj.offsetParent) != null);
            }
            return new Point(curleft, curtop);
        }
        public Point GetScroll()
        {
            IHTMLDocument2 pDoc2 = WebbrowserObject.Document as IHTMLDocument2;
            if (pDoc2 == null) return new Point(0, 0);
            IHTMLDocument3 pDoc3 = WebbrowserObject.Document as IHTMLDocument3;
            if (pDoc3 == null) return new Point(0, 0);

            IHTMLElement pElem = null;
            IHTMLElement2 pBodyElem = null;


            int sw2 = 0;
            int sh2 = 0;
            int sw3 = 0;
            int sh3 = 0;
            pElem = pDoc2.body;

            pBodyElem = pElem as IHTMLElement2;
            if (pBodyElem != null)
            {
                sw2 = pBodyElem.scrollLeft;
                sh2 = pBodyElem.scrollTop;
            }
            pElem = pDoc3.documentElement;
            if (pElem != null)
            {
                pBodyElem = pElem as IHTMLElement2;
                if (pBodyElem != null)
                {
                    sw3 = pBodyElem.scrollLeft;
                    sh3 = pBodyElem.scrollTop;
                }
            }
            return new Point(sw2 > sw3 ? sw2 : sw3, sh2 > sh3 ? sh2 : sh3);
        }


        private Selector selector = null;
        public Selector Selector
        {
            get {
                if (selector == null) {
                    selector = new Selector(this);
                }
                return selector;
            }     
        }


        public object IWebBrowser2
        {
            get { return WebbrowserObject; }
        }


        public void OpenNewBrowser(string url)
        {
            if (NewWin != null) {
                //NewWin.Goto(this, url);
            }
        }

        /// <summary>
        /// 通过css selector 获取元素
        /// 包含iframe处理格式#iframe::.selector
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ElementCollection SelectMany(string selector)
        {
            while (Busy)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            BrowserWindow watinBrowser = new BrowserWindow(this);
            int index = selector.IndexOf("::");
            if (index != -1)
            {
                var iframeSelector = selector.Substring(0, index);
                var subSelector = selector.Substring(index + 2);
                Frame frame = this.WatinBrowser.Frames.Filter(WatiN.Core.Find.BySelector(iframeSelector)).First();
                index = subSelector.IndexOf("::");
                //iframe嵌套iframe的情况
                if (index != -1)
                {
                    iframeSelector = subSelector.Substring(0, index);
                    frame = watinBrowser.Frames.Filter(WatiN.Core.Find.BySelector(iframeSelector)).First();
                    subSelector = subSelector.Substring(index + 2);
                }
                return frame.Elements.Filter(WatiN.Core.Find.BySelector(subSelector));
            }
            else
            {
                QuerySelectorConstraint constraint = WatiN.Core.Find.BySelector(selector);
                var elements = watinBrowser.Elements.Filter(constraint);
                return elements;
            }

        }

        public bool SelectAny(string selector)
        {
            return SelectMany(selector).Any();
        }


        public Element SelectSingleSelect(string selector)
        {
            return SelectMany(selector).FirstOrDefault();
        }


     
    }
}
