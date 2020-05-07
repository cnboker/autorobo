using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.UserControls;
using AutoRobo.Core.UserControls;
using System.Linq;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionCall : ActionBase
    {
        public ActionCall() {
           
        }
        /// <summary>
        /// 调用活动名称
        /// </summary>
        public string FunName { get; set; }

        public override string ElementName
        {
            get
            {
                return FunName;
            }

        }

        public override string ActionDisplayName
        {
            get { return string.Format("{0}过程调用", ElementName); }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("ScriptPart.png");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucActionCall(editorAction);
        }  

        public override void Perform()
        {
            var list = AppContext.ActionModel.SubActionModel;
            var action = list.FirstOrDefault(c => ((ActionScriptPart)c).Name == FunName);
            if (action != null) {                
                action.Perform();
            }
        }

        public override string GetDescription()
        {
            return "过程调用";
        }

   

        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            FunName = GetAttribute(node, "FunName");
        }

       
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("FunName", FunName);
        }
    }
}
