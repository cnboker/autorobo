using System;
using AutoRobo.Core.Plugins;
using IfacesEnumsStructsClasses;
using Util;
using System.Windows.Forms;
using AutoRobo.PlulgIn.Properties;
using mshtml;

namespace AutoRobo.PlulgIn.Answer
{
    public partial class CutPaper : PopIE, IPlugin
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "剪报工具";
            if (DesignMode) return;
            Goto(StringHelper.Domain + "/portal/answers/create");
        }

        protected override void OnDocumentComplete(object sender, EventArgs args)
        {
            MyBrowser hostBrowser = Host.Browser as MyBrowser;
            if (hostBrowser != null)
            {
                IHTMLElement el = browser.GetElementByID(true, "content");
                string selectText = hostBrowser.GetSelectedText(true, false);
                if (selectText == null)
                {
                    selectText = "请选择要收藏的文本";
                    el.setAttribute("placeholder", selectText, 0);
                }
                else
                {
                    el.setAttribute("value", selectText, 0);
                }
            }
        }
    

        public IPluginHost Host
        {
            get;
            set;
        }

        public Keys DataKey
        {
            get { return Keys.D | Keys.Control; }
        }


        public string StripItemText
        {
            get { return "剪报[CTRL+D]"; }
        }

        public System.Drawing.Bitmap StripItemImage
        {
            get { return Resources.cutInfo; }
        }
    }
}
