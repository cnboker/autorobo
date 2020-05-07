using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using Util;
using AutoRobo.Core.ActionBuilder;
using mshtml;
using AutoRobo.Core.Models;
using System.Drawing;
using MouseKeyboardLibrary;


namespace AutoRobo.Core.Actions
{
 [Serializable]
    public class ActionTypeText : ActionElementBase
    {       
        public bool ValueOnly = true;
        public bool Overwrite = true;
        public string TextToType { get; set; }
        public string FormatString { get; set; }

        public ActionTypeText() {
            CheckDuplication = true;
        }

 
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            IHTMLElement activeElement = parameter.Element;
            if (activeElement == null) return false;
            SetFindMethod(parameter.Element);
            string value = ActiveElementAttribute(activeElement, "value");
            if (!string.IsNullOrEmpty(value)) {
                TextToType = value;
            }          
            Overwrite = true;
            return true;
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucTypeText(editorAction);
        }     

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("TypeText.bmp");
        }

   
        public override string DefaultValue
        {
            get
            {
                return TextToType;
            }
          
        }

        //public override string IHTMLElementValue
        //{
        //    get
        //    {
        //        string val = "";
        //        var el = GetElement();
        //        if (ElementType == ElementTypes.TextField) {
        //            val = el.GetAttributeValue("value").ToString(); 
        //        }else{
        //            val = el.GetAttributeValue("Innerhtml").ToString();
        //        }
        //        return val;
        //    }
        //}

        public override void Perform()
        {
            string val = Eval(MapName);
            if (!string.IsNullOrEmpty(val))
            {
                TextToType = val;
            }

            var constraint = GetConstraint();

            if (ElementType == ElementTypes.TextField || ElementType == ElementTypes.ValidateCode)
            {
                TextField field = GetWindow().TextField(constraint);
                TextToType = Format(TextToType);
                //获取纯文本
                TextToType = StringParser.removeHtml(TextToType);
                if (string.IsNullOrEmpty(TextToType)) return;
                if (ValueOnly)
                {                   
                    field.Value = TextToType;                    
                }
                else if (Overwrite)
                {
                    field.TypeText(TextToType);
                }
                else
                {
                    field.AppendText(TextToType);
                }
               
            }
            else
            {
                var el = GetElement();
                string text = TextToType;
                //if (ScriptCaller.LinkCreator != null) { 
                //    text = ScriptCaller.LinkCreator.Create(text);
                //}
              
                el.SetAttributeValue("Innerhtml", text.Replace("\n","<br/>"));
              
                //Rectangle rect = Window.GeScreenPosition(el.GetHtmlElement());

                //MouseSimulator.Position = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                //MouseSimulator.Click(MouseButton.Left);
                //Clipboard.SetText(text);
                //Window.GetMyBrowser().ExecCommand(false, "Paste", false, text);
            }
        }

        
        private string Format(string text)
        {
            if (string.IsNullOrEmpty(FormatString)) return text;
            var ret = "";
            if (FormatString[0] == '{' && FormatString[FormatString.Length - 1] == '}')
            {
                var arr = FormatString.Substring(1, FormatString.Length - 2).Split(":".ToCharArray());
                var type =  Convert.ToInt16(arr[0]);
                var  size = Convert.ToInt16(arr[1]);
                ret = RandomStringHelper.RandomString((RandomType)Enum.Parse(typeof(RandomType), type.ToString()), size);
            }
            return ret;
        }

        public override bool Validate()
        {
            bool result;
            try
            {
                string text = ((TextField)GetElement()).Value;
                result = text == TextToType;
                if (!result) ErrorMessage = "Text does not match";
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
        //    if (ValueOnly) line.ModelFunction = "Value = \"" + PerformReplacement(TextToType) + "\"" + Formatter.LineEnding;
        //    if (Overwrite) line.ModelFunction = "TypeText(\"" + PerformReplacement(TextToType) + "\")" + Formatter.LineEnding;
        //    else line.ModelFunction = "AppendText(\"" + PerformReplacement(TextToType) + "\")" + Formatter.LineEnding;
        //    builder.Append(line.ModelFunction);
        //    line.FullLine = builder.ToString();
        //    return line;
        //}

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            TextToType = node.Attributes["TextToType"].Value;
            if (node.Attributes["FormatString"] != null) FormatString = node.Attributes["FormatString"].Value;
            if (node.Attributes["MapName"] != null) MapName = node.Attributes["MapName"].Value;
            if (node.Attributes["ValueOnly"] != null) ValueOnly = node.Attributes["ValueOnly"].Value == "1";
            if (node.Attributes["Overwrite"] != null) Overwrite = node.Attributes["Overwrite"].Value == "1";            
        }

  

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("TextToType", TextToType);
            writer.WriteAttributeString("ValueOnly", ValueOnly ? "1" : "0");
            writer.WriteAttributeString("Overwrite", Overwrite ? "1" : "0");
            if (!string.IsNullOrEmpty(MapName))
            {
                writer.WriteAttributeString("MapName", MapName);
            }
            if (!string.IsNullOrEmpty(FormatString))
            {
                writer.WriteAttributeString("FormatString", FormatString);
            }
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "输入框"; }
        }
       
    }
}
