using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using mshtml;
using System.Text.RegularExpressions;
using Util;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core
{
    public class ElementsSelector : ISelectors
    {
        private List<ElementSelector> selectorElements = new List<ElementSelector>();

        public List<IHTMLElement> SelectorElements
        {
            get
            {
                if (selectorElements == null) return null;
                return selectorElements.Select(c => c.SelectorElement).ToList();
            }
        }

        public void Highlight(IHTMLElement[] elements)
        {
            Restore();

            foreach (var element in elements) {
                ElementSelector selector = new ElementSelector();
                selector.Highlight(element);
                selectorElements.Add(selector);
            }
        }

        /// <summary>
        /// 恢复原始原来状态
        /// </summary>
        public void Restore()
        {            
            foreach (var selector in selectorElements) {
                selector.Restore();
            }
            selectorElements.Clear();
        }

        public bool Highlighting
        {
            get { return selectorElements.Count > 0; }
        }

       
    }
}
