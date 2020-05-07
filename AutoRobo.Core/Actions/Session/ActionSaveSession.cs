using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Util;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 保存Session
    /// </summary>
    public class ActionSaveSession : ActionBase
    {
        //private string key = "watin-url-cookie";
        public override string ActionDisplayName
        {
            get { return "保存Session"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("sessions.png");
        }

        public override void Perform()
        {
            Window.SavePageSession();
            
        }

        public override string GetDescription()
        {
            return "保存Session";
        }


    }
}
