using System;
using System.Drawing;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using mshtml;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionRadio : ActionElementBase
    {
      
        private IHTMLElement ActiveElement { get; set; }
        public bool Checked { get; set; }
        
        public ActionRadio()
        {
            ElementType = ElementTypes.RadioButton;            
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return null;
        }

        public override Bitmap GetIcon()
        {
            return GetIconFromFile("RadioButton.bmp");
        }

        public override string ActionDisplayName
        {
            get { return "µ¥Ñ¡¿ò"; }
        }
        //public override string IHTMLElementValue
        //{
        //    get
        //    {
        //        var element = GetIHTMLElement();
        //        return element.outerHTML.ToLower().Contains("checked") ? "1" : "0";
        //    }
        //}
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
             Checked = !(parameter.Element.outerHTML.ToLower().Contains("checked") == true) ;
            return base.Parse(parameter);
        }

        public override void Perform()
        {
            string val = Eval(MapName);
            if (!string.IsNullOrEmpty(val))
            {
                Checked = Convert.ToBoolean(val);
            }
            var element = (RadioButton)GetElement();
            if (element != null)
            {
                ActiveElement = (IHTMLElement)((IEElement)element.NativeElement).AsHtmlElement;
                element.Checked = Checked;
            }
        }

        public override bool Validate()
        {
            bool result;

            try
            {
                var element = (RadioButton)GetElement();
                result = element.Checked == Checked;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }

            return result;
        }

        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var line = new CodeLine();
        //    line.Attributes = FindMechanism;
        //    var builder = new StringBuilder();
        //    line.Frames = FrameList;
        //    line.ModelPath = GetDocumentPath(Formatter);
        //    builder.Append(line.ModelPath);
        //    builder.Append(Formatter.MethodSeparator);
        //    builder.Append(ElementType);
        //    builder.Append("(" + FindMechanism.ToString() + ")");
        //    line.ModelLocalProperty = builder.ToString();
        //    builder.Append(Formatter.MethodSeparator);
        //    line.ModelFunction = "Checked = " + Checked.ToString().ToLower() + Formatter.LineEnding;
        //    builder.Append(line.ModelFunction);
        //    line.FullLine = builder.ToString();
        //    return line;
        //}

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
