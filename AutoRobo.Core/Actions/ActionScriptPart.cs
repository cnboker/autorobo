using System;
using System.Xml;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;

namespace AutoRobo.Core.Actions
{
  
    /// <summary>
    /// 过程，子脚本
    /// </summary>
    public class ActionScriptPart : ActionDataBlock
    {
        /// <summary>
        /// 过程名称
        /// </summary>
        public string Name { get; set; }

        public override string ElementName
        {
            get
            {
                return Name;
            }
        }

        public override void Perform()
        {
            //将运行时MapAttribute赋值给MapAttribute
            MapAttribute = AppContext.CurrentWorker.ModelView.MapAttribute;
            base.Perform();
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucScriptPartStep1(editorAction);
        }  

        public override string ActionDisplayName
        {
            get { return string.Format("{0}过程", ElementName); }
        }

   
        public override string GetDescription()
        {
            return "过程";
        }


        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            Name = GetAttribute(node, "Name") ;         
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("Name", Name);
            base.WriterAddAttribute(writer);
        }

     
    }
}
