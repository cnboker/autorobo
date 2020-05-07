using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using AutoRobo.Core.ns;
using Jint;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 字符串过滤器
    /// </summary>
    public class StringPattern
    {
        public StringPattern() {
            Pattern = StringMode.Javascript;
        }
        /// <summary>
        /// 提取内容过滤方式
        /// </summary>
        public StringMode Pattern { get; set; }
        /// <summary>
        /// 表达式
        /// </summary>
        public string Expression { get; set; }


        public string GetString(string rawString)
        {
            if (string.IsNullOrEmpty(Expression)) return rawString;
            string result = "";
            if (Pattern == StringMode.Regex)
            {
                Regex regex = new Regex(Expression);
                Match match = regex.Match(rawString);
                 if (match.Success)
                 {
                     result = match.Value;
                 }
            }
            else if (Pattern == StringMode.Javascript)
            {
                JintEngine engine = JintCreator.Create();
                try
                {                    
                    rawString = rawString.Replace("\r\n", "\n").Replace("\n", "\\n").Replace("'", "\\'");
                    var val = engine.Run("(function(){" + Expression + "})('" + rawString + "')");
                    result = (val == null ? "" : val.ToString());
                }
                catch
                {

                }
            }
            else if (Pattern == StringMode.Replace) {
                string[] rep = Expression.Split(",".ToCharArray());
                result = rawString;

                for (int i = 0; i < rep.Length; i++)
                {
                    result = result.Replace(rep[i], rep[i + 1]);
                    i++;
                }
            }         
            return result;
        }

        public  void LoadFromXml(XmlNode node)
        {
            Expression = GetAttribute(node, "StringPattern.Expression");

            var stringModeEnum = GetAttribute(node, "StringPattern.Pattern");
            if (!string.IsNullOrEmpty(stringModeEnum))
            {
                this.Pattern = (StringMode)Enum.Parse(typeof(StringMode), stringModeEnum);
            }    
        }

        public string GetAttribute(XmlNode node, string name)
        {
            if (node.Attributes[name] != null)
            {
                return node.Attributes[name].Value;
            }
            return "";
        }

        public  void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("StringPattern.Expression", Expression);
            writer.WriteAttributeString("StringPattern.Pattern", Convert.ToInt16(this.Pattern).ToString());
        }
    }
}
