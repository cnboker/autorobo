using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using System.Collections;
using WatiN.Core.Native.InternetExplorer;
using mshtml;


namespace AutoRobo.Core.Actions
{
    public class XPathFinder
    {
        public static string GetXPath(IHTMLElement el)
        {
            mshtml.IHTMLElement element = el as mshtml.IHTMLElement;
            return GetXPath(element,true);
        }

        //public static string GetXPath(IHTMLElement el, bool fullPath)
        //{
        //    mshtml.IHTMLElement element = el as mshtml.IHTMLElement;
        //    return GetXPath(element,fullPath);
        //}

        public static string GetXPath(Element element) {
            var nativeElement = element.NativeElement as IEElement;
            return GetXPath(nativeElement.AsHtmlElement, true);
        }

        //public static string GetXPath(mshtml.IHTMLElement element)
        //{
        //    return GetXPath(element, true);
        //}

        public static string GetXPath(mshtml.IHTMLElement element, bool fullPath)
        {            
            if (element == null)
                return "";
            mshtml.IHTMLElement currentNode = element;
            ArrayList path = new ArrayList();

            while (currentNode != null)
            {
                string pe = getNode(currentNode, fullPath);
                if (pe != null)
                {
                    path.Add(pe);
                    if (pe.IndexOf("@id") != -1)
                        break;  // Found an ID, no need to go upper, absolute path is OK
                }
                currentNode = currentNode.parentElement;
            }
            path.Reverse();
            return join(path, "/").ToLower();
        }

        private static string join(ArrayList items, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in items)
            {
                if (item == null)
                    continue;

                sb.Append(delimiter);
                sb.Append(item);
            }
            return sb.ToString();
        }

        static private IHTMLDOMNode SkipText(IHTMLDOMNode psDom)
        {
            while (psDom != null && psDom.nodeName == "#text")
            {
                psDom = psDom.previousSibling;
            }
            return psDom;
        }

        private static string getNode(mshtml.IHTMLElement node, bool fullPath)
        {
            string nodeExpr = node.tagName;
            if (nodeExpr == null)  // Eg. node = #text
                return null;
            if (node.id != "" && node.id != null && !fullPath)
            {
                nodeExpr += "[@id='" + node.id + "']";
                // We don't really need to go back up to //HTML, since IDs are supposed
                // to be unique, so they are a good starting point.
                return "/" + nodeExpr;
            }

            // Find rank of node among its type in the parent
            int rank = 1;
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
            if (rank > 1)
            {
                nodeExpr += "[" + rank + "]";
            }
            else
            { // First node of its kind at this level. Are there any others?
                mshtml.IHTMLDOMNode nsDom = nodeDom.nextSibling;
                psDom = SkipText(psDom);
                mshtml.IHTMLElement ns = nsDom as mshtml.IHTMLElement;
                while (ns != null)
                {
                    if (ns.tagName == node.tagName)
                    { // Yes, mark it as being the first one
                        nodeExpr += "[1]";
                        break;
                    }
                    nsDom = nsDom.nextSibling;
                    ns = nsDom as mshtml.IHTMLElement;
                }
            }
            return nodeExpr;
        }

        //public string GetElementXPath(IHTMLDOMNode elt)
        //{
        //    if (elt == null) return "";
        //    var path = "";
        //    for (; elt.nodeType == 1; elt = elt.parentNode)
        //    {
                
        //        int idx = GetElementIdx(elt);
        //        string xname = elt.nodeName;               
        //        if (idx > 1) xname += "[" + idx + "]";                
        //        path = "/" + xname + path;
        //    }

        //    return path.ToLower();
        //}


        //private int GetElementIdx(IHTMLDOMNode elt)
        //{
        //    var count = 1;
        //    for (var sib = elt.previousSibling; sib != null; sib = sib.previousSibling)
        //    {
        //        if (sib.nodeType == 1 && sib.nodeName == elt.nodeName) count++;
        //    }
           
        //    return count;
        //}


        //public string GetElementXPath(Element elt)
        //{
        //    if (elt == null) return "";
        //    var path = "";
        //    for (; elt != null; elt = elt.Parent)
        //    {
        //        int idx = GetElementIdx(elt);
        //        string xname = elt.TagName;
        //        if (idx > 1) xname += "[" + idx + "]";
        //        path = "/" + xname + path;
        //    }

        //    return path;
        //}

        //private int GetElementIdx(Element elt)
        //{
        //    var count = 1;
        //    for (var sib = elt.PreviousSibling; sib != null; sib = sib.PreviousSibling)
        //    {
        //        if (sib != null && sib.TagName == elt.TagName) count++;
        //    }

        //    return count;
        //}
    }
}
