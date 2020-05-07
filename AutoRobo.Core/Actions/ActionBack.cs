using System.Xml;
using AutoRobo.Core.Formatters;
using System;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionBack : ActionBase
    {
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("back.bmp");
        }

        public override string GetDescription()
        {
            return "页面后退";
        }

        public override void Perform()
        {
            //Window.Back();            
            ICoreBrowser browser = AppContext.Browser as ICoreBrowser;
            browser.GoBack();
        }

        public override string ActionDisplayName
        {
            get { return "后退"; }
        }
    }
}
