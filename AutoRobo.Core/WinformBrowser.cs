using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core;
using mshtml;

namespace AutoRobo
{
    public class WinformBrowser : System.Windows.Forms.WebBrowser, ICoreBrowser
    {
        public IHTMLElement EditableActiveElement
        {
            get;
            set;
        }

        public IHTMLElement SelectorElement
        {
            get;
            set;
        }

        public bool AllowAlert
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void HighlightElement(IHTMLElement element)
        {
            throw new NotImplementedException();
        }


        public string LocationUrl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

 

        public object IWebBrowser2
        {
            get { return ActiveXInstance; }
        }


        public Selector Selector
        {
            get { throw new NotImplementedException(); }
        }


        public void OpenNewBrowser(string url)
        {
            throw new NotImplementedException();
        }


        public BrowserWindow WatinBrowser
        {
            get { throw new NotImplementedException(); }
        }


        public WatiN.Core.ElementCollection SelectMany(string _cssSelector)
        {
            throw new NotImplementedException();
        }


        public bool SelectAny(string selector)
        {
            throw new NotImplementedException();
        }
    }
}
