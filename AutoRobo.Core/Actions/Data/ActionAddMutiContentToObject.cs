using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoRobo.Core.ActionBuilder;
using Util;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 增加多项到变量
    /// </summary>
    public abstract class ActionAddMutiContentToObject : ActionFetchText
    {
        /// <summary>
        /// 获取多项
        /// </summary>
        /// <returns></returns>
        protected List<string> GetMutiContent() {
            
            ElementCollection elements = GetWindow().Elements.Filter(GetConstraint());
            List<string> list = new List<string>();
            foreach (var el in elements)
            {
                string val = GetValue(el);
                if (!(string.IsNullOrEmpty(val) && FliterEmptyString))
                {
                    list.Add(val);
                }
            }
            return list;
        }

   
        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            ActionAddListToListParamter p = parameter as ActionAddListToListParamter;

            string cssSelector = AppContext.Browser.Selector.GetSelector(p.Element, p.PairElement);

            SetFindMethod(p.Element, FindMethods.CssSelector, cssSelector);
            //加亮元素
            Highlight();
            return true;
        }

        /// <summary>
        /// refer:http://forum.jquery.com/topic/difference-between-nth-child-and-eq
        /// nth-child:(1)的意思是父元素下子元素的位置
        /// eq:(1)表示当前元素的位置
        /// 举例说明
        /// <div>
        //  <p>div1 p1</p>
        //  <p>div1 p2</p>
        //  <p>div1 p3</p>
        //  <p>div1 p4</p>
        //</div>
        //<div>
        //      <p>div2 p1</p>
        //      <p>div2 p2</p>
        //      <p>div2 p3</p>
        //      <p>div2 p4</p>
        //</div>
        //$('div p:eq(2)').css('color','red');
        //$('div p:nth-child(2)').css('background-color','green');
        //div1 p1
        //div1 p2 (green)
        //div1 p3 (red)
        //div1 p4
        //div2 p1
        //div2 p2 (green)
        //div2 p3
        //div2 p4
      
//        /// <summary>
//        /// 转换xpath到selector以提高查询效率
//        /// </summary>
//        /// <param name="xpath"></param>
//        /// <returns></returns>
//        private string XPathToCss1(string xpath)
//        {
//            string script = @"             
//                .replace(/\[(\d+?)\]/g, function (s, m1) { return '[' + (m1 - 1) + ']'; })
//                .replace(/\/{2}/g, '')
//                .replace(/\/+/g, '>')
//                .replace(/@/g, '')
//                .replace(/\[(\d+)\]/g, ':eq($1)')
//                .replace(/^\s+/, '');
//                
//            ";
//            object result = JS.Eval(Window, "'" + xpath + "'" + script);
//            string selector = (result == null ? "" : result.ToString());
//            string[] selectorTokens = selector.Split(">".ToCharArray());
//            List<string> tokens = new List<string>();
//            //找到tr[*]
//            bool findTrAsterisk = false;
            
//            //对表列多选特殊处理, 将td,td:eq(n)替换为td:nth-child(1),td:nth-child(q)
//            string lastToken = "";
//            foreach (string token in selectorTokens)
//            {
//                try
//                {
//                    if (token == "tr[*]")
//                    {
//                        tokens.Add("tr");
//                        findTrAsterisk = true;
//                        continue;
//                    }
//                    if (token == "td[*]")
//                    {                                            
//                        if (lastToken.IndexOf("tr") >= 0)
//                        {
//                            tokens.RemoveAt(tokens.Count - 1);
//                            if (lastToken == "tr")
//                            {                               
//                                tokens.Add("tr:nth-child(1)");
//                            }
//                            else
//                            {
//                                string intString = (new Regex(@"\(([^}]*)\)")).Match(lastToken).Value;
//                                int val = Convert.ToInt32(intString.Replace("(", "").Replace(")", "")) + 1;
//                                tokens.Add("tr:nth-child(" + val + ")");
//                            }                            
//                        }
//                        tokens.Add("td");
//                        continue;
//                    }
                   
//                    if (findTrAsterisk && token.IndexOf("td") >= 0)
//                    {
//                        findTrAsterisk = false;
//                        if (token == "td")
//                        {
//                            tokens.Add("td:nth-child(1)");
//                        }
//                        else
//                        {
//                            string intString = (new Regex(@"\(([^}]*)\)")).Match(token).Value;
//                            int val = Convert.ToInt32(intString.Replace("(", "").Replace(")", "")) + 1;
//                            tokens.Add("td:nth-child(" + val + ")");
//                        }
//                        continue;
//                    }
                    
//                    tokens.Add(token.Replace("[*]", ""));
//                }
//                finally {
//                    lastToken = token;
//                }
//            }
//            return string.Join(">", tokens.ToArray());
//        }
    }
}
