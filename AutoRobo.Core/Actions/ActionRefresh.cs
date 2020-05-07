using System.Xml;
using AutoRobo.Core.Formatters;
using System;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionRefresh : ActionBase
    {
      
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("refresh.bmp");
        }

        public override string GetDescription()
        {
            return "页面刷新";
        }

        public override void Perform()
        {
            //Window.Refresh();
            MyBrowser browser = AppContext.Browser as MyBrowser;
            browser.Refresh();
        }

        public override string ActionDisplayName
        {
            get { return "刷新"; }
        }

    

    
    }
}
