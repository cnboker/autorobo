using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    public class ActionGoTo : ActionBase
    {
        /// <summary>
        /// 跳转Action ID
        /// </summary>
        public string NActionID { get; set; }

        public override string ActionDisplayName
        {
            get { return "跳转活动"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("goto.png");
        }

        public override void Perform()
        {
            AppContext.CurrentWorker.GoTo(NActionID);
        }

        public override string GetDescription()
        {
            return "跳转活动";
        }

        public override AutoRobo.UserControls.ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucActionGoTo(editorAction);
        }
 

        public override void LoadFromXml(System.Xml.XmlNode node)
        {
            NActionID = node.Attributes["NActionID"].Value;
            base.LoadFromXml(node);
        }

        public override void WriterAddAttribute(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("NActionID", NActionID);
            base.WriterAddAttribute(writer);
        }
    }
}
