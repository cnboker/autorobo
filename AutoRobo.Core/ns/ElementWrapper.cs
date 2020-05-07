using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MouseKeyboardLibrary;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using mshtml;

namespace AutoRobo.Core.ns
{
    public class ElementWrapper
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ns.ElementWrapper");
        private Element element = null;
        private Browser browser = null;

        public Element Element
        {
            get
            {
                return element;
            }
        }

        public string Text
        {
            get
            {
                return element.Text;
            }
        }
        public ElementWrapper previous
        {
            get
            {
                return new ElementWrapper(browser, element.PreviousSibling);
            }
        }
        public ElementWrapper next
        {
            get
            {
                //return element.NextSibling;
                return new ElementWrapper(browser, element.NextSibling);
            }
        }

        public ElementWrapper parent {
            get {
                return new ElementWrapper(browser, element.Parent);
            }
        }

        public void show() {
            if (element == null) return;
            element.Show();
        }

        public void hide() {
            if (element == null) return;
            element.Hide();
        }

        /// <summary>
        /// 获取直接第一个子元素
        /// </summary>
        public ElementWrapper child() {
            if (element == null) return null;
            var _child = element.Children().SingleOrDefault();
            if (_child != null)
            {
                return new ElementWrapper(browser, _child);
            }
            return null;
        }

        /// <summary>
        /// 获取子元素（不一定是直接子元素）
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public ElementWrapper child(string selector)
        {
            if (element == null) return null;
            return Method<ElementWrapper>.Watch("child selector:" + selector, () =>
            {
                return find(selector).FirstOrDefault();
            });
           
        }

        public bool contain(string keyword) {
            if (element == null) return false;
            return element.Text.Contains(keyword);
        }

        /// <summary>
        /// 子元素遍历, 非直接子元素
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="fun"></param>
        public void each(string selector, Action<int, ElementWrapper> fun) {
            try
            {
                Method<object>.Watch("each," + selector, () =>
                {
                    var elements = find(selector);
                    int index = 0;
                    foreach (var o in elements)
                    {                       
                        if (fun != null)
                        {
                            fun(index, o);
                        }
                        index++;
                    }
                });
            }
            catch (Exception ex) {
                logger.Fatal(ex);
            }
        }

        private IEnumerable<ElementWrapper> find(string selector)
        {
            IElementContainer iec = element as IElementContainer;
            return iec.Elements.Filter(Find.BySelector(selector)).Select(c => new ElementWrapper(browser, c));
        }
        /// <summary>
        /// 获取直接子元素列表
        /// </summary>
        /// <param name="fun"></param>
        public void each(Action<int, ElementWrapper> fun) {
            int index = 0;
            foreach (var o in element.Children()) {                
                if (fun != null) {
                    fun(index, new ElementWrapper(browser, o));
                }
                index++;
            }
        }

        /// <summary>
        /// 当前元素先前跳到下几个元素
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public ElementWrapper skip(int n)
        {
            if (n == 0) return this;
            Element next = element;
            while (n > 0)
            {
                next = next.NextSibling;
                if (next == null) break;
                n--;
            }
            //return next;
            return new ElementWrapper(browser, next);
        }

        public string attr(string attr) {
            if (element == null) return "";
            return element.GetAttributeValue(attr);
        }

        public void val(string text) {
            if (element == null) return;
            TextField textField = element as TextField;
            if (textField != null)
            {
                textField.Value = text;
            }
            else {
                element.SetAttributeValue("Innerhtml", text);
            }
        }

        public void appendText(string text)
        {
            if (element == null) return;
            TextField textField = element as TextField;
            if (textField == null) return;
            textField.AppendText(text);
        }

        public ElementWrapper skipBack(int n)
        {
            if (n == 0) return this;
            Element previous = element;
            while (n > 0)
            {
                previous = previous.PreviousSibling;
                if (previous == null) break;
                n--;
            }
            //return previous;
            return new ElementWrapper(browser, previous);
        }

        public ElementWrapper(Browser browser, Element element)
        {
            this.browser = browser;
            this.element = element;
        }

        public void typeText(string text)
        {
            TextField textField = element as TextField;
            if (textField == null) return;
            textField.TypeText(text);
        }

        public string value {
            get {
                TextField textField = element as TextField;
                if (textField == null) return "";
                return textField.Value;
            }
        }

        public string text {
            get {
                return element.Text;
            }
        }

        public string html {
            get {
                return element.InnerHtml;
            }
        }

        public void click()
        {
            if (element != null)
            {
                var nativeElement = (IEElement)element.NativeElement;
                IHTMLElement el = (IHTMLElement)nativeElement.AsHtmlElement;
                nativeElement.AsHtmlElement.scrollIntoView();
                try
                {
                    string url = "";
                    IHTMLDOMNode node = el as IHTMLDOMNode;
                    bool _blank = false;
                    if (node.nodeName == "A")
                    {
                        var target = el.getAttribute("target", 0);
                        url = el.getAttribute("href", 0).ToString();
                        //logger.Info("click url:" + url);
                        if (url.ToLower().IndexOf("mailto:") >= 0) return;
                        //如果是<a>标签， target="_blank", 则增加导航
                        if (target != null && target.ToString() == "_blank")
                        {
                            _blank = true;
                        }
                    }

                    element.Click();
                    if (_blank)
                    {
                        System.Threading.Thread.Sleep(200);
                        browser.GoTo(url);
                    }
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }
            }
        }


        private void SimulateClick(IHTMLElement el)
        {
            BrowserWindow bw = browser as BrowserWindow;
            if (bw != null)
            {
                MyBrowser wb = bw.WBBrowser as MyBrowser;

                Point p = wb.GetScroll();
                int scrollTop = p.Y;
                int scrollLeft = p.X;
                logger.InfoFormat("scrolltop:{0},scrollleft:{1}", scrollTop, scrollLeft);
               
                Rectangle rect = browser.GetAbsolutePosition(el);
                
                System.Windows.Forms.Control control = wb as System.Windows.Forms.Control;
                //获取控件绝对位置
                Point location = control.PointToScreen(Point.Empty);

                Point newPosition = new Point(rect.X + rect.Width / 2 - scrollLeft, rect.Y + location.Y - scrollTop + 2);
                logger.InfoFormat("new position, x:{0}, y:{0}", newPosition.X, newPosition.Y);
                AutoRobo.Core.Macro.Macror.LinearSmoothMove(newPosition, 200);
                MouseSimulator.X = newPosition.X;
                MouseSimulator.Y = newPosition.Y;
                MouseSimulator.Click(System.Windows.Forms.MouseButtons.Left);
            }
        }

    }
}
