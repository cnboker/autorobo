using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls;
using System.Xml;
using AutoRobo.Core.Models;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 变量引用
    /// </summary>
    public abstract class ActionVariableReference : ActionBase
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }
       

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSelectVariable(editorAction);
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            VariableName = GetAttribute(node, "VariableName");
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("VariableName", VariableName);
            base.WriterAddAttribute(writer);
        }
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("variable.png");
        }
       
    }
}
