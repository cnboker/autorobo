using SHDocVw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core
{
    public class MyIE : IE, ICoreBrowser
    {
        private IWebBrowser2 browser2 = null;

        public MyIE() {
            mshtml.IHTMLDocument2 doc = ((IEDocument)(DomContainer.NativeDocument)).HtmlDocument;
            browser2 = CrossFrameIE.GetIWebBrowser2(doc.parentWindow);
        }
        public string DocumentTitle
        {
            get { return this.DocumentTitle; }
        }

        public bool AllowAlert
        {
            get;
            set;
        }

        public string LocationUrl
        {
            get;
            set;
        }

        public void Navigate(string urlString)
        {
            base.GoTo(urlString);            
        }

        public bool CanGoBack
        {
            get { return base.NativeBrowser.GoBack(); }
        }

        public bool CanGoForward
        {
            get { return base.NativeBrowser.GoForward(); }
        }

        public bool GoBack()
        {
            return base.NativeBrowser.GoBack();
        }

        public bool GoForward()
        {
            return base.NativeBrowser.GoForward();
        }

        public void GoHome()
        {
            
        }

        public void Stop()
        {
            
        }

        public Selector Selector
        {
            get { throw new NotImplementedException(); }
        }

        public object IWebBrowser2
        {
            get {
                return browser2;
            }
        }

        public void OpenNewBrowser(string url)
        {
            throw new NotImplementedException();
        }




        public BrowserWindow WatinBrowser
        {
            get { throw new NotImplementedException(); }
        }


        public ElementCollection SelectMany(string _cssSelector)
        {
            throw new NotImplementedException();
        }


        public bool SelectAny(string selector)
        {
            throw new NotImplementedException();
        }
    }
}
