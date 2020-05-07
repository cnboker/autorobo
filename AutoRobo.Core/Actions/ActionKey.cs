using System;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionKey : ActionElementBase
    {
    
        public enum KeyFunctions
        {
            Up, Down, Press
        }

        public KeyFunctions KeyFunction { get; set; }
        public char KeyToPress { get; set; }

        public ActionKey(){
        }

    
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            CheckDuplication = false;
            base.Parse(parameter);
            return true;
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucKey(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("KeyPress.bmp");
        }

        public override string GetDescription()
        {
            return "°´¼ü "+KeyFunction;
        }

        public override void Perform()
        {
            Element element = GetElement();

            if (element != null)
            {
                if (KeyFunction == KeyFunctions.Up) element.KeyUp(KeyToPress);
                else if (KeyFunction == KeyFunctions.Down) element.KeyDown(KeyToPress);
                
                else element.KeyPress(KeyToPress);
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
            KeyFunction = (KeyFunctions) Enum.Parse(typeof(KeyFunctions), node.Attributes["KeyFunction"].Value);
            KeyToPress = Convert.ToChar(node.Attributes["KeyToPress"].Value);            
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("KeyFunction", KeyFunction.ToString());
            writer.WriteAttributeString("KeyToPress", KeyToPress.ToString());
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "°´¼ü"; }
        }
    }
}
