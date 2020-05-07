using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.UserControls;
using WatiN.Core.Native.InternetExplorer;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionSubmitClick : ActionClick
    {
        private bool autoSubmit = true;
        
        /// <summary>
        /// 是否自动触发提交按钮
        /// </summary>
        public bool AutoSubmit
        {
            get
            {
                return autoSubmit;
            }
            set
            {
                autoSubmit = value;
            }
        }
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            if (parameter.Element == null) return false;
            try
            {
                SetFindMethod(parameter.Element);
            }
            catch
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 提交前是否记录用户调整痕迹
        /// </summary>
        public bool RecordMark { get; set; }

        public ActionSubmitClick() {
        }
      
        public override string ActionDisplayName
        {
            get { return "提交按钮"; }
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSubmitClick(editorAction);
        }

        protected override bool OnPerform(WatiN.Core.Element element)
        {
            if (autoSubmit)
            {
                element.Click();
            }
            else {
                var nativeElement = element.NativeElement as IEElement;
                mshtml.IHTMLElement el = nativeElement.AsHtmlElement;                
                el.setAttribute("__recordmark", RecordMark ? "1" : "0");             
            }
            return true;
        }
     
        public override void LoadFromXml(XmlNode node)
        {
            if (node.Attributes.GetNamedItem("AutoSubmit") != null)
            {
                AutoSubmit = node.Attributes.GetNamedItem("AutoSubmit").Value.ToString() == "1";
            }
            if (node.Attributes.GetNamedItem("RecordMark") != null)
            {
                RecordMark = node.Attributes.GetNamedItem("RecordMark").Value.ToString() == "1";
            }
            base.LoadFromXml(node);
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("AutoSubmit", AutoSubmit ? "1" : "0");
            writer.WriteAttributeString("RecordMark", RecordMark ? "1" : "0");
            base.WriterAddAttribute(writer);
        }

    }
}
