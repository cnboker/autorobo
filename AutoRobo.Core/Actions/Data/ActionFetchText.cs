using System;
using System.Xml;
using WatiN.Core;
using AutoRobo.Core.Actions.Data;
using AutoRobo.Core.Formatters;
using AutoRobo.Core.Interface;
using AutoRobo.Core.UserControls;


namespace AutoRobo.Core.Actions
{
    public enum ExpressType
    {
        Regex,
        Javascript
    }

    public abstract class ActionFetchText : ActionElementBase, ICodeFormatterAcceptor, IElementBinding
    {        
        /// <summary>
        /// 变量名称(数组或表格变量)
        /// </summary>
         public string ObjectName { get; set; }    
        /// <summary>
        /// 使用对字符串处理逻辑
        /// </summary>
        public StringPattern Pattern { get; set; }
        /// <summary>
        /// 页面元素属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 空字符串不添加
        /// </summary>
        public bool FliterEmptyString { get; set; }
        /// <summary>
        /// 操作方式
        /// </summary>
        public ExtractType ExtractType { get; set; }

        public ActionFetchText() {
            Pattern = new StringPattern() { Pattern = StringMode.Javascript };
        }

        public IElementContainer ContextElement
        {
            get;
            set;
        }
        /// <summary>
        /// 当前元素是否为遍历元素子元素，且元素selector设置section selector(短selector)
        /// </summary>
        public bool IsContextBinding { get; set; }
        /// <summary>
        /// 获取该元素所在窗口， 可能是iframe或独立window
        /// </summary>
        /// <returns></returns>
        public override IElementContainer GetWindow()
        {
            if (IsContextBinding)
            {
                return ContextElement;
            }
            return base.GetWindow();
        }

      

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucAddDataToList(editorAction);
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("array.png");
        }

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            IsContextBinding = GetBooleanAttribute(node, "IsContextBinding");
            ObjectName = GetAttribute(node, "ObjectName");
            PropertyName = GetAttribute(node, "PropertyName");
            Pattern.LoadFromXml(node);
            FliterEmptyString = GetBooleanAttribute(node, "FliterEmptyString");
            string stringExtractType = GetAttribute(node, "ExtractType");
            if (!string.IsNullOrEmpty(stringExtractType))
            {
                this.ExtractType = (ExtractType)Enum.Parse(typeof(ExtractType), stringExtractType);
            }    
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("ObjectName", ObjectName);
            writer.WriteAttributeString("PropertyName", PropertyName);
            Pattern.WriterAddAttribute(writer);
            writer.WriteAttributeString("FliterEmptyString", FliterEmptyString ? "1" : "0");
            writer.WriteAttributeString("IsContextBinding", IsContextBinding ? "1" : "0");
            writer.WriteAttributeString("ExtractType", Convert.ToInt16(this.ExtractType).ToString());
            base.WriterAddAttribute(writer);
        }
        /// <summary>
        /// 设计时获取值
        /// </summary>
        /// <returns></returns>
        public string GetDefaultValue() {
            return GetValue(GetElement(), true);
        }

        protected string GetValue(Element el) {
            return GetValue(el, false);
        }
        /// <summary>
        /// 增加数据到数组变量
        /// </summary>
        /// <param name="el"></param>
        /// <param name="rawdata">是否获取未加工的数据</param>
        protected string GetValue(Element el, bool rawdata) {
            if (el == null) return "";
            string value = "";    
            
            if (el.Exists)
            {
                if (el is SelectList)
                {
                    if (PropertyName == "Text")
                    {
                        value = ((SelectList)el).SelectedItem;
                    }
                    else if (PropertyName == "Value") {
                        value = ((SelectList)el).SelectedOption.Value;
                    }
                }
                else
                {
                    if (PropertyName == "Text")
                    {
                        value = el.Text;
                    }
                    else if (PropertyName == "Html")
                    {
                        value = el.InnerHtml;
                    }
                    else
                    {
                        value = el.GetAttributeValue(PropertyName);
                    }                   
                }               
            }
            if (!rawdata)
            {
                if (Pattern != null)
                {
                    value = Pattern.GetString(value);
                }
            }
            return value;
        }


        public void Accept(ICodeFormatterVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
