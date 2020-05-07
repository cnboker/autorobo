using System.Xml;
using AutoRobo.Core.Formatters;
using System;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionForward : ActionBase
    {
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("forward.bmp");
        }

        public override string ActionDisplayName
        {
            get { return "向前"; }
        }
        public override string GetDescription()
        {
            return "页面向前";
        }

        public override void Perform()
        {
            MyBrowser browser = AppContext.Browser as MyBrowser;
            //Window.Forward();
            browser.GoForward();
        }

     

   
       
    }
}
