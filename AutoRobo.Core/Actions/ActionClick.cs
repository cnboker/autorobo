using System;
using System.Drawing;
using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using AutoRobo.UserControls;
using WatiN.Core;
using AttributeConstraint = WatiN.Core.Constraints.AttributeConstraint;
using IHTMLDocument2 = mshtml.IHTMLDocument2;
using IHTMLElement = mshtml.IHTMLElement;
using System.Threading;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Models;
using mshtml;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionClick : ActionElementBase, ICodeFormatterAcceptor
    {
        public bool NoWait { get; set; }
       
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            ActionClickParameter ap = parameter as ActionClickParameter;
            IHTMLElement activeElement = ap.Element;
            if (activeElement == null) return false;
            base.Parse(parameter);       
            bool result = false;
            switch (ElementType)
            {    
                case ElementTypes.SelectList:
                    //注入onchange事件
                    RunScript(ScriptLoader.GetSelectChangeEventTrigger(), true);
                    result = true;
                    break;
                case ElementTypes.Button:
                case ElementTypes.Image:
                case ElementTypes.Link:                
                case ElementTypes.TextField:
                case ElementTypes.CheckBox:
                case ElementTypes.RadioButton:
                    result = true;
                    break;

                case ElementTypes.Area:
                case ElementTypes.Body:
                case ElementTypes.Form:
                case ElementTypes.Frame:
                case ElementTypes.Label:
                case ElementTypes.Para:
                case ElementTypes.Table:
                case ElementTypes.TableBody:
                case ElementTypes.TableCell:
                case ElementTypes.TableRow:
                case ElementTypes.Element:
                case ElementTypes.Div:
                case ElementTypes.Span:
                    if (ap.Filter == ActionFliter.Lower)
                    {
                        result = true;
                    }
                    break;
                default:
                    break;
            }
            
            return result;
        }

     
        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucClick(editorAction);
        }
        public override Bitmap GetIcon()
        {
            // change image based on target
            return GetIconFromFile("Click.bmp");
        }

    
        public override void Perform()
        {
            Element element = GetElement();
            if (element != null)
            {
                OnPerform(element);
            }
        }

        protected virtual bool OnPerform(Element element)
        {            
            element.Click(); 
            return true;
        }

        public override bool Validate()
        {
            bool result;

            try
            {
                Element element = GetElement();
                result = element.Exists;
                Status = StatusIndicators.StepContinue;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
                Status = StatusIndicators.StepFailure;
            }

            return result;
        }

    

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
           
            if (node.Attributes.GetNamedItem("NoWait") != null)
            {                
                NoWait = node.Attributes.GetNamedItem("NoWait").Value.ToString() == "1";
            }
                                  
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("NoWait", NoWait ? "1" : "0");
            base.WriterAddAttribute(writer);
        }
        public override string ActionDisplayName
        {
            get { return "单击"; }
        }

        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
