using System;
using System.Xml;
using AutoRobo.Core.Formatters;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 运行弹出窗口
    /// </summary>
    [Serializable]
    public class ActionOpenWindow : ActionBase
    {
       
        public string BrowserURL;
        public string BrowserTitle;

        public ActionOpenWindow(){}

        public ActionOpenWindow(string Title, string URL)
        {
            BrowserURL = URL;
            BrowserTitle = Title;
        }
        public override string ActionDisplayName
        {
            get { return "窗体打开"; }
        }

        /// <summary>
        /// retrieve the icon for the action
        /// </summary>
        /// <returns>icon as a bitmap</returns>
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("window.bmp");
        }

        public override void Perform()
        {
            MyBrowser browser = AppContext.Browser as MyBrowser;
            browser.PopWindow(true); 
        }

        public override string GetDescription()
        {
            string description = "打开弹出窗口 " + BrowserTitle;
            if (BrowserTitle == "") description = "Open Window " + BrowserURL;
            return description;
        }


    }
}
