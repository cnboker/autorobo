using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using WatiN.Core.DialogHandlers;
using AutoRobo.UserControls;
using System;


namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionConfirmHandler : ActionBase
    {

        private readonly BrowserWindow browser;
        public ActionConfirmHandler() { }

        public ActionConfirmHandler(BrowserWindow browser, int hWnd)
        {
            this.browser = browser;
            this.hWnd = hWnd;
        }
        public ActionConfirmHandler(BrowserWindow browser, int hWnd, System.Windows.Forms.DialogResult dialogResult)
        {
            this.browser = browser;
            this.hWnd = hWnd;
            this.dialogResult = dialogResult;
        }
        private int hWnd;

        public int HWnd
        {
            get { return hWnd; }
            set { hWnd = value; }
        }
        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("AlertHandler.bmp");
        }

        public override ucBaseEditor GetEditor(IEditorAction editorAction)
        {
            return new ucConfirm(editorAction);
        }

        public override string GetDescription()
        {
            return "处理确认弹出框";
        }

        //确认框返回值
        private System.Windows.Forms.DialogResult dialogResult;
        public System.Windows.Forms.DialogResult DialogResult
        {
            get { return dialogResult; }
            set { dialogResult = value; }
        }
        public override void Perform()
        {
            //        browser.ParentScript.MainBrowser.HandleAlert = true;


        }

        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var builder = new StringBuilder();
        //    //if (Formatter.DeclaredAlertHandler)
        //    //{
        //    //    builder.AppendLine("adhdl = " + Formatter.NewDeclaration + " AlertDialogHandler()" + Formatter.LineEnding);
        //    //}
        //    //else
        //    //{
        //    //    builder.AppendLine(Formatter.ClassNameFormat(typeof(AlertDialogHandler), "adhdl") + "  = " + Formatter.NewDeclaration + " AlertDialogHandler()" + Formatter.LineEnding);
        //    //}

        //    //if (ParentPage != null)
        //    //    builder.AppendLine(ParentPage.FriendlyName + ".AddDialogHandler(adhdl)" + Formatter.LineEnding);
        //    //else builder.AppendLine("window.AddDialogHandler(adhdl)" + Formatter.LineEnding);
        //    //builder.AppendLine("adhdl.OKButton.Click()" + Formatter.LineEnding);
        //    var line = new CodeLine { NoModel = true, FullLine = builder.ToString() };
        //    return line;
        //}


        public override string ID
        {
            get
            {
                return (GetType().ToString() + "." + hWnd.ToString());

            }
        }

        public override string ActionDisplayName
        {
            get { return "处理确认弹出框"; }
        }
    }
}
