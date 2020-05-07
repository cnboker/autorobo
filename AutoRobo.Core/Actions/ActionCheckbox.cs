using System;
using System.Drawing;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionCheckbox : ActionElementBase
    {      
        //private IfacesEnumsStructsClasses.IHTMLElement ActiveElement { get; set; }
        public bool Checked { get; set; }

        public ActionCheckbox()
        {
            ElementType = ElementTypes.CheckBox;            
        }

        public override string ElementName
        {
            get
            {
                return FindMechanism[0].FindValue;
            }
        }

        public override string DefaultValue
        {
            get
            {
                return Checked.ToString();
            }
        }

        //public override string IHTMLElementValue
        //{
        //    get
        //    {
        //        var element = GetIHTMLElement();
        //        return element.outerHTML.ToLower().Contains("checked") ? "1" : "0";
        //    }
        //}
        public override string ActionDisplayName
        {
            get { return "¸´Ñ¡¿ò"; }
        }
        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucCheckBox(editorAction);
        }

        public override Bitmap GetIcon()
        {
            return GetIconFromFile("CheckBox.bmp");
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            Checked = !(parameter.Element.outerHTML.ToLower().Contains("checked") == true);
            return base.Parse(parameter);
        }
        public override void Perform()
        {

            string val = Eval(MapName);

            if (!string.IsNullOrEmpty(val))
            {
                Checked = (val == "1");
            }

            var element = GetElement() as CheckBox;
            if (element != null)
            {
                element.Checked = Checked;
            }
        }

 
        public override bool Validate()
        {
            bool result;

            try
            {
                var element = (CheckBox)GetElement();
                result = element.Checked == Checked;
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
            Checked = node.Attributes["Checked"].Value == "1";            
        }

       
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("Checked", Checked ? "1" : "0");
            base.WriterAddAttribute(writer);
        }
    }
}
