using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AutoRobo.Core;
using AutoRobo.Core.ns;
using AutoRobo.Core.Plugins;
using AutoRobo.WebApi;
using Newtonsoft.Json;
using Util;
using AutoRobo.Core.SEO;
using AutoRobo.PlulgIn.Properties;
using WatiN.Core;
using mshtml;
using Newtonsoft.Json.Linq;

namespace AutoRobo.PlulgIn.Answer
{
    public partial class Reply : PopIE, IPlugin
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("Reply");
        MyBrowser hostBrowser = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "回帖工具";
            if (DesignMode) return;
            hostBrowser = Host.Browser as MyBrowser;
            browser.WBLButtonUp += new csExWB.HTMLMouseEventHandler(browser_WBLButtonUp);
            Goto(StringHelper.Domain + "/portal/answers/index");
        }

        
        protected override void OnDocumentComplete(object sender, EventArgs args)
        {
            Method<string>.Watch("search match", () =>
            {
                NLResult result = NatualLinkCreator.Instance.Create(this.hostBrowser.GetText(true), LinkStyle.Standard);                
                CreateMatchAnchors(result.MatchWords);
                if (result.MatchWords.Count > 0)
                {
                    IHTMLInputElement searchInput = browser.GetElementByID(true, "keyword") as IHTMLInputElement;
                    searchInput.value = result.MatchWords[0];                   
                }
            });
        }

        private void CreateMatchAnchors(List<string> matchWords)
        {            
            StringBuilder sb = new StringBuilder();            
            foreach (var o in matchWords)
            {
                sb.AppendFormat("<a href=\"#\" class=\"matchKeyword\">{0}</a>", o);
                sb.Append("&nbsp;");
            }
            IHTMLElement div = browser.GetElementByID(true, "matchKeyword") as IHTMLElement;
            if (string.IsNullOrEmpty(div.innerHTML))
            {
                div.innerHTML = sb.ToString();
            }
        }

        void browser_WBLButtonUp(object sender, csExWB.HTMLMouseEventArgs e)
        {
            MyBrowser hostBrowser = Host.Browser as MyBrowser;
            if (hostBrowser == null) return;

            IHTMLElement elem = e.SrcElement as mshtml.IHTMLElement;
            mshtml.HTMLImg img = elem as mshtml.HTMLImg;
            if (img != null && img.className == "select")
            {
                string text = img.parentElement.innerText;
                IHTMLInputElement searchInput = browser.GetElementByID(true, "keyword") as IHTMLInputElement;
                NLResult result = NatualLinkCreator.Instance.Create(text, LinkStyle.Standard);
                Clipboard.SetText(result.OutputText);
                AutoReplay(hostBrowser.LocationUrl, result.OutputText);
                this.Close();
            }
        }

        private void AutoReplay(string url, string text) {
            try
            {
                bool result = QueryScriptRule(url, text);
                if (!result)
                {
                    DefaultRule(text);
                }
            }
            catch (Exception ex) {
                logger.Fatal(ex);
            }
        }
        /// <summary>
        /// 将选择文本粘帖到主浏览器激活控件上
        /// </summary>
        /// <param name="text"></param>
        private void DefaultRule(string text) {
            IHTMLElement active = hostBrowser.GetActiveElement() as mshtml.IHTMLElement;
            if (active is mshtml.HTMLInputElement)
            {
                mshtml.HTMLInputElement input = active as mshtml.HTMLInputElement;
                input.value = text;
            }
            else if (active is mshtml.HTMLTextAreaElement)
            {
                mshtml.HTMLTextAreaElement area = active as mshtml.HTMLTextAreaElement;
                area.innerText = text;
            }
            else if (active is mshtml.HTMLBody)
            {

                string contentEditable = active.getAttribute("contentEditable", 0) as string;
                if (!string.IsNullOrEmpty(contentEditable) && contentEditable == "true")
                {
                    mshtml.HTMLBody body = active as mshtml.HTMLBody;
                    body.innerText = text;
                    return;
                }

                IHTMLElement frame = BrowserExtensions.GetFrame(active) as mshtml.IHTMLElement;
                if (frame != null)
                {
                    mshtml.HTMLBody body = active as mshtml.HTMLBody;
                    body.innerText = text;
                }
            }
            else
            {
                IHTMLElementCollection ec = hostBrowser.GetElementsByTagName(true, "TEXTAREA") as IHTMLElementCollection;
                if (ec.length > 0)
                {
                    mshtml.HTMLTextAreaElement area = ec.item(null, 0) as mshtml.HTMLTextAreaElement;
                    area.innerText = text;
                }
                else
                {
                    ec = hostBrowser.GetElementsByTagName(true, "INPUT") as IHTMLElementCollection;
                    mshtml.HTMLInputElement input = ec.item(null, 0) as mshtml.HTMLInputElement;
                    input.value = text;
                }
            }
        }

        /// <summary>
        /// 查找主浏览器网站有无对于脚本逻辑，如果有执行脚本逻辑
        /// </summary>
        private bool QueryScriptRule(string url, string text) {
            string domain = StringHelper.GetDomain(url);
            JObject jso = HttpRequestWapper.GetJsonObject(ServerApiInvoker.APIRoot + "get_comment_script?url=" + domain);
            if (jso == null) return false;
            string script = jso["script"].ToString();

            try {
                //JintCreator.Create(hostBrowser)
                //.SetParameter("comment", text)
                //.Run(script);
            }
            catch (Exception ex) {
                logger.Fatal(ex);
                return false;
            }
            return true;
        }

     
        public IPluginHost Host
        {
            get;
            set;
        }

        public Keys DataKey
        {
            get { return Keys.R | Keys.Control; }
        }


        public string StripItemText
        {
            get { return "回帖[CTRL+R]"; }
        }

        public System.Drawing.Bitmap StripItemImage
        {
            get { return Resources.at; }
        }
    }
}
