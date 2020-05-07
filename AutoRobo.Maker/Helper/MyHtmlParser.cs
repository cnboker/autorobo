using System;
using System.Collections.Generic;

using System.Text;
using HtmlAgilityPack;

namespace AutoRobo.Helper
{
    public class MyHtmlParser
    {
        private string html;
        public HtmlDocument Doc { get; set; }
        public HtmlNode Root { get; set; }

        public MyHtmlParser(string html)
        {
            this.html = html;
            HtmlNode.ElementsFlags.Remove("form");        
            Doc = new HtmlDocument();
            Doc.LoadHtml(html);
            Root = Doc.DocumentNode;
        }     

        /// <summary>
        /// Returns HtmlNodeCollection based on given pattern based XPath.
        /// Example: "//body;//form;//table[3];//input"
        /// </summary>
        /// <param name="node">HtmlAgilityPack.HtmlNode</param>
        /// <param name="pattern">String</param>
        /// <returns>HtmlNodeCollection</returns>
        public HtmlNodeCollection SelectNodesByPattern(string pattern)
        {          
            HtmlNode node = Root;
            string[] expressions = pattern.Trim().Split(new char[] { ';' });

            HtmlNode tempNode = node;
            int incr = 0;

            for (incr = 0; incr < expressions.Length - 1; incr++)
                if (tempNode != null)
                    tempNode = tempNode.SelectSingleNode(expressions[incr]);

            if (tempNode != null)
                return tempNode.SelectNodes(expressions[incr]);

            return null;
        }

        private HtmlNode FindCorrespondingInputNode(HtmlTextNode fieldNode)
        {
            for (var currentNode = fieldNode.NextSibling;
                 currentNode != null && currentNode.NodeType != HtmlNodeType.Text;
                 currentNode = currentNode.NextSibling)
            {
                if (currentNode.Name == "input"
                 && !currentNode.Attributes["type"].Value.Contains("hidden"))
                {
                    return currentNode;
                }
            }
            return null;
        }
    }
}
