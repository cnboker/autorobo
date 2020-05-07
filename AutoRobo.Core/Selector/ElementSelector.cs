using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using mshtml;

namespace AutoRobo.Core
{
    /// <summary>
    /// 元素加亮器
    /// </summary>
    public class ElementSelector : ISelector
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger("ElementSelector");       

        private CssElement selectorElement;

        public virtual void Highlight(IHTMLElement element)
        {
            Highlight(element, null);
        }
        public virtual void Highlight(IHTMLElement element, CssStyle style)
        {
            if (element == null) return;
            
            if (selectorElement !=null && element == selectorElement.HTMLElement) return;
            try
            {                
                selectorElement = new CssElement(element, style);
                BeforeHighlight(selectorElement);
                selectorElement.SaveOriginalAttribute();
                selectorElement.ChangeElementAttribute();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                
            }
        }

        public virtual void BeforeHighlight(CssElement element) { 
        
        }
        /// <summary>
        /// 恢复原始原来状态
        /// </summary>
        public virtual void Restore() {
            try
            {
                if (selectorElement != null)
                {
                    selectorElement.RestoreOriginalAttribute();
                }
                selectorElement = null;            
            }
            catch (Exception) {
               
            }
        }

        public IHTMLElement SelectorElement
        {
            get
            {
                if (selectorElement == null) return null;
                return selectorElement.HTMLElement;
            }

        }

        public bool Highlighting
        {
            get { return (selectorElement != null); }
        }

  
    }
}
