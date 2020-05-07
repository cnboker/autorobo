using System;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using System.Threading;
using AutoRobo.DataMapping;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionSleep : ActionBase
    {
        public override string DefaultValue
        {
            get
            {
                return SleepTime.ToString();
            }        
        }
        public int SleepTime { get; set; }


        public override string ActionDisplayName
        {
            get { return "休眠"; }
        }

        public ActionSleep() {
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {            
            SleepTime = 30;          
            return true;
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("Sleep.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSleep(editorAction);
        }

        public override string GetDescription()
        {
            return "等待 " + SleepTime + " 毫秒.";
        }

        public override void Perform()
        {   
            Thread.Sleep(SleepTime);
        }


      
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("SleepTime", SleepTime.ToString());
            base.WriterAddAttribute(writer);
        }
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            SleepTime = Convert.ToInt32(node.Attributes["SleepTime"].Value);        
        }
       
    }
}
