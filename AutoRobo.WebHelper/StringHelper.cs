using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using CsvHelper;
using AutoRobo.WebHelper.Tokenizer;

namespace Util
{
    public class StringHelper
    {
        private static string domain = "";

        public static string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(domain))
                {
                    domain = System.Configuration.ConfigurationManager.AppSettings["root"];
                }
                return domain;
            }
        }
        /// <summary>
        /// 打开浏览器
        /// </summary>
        /// <param name="url"></param>
        public static void OpenIE(string url) {
            System.Diagnostics.Process.Start(url);
        }
        /// <summary>
        /// 同时检查http或https2种情况
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHttpUrl(string url)
        {
            if (url.ToLower().IndexOf("http") != 0) {
                return url.Insert(0, "http://");
            }
            return url;
        }     

        /// <summary>
        ///  /// http://work.china.alibaba.com 返回work.china.alibaba.com
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDomain(string url)
        {
            Uri uri = new Uri(GetHttpUrl(url));
            return uri.Host;
        }

        /// <summary>
        ///  /// http://work.china.alibaba.com 返回work.china.alibaba.com
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHttpDomain(string rawUrl)
        {
            var uri = new Uri(rawUrl); // to get the url from request or replace by your own
            var domain = uri.GetLeftPart(UriPartial.Authority);
            return domain;
        }

        /// <summary>
        /// 通过Url获取二级域名, 比如传入 http://www.cnboker.com, 返回cnboker.com
        /// http://work.china.alibaba.com 返回china.alibaba.com
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public string GetRootDomain(string url)
        {
            url = GetHttpUrl(url);
            var hostName = (new Uri(url)).Host;
            var splitHostName = hostName.Split('.');
            var secondLevelHostName = "";
            if (splitHostName.Length > 2)
            {
                for (int i = 1; i < splitHostName.Length; i++)
                {
                    secondLevelHostName += splitHostName[i] + (i == (splitHostName.Length-1) ? "" : ".");
                }
                return secondLevelHostName;
            }
            return hostName.Replace("http://","");
        }

        public static string GetRandom(int len)
        {
            System.Threading.Thread.Sleep(2);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random(((int)(((ulong)tick) & 0xffffffffL)) | ((int)(tick >> 0x20)));
            string chars = "abcdefghigklmnopqrstuvwxyz";
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str = str + chars[ran.Next(0, chars.Length)].ToString();
            }
            return str;
        }

        public static string GetRandomDigit(int len)
        {
            System.Threading.Thread.Sleep(2);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random(((int)(((ulong)tick) & 0xffffffffL)) | ((int)(tick >> 0x20)));
            string chars = "123456789";
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str = str + chars[ran.Next(0, chars.Length)].ToString();
            }
            return str;
        }

        /// <summary>
        /// 检查字符串是否包含关键字列表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static bool Contain(string source, string[] keywords) {
            string regex = string.Format(@"\b{0}({0})\w*\b", string.Join("|", keywords));
            return Regex.IsMatch(source, regex);            
        }

        /// <summary>
        /// 获取随机值
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int GetRandomValue(int maxValue) {
            System.Threading.Thread.Sleep(5);
            Random r = new Random((int)DateTime.Now.Ticks);
            int seed = r.Next(0, maxValue - 1);
            return seed;
        }

        public static string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var length = dataTable.Columns[i].ColumnName.Length;
                if (columnsWidths[i] < length)
                    columnsWidths[i] = length;
            }

            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
            }
            output.Append("|");
            output.AppendLine();
            output.Append(new string('=', output.Length));
            output.AppendLine();
            
            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();                    
                    output.Append("|" + PadCenter(text, columnsWidths[i] + 2));
                }
                output.Append("|");
                output.AppendLine();
            }
            return output.ToString();
        }

        private static string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        } 

     
        /// <summary>
        /// 提取字符串变量，变量格式$var，如$var,hello world, 返回['$var']
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> GetVariable(string input)
        {
            StringTokenizer tok = new StringTokenizer(input);
            List<string> variables = new List<string>();
            Token token;
            do
            {
                token = tok.Next();                
                if (token.Kind == TokenKind.Variable)
                {
                    variables.Add(token.Value);
                }
            } while (token.Kind != TokenKind.EOF);
            return variables;
        }

    }
}
