using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class TextAreaDataMapField : InputDataMapField
    {
        public override void TableHtmlChildRender(StringBuilder sb)
        {
            sb.AppendFormat("<textarea id=\"{0}\" class=\"{1} ckeditor \" name=\"{0}\" rows=12 cols=80>{2}</textarea>",
                this.DisplayName, Required ? "required" : "",  System.Web.HttpUtility.UrlDecode(this.Value));
        }
    }
}
