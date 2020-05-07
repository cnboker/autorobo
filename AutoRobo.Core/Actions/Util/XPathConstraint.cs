using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Constraints;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using System.Text.RegularExpressions;
using System.Linq;
using mshtml;

namespace AutoRobo.Core.Actions
{
    public class XPathConstraint : Constraint
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("XPathConstraint");    
        private string[] nodes = null;
        private bool requireRegex = false;
        private string xpath = "";
        public XPathConstraint(string path)
        {
            //regex只处理/html/tr[*]/td类似的情况（substring取消开头"/")      
            this.nodes = path.Substring(1,path.Length-1).ToLower().Split("/".ToCharArray()).Select(c => (c.IndexOf("[") > 0 ? c : c + "[1]")).ToArray();
            requireRegex = path.IndexOf("*") > 0;
            this.xpath = path;
        }
        /// <summary>
        /// 标识约束,用于字符串比较
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo(System.IO.TextWriter writer)
        {
            writer.Write("{0}", xpath);
        }

        protected override bool MatchesImpl(WatiN.Core.Interfaces.IAttributeBag attributeBag, ConstraintContext context)
        {            
            var element = attributeBag as Element;
            if (element == null) {
                var frame = attributeBag as Frame;
                if (frame != null) {
                    element = frame.FrameElement;
                }
            }
            var nativeElement = element.NativeElement as IEElement;
            mshtml.IHTMLElement el = nativeElement.AsHtmlElement;
            return XPathCompare((IHTMLDOMNode)el);      
        }

        bool XPathCompare(IHTMLDOMNode element)
        {

            IHTMLDOMNode parent = element;
             int index = nodes.Length;
             bool _compare = true;
             
             while (parent != null)
             {
                 index--;
                 if (index == 0) break;
                 var s = nodes[index];
                 var t = parent.nodeName.ToLower();
                 _compare = GetTag(s) == t;
                 if (!_compare) break;
                 t =  t + "[" + GetElementIdx(parent) + "]";
                 //logger.Info("s=" + s + "; t=" + t);
                _compare = (s == t);
                if (!_compare)
                {
                    //是否有模糊匹配的情况（包含*）
                     if (!requireRegex) { break; }
                     else {
                         //不包含*比较失败
                         if (s.IndexOf("*") == -1)
                         {
                             break;
                         }
                         else {                          
                             //包含*, 则比较标签，标签不相同比较失败
                             _compare = GetTag(s) == GetTag(t);
                             if (!_compare) break;
                         }
                     }
                 }
                 parent = parent.parentNode;
             }
             return _compare;
        }

         private string GetTag(string tag)
         {
             int index = tag.IndexOf("[");
             if (index == -1) return tag;
             return tag.Substring(0, index);
         }

         private int GetElementIdx(IHTMLDOMNode elt)
         {
             var count = 1;
             for (var sib = elt.previousSibling; sib != null; sib = sib.previousSibling)
             {
                 if (sib != null && sib.nodeName == elt.nodeName) count++;
             }
             return count;
         }
    }
}
