using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WatiN.Core;
using mshtml;
using System.Linq;
using WatiN.Core.Native.InternetExplorer;
using System.Collections;
using System.Text.RegularExpressions;

namespace AutoRobo.Core
{
 
    public class Selector
    {
        private string _cssSelector;
        /// <summary>
        /// 获取或设置选择元素的Css Selector
        /// </summary>
        public string CssSelector { get { return _cssSelector; } }

        private ElementsSelector elementsSelector;
        private ElementSelector selectAreaSelector;
        /// <summary>
        /// 选择区域
        /// </summary>
        public virtual IHTMLElement SelectAreaSelector
        {
            get
            {
                if (selectAreaSelector == null)
                {
                    return null;
                }
                return selectAreaSelector.SelectorElement;
            }
        }
        /// <summary>
        /// 加亮元素
        /// </summary>
        public virtual IHTMLElement SelectorElement
        {
            get
            {
                if (elementsSelector.SelectorElements == null) {
                    return null;
                }
                if (elementsSelector.SelectorElements.Count > 0) {
                    return elementsSelector.SelectorElements[0];
                }
                return null;
            }
        }
      
       
        /// <summary>
        /// 选择元素组
        /// </summary>
        public virtual IHTMLElement[] SelectorElements
        {
            get
            {
                return elementsSelector.SelectorElements.ToArray();
            }
        }

        public bool Highlighting
        {
            get
            {
                return  elementsSelector.Highlighting;
            }
        }

        private ICoreBrowser coreBrowser;
        public Selector(ICoreBrowser coreBrowser) {
            elementsSelector = new ElementsSelector();
            this.coreBrowser = coreBrowser;
        }

        /// <summary>
        /// 选择页面某个区域，在选定的区域内获取数据
        /// </summary>
        /// <param name="element"></param>
        public void HighlightArea(IHTMLElement element) {
            if (selectAreaSelector == null)
            {
                selectAreaSelector = new ElementSelector();
            }
            else {
                selectAreaSelector.Restore();
            }
            selectAreaSelector.Highlight(element, new CssStyle() { BackgroundColor = "#a0c5e8", OutlineWidth="1px" });
        }

        public void HighlithBySelector(string cssSelector) {
            _cssSelector = cssSelector;
            List<IHTMLElement> elements = new List<IHTMLElement>();
            try
            {
                ElementCollection ec = coreBrowser.SelectMany(_cssSelector);

               
                foreach (var element in ec)
                {
                    IHTMLElement el = element.GetHtmlElement() as mshtml.IHTMLElement;
                    elements.Add(el);
                }
            }
            catch (Exception ex) {
                //解决使用nth-of-type报告脚本错误问题
                if (cssSelector.IndexOf("nth-of-type") != -1) {
                    HighlithBySelector(cssSelector.Replace("nth-of-type", "nth-child"));
                    return;
                }
            }
            var arr = elements.ToArray();
            Highlight(arr);
           
        }

        public void Highlight(IHTMLElement element) {
            IHTMLElement[] arr = new IHTMLElement[] { element };
            elementsSelector.Highlight(arr);
            _cssSelector = GetCssPath(element, false);
            
        }

        /// <summary>
        /// 选择近似元素列表
        /// </summary>
        /// <param name="element1"></param>
        /// <param name="element2"></param>
        public void HighlightPareElements(IHTMLElement element1, IHTMLElement element2) {
            _cssSelector = GetSelector(element1, element2);
            if (string.IsNullOrEmpty(_cssSelector))
            {
                throw new ApplicationException("非近似元素");               
            }
            HighlithBySelector(_cssSelector);
        }

        public void Highlight(IHTMLElement[] elements) {
            elementsSelector.Highlight(elements);
        }
     
        public void Restore() {
            elementsSelector.Restore();
       
        }

        public void AreaRestore() {
            if (selectAreaSelector != null)
            {
                selectAreaSelector.Restore();
            }
        }
        /// <summary>
        /// 获取2个相近元素获取列表CSS SELECTOR
        /// </summary>
        /// <param name="first"></param>
        /// <param name="sencond"></param>
        /// <returns></returns>
        private string GetSelector(IHTMLElement first, IHTMLElement sencond)
        {
            string path1 = GetCssPath(first, false, false);
            string path2 = GetCssPath(sencond, false, false);

            string cssSelector = CssStringCompare(path1, path2);

            if (string.IsNullOrEmpty(cssSelector))
            {
                path1 = GetCssPath(first, true);
                path2 = GetCssPath(sencond, true);
                cssSelector = CssStringCompare(path1, path2);
            }

            if (string.IsNullOrEmpty(cssSelector)) return "";
            cssSelector = RefineCssSelector(cssSelector);
            return cssSelector;
        }

        public string GetCssPath(Element element, bool fullPath)
        {
            var nativeElement = element.NativeElement as IEElement;
            return GetCssPath(nativeElement.AsHtmlElement, fullPath);
        }

        public string GetCssPath(mshtml.IHTMLElement element, bool fullPath)
        {
            return GetCssPath(element, fullPath, true);
        }

        /// <summary>
        /// id或className需要分大小写
        /// </summary>
        /// <param name="element"></param>
        /// <param name="fullPath"></param>
        /// <param name="ignoreSelectArea">是否检查选择区域</param>
        /// <returns></returns>
        private string GetCssPath(mshtml.IHTMLElement element, bool fullPath, bool ignoreSelectArea)
        {
            if (element == null)
                return "";

            if (SelectAreaSelector != null && !ignoreSelectArea)
            {
                if (!element.IsChildOf(SelectAreaSelector)) {
                    throw new ApplicationException("必须在选择区域内定位元素，定位元素失败!");
                }
                string childCssSelector = GetContextSelector(SelectAreaSelector, element);
                return GetCssPath(SelectAreaSelector, false, true) + " " + childCssSelector;
            }

            mshtml.IHTMLElement currentNode = element;
            ArrayList path = new ArrayList();

            while (currentNode != null)
            {
                string pe = getNode(currentNode, fullPath);
                if (pe != null)
                {
                    path.Add(pe);
                    if (pe.IndexOf("#") != -1)
                        break;  // Found an ID, no need to go upper, absolute path is OK
                }
                currentNode = currentNode.parentElement;
            }
            path.Reverse();
            return join(path, ">");
        }
       
        /// <summary>
        /// 获取CSS Selector(当包含上下文的时候调用此方法)
        /// http://stackoverflow.com/questions/3390396/how-to-check-for-undefined-in-javascript
        /// </summary>
        /// <param name="element"></param>
        public string GetContextSelector(mshtml.IHTMLElement context, mshtml.IHTMLElement element)
        {
            SetContextSelector(context);
            var originalId = element.id;
            if (string.IsNullOrEmpty(originalId))
            {
                var id = "__" + IEUtils.IEVariableNameHelper.CreateVariableName();
                element.id = id;
            }
            mshtml.IHTMLWindow2 parentWindow = ((IHTMLDocument2)(element.document)).parentWindow as mshtml.IHTMLWindow2;
            IEUtils.RunScript(ScriptLoader.GetJquerySelector(""), parentWindow);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("var el = $jq('#{0}');", element.id));
            sb.AppendLine();

            sb.Append("if(typeof __context == 'undefined'){");
            sb.AppendLine();
            sb.Append("__context = '';");
            sb.AppendLine();
            sb.Append("}");
            sb.AppendLine();
            sb.Append(" var selector = el.getCssPath(__context);");
            sb.AppendLine();
            sb.Append("__context = 'undefined';");
            sb.Append("return selector");
            object result = JS.FunEval(AppContext.Current.Window, sb.ToString());
            element.id = originalId;
            return result == null ? "" : result.ToString();
        }

        /// <summary>
        /// 设置上下文变量(__context)
        /// </summary>
        /// <param name="element"></param>
        private void SetContextSelector(mshtml.IHTMLElement element)
        {

            var originalId = element.id;
            if (string.IsNullOrEmpty(originalId))
            {
                var id = "__" + IEUtils.IEVariableNameHelper.CreateVariableName();
                element.id = id;
            }
            mshtml.IHTMLWindow2 parentWindow = ((IHTMLDocument2)(element.document)).parentWindow as mshtml.IHTMLWindow2;
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("__context = $jq('#{0}');", element.id));
            IEUtils.RunScript(ScriptLoader.GetJqueryInstallScript(), parentWindow);
            IEUtils.RunScript(sb.ToString(), parentWindow);
            element.id = originalId;
        }

        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private string RefineCssSelector(string selector)
        {
            string[] selectorTokens = selector.Split(">".ToCharArray());
            List<string> tokens = new List<string>();
            //找到tr[*]
            bool findAsterisk = false;

            //对表列多选特殊处理, 将td,td:eq(n)替换为td:nth-child(1),td:nth-child(q)
            //:nth-of-type(n) 选择器匹配属于父元素的特定类型的第 N 个子元素的每个元素.
            //:nth-child() 选择器，该选择器选取父元素的第 N 个子元素，与类型无关。
            foreach (string token in selectorTokens)
            {
                if (token.Contains("[*]"))
                {
                    tokens.Add(token.Replace("[*]", ""));
                    findAsterisk = true;
                    continue;
                }
                if (findAsterisk && token.IndexOf(":eq(") >= 0)
                {
                    var tag = token.Substring(0, token.IndexOf(":"));

                    string intString = (new Regex(@"\(([^}]*)\)")).Match(token).Value;
                    int val = Convert.ToInt32(intString.Replace("(", "").Replace(")", "")) + 1;
                    tokens.Add(tag + ":nth-of-type(" + val + ")");
                    //findAsterisk = false;
                }
                else
                {
                    tokens.Add(token);
                }
            }
            return string.Join(">", tokens.ToArray());
        }

    

        private string join(ArrayList items, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in items)
            {
                if (item == null)
                    continue;
                sb.Append(item);
                sb.Append(delimiter);
            }
            return sb.ToString(0, sb.Length - 1);
        }

        private IHTMLDOMNode SkipText(IHTMLDOMNode psDom)
        {
            while (psDom != null && psDom.nodeName == "#text")
            {
                psDom = psDom.previousSibling;
            }
            return psDom;
        }

        private string getNode(mshtml.IHTMLElement node, bool fullPath)
        {
            string nodeExpr = node.tagName;
            if (nodeExpr == null)  // Eg. node = #text
                return null;
            if (node.id != "" && node.id != null && !fullPath)
            {
                nodeExpr += "#" + node.id;
                // We don't really need to go back up to //HTML, since IDs are supposed
                // to be unique, so they are a good starting point.
                return nodeExpr;
            }

            // Find rank of node among its type in the parent
            int rank = 0;
            mshtml.IHTMLDOMNode nodeDom = node as mshtml.IHTMLDOMNode;
            mshtml.IHTMLDOMNode psDom = nodeDom.previousSibling;
            psDom = SkipText(psDom);
            mshtml.IHTMLElement ps = psDom as mshtml.IHTMLElement;
            while (ps != null)
            {
                if (ps.tagName == node.tagName)
                {
                    rank++;
                }
                psDom = psDom.previousSibling;
                psDom = SkipText(psDom);
                ps = psDom as mshtml.IHTMLElement;
            }
            if (rank > 0)
            {
                nodeExpr += ":eq(" + rank + ")";
            }
            else
            { // First node of its kind at this level. Are there any others?
                mshtml.IHTMLDOMNode nsDom = nodeDom.nextSibling;
                while (nsDom != null && nsDom.nodeName == "#text")
                {
                    nsDom = nsDom.nextSibling;
                }
                //nsDom = SkipText(nsDom);
                mshtml.IHTMLElement ns = nsDom as mshtml.IHTMLElement;
                while (ns != null)
                {
                    if (ns.tagName == node.tagName)
                    { // Yes, mark it as being the first one
                        nodeExpr += ":eq(0)";
                        break;
                    }
                    nsDom = nsDom.nextSibling;
                    ns = nsDom as mshtml.IHTMLElement;
                }
            }
            return nodeExpr;
        }

        /// <summary>
        /// 比较2个XPATH是否是同位路径，如果是返回带*路径,否则返回空字符串
        /// </summary>
        /// <param name="xpath1"></param>
        /// <param name="xpath2"></param>
        /// <returns></returns>
        private string CssStringCompare(string css1, string css2)
        {

            string[] p1 = css1.Split(">".ToCharArray());
            string[] p2 = css2.Split(">".ToCharArray());
            //选最短的作为遍历长度
            int len = p1.Length < p2.Length ? p1.Length : p2.Length;
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int i = 0; i < len; i++)
            {
                if (p1[i] == p2[i])
                {
                    sb.Append(p1[i]);
                    sb.Append(">");
                    continue;
                }
                else
                {
                    string tag = GetTag(p1[i]);
                    if (tag != GetTag(p2[i]))
                    {
                        return null;
                    }
                    else
                    {
                        sb.Append(tag + "[*]");
                        sb.Append(">");
                        count++;
                    }
                }
            }
            if (count > 1)
            {
                return null;
            }
            sb.Length = sb.Length - 1;
            return sb.ToString();
        }


        private string GetTag(string tag)
        {
            int index = tag.IndexOf(":");
            if (index == -1) return tag;
            return tag.Substring(0, index);
        }

    }
}
