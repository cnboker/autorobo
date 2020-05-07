using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using System.Threading;
using AutoRobo.Core;
using SHDocVw;

namespace AutoRobo.Core.ns
{
    /// <summary>
    /// autorobo javascript api
    /// </summary>
    public class JsBrowser
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ns.IE");

       
        private Browser _browser = null;

        private Browser browser {
            get {               
                return _browser;
            }
        }

        public JsBrowser(Browser browser)
        {
            this._browser = browser;
        }
       

        public string url {
            get {
                return browser.Uri.ToString();
            }
        }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="url"></param>
        public void nav(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                browser.GoTo(url);
             
                while (((SHDocVw.InternetExplorer)((IE)browser).InternetExplorer).Busy
            || ((SHDocVw.InternetExplorer)((IE)browser).InternetExplorer).ReadyState != tagREADYSTATE.READYSTATE_COMPLETE)
                {
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);                    
                }
            }
        }

        public void eval(string javascript)
        {
            try
            {
                logger.Info(javascript);
                browser.RunScript(javascript);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
        /// <summary>
        /// 随机链接点击
        /// </summary>
        /// <param name="time"></param>
        public void anyClick(double time)
        {
            anyClick(time, 5000);
        }

        /// <summary>
        /// 在当前页随机点击
        /// </summary>
        public void anyClick(double time, double waitTime)
        {
            try
            {
                Random r = new Random(DateTime.Now.Second * DateTime.Now.Year);
                int t = Convert.ToInt16(time);
                if (t > 3)
                {
                    t = 3;
                }
                for (int i = 0; i < t; i++)
                {
                    int len = browser.Links.Count;
                    if (len == 0) break;
                    int seed = r.Next(0, len - 1);
                    var link = browser.Links[seed];
                    if (link.Url.ToLower().IndexOf("mailto:") >= 0) continue;
                    if (link.Text == "注销") continue;
                    link.Click();
                    Thread.Sleep(Convert.ToInt32(waitTime));
                }
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
        }
    }

}
