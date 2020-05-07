using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;
using Util;
using WatiN.Core;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// 恢复Session
    /// </summary>
    public class ActionRestoreSession:ActionBase
    {
        //private string key = "watin-url-cookie";

        /// <summary>
        /// 是否弹出对话框提示用户是否恢复上一次会话
        /// </summary>
        public bool IsRestoreSession { get; set; }

        public override string ActionDisplayName
        {
            get { return "恢复Session"; }
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("sessions.png");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucSession(editorAction);
        }

        public override void Perform()
        {
            if (IsRestoreSession)
            {
                if (MessageBox.Show("是否恢复上一次会话", "恢复会话确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Window.RestorePageSession();
                }
            }
            else
            {
                Window.RestorePageSession();
            }
        }
       
       
        public override void LoadFromXml(XmlNode node)
        {
            base.LoadFromXml(node);
            IsRestoreSession = GetBooleanAttribute(node, "IsRestoreSession");
        }

        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("IsRestoreSession", IsRestoreSession ? "1" : "0");
        }

        public override string GetDescription()
        {
            return "恢复Session";
        }


    }
}
