using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core.Plugins;
using Util;
using System.Security.Permissions;
using mshtml;

namespace AutoRobo.PlulgIn.Answer
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class BackForm : PopIE, IPlugin
    {
        MyBrowser hostBrowser = null;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            hostBrowser = Host.Browser as MyBrowser;
            Text = "后台管理";
            if (DesignMode) return;
            browser.ObjectForScripting = this;
            Goto(StringHelper.Domain + "/host/task?url=" + HostUrl);
        }

        public string HostUrl
        {
            get
            {
                return StringHelper.GetDomain(hostBrowser.LocationUrl);
            }
        }
        public string HostTitle
        {
            get
            {                
                return hostBrowser.GetTitle(true);
            }
        }

        public string GetMeta(string name)
        {
            var cc = hostBrowser.GetElementsByTagName(true, "META");
            mshtml.IHTMLElement el = cc.item(name, 0) as IHTMLElement;
            return el.getAttribute("content") as string;
        }

 
        protected override void OnDocumentComplete(object sender, EventArgs args)
        {
            
            base.OnDocumentComplete(sender, args);
        }


        public IPluginHost Host
        {
            get;
            set;
        }

        public void InstallStripItem()
        {
            
        }

        public Keys DataKey
        {
            get { return Keys.B | Keys.Control; }
        }


        public string StripItemText
        {
            get { return ""; }
        }

        public Bitmap StripItemImage
        {
            get { return null; }
        }
    }
}
