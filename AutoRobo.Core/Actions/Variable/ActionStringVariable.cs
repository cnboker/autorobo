using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 字符串变量
    /// </summary>
    public class ActionStringVariable : ActionVariable
    {
        private string val = "";

        public override string GetTypeName()
        {
            return "字符串";
        }

        public override string GetDescription()
        {
            return "字符串变量";
        }

        public override object Data
        {
            get { return val; }
            set { val = (value == null ? "" : value.ToString()); }
        }

        public override void Reset()
        {
            val = "";
        }
        public override string ToString()
        {
            return val;
        }

        public override void LoadFromXml(XmlNode node)
        {            
            val = GetAttribute(node, "Value");
            base.LoadFromXml(node);
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("Value", val);
            base.WriterAddAttribute(writer);
        }
    }
}
