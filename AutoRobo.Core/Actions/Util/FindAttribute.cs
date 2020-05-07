using System;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    public class FindAttribute
    {
        public FindMethods FindMethod { get; set; }
        public string FindValue { get; set; }
        /// <summary>
        /// 查找名称这里定义为ID
        /// </summary>
        public string FindName { get; set; }
        public bool Regex { get; set; }

        public FindAttribute() {}

        public FindAttribute(FindMethods findmethod, string findvalue, bool regex)
        {
            FindMethod = findmethod;
            FindValue = findvalue;
            Regex = regex;
        }

        public FindAttribute(FindMethods findmethod, string findname, string findvalue, bool regex)
        {
            FindMethod = findmethod;
            FindName = findname;
            FindValue = findvalue;
            Regex = regex;
        }

        public void FromXml(XmlNode node)
        {
            FindMethod = (FindMethods) Enum.Parse(typeof (FindMethods), node.Attributes["FindMethod"].Value);
            FindName = node.Attributes["FindName"].Value;
            FindValue = node.Attributes["FindValue"].Value;
            Regex = node.Attributes["Regex"].Value == "1";
        }

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Finder");
            writer.WriteAttributeString("FindMethod", FindMethod.ToString());
            writer.WriteAttributeString("FindName", FindName);
            writer.WriteAttributeString("FindValue", FindValue);
            writer.WriteAttributeString("Regex", Regex?"1":"0");
            writer.WriteEndElement();
        }

        public override string ToString()
        {
            string result;
            string finder = FindMethod.ToString() != "Href" ? FindMethod.ToString() : "Url";
            string findvalue = "";
            try
            {
                findvalue = FindValue != null ? FindValue.Replace("\"", "\\\"") : "";
            }
            catch (Exception)
            {
            }
            if (string.IsNullOrEmpty(FindName)) result = "Find.By" + finder + "(\"" + findvalue + "\")";
            else result = "Find.By" + finder + "(\"" + FindName + "\", \"" + findvalue + "\")";
            return result;
        }

        public string ToAttribute()
        {
            string result;
            string finder = FindMethod.ToString() != "Href" ? FindMethod.ToString() : "Url";
            string findvalue = FindValue.Replace("\"", "\\\"");
            if (string.IsNullOrEmpty(FindName)) result = "FindBy(" + finder + " = \"" + findvalue + "\")";
            else result = "FindBy(" + finder + " = \"" + FindName + "\", \"" + findvalue + "\")";
            return result;
        }
    }
}
