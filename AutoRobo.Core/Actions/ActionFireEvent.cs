using System;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionFireEvent : ActionElementBase
    {     
        public string EventName { get; set; }
        public bool NoWait { get; set; }
        public NameValueCollection EventParameters { get; set; }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucFireEvent(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("FireEvent.bmp");
        }

        public override string GetDescription()
        {
            return "触发事件 "+EventName;
        }

        public override void Perform()
        {
            Element element = GetElement();
            if (element != null)
            {
                if (NoWait) element.FireEventNoWait(EventName, EventParameters);
                else element.FireEvent(EventName, EventParameters);
            }
        }

        public override bool Validate()
        {
            bool result;

            try
            {
                Element element = GetElement();
                result = element.Exists;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }

            return result;
        }

      
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            NoWait = node.Attributes.GetNamedItem("NoWait").ToString() == "1";
            EventName = node.Attributes.GetNamedItem("EventName").ToString();
            XmlNodeList list = node.SelectNodes("EventParameter");
            if (list != null)
            {
                foreach (XmlNode child in list)
                {
                    EventParameters.Add(child.Attributes["Key"].Value, child.Attributes["Value"].Value);
                }
            }            
        }

     

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("EventName", EventName);
            writer.WriteAttributeString("NoWait", NoWait ? "1" : "0");

            for (int i = 0; i < EventParameters.Count; i++)
            {
                string key = EventParameters.AllKeys[i];
                writer.WriteStartElement("EventParameter");
                writer.WriteAttributeString("Key", key);
                writer.WriteAttributeString("Value", EventParameters[key]);
                writer.WriteEndElement();
            }
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "事件"; }
        }
    }
}
