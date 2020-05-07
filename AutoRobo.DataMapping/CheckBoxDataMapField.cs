using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
   
    public class CheckBoxDataMapField : MultipleDataMapField
    {
        public override void TableHtmlChildRender(StringBuilder sb)
        {
            foreach (var o in Options)
            {
                sb.AppendFormat("<input type=\"checkbox\"  name=\"{0}\" value=\"{1}\" {2}/>{1}",
                    DisplayName, System.Web.HttpUtility.UrlDecode(o.Value), o.BaseLine ? "checked" : "");
            }
        }
    }
}
