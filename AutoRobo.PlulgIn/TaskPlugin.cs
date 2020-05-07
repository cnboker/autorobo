using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Plugins;
using System.Windows.Forms;
using AutoRobo.PlulgIn.Properties;

namespace AutoRobo.PlulgIn
{
    
    public class TaskPlugin : IPlugin
    {
  
        public IPluginHost Host
        {
            get;
            set;
        }

        public Keys DataKey
        {
            get { return Keys.T | Keys.Control; }
        }


        public string StripItemText
        {
            get { return "页面设别器"; }
        }

        public System.Drawing.Bitmap StripItemImage
        {
            get { return Resources.taskButton; }
        }


        public void Show()
        {
            
        }
    }
}
