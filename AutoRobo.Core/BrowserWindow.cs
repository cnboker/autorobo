using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core
{
    public class BrowserWindow : WatiN.Core.IE
    {
       
        public ICoreBrowser WBBrowser;

        public string FriendlyName { get; set; }

        public void AttachToIE()
        {
            CreateIEBrowser(WBBrowser.IWebBrowser2);
        }

        public BrowserWindow(ICoreBrowser IEContainer)
            : base(IEContainer.IWebBrowser2)
        {
            WBBrowser = IEContainer;
        }

        public BrowserWindow(IEBrowser browser)
            : base(browser)
        {
            
        }

    }
}
