using AutoRobo.UserControls;
using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    public class ActionOpenDetailPage : ActionBlock
    {       
        /// <summary>
        /// 父浏览器
        /// </summary>
        private ICoreBrowser parentBrwoser;
        private string BrowserName { get; set; }

        public ActionOpenDetailPage() {
            BrowserName = "Browser" + DateTime.Now.Ticks.ToString();
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            string cssSelector = AppContext.Browser.Selector.CssSelector;
            IHTMLElement el = AppContext.Browser.Selector.SelectorElement;
            if (string.IsNullOrEmpty(cssSelector))
            {
                MessageBox.Show("通过\"选择器\"先选择内容");
                return false;
            }
            SetFindMethod(el, FindMethods.CssSelector, cssSelector);
            return true;
        }

        public override void Perform()
        {
            try
            {
                List<string> list = GetHrefList();
                parentBrwoser = AppContext.Browser;
                AppContext.Browser = AppContext.MainWindow.Switch(BrowserName);

                for (int i = 0; i < list.Count; i++)
                {
                    AppContext.Browser.Navigate(list[i]);
                    base.Perform();
                    Thread.Sleep(1000);
                }
                AppContext.Browser = parentBrwoser;

            }
            catch (Exception ex) {
                AppContext.Browser = parentBrwoser;
            }
        }

        private List<string> GetHrefList() {
            ElementCollection elements = GetWindow().Elements.Filter(GetConstraint());
            List<string> list = new List<string>();
            foreach (var el in elements)
            {
                Link anchor = el as Link;
                if (anchor == null)
                {
                    anchor = el.Parent as Link;
                }
                if(anchor != null){
                    list.Add(anchor.Url);
                }
            }
            return list;
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            BrowserName = GetAttribute(node, "BrowserName");
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("BrowserName", BrowserName.ToString());
            base.WriterAddAttribute(writer);
        }

        public override string ActionDisplayName
        {
            get { return "打开详情页面"; }
        }

        public override string GetDescription()
        {
            return "打开详情页面";
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("popwindow.png");
        }

        public override void Enter()
        {
            parentBrwoser = AppContext.Browser;
            List<string> list = GetHrefList();
            ICoreBrowser detailBrowser = AppContext.MainWindow.Switch(BrowserName);
            if (list.Count > 0) {
                detailBrowser.Navigate(list[0]);
            }
        }

        public override void Exit()
        {
            System.Windows.Forms.Control c = parentBrwoser as System.Windows.Forms.Control;
            AppContext.MainWindow.Switch(c.FindForm().Name);
        }
    }
}
