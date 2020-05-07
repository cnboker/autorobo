using System;
using System.Xml;
using AutoRobo.Core.Formatters;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 运行关闭弹出窗口
    /// </summary>
    [Serializable]
    public class ActionCloseWindow : ActionBase
    {
        public string BrowserURL;
        public string BrowserTitle;

        public ActionCloseWindow() {}

        public ActionCloseWindow(string Title, string URL)
        {
            BrowserURL = URL;
            BrowserTitle = Title;
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            BrowserURL = Window.Url;
            BrowserTitle = Window.Title;
            return true;
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("CloseWindow.bmp");
        }

        public override void Perform()
        {
           // Window.Close();
            MyBrowser browser = AppContext.Browser as MyBrowser;
            browser.PopWindow(false); 
        }

        public override string GetDescription()
        {
            string description = "关闭弹出窗口 " + BrowserTitle;
            if (BrowserTitle == "") description = "关闭弹出窗口 " + BrowserURL;
            return description;
        }



     

        public override string ActionDisplayName
        {
            get { return "窗体关闭"; }
        }
    }
}
