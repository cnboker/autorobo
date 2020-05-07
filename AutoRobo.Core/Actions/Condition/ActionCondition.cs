using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core;
using System.Text.RegularExpressions;
using System.Xml;
using AutoRobo.UserControls;
using AutoRobo.Core.UserControls;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 条件测试，元素内容符合表达式则继续执行
    /// </summary>
    public class ActionCondition : ActionBlock
    {
        public override string ActionDisplayName
        {
            get { return "元素内容测试"; }
        }
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// 内容类型枚举
        /// </summary>
        public enum AvailableTextFormat { Text, Html }
        public enum MatchMode
        {
            /// <summary>
            /// 当前元素存在则执行
            /// </summary>
            Exist,
            /// <summary>
            /// 内容匹配
            /// </summary>
            Content
        }
        /// <summary>
        /// 匹配文本类型
        /// </summary>
        public AvailableTextFormat TextFormat { get; set; }
        public MatchMode Mode { get; set; }
        /// <summary>
        /// 是否区分大小写
        /// </summary>
        public bool Ignorecase { get; set; }

        public override void Perform()
        {
            Element element = GetElement();
            if (Mode == MatchMode.Exist)
            {
                if (element.Exists)
                {
                    base.Perform();
                }
            }
            else
            {
                if (!element.Exists) return;
                string text = element.Text;
                if (TextFormat == AvailableTextFormat.Html)
                {
                    text = element.InnerHtml;
                }
                if ((new Regex(Pattern)).IsMatch(text))
                {
                    base.Perform();
                }
            }
        }
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("TestElement.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucCondition(editorAction);
        }

        public override string GetDescription()
        {
            return "测试元素是否包含指定内容文本";
        }

   

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            string modeString = GetAttribute(node, "Mode");
            if (!string.IsNullOrEmpty(modeString))
            {
                Mode = (MatchMode)Enum.Parse(typeof(MatchMode), node.Attributes["Mode"].Value);
            }
            TextFormat = (AvailableTextFormat)Enum.Parse(typeof(AvailableTextFormat), node.Attributes["TextFormat"].Value);
            Ignorecase = GetBooleanAttribute(node, "Ignorecase");
            Pattern = GetAttribute(node, "Pattern");            
        }


        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("TextFormat", TextFormat.ToString());
            writer.WriteAttributeString("Mode", Mode.ToString());
            writer.WriteAttributeString("Ignorecase", Ignorecase ? "1" : "0");
            writer.WriteAttributeString("Pattern", Pattern);
            base.WriterAddAttribute(writer);
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
