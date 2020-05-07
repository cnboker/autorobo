using System;
using System.Collections.Generic;

using System.Text;
using WatiN.Core;
using WatiN.Core.Native.InternetExplorer;
using mshtml;
using csExWB;
using System.Threading;
using System.Windows.Forms;
using AutoRobo.UserControls;
using AutoRobo.Core.UserControls;
using System.Xml;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionValidateImage : ActionElementBase, IValidateGroup
    {
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("code.png");
        }
        public ActionValidateImage() {  }

        public ActionValidateCode ActionValidateCode { get; set; }

     

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucValidateImage(editorAction);
        }
        /// <summary>
        /// 用户输入值
        /// </summary>
        public string InputValue { get; set; }
        /// <summary>
        /// 缺省是否为隐藏状态， 只有验证码输入框聚焦后才会显示出来
        /// </summary>
        public bool IsHide { get; set; }

        public override void Perform()
        {
            //验证码获得焦点后，隐藏的验证码图片才显示出来
            if (IsHide)
            {
                //触发验证码显示
                if (ActionValidateCode != null)
                {
                    ActionValidateCode.GetElement().Focus();
                    Thread.Sleep(500);
                    Application.DoEvents();
                }
            }

            Element element = GetElement();
            //element.WaitUntilExists();
            var nativeElement = element.NativeElement as IEElement;

            mshtml.IHTMLElement el = nativeElement.AsHtmlElement as mshtml.IHTMLElement;
            el.scrollIntoView();
            ShowDialog((IHTMLElement)el);
            //if (imgId == null) {
            //    webBrowser.DrawThumb((IHTMLElement)nativeElement.AsHtmlElement, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //}
         
        }

        private void ShowDialog(IHTMLElement el ) {
            cEXWB webBrowser = AppContext.Browser as cEXWB;
            if (webBrowser.InvokeRequired)
            {
                webBrowser.Invoke(new Action(Perform));
                return;
            }
            ValidateCodeDialog dialog = new ValidateCodeDialog(webBrowser, el);

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InputValue = dialog.ValidateCode;
            }            
        }
        public override string ActionDisplayName
        {
            get { return "验证码图片"; }
        }
        public override string GetDescription()
        {
            return "验证码图片";
        }

     
        public override void WriterAddAttribute(XmlWriter writer)
        {
            writer.WriteAttributeString("IsHide", IsHide ? "1" : "0");
            writer.WriteAttributeString("GroupName", GroupName);
            base.WriterAddAttribute(writer);
        }

        public override void LoadFromXml(System.Xml.XmlNode node)
        {
            if (node.Attributes["GroupName"] != null) GroupName = node.Attributes["GroupName"].Value;
            if (node.Attributes["IsHide"] != null) IsHide = (node.Attributes["IsHide"].Value == "1");            
            base.LoadFromXml(node);
        }
        public string GroupName
        {
            get;
            set;
        }
    }
}
