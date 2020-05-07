using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.UserControls;
using System.Xml;
using AutoRobo.Core.Models;
using System.Data;
using AutoRobo.Core.Formatters;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 增加数组数据到表格
    /// </summary>
    public abstract class ActionAddListToTable : ActionBase, ICodeFormatterAcceptor
    {        
        public string TableName { get; set; }
        public string ObjectName { get; set; }

        public override string ElementName
        {
            get
            {
                return TableName;
            }          
        }

        public override string DefaultValue
        {
            get
            {
                return ObjectName;
            }
            
        }

        public override void Perform()
        {          
               
        }

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucActionAddListToTable(editorAction);
        }
       

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("table.png");
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            TableName = GetAttribute(node, "TableName");
            ObjectName = GetAttribute(node, "ObjectName");               
        }
      
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("ObjectName", ObjectName);
            writer.WriteAttributeString("TableName", TableName);          
            base.WriterAddAttribute(writer);
        }



        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
