using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class RadioDataMapField : MultipleDataMapField
    {
        public override void TableHtmlChildRender(StringBuilder sb)
        {
            foreach (var o in Options)
            {
                var optValue = System.Web.HttpUtility.UrlDecode(o.Value);
                var val = System.Web.HttpUtility.UrlDecode(Value);
                sb.AppendFormat("<input type=\"radio\"  name=\"{0}\" value=\"{1}\" {2}/>{1}",
                    DisplayName, optValue, (optValue == val) ? "checked" : "");
            }
        }
    }
}
