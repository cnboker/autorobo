using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using mshtml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using AutoRobo.Core.ActionBuilder;
using System.Windows.Forms;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionSelectList : ActionElementBase
    {
     
        public bool ByValue { get; set; }
        private IHTMLElement ActiveElement { get; set; }
        private string _selectedValue;
        public string SelectedText { get; set; }

        public override string DefaultValue
        {
            get
            {
                return SelectedValue;
            }
           
        }

        public override string ActionDisplayName
        {
            get { return "列表框"; }
        }

        public string SelectedValue
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedValue))
                {
                    if (ByValue)
                    {
                        _selectedValue = ActiveElementAttribute(ActiveElement, "value");
                    }
                    else
                    {
                        _selectedValue = GetSelectValue();
                    }
                }
                return _selectedValue;
            }
            set { _selectedValue = value; }
        }

        private string GetSelectValue() {
            var val = "";
            var sel = ActiveElement as IHTMLSelectElement;
            if (sel == null) return null;
            for (int i = 0; i < sel.length; i++)
            {
                var op = sel.item(i, i) as IHTMLOptionElement;
                if (op != null && op.selected)
                {
                    val = op.value;
                    SelectedText = op.text;
                    break;
                }
            }
            return val;
        }

        public bool Regex { get; set; }

        public ActionSelectList() {
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            base.Parse(parameter);
           
            SelectParameter sp = parameter as SelectParameter;
            ByValue = sp.ByValue;            
            var selectedElement = parameter.Element as IHTMLSelectElement;
            if (selectedElement == null) return false;
            //var optionElements = selectedElement.options as mshtml.HTMLSelectElementClass;
            //if (optionElements == null) return false;
            //foreach (IHTMLOptionElement option in selectedElement)
            for(int i = 0; i < selectedElement.length; i++)
            {
                IHTMLOptionElement option = selectedElement.item(i, i) as IHTMLOptionElement;
                if (option.selected)
                {
                    SelectedValue = option.value;
                    SelectedText = option.text;
                    break;
                }
            }
            return true;
        }


        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSelectList(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("SelectList.bmp");
        }


        public override void Perform()
        {
            string val = Eval(MapName);
            if (!string.IsNullOrEmpty(val))
            {
                SelectedValue = val;
            }

            var element = (SelectList)GetElement();
            if (element != null)
            {
                //element.Click();
                ActiveElement = (IHTMLElement)((IEElement)element.NativeElement).AsHtmlElement;
                if (ByValue)
                {
                    if (Regex)
                    {
                        element.SelectByValue(new Regex(SelectedValue));
                    }
                    else
                    {
                        element.SelectByValue(SelectedValue);
                      
                    }
                }
                else
                {
                    if (Regex)
                    {
                        element.Select(new Regex(SelectedValue));
                    }
                    else
                    {
                        element.Select(SelectedValue);
                    }
                }
               
            }
        }

        public override bool Validate()
        {
            bool result;

            try
            {
                var element = (SelectList) GetElement();
                string itemdata = ByValue ? element.SelectedOption.Value : element.SelectedOption.Text;

                ActiveElement = (IHTMLElement)((IEElement)element.NativeElement).AsHtmlElement;
                if (Regex) result = System.Text.RegularExpressions.Regex.IsMatch(itemdata, SelectedValue);
                else result = itemdata == SelectedValue;
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

        //    string localvalue = PerformReplacement(SelectedValue);

        //    if (Regex)
        //    {
        //        if (ByValue) line.ModelFunction = "SelectByValue(new Regex(\"" + localvalue + "\"))";
        //        else line.ModelFunction = "Select(new Regex(\"" + localvalue + "\"))";
        //    }
        //    else
        //    {
        //        if (ByValue) line.ModelFunction = "SelectByValue(\"" + localvalue + "\")";
        //        else line.ModelFunction = "Select(\"" + localvalue + "\")";
        //    }
        //    line.ModelFunction += Formatter.LineEnding;
        //    builder.Append(line.ModelFunction);
        //    line.FullLine = builder.ToString();
        //    return line;
        //}

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            ByValue = node.Attributes["ByValue"].Value == "1";
            Regex = node.Attributes["Regex"].Value == "1";
            SelectedValue = node.Attributes["SelectedValue"].Value;            
        }

    

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("ByValue", ByValue ? "1" : "0");
            writer.WriteAttributeString("SelectedValue", SelectedValue);
            writer.WriteAttributeString("Regex", Regex ? "1" : "0");
            base.WriterAddAttribute(writer);
        }
    }
}
