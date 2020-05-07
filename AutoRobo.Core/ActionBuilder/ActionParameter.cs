using System;
using System.Collections.Generic;
using System.Text;
using mshtml;

namespace AutoRobo.Core.ActionBuilder
{
    public class ActionParameter
    {
        /// <summary>
        /// 添加活动名称
        /// </summary>
        public string ActionName { get; set; }

        public IHTMLElement Element { get; set; }

        public bool XPathFinder { get; set; }
      
        public ActionParameter() { }

        public ActionParameter(IHTMLElement element) {            
            this.Element = element;
        }
    
    }
}
