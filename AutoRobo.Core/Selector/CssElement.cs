using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.Linq;

namespace AutoRobo.Core
{
    public class CssStyle {
        /// <summary>
        /// 边框样式
        /// </summary>
        public string OutlineStyle { get; set; }
        /// <summary>
        /// 边框颜色
        /// </summary>
        public string OutlineColor { get; set; }
        /// <summary>
        /// 边框宽度
        /// </summary>
        public string OutlineWidth { get; set; }
        /// <summary>
        /// 加亮背景色
        /// </summary>
        public string BackgroundColor { get; set; }
    }

    public class CssElement
    {
        private IHTMLElement element;
        /// <summary>
        /// 边框样式
        /// </summary>
        public string OutlineStyle {get;set;}
        /// <summary>
        /// 边框颜色
        /// </summary>
        public string OutlineColor { get; set; }
        /// <summary>
        /// 边框宽度
        /// </summary>
        public string OutlineWidth { get; set; }
        /// <summary>
        /// 加亮背景色
        /// </summary>
        public string BackgroundColor { get; set; }
       
        private string originalOutlineStyle;
        private string originalOutlineColor;
        private string originalOutlineWidth;
        private string originalBackgroundColor;

        public CssElement(IHTMLElement element)
        {
            this.element = element;
            OutlineStyle = "dashed";
            OutlineColor = "#0000ff";
            OutlineWidth = "2px";
            BackgroundColor = "#f1fee2";           
        }

        public CssElement(IHTMLElement element, CssStyle style):this(element) {
            if (style != null)
            {
                if (!string.IsNullOrEmpty(style.OutlineStyle))
                {
                    OutlineStyle = style.OutlineStyle;
                }
                if (!string.IsNullOrEmpty(style.OutlineColor))
                {
                    OutlineColor = style.OutlineColor;
                }
                if (!string.IsNullOrEmpty(style.OutlineWidth))
                {
                    OutlineWidth = style.OutlineWidth;
                }
                if (!string.IsNullOrEmpty(style.BackgroundColor))
                {
                    BackgroundColor = style.BackgroundColor;
                }
            }
        }

        public IHTMLElement HTMLElement
        {
            get { return this.element; }
        }
    
        public void SaveOriginalAttribute()
        {
            mshtml.IHTMLElement el = element as mshtml.IHTMLElement;
            Object att = el.style.getAttribute("outline-style", 0);
            originalOutlineStyle = (att != null ? att.ToString() : "");

            att = el.style.getAttribute("outline-color", 0);
            originalOutlineColor = (att != null ? att.ToString() : "");

            att = el.style.getAttribute("outline-width", 0);
            originalOutlineWidth = (att != null ? att.ToString() : "");

            att = el.style.getAttribute("background-color", 0);
            originalBackgroundColor = (att != null ? att.ToString() : "");

        }

        public void RestoreOriginalAttribute()
        {
            mshtml.IHTMLElement el = element as mshtml.IHTMLElement;
            el.style.setAttribute("outline-style", originalOutlineStyle, 1);
            el.style.setAttribute("outline-color", originalOutlineColor, 1);
            el.style.setAttribute("outline-width", originalOutlineWidth, 1);
            el.style.setAttribute("background-color", originalBackgroundColor, 1);
          
        }

        public void ChangeElementAttribute()
        {
            mshtml.IHTMLElement el = element as mshtml.IHTMLElement;
            string[] excludeTags = new string[] { "head", "meta", "style", "script", "noscript", "link"};
            if(excludeTags.Contains(el.tagName.ToLower())) return;
            el.style.setAttribute("outline-style", OutlineStyle, 1);
            el.style.setAttribute("outline-color", OutlineColor, 1);
            el.style.setAttribute("outline-width", OutlineWidth, 1);
            el.style.setAttribute("background-color", BackgroundColor, 1);
            
        }

    }

}
