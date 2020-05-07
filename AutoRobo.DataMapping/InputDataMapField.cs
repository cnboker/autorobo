using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.DataMapping
{
    public class InputDataMapField : DataMapField
    {

        public override void AddAttribute(string name, string value)
        {
            if (name == "values") {
                DisplayName = value;
            }
            base.AddAttribute(name, value);
        }

        public override void TableHtmlChildRender(StringBuilder sb)
        {
            sb.AppendFormat("<input type=\"text\" id=\"{0}\" class=\"text {1}\" name=\"{0}\" value=\"{2}\"/>",
                DisplayName, Required ? "required" : "", System.Web.HttpUtility.UrlDecode(this.Value));
        }

        public override void DesignHtmlChildRender(StringBuilder sb)
        {
            sb.AppendFormat("<label>{0}</label>", this.DisplayName);
        }
    }
}
