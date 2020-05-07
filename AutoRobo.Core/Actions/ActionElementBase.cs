using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoRobo.UserControls;
using mshtml;
using WatiN.Core;
using WatiN.Core.Constraints;
using WatiN.Core.Native.InternetExplorer;
using IHTMLDocument2 = mshtml.IHTMLDocument2;
using IHTMLElement = mshtml.IHTMLElement;
using AutoRobo.Core.Models;
using System.Windows.Forms;
using System.Linq;
using AutoRobo.WebHelper;

namespace AutoRobo.Core.Actions
{
    public abstract class ActionElementBase : ActionBase
    {      
        public FindAttributeCollection FrameList = new FindAttributeCollection();
        public FindAttributeCollection FindMechanism = new FindAttributeCollection();
   

        public ElementTypes ElementType { get; set; }
        public ActionElementBase() {
            ElementType = ElementTypes.Element;
            CheckDuplication = true;
        }

        public Selector Selector {
            get {
                return AppContext.Browser.Selector;
            }
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            if (parameter.Element == null) return false;
            try
            {                
                //SetFindMethod(parameter.Element);
                SetFindMethod(parameter.Element, FindMethods.CssSelector, Selector.CssSelector);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string ElementId {
            get {
                foreach (var o in FindMechanism) {
                    if (o.FindMethod == FindMethods.Id) {
                        return o.FindValue;
                    }
                }
                return "";
            }
        }

        public override string ElementName
        {
            get
            {
                return FindMechanism[0].FindValue;
            }
        
        }

        public override string GetDescription()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < FindMechanism.Count; i++)
            {
                if (i > 0) builder.Append(" and ");
                builder.Append(FindMechanism[i].FindMethod + " = " + FindMechanism[i].FindValue);
            }
            return builder.ToString();
        }
      
        //public string GetDocumentPath(ICodeFormatter Formatter)
        //{
        //    var builder = new StringBuilder();
        //    builder.Append(Window.FriendlyName);
        //    if (FrameList.Count > 0)
        //    {
        //        builder.Append(Formatter.MethodSeparator);
        //        builder.Append("Frame(" + FrameList + ")");
        //    }
        //    return builder.ToString();
        //}

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return null;
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);

            string elementTypeString = node.SelectSingleNode("ElementType").InnerText;
            ElementType = (ElementTypes) Enum.Parse(typeof (ElementTypes), elementTypeString);
         
            XmlNode findernode = node.SelectSingleNode("FinderSet");
            
            XmlNodeList finderList = null;
            if (findernode != null)
            {
                finderList = findernode.SelectNodes("Finder");
                if (finderList != null)
                {
                    foreach (XmlNode child in finderList)
                    {
                        var attribute = new FindAttribute();
                        attribute.FromXml(child);
                        FindMechanism.Add(attribute);
                    }
                }
            }

            XmlNode framenode = node.SelectSingleNode("Frames");
            if (framenode != null)
            {
                finderList = framenode.SelectNodes("Finder");
                if (finderList != null)
                {
                    foreach (XmlNode child in finderList)
                    {
                        var attribute = new FindAttribute();
                        attribute.FromXml(child);
                        FrameList.Add(attribute);
                    }
                }
            }
        }

        private void SaveElementSettings(XmlWriter writer)
        {
            writer.WriteElementString("ElementType", ElementType.ToString());            
            FindMechanism.ToXML("FinderSet", writer);
            FrameList.ToXML("Frames", writer);      
        }

        public Element FindElement(ElementTypes ElementType, Constraint constraint) {
             Element result = null;
            switch (ElementType)
            {
                case ElementTypes.Area: result = GetWindow().Area(constraint); break;
                case ElementTypes.Button: result = GetWindow().Button(constraint); break;
                case ElementTypes.CheckBox: result = GetWindow().CheckBox(constraint); break;
                case ElementTypes.Div: result = GetWindow().Div(constraint); break;
                case ElementTypes.Element: result = GetWindow().Element(constraint); break;
                case ElementTypes.Image: result = GetWindow().Image(constraint); break;
                case ElementTypes.Label: result = GetWindow().Label(constraint); break;
                case ElementTypes.Link: result = GetWindow().Link(constraint); break;
                case ElementTypes.Para: result = GetWindow().Para(constraint); break;
                case ElementTypes.RadioButton: result = GetWindow().RadioButton(constraint); break;
                case ElementTypes.SelectList: result = GetWindow().SelectList(constraint); break;
                case ElementTypes.Span: result = GetWindow().Span(constraint); break;
                case ElementTypes.Table: result = GetWindow().Table(constraint); break;
                case ElementTypes.TableRow: result = GetWindow().TableRow(constraint); break;
                case ElementTypes.TableCell: result = GetWindow().TableCell(constraint); break;
                case ElementTypes.TableBody: result = GetWindow().TableBody(constraint); break;
                case ElementTypes.TextField: 
                case ElementTypes.ValidateCode: 
                    result = GetWindow().TextField(constraint); 
                    break;
                case ElementTypes.Body:
                    IElementContainer container = GetWindow();
                    if (container is Document)
                    {
                        result = ((Document)container).Body; 
                    }
                    break;
                default: 
                    result = GetWindow().Elements.Filter(constraint).First();
                    break;
            }
            return result;
        }

        public Element GetElement()
        {
            var constraint = GetConstraint();
            return FindElement(this.ElementType, constraint);
            //ElementCollection ec = GetElements();
            //if (ec == null) return null;
            //return ec.FirstOrDefault();
        }

        public ElementCollection GetElements() {
            IElementContainer container = GetWindow();
            if (container == null) return null;
            var constraint = GetConstraint();
            if (constraint == null) return null;
            return container.Elements.Filter(constraint);
        }

        public List<IHTMLElement> GetIHTMLElements()
        {
            List<IHTMLElement> iels = new List<IHTMLElement>();
            ElementCollection els = GetElements();
            foreach (var element in els)
            {
                if (element == null) continue;
                var el = element.NativeElement as IEElement;
                iels.Add(el.AsHtmlElement);
            }
            return iels;
        }

        public mshtml.IHTMLElement GetIHTMLElement() {
            Element element = GetElement();
            if (element == null) return null;
            var el = element.NativeElement as IEElement;
            return el.AsHtmlElement;
        }

        public FileUpload GetFileElement()
        {      
            Constraint constraint = GetConstraint();
            return GetWindow().FileUpload(constraint);
        }

    
        /// <summary>
        /// 获取该元素所在窗口， 可能是iframe或独立window
        /// </summary>
        /// <returns></returns>
        public virtual IElementContainer GetWindow()
        {          
            IElementContainer window = GetFrame();
            if (window == null)
            {
                window = Window;
            }
            return window;
        }


        public IElementContainer GetFrame()
        {
            Frame frame = null;
            Constraint constraint = null;
            if (FrameList.Count > 0)
            {
                if (FrameList.Count > 0)
                    constraint = GetConstraint(FrameList[0]);               
                frame = Window.Frame(constraint);
                for (int i = 1; i < FrameList.Count; i++) {
                    frame = frame.Frame(GetConstraint(FrameList[i]));
                }                
            }
            return frame;
        }

        public virtual Constraint GetConstraint()
        {
            Constraint constraint = null;
            if (FindMechanism.Count == 0) return null;
            if (FindMechanism.Count > 0) constraint = GetConstraint(FindMechanism[0]);
            if (FindMechanism.Count > 1)
            {
                for (int i = 1; i < FindMechanism.Count; i++)
                {
                    constraint.And(GetConstraint(FindMechanism[i]));
                }
            }
            return constraint;
        }

        static public Constraint GetConstraint(FindAttribute attribute)
        {
            string findvalue = attribute.FindValue;
            Constraint constraint = null;
            switch (attribute.FindMethod)
            {
                case FindMethods.Alt:
                    constraint = Find.ByAlt(findvalue);
                    break;
                case FindMethods.Class:
                    constraint = Find.ByClass(findvalue);
                    break;
                case FindMethods.For:
                    constraint = Find.ByFor(findvalue);
                    break;
                case FindMethods.Id:
                    constraint = attribute.Regex ? Find.ById(new System.Text.RegularExpressions.Regex(findvalue)) : Find.ById(findvalue);
                    break;
                case FindMethods.Index:
                    try
                    {
                        constraint = Find.ByIndex(Convert.ToInt32(findvalue));
                    }
                    catch {
                        MessageBox.Show(string.Format("值{0}格式不正确", findvalue));
                    }
                    break;
                case FindMethods.Name:
                    constraint = attribute.Regex ? Find.ByName(new System.Text.RegularExpressions.Regex(findvalue)):Find.ByName(findvalue);
                    break;
                case FindMethods.Src:
                    constraint = Find.BySrc(findvalue);
                    break;              
                case FindMethods.Text:
                    constraint = Find.ByText(findvalue);
                    break;
                case FindMethods.Title:
                    constraint = Find.ByTitle(findvalue);
                    break;
                case FindMethods.Url:
                    constraint = Find.ByUrl(findvalue);
                    break;
                case FindMethods.Value:
                    constraint = Find.ByValue(findvalue);
                    break;
                case FindMethods.Href:
                    constraint = attribute.Regex ? Find.By("href", new System.Text.RegularExpressions.Regex(findvalue)) : Find.By("href", findvalue);                        
                    break;
                case FindMethods.Custom:
                    constraint = Find.By(attribute.FindName, findvalue);
                    break;              
                case FindMethods.XPath:
                    constraint = new XPathConstraint(findvalue);
                    break;
                case FindMethods.ProximityText:
                    constraint = new ProximityTextConstraint(findvalue);
                    break;
                case FindMethods.CssSelector:
                    constraint = Find.BySelector(findvalue);
                    break;
            }

            return constraint;
        }

        static public string ActiveElementAttribute(IHTMLElement element, string AttributeName)
        {
            if (element == null)
            {
                return "";
            }

            string strValue = "";
            try
            {
                strValue = element.getAttribute(AttributeName, 0) as string ?? "";
                if (string.IsNullOrEmpty(strValue)) {
                    strValue = element.innerText;
                }
            }
            catch (Exception)
            {
            }

            return strValue;
        }
        private FindAttribute GetCssPathFinder(IHTMLElement element)
        {
            FindAttribute Finder = Finder = new FindAttribute();
            Finder.FindMethod = FindMethods.CssSelector;
            string selector = element.getAttribute("__selector", 0) as string;
            if (!string.IsNullOrEmpty(selector))
            {
                Finder.FindValue = selector;
            }
            else
            {
                Finder.FindValue = this.AppContext.Browser.Selector.GetCssPath(element,false);
            }
            return Finder;
        }

      
        private FindAttribute GetXPathFinder(IHTMLElement element)
        {
            FindAttribute Finder = Finder = new FindAttribute();
            Finder.FindMethod = FindMethods.XPath;
            Finder.FindValue = XPathFinder.GetXPath(element);
            return Finder;
        }

        public void SetFindMethod(IHTMLElement element) { 
            SetFindMethod(element, GetFinder(element));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="Finder">优先finder设置</param>
        public void SetFindMethod(IHTMLElement element, FindAttribute Finder)
        {             
             int count = 0;
             if (Finder != null) {
                 FindMechanism.Add(Finder);
                 count = GetElementCount(element.tagName);
                 if (count > 1)
                 {
                     FindMechanism.Clear();
                 }
                 else {
                     SetFrameList(element);
                     ElementType = TagStringToElementType(element);
                     return;
                 }
             }
             Finder = GetFinder(element, FindMethods.CssSelector);
             FindMechanism.Add(Finder);
             mshtml.IHTMLDOMNode node = element as mshtml.IHTMLDOMNode;
             count = GetElementCount(node.nodeName);            
             //不支持CssSelector,试用xpath定位
             if (count == -1) {
                 FindMechanism.Clear();
                 Finder = GetFinder(element, FindMethods.XPath);
                 FindMechanism.Add(Finder);
             }             
             SetFrameList(element);
             ElementType = TagStringToElementType(element);
        }

        protected FindAttribute GetFinder(IHTMLElement element, FindMethods method)
        {
            FindAttribute finder = new FindAttribute();
            finder.FindMethod = method;
            if (method == FindMethods.XPath) {                
                finder.FindValue = XPathFinder.GetXPath(element);
            }
            else if (method == FindMethods.CssSelector)
            {
                finder = GetCssPathFinder(element);
            }
            else {
                finder.FindValue = ActiveElementAttribute(element, method.ToString());
            }
            return finder;       
        }

        public void SetFindMethod(IHTMLElement element, FindMethods method, string value)
        {
            FindAttribute Finder = new FindAttribute();
            Finder.FindMethod = method;
            Finder.FindValue = value;
            FindMechanism.Add(Finder);
            if (element == null) return;
            SetFrameList(element);
            ElementType = TagStringToElementType(element);
        }
     
        private void SetFrameList(IHTMLElement el)
        {                     
            List<IHTMLElement> frames = new List<IHTMLElement>();           
            IHTMLElement frame = BrowserExtensions.GetFrame(el);
            while (frame != null) {
                frames.Add(frame);
                frame = BrowserExtensions.GetFrame(frame);
            }
            foreach (var frameElement in frames)
            {
                FindAttribute attribute = GetFrameFinder((IHTMLElement)frameElement);
                if (attribute != null) FrameList.Add(attribute);
            }
            FrameList.Reverse();
        }

        /// <summary>
        /// frame不支持css selector查找
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        FindAttribute GetFrameFinder(IHTMLElement element) {
            if (!string.IsNullOrEmpty(ActiveElementAttribute(element, "id")))
            {
                FindAttribute Finder = Finder = new FindAttribute();
                Finder.FindMethod = FindMethods.Id;
                Finder.FindValue = ActiveElementAttribute(element, "id");
                return Finder;
            }
            else if (!string.IsNullOrEmpty(ActiveElementAttribute(element, "name")))
            {
                FindAttribute Finder = Finder = new FindAttribute();
                Finder.FindMethod = FindMethods.Name;
                Finder.FindValue = ActiveElementAttribute(element, "name");
                return Finder;
            }
            else {
                return GetXPathFinder(element);
            }
        }


        private int GetElementCount(string tagName)
        {

            ElementCollection collection;
            try
            {
                collection = Window.ElementsWithTag(tagName).Filter(GetConstraint());
                return collection.Count;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        //取元素属性集合
        public  FindAttribute GetFinder(IHTMLElement element)
        {
            FindAttribute Finder = null;
            foreach (var method in AppSettings.Instance.FindPattern)
            {
                if (IsMethodUsed(method)) continue;
                Finder = new FindAttribute();
                Finder.FindValue = ActiveElementAttribute(element, method.ToString());
                Finder.FindMethod = method;
                if (Finder.FindValue != "")
                {
                    break;
                }
                else {
                    Finder = null;
                }
            }
            return Finder;
        }

        public bool IsMethodUsed(FindMethods method)
        {
            foreach (FindAttribute attribute in FindMechanism)
            {
                if (attribute.FindMethod == method) return true;
            }
            return false;
        }
        //判断此元素属性在不在集合中
        public bool IsMethodUsed(string attrName)
        {
            foreach (FindAttribute attribute in FindMechanism)
            {
                if (attribute.FindMethod.ToString() == attrName) return true;
            }
            return false;
        }

        static public ElementTypes TagStringToElementType(IHTMLElement element)
        {
            if (element == null) return ElementTypes.Element;

            ElementTypes tagtype;

            string tagName;
            try
            {
                IHTMLDOMNode node = element as IHTMLDOMNode;
                tagName = node.nodeName.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return ElementTypes.Element;
            }

            if (tagName == "input")
            {
                string tagstring = ActiveElementAttribute(element, "type").ToLower(System.Globalization.CultureInfo.InvariantCulture);
                switch (tagstring)
                {
                    case "button":
                        tagtype = ElementTypes.Button;
                        break;
                    case "submit":
                        tagtype = ElementTypes.Button;
                        break;
                    case "reset":
                        tagtype = ElementTypes.Button;
                        break;
                    case "radio":
                        tagtype = ElementTypes.RadioButton;
                        break;
                    case "checkbox":
                        tagtype = ElementTypes.CheckBox;
                        break;
                    case "file":
                        tagtype = ElementTypes.FileUpload;
                        break;
                    case "image":
                        tagtype = ElementTypes.Image;
                        break;
                    default:
                        tagtype = ElementTypes.TextField;
                        break;
                }
            }
            else
            {
                switch (tagName)
                {
                    case "select":
                        tagtype = ElementTypes.SelectList;
                        break;
                    case "span":
                        tagtype = ElementTypes.Span;
                        break;
                    case "div":
                        tagtype = ElementTypes.Div;
                        break;
                    case "a":
                        tagtype = ElementTypes.Link;
                        break;
                    case "li":
                        tagtype = ElementTypes.Element;
                        break;
                    case "img":
                        tagtype = ElementTypes.Image;
                        break;
                    case "form":
                        tagtype = ElementTypes.Form;
                        break;
                    case "iframe":
                    case "frame":
                        tagtype = ElementTypes.Frame;
                        break;
                    case "p":
                        tagtype = ElementTypes.Para;
                        break;
                    case "table":
                        tagtype = ElementTypes.Table;
                        break;
                    case "tbody":
                        tagtype = ElementTypes.TableBody;
                        break;
                    case "tr":
                        tagtype = ElementTypes.TableRow;
                        break;
                    case "td":
                        tagtype = ElementTypes.TableCell;
                        break;
                    case "textarea":
                        tagtype = ElementTypes.TextField;
                        break;
                    case "body":
                        tagtype = ElementTypes.Body;
                        break;
                    case "button":
                        tagtype = ElementTypes.Button;
                        break;
                    default:
                        tagtype = ElementTypes.Element;
                        break;
                }
            }

            return tagtype;
        }


        public override void WriterAddAttribute(XmlWriter writer)
        {
            SaveElementSettings(writer);
            base.WriterAddAttribute(writer);
        }

        /// <summary>
        /// 运行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="isInjectJquery">是否需要注入jquery</param>
        public void RunScript(string script, bool isInjectJquery) {
            var el = this.GetIHTMLElement();
            mshtml.IHTMLWindow2 parentWindow = ((IHTMLDocument2)(el.document)).parentWindow as mshtml.IHTMLWindow2;
            if (isInjectJquery) {
                IEUtils.RunScript(ScriptLoader.GetJqueryInstallScript(), parentWindow);
            }
            IEUtils.RunScript(script, parentWindow);            
        }

        private string id;
        public override string ID
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = ("el_" + ShortUrl.Expand(ElementName).ToString());
                }
                return id;
            }
        }

        /// <summary>
        /// 加亮活动元素
        /// </summary>
        /// <param name="action"></param>
        public void Highlight()
        {            
            Selector selector = this.AppContext.Browser.Selector;
            var elements = GetElements();
            if (elements.Count == 0)
            {
                MessageBox.Show("元素未找到");
            }
            else
            {
                selector.Highlight(elements.Select(c => c.GetHtmlElement()).ToArray());
            }
        }

        public void Restore() {
            Selector selector = this.AppContext.Browser.Selector;
            selector.Restore();
        }
    
    }
}
