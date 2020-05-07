using System;
using System.Drawing;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionDirectionKey : ActionBase
    {
        public string DirectionKey { get; set;}

       
        public ActionDirectionKey()
        {
            DirectionKey = "";
           
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            //ParentPage = NewWebPage;
            CheckDuplication = false;
            return true;
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucDirectKey(editorAction);
        }

        public override Bitmap GetIcon()
        {
            return GetIconFromFile("TypeText.bmp");
        }
        public override string ActionDisplayName
        {
            get { return "发送键值"; }
        }
        public override void Perform()
        {
            string val = Eval(MapName);
            if (!string.IsNullOrEmpty(val))
            {
                DirectionKey = val;
            }
            System.Windows.Forms.SendKeys.SendWait(DirectionKey);            
        }

        public override string GetDescription()
        {
            return "键入 " + DirectionKey;
        }

        public override bool Validate()
        {
            return true;
        }



        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            DirectionKey = node.Attributes["DirectionKey"].Value;
            if (node.Attributes["MapName"] != null) MapName = node.Attributes["MapName"].Value;
        }

   
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("DirectionKey", DirectionKey);
            if (!string.IsNullOrEmpty(MapName))
            {
                writer.WriteAttributeString("MapName", MapName);
            }    
            base.WriterAddAttribute(writer);
        }
    }
}
