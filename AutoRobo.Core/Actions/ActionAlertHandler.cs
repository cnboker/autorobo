using System.Text;
using System.Xml;
using AutoRobo.Core.Formatters;
using WatiN.Core.DialogHandlers;
using System;

namespace AutoRobo.Core.Actions
{
    [Serializable]
    public class ActionAlertHandler : ActionBase
    {
      
        private readonly BrowserWindow browser;

        public ActionAlertHandler(){
            
        }

        public ActionAlertHandler(BrowserWindow browser)
        {
            this.browser = browser;
           
        }

        public override System.Drawing.Bitmap GetIcon()
        {
            return GetIconFromFile("AlertHandler.bmp");
        }

        public override string GetDescription()
        {
            return "处理消息弹出框";
        }

        public override void Perform()
        {
           //Window.HandleAlert = true;
            
        }

        //public override CodeLine ToCode(ICodeFormatter Formatter)
        //{
        //    var builder = new StringBuilder();
        //    if (Formatter.DeclaredAlertHandler)
        //    {
        //        builder.AppendLine("adhdl = " + Formatter.NewDeclaration + " AlertDialogHandler()" + Formatter.LineEnding);
        //    }
        //    else
        //    {
        //        builder.AppendLine(Formatter.ClassNameFormat(typeof(AlertDialogHandler), "adhdl") + "  = " + Formatter.NewDeclaration + " AlertDialogHandler()" + Formatter.LineEnding);
        //    }

        //    //if (ParentPage != null)
        //     //   builder.AppendLine(ParentPage.FriendlyName + ".AddDialogHandler(adhdl)" + Formatter.LineEnding);
        //    //else 
        //    builder.AppendLine("window.AddDialogHandler(adhdl)" + Formatter.LineEnding);
        //    builder.AppendLine("adhdl.OKButton.Click()" + Formatter.LineEnding);
        //    var line = new CodeLine {NoModel = true, FullLine = builder.ToString()};
        //    return line;
        //}

      
        public override string ActionDisplayName
        {
            get { return "消息框处理"; }
        }
    }
}
