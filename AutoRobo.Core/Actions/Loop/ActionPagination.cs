using System;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 分页活动
    /// </summary>
    public class ActionPagination : ActionBlock
    {       
        /// <summary>
        /// 执行页数，-1表示无限执行
        /// </summary>
        public int PageCount { get; set; }

        public override string ActionDisplayName
        {
            get { return "分页"; }
        }

        public override bool Parse(ActionBuilder.ActionParameter parameter)
        {
            if (parameter.Element == null) return false;
            try
            {
                SetFindMethod(parameter.Element, GetFinder(parameter.Element, FindMethods.Text));
            }
            catch
            {
                return false;
            }
            return true;
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("pagination.png");                       
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucPagination(editorAction);
        }  

        public override string GetDescription()
        {
            return "分页";
        }

        public override void Perform()
        {
            Element element = GetElement();
            if (PageCount == -1)
            {
                do
                {
                    base.Perform();
                    
                    if (ElementIsAvailable(element))
                    {
                        element.Click();
                        element = GetElement();
                    }
                    else
                    {
                        break;
                    }
                } while (true);
            }
            else {
                for (int i = 0; i < PageCount; i++) {
                    base.Perform();
                    if (ElementIsAvailable(element))
                    {
                        element.Click();
                        element = GetElement();
                    }
                }
            }            
        }

        private bool ElementIsAvailable(Element element)
        {
            if (element == null) return false;
            if (element is Button)
            {
                return element.Exists && ((Button)element).Enabled;
            }
            else {
                return element.Exists;
            }
        }
      
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            PageCount = GetIntAttribute(node, "PageCount");
        }


        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("PageCount", PageCount.ToString());
            base.WriterAddAttribute(writer);
        }



        public override void Enter()
        {
           
        }

        public override void Exit()
        {
            
        }
    }
}
