using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WatiN.Core;
using csExWB;
using System.Windows.Forms;
using WatiN.Core.Native.InternetExplorer;
using AutoRobo.Core.UserControls;
using AutoRobo.UserControls;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionValidateCode : ActionTypeText, IValidateGroup
    {
      
        public ActionValidateCode() {  }
        /// <summary>
        /// 关联验证图片Action
        /// </summary>
        public ActionValidateImage ValidateImage { get; set; }
        /// <summary>
        /// 是否是手机验证码
        /// </summary>
        public bool IsMobileValidate { get; set; }

      
        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucValidateCode(editorAction);
        }

        public override void Perform()
        {
            cEXWB webBrowser = AppContext.Browser as cEXWB;
            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new Action(Perform));
                return;
            }
            bool exists = Window.Elements.Exists(GetConstraint());
            if (exists)
            {
                Element element = GetElement();
                //弹出对话框输验证码
                if (ValidateImage != null && !string.IsNullOrEmpty(ValidateImage.InputValue))
                {
                    TextToType = ValidateImage.InputValue;
                    base.Perform();
                }
                else if (IsMobileValidate)
                {
                    ValidateCodeDialog dialog = new ValidateCodeDialog(webBrowser);
                    dialog.Activate();
                    dialog.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(dialog.ValidateCode))
                        {
                            TextToType = dialog.ValidateCode;
                            base.Perform();
                        }
                    }
                }
                else //在浏览器里面输验证码
                {
                    //var nativeElement = element.NativeElement as IEElement;
                    //nativeElement.AsHtmlElement.scrollIntoView();
                    //webBrowser.WBKeyDown -= new WBKeyDownEventHandler(webBrowser_WBKeyDown);
                    //webBrowser.WBKeyDown += new WBKeyDownEventHandler(webBrowser_WBKeyDown);
                    //this.Breakpoint = BreakpointIndicators.ActiveBreakpoint;
                }
            }
        }

        public override string ActionDisplayName
        {
            get { return "验证码"; }
        }
        void webBrowser_WBKeyDown(object sender, WBKeyDownEventArgs e)
        {
            if (e.keycode == Keys.Return)
            {
                AppContext.CurrentWorker.Continue();
            }
        }

        public override void LoadFromXml(XmlNode node)
        {
            if (node.Attributes["GroupName"] != null) GroupName = node.Attributes["GroupName"].Value;
            if (node.Attributes["IsMobileValidate"] != null) IsMobileValidate = node.Attributes["IsMobileValidate"].Value == "1" ? true : false;
            base.LoadFromXml(node);
        }


        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("GroupName", GroupName);
            writer.WriteAttributeString("IsMobileValidate", IsMobileValidate ? "1" : "0");
            base.WriterAddAttribute(writer);
        }


        public string GroupName
        {
            get;
            set;
        }
    }
}
