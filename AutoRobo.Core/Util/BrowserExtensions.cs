using System;
using System.Linq;
using AutoRobo.Core.Actions;
using WatiN.Core;
using WatiN.Core.Constraints;
using SHDocVw;
using System.Threading;
using System.Text.RegularExpressions;
using Util;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using WatiN.Core.Native.InternetExplorer;
using System.Text;
using AutoRobo;
using AutoRobo.Core;
using AutoRobo.WebHelper;
using mshtml;

namespace WatiN.Core
{
    static public class BrowserExtensions
    {
        static log4net.ILog logger = log4net.LogManager.GetLogger("BrowserExtensions");

        /// <summary>
        /// 获取元素容器iframe, 如果不存在返回null
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        static public IHTMLElement GetFrame(IHTMLElement element)
        {
            if (element == null) return null;
            mshtml.IHTMLDocument2 doc = null;
            doc = element.document as mshtml.IHTMLDocument2;
            return GetFrame(doc);
        }

        /// <summary>
        /// 获取文档也包含iframe列表,支持跨域查找
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        static public IHTMLElementCollection GetFrames(IHTMLDocument2 doc) {
            IHTMLDocument3 doc3 = doc as IHTMLDocument3;
            return doc3.getElementsByTagName("iframe");
            //mshtml.IHTMLWindow2 window2 = null;
            //window2 = doc.parentWindow as mshtml.IHTMLWindow2;
            //SHDocVw.IWebBrowser2 browser = CrossFrameIE.GetIWebBrowser2(window2);
            //mshtml.IHTMLDocument3 parentDoc = browser.Parent as mshtml.IHTMLDocument3;
            //if (parentDoc == null) return null;
            //return parentDoc.getElementsByTagName("iframe");            
        }

        static public IHTMLElement GetFrame(mshtml.IHTMLDocument2 doc)
        {
            IHTMLElement frame = null;
            mshtml.IHTMLWindow2 window2 = null; 
            try
            {
                window2 = doc.parentWindow as mshtml.IHTMLWindow2;
                if (window2 != null)
                {
                    return ((mshtml.HTMLWindow2)window2).frameElement as IHTMLElement;
                }     
            }
            catch (UnauthorizedAccessException) {
                //主页面和IFRAME页面处于不同域名的时候会报UnauthorizedAccessException，下面通过IHTMLWindow2->IWebBrowser2
                //比较IWebBrowser2和iframe元素url定位iframe
                SHDocVw.IWebBrowser2 browser = CrossFrameIE.GetIWebBrowser2(window2);
                mshtml.IHTMLDocument3 parentDoc = browser.Parent as mshtml.IHTMLDocument3;
                mshtml.IHTMLElementCollection framesCollection = parentDoc.getElementsByTagName("iframe") as mshtml.IHTMLElementCollection;
                foreach (mshtml.IHTMLElement f in framesCollection)
                {
                    SHDocVw.IWebBrowser2 wb2 = (SHDocVw.IWebBrowser2)((mshtml.HTMLFrameElement)f);
                    if (wb2.LocationURL == browser.LocationURL)
                    {
                        frame = f as IHTMLElement;
                        break;
                    }
                } 
            }
            catch (Exception)
            {
                return null;
            }
            return frame;
        }

        /// <summary>
        /// 备份当前页面SESSION, 需要IE8版本以上浏览器才可以支持， 所以应用程序需要修改注册表设置模拟器版本
        /// 做法:
        /// </summary>
        static public void SavePageSession(this Browser browser) { 
            RequireJQueryInstall(browser);
            string fileName = browser.Uri.Host;
            fileName = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), string.Format("{0}.session", fileName));
          
            string code = @"
                (function(){
                    return JSON.stringify({
                    url: window.location.href,                    
                    form: $jq('form').serialize()
                 });
                })()            
                ";
            object result = JS.Eval(browser, code);
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(fileName, false);
                sw.Write(result.ToString());
            }
            finally {
                if (sw != null) {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// 还原当前页面SESSION
        /// </summary>
        static public void RestorePageSession(this Browser browser)
        {
            RequireJQueryInstall(browser);                        
            string fileName = browser.Uri.Host;
            fileName = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.ApplicationData), string.Format("{0}.session", fileName));
            StreamReader sw = null;
            string session = "";
            if (!File.Exists(fileName)) return;
            try
            {
                sw = new StreamReader(fileName);
                session = sw.ReadToEnd();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }

            string code = @"
                    var obj = " + session + @";
                          
                    return obj.url;       
                ";
            object url = JS.FunEval(browser, code);
            if (url != null)
            {
                //先打开原始页面
                browser.GoTo(url.ToString());
                //回复FORM内容
                code = @"
                    var obj =" + session + @";
                    $jq('form').deserialize(obj.form);                          
                   ";
                IEUtils.RunScript(code, GetWindows(browser));
                browser.AttachToIE();
            }      
        }

        static public mshtml.IHTMLWindow2 GetWindows(this Browser browser) {
            mshtml.IHTMLDocument2 doc = ((IEDocument)(browser.DomContainer.NativeDocument)).HtmlDocument;
            return doc.parentWindow;
        }
        static public void RequireJQueryInstall(this Browser browser){            
            mshtml.IHTMLDocument2 doc = ((IEDocument)(browser.DomContainer.NativeDocument)).HtmlDocument;
            IEUtils.RunScript(ScriptLoader.GetJqueryInstallScript(), doc.parentWindow);
         }
      

        /// <summary>
        /// 获取某一个元素的屏幕位置
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        static public Rectangle GeScreenPosition(this Browser browser, IHTMLElement element) {
            Rectangle rect = GetAbsolutePosition(browser, element);
            IHTMLElement frame = GetFrame(element);
            Rectangle frameRect ;
            while (frame != null) { 
                frameRect =  GetAbsolutePosition(browser, frame);
                rect.Offset(frameRect.X, frameRect.Y);
                frame = GetFrame(frame);
            }
            //frame = GetFrame(frame);           
            //if (frame != null)
            //{
            //    frameRect = GetAbsolutePosition(browser, frame);
            //    rect.Offset(frameRect.X, frameRect.Y);
            //}
            int width = element.offsetWidth;
            int height = element.offsetHeight;
            MyBrowser my = browser.GetMyBrowser();
            rect.Offset(my.PointToScreen(Point.Empty));
            return rect;
        }

        

        static public Rectangle GetAbsolutePosition(this Browser browser, IHTMLElement element)
        {
            int x = element.offsetLeft;
            int y = element.offsetTop;

            IHTMLElement offsetParent = element.offsetParent;
            while (offsetParent != null)
            {
                x += offsetParent.offsetLeft;
                y += offsetParent.offsetTop;
                offsetParent = offsetParent.offsetParent;
            }
            int width = element.offsetWidth;
            int height = element.offsetHeight;
            return new Rectangle(x, y, width, height);
        }

        static public ElementCollection GetElementsByXPath(this IElementContainer container, string xpath)
        {
            DateTime now = DateTime.Now;
            xpath = xpath.Trim();
            if (xpath[xpath.Length - 1] == '/')
            {
                xpath = xpath.Substring(0, xpath.Length - 1);
            }
            var constraint = ActionElementBase.GetConstraint(new FindAttribute()
            {
                FindMethod = FindMethods.XPath,
                FindValue = xpath,
                Regex = true,
            });
            string tagName = GetLastTag(xpath);

            int index = xpath.IndexOf("*");
            ElementCollection elements = null;
            if (index > 0)
            {
                elements = GetElementsByXPath(container, xpath.Substring(0, xpath.Substring(0, index).LastIndexOf("/")));

                if (elements.Count > 0)
                {
                    return GetElementsByTag((IElementContainer)elements[0], tagName, constraint);
                }
                return null;
            }
        
            elements = GetElementsByTag(container, tagName, constraint);
            logger.Info("GetElementsByXPath:" + DateTime.Now.Subtract(now).TotalMilliseconds);
            return elements;
        }

        static public ElementCollection GetElementsByTag(this IElementContainer container, string tagName)
        {
            return GetElementsByTag(container, tagName, null);
        }

        static public ElementCollection GetElementsByTag(this IElementContainer container, string tagName, Constraint constraint)
        {
           
            string[] parameters = null;
            if (tagName.ToLower() == "input")
            {
                parameters = new string[] { "checkbox", "text", "hidden", "radio", "password", "button", "submit", "reset", "file", "image" };
            }

            ElementCollection elements = null;
            elements = container.ElementsWithTag(tagName, parameters);
            if (constraint != null)
            {
                elements = elements.Filter(constraint);
            }
            if (!elements.Any())
            {
                Browser browser = container as Browser;
                if (browser != null)
                {
                    foreach (var frame in browser.Frames)
                    {
                        elements = frame.ElementsWithTag(tagName, parameters);
                        if (constraint != null)
                        {
                            elements = elements.Filter(constraint);
                        }
                        if (elements.Any()) break;
                    }
                }
            }
           
            return elements;
        }

      

        static public string GetLastTag(string xpath)
        {
           
            int e1 = xpath.LastIndexOf("[");
            int e2 = xpath.LastIndexOf("/");
            return e1 < e2 ? xpath.Substring(e2 + 1) : xpath.Substring(e2 + 1, e1 - e2 - 1);
        }

        /// <summary>
        /// 等待满足条件返回， 30秒超时
        /// </summary>
        /// <param name="condition"></param>
        public static void Wait(this Browser browser, Func<bool> condition)
        {
            var timeout = DateTime.UtcNow + TimeSpan.FromSeconds(30);

            while (!condition() && DateTime.UtcNow < timeout)
            {
                Pause();
            }

            Pause();
        }

        private static void Pause(double seconds = 0.5)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        public static MyBrowser GetMyBrowser(this Browser browser) {
            BrowserWindow bw = browser as BrowserWindow;
            if (bw == null) return null;
            return bw.WBBrowser as MyBrowser;
        }

        public static void TryWait(this Browser browser, Action action)
        {
            TryWait(browser, action, null);
        }

        /// <summary>
        /// 执行一段逻辑，异常重新执行，直到超时
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="action"></param>
        /// <param name="exceptionAction"></param>
        public static void TryWait(this Browser browser, Action action, Action exceptionAction)
        {
            browser.Wait(() =>
            {
                try
                {
                    action();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("RCW"))
                    {
                        if (exceptionAction != null)
                        {
                            logger.Info("RCW exception execute exceptionAction");
                            exceptionAction();
                        }
                    }
                    logger.Fatal(ex);
                    return false;
                }
            });
        }

 
        static public void InjectScript(this Browser browser, string script)
        {
            AttachToIE(browser);
            browser.RunScript(script);

            //foreach (var frame in browser.Frames)
            //{
            //    try
            //    {
            //        frame.RunScript(script);
            //    }
            //    catch { }
            //}
            ////重新挂接浏览器
            //AttachToIE(browser);
        }

        static public void AttachToIE(this Browser browser) {
            BrowserWindow ie = browser as BrowserWindow;
            if (ie == null) return;
            ie.AttachToIE();
        }
        static private void TryInjectScript(this Browser browser, Action action)
        {
            browser.TryWait(
            () =>
            {
                action();
            },
            () =>
            {
                BrowserWindow ie = browser as BrowserWindow;
                ie.AttachToIE();
                logger.Info("TryInjectScript has attachToIE");
            });
        }

   
   

        static private int GetElmentCount(ElementCollection elements)
        {
            int count = 0;
            AutoRobo.Core.App.Wait(() =>
            {
                try
                {
                    count = elements.Count;
                    return true;
                }
                catch
                {
                    return false;
                }
            });
            return count;
        }

       

    }
}
