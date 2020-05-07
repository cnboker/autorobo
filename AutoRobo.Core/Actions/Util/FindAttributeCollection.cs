using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    public class FindAttributeCollection : List<FindAttribute>
    {
        public string GetName()
        {
            string name = "";

            foreach (FindAttribute attribute in this)
            {
                name += attribute.FindValue;
            }

            return name;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (FindAttribute attribute in this)
            {
                if (builder.Length > 0) builder.Append(" && ");
                builder.Append(attribute);
            }

            return builder.ToString();
        }

        public string ToString(DataRow row)
        {
            var builder = new StringBuilder();

            foreach (FindAttribute attribute in this)
            {
                if (builder.Length > 0) builder.Append(" && ");
                string strAttribute = attribute.ToString();

                if (row != null)
                {
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        strAttribute = Regex.Replace(strAttribute, "\\[\\%"+column.ColumnName+"\\%\\]", row[column.ColumnName].ToString(),
                                                     RegexOptions.IgnoreCase);
                    }
                }

                builder.Append(strAttribute);
            }

            return builder.ToString();
        }

        public string ToAttribute(DataRow row)
        {
            var builder = new StringBuilder();

            foreach (FindAttribute attribute in this)
            {
                if (builder.Length > 0) builder.Append(" && ");
                string strAttribute = attribute.ToAttribute();

                if (row != null)
                {
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        strAttribute = Regex.Replace(strAttribute, "\\[\\%" + column.ColumnName + "\\%\\]", row[column.ColumnName].ToString(),
                                                     RegexOptions.IgnoreCase);
                    }
                    builder.Append(strAttribute);
                }
                else
                {
                    if (builder.Length > 0) builder.Append(" && ");
                    builder.Append(attribute.ToAttribute());
                }                
            }

            return builder.ToString();
        }

        public void ToXML(string CollectionName, XmlWriter writer)
        {
            if (Count == 0) return;
            
            writer.WriteStartElement(CollectionName);
            foreach (FindAttribute attribute in this)
            {
                attribute.ToXml(writer);
            }
            writer.WriteEndElement();
        }
    }
}
