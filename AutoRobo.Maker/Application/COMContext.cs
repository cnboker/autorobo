using AutoRobo.ClientLib.PageHooks;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using mshtml;
using System.Runtime.InteropServices.Expando;
using System.Security.Permissions;

namespace AutoRobo.Robo.Application
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class COMContext
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(COMContext));
        AppContext context = null;

        public COMContext(AppContext context)
        {
            this.context = context;
        }

        public void Call(string serviceType, object args)
        {
            ICustomIdentity Identity = new CustomIdentity();
            IExpando expando = args as IExpando;
            if (expando.Contain("IsAuthenticated"))
                Identity.IsAuthenticated = expando.GetBoolean("IsAuthenticated");

            if (expando.Contain("UserId"))
                Identity.UserId = expando.GetString("UserId");
            if (expando.Contain("Ticket"))
            {
                Identity.Ticket = expando.GetString("Ticket");
            }
            context.Identity = Identity;
        } 
  
        public void SelectOnChange(object element)
        {            
            if (!context.State.IsRecord) return;
            
            IHTMLElement select = element as IHTMLElement;
            logger.Info("selectonchange:" + select.tagName);
            context.ActionModel.AddAction(context, ActionEnum.ActionSelectList.ToString(),
            new Core.ActionBuilder.SelectParameter()
            {

                ByValue = ((mshtml.IHTMLElement)element).getAttribute("value", 0) != null,
                Element = select,
                XPathFinder = false                
            });
        }

        public void Click(object element, string jquerySelector) {
            logger.Info(element.GetType());
            logger.Info("selector path:" + jquerySelector);
            IHTMLElement el = element as IHTMLElement;
            
            el.setAttribute("selector", jquerySelector, 0);
            //if (!makerView.EnableClickEventTrigger) return;
            //WatinContext context = WatinContextFactory.GetContext(coreBrowser);
            //IHTMLElement el = element as IHTMLElement;
            //CollectDialog dialog = new CollectDialog();
            //dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            //ActionBase action = null;
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
            //    if (dialog.CollectEventType == CollectEventType.CollectionContent) {
            //       action= context.ProjectSet.CreateAction(coreBrowser, ActionEnum.ExtractText,
            //        new Core.ActionBuilder.ActionParameter()
            //        {
            //            Element = el,
            //            CssSelector = jquerySelector                
            //        });
            //    }
            //    else if (dialog.CollectEventType == CollectEventType.MouseClick)
            //    {
            //       action= context.ProjectSet.CreateAction(coreBrowser, ActionEnum.BrowserClick,
            //        new Core.ActionBuilder.ActionClickParameter()
            //        {
            //            Element = el,                       
            //            CheckDuplication = true
            //        });
            //    }
            //    if (action != null)
            //    {
            //        IActivityWorkshopView wsv = makerView.QueryView(typeof(IActivityWorkshopView)) as IActivityWorkshopView;
            //        IEditorAction editorProvider = wsv as IEditorAction;
            //        var editor = action.GetEditor(editorProvider);
            //        if (editor != null)
            //        {
            //            action.RowStatue = ActionStatue.New;
            //            editor.Action = action;
            //            editorProvider.ShowEditAction(editor);
            //        }
            //    }              
            //}
        }


    }
}
