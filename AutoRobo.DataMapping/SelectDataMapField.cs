using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class SelectDataMapField : MultipleDataMapField
    {
        public bool Multiple { get; set; }
       
        public override void AddAttribute(string name, string value)
        {
            if (name == "multiple")
            {
                Multiple = (value == "checked");
            }
            base.AddAttribute(name, value);
        }

        public override void TableHtmlChildRender(StringBuilder sb)
        {
            sb.AppendFormat("<select name=\"{0}\">", DisplayName);
            foreach (var o in Options)
            {
                var optValue = System.Web.HttpUtility.UrlDecode(o.Value);
                var val = System.Web.HttpUtility.UrlDecode(Value);
                sb.AppendFormat("<option value=\"{0}\" {1}>{0}</option>",
                   optValue, (optValue == val) ? "selected" : "");
            }
            sb.AppendFormat("</select>");
        }
    }
}
