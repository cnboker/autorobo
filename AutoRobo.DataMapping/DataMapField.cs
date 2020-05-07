using System.Text;

namespace AutoRobo.DataMapping
{

    public class DataMapField
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(DataMapField));

        public string CssClass
        {
            get;
            set;
        }

    

        private string displayName = "";

        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
            }
        }

        private string _value = "";

        /// <summary>
        /// 获取值， 原始文本是经过编码后文本，所以需要界面，赋值的时候需要重新编码
        /// </summary>
        public string Value
        {
            get
            {
                return System.Web.HttpUtility.UrlDecode(_value);
            }
            set
            {
                _value = value;
            }
        }

        public string GetDesignHtml()
        {
            return designHtml.ToString();
        }

        public string GetTableHtml()
        {
            return tableHtml.ToString();

        }

        private StringBuilder designHtml = new StringBuilder();
        private StringBuilder tableHtml = new StringBuilder();


        public bool Required
        {
            get;
            set;
        }

        public DataMapField()
        {

        }

        public virtual void AddAttribute(string name, string value)
        {
            if (name == "cssClass")
            {
                CssClass = value;
            }
            else if (name == "required")
            {
                Required = (value == "checked");
            }
        }

        public void DesignHtmlRender()
        {

        }

        public void TableHtmlRender()
        {
            tableHtml = new StringBuilder();
            tableHtml.AppendFormat("<div class=\"control-group\">");
            tableHtml.AppendFormat("<label class=\"control-label\" for=\"{0}\">", DisplayName);
            tableHtml.AppendFormat("{0}{1}", DisplayName, Required ? "*" : "");
            tableHtml.AppendFormat("</label>");
            tableHtml.Append("<div class=\"controls\">");
            TableHtmlChildRender(tableHtml);
            tableHtml.Append("</div>");
            tableHtml.AppendFormat("</div>");         
        }

        public virtual void TableHtmlChildRender(StringBuilder sb)
        {

        }

        public virtual void DesignHtmlChildRender(StringBuilder sb)
        {

        }

    }
}
