using System.Collections.Generic;
using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.Core.ActionBuilder;
using AutoRobo.Core.Actions;
using AutoRobo.Core.Models;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using mshtml;
using Util;
using WatiN.Core;
using Newtonsoft.Json;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    public class MatchResultHandler : IRequestProcessor
    {
        protected log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MatchResultHandler));
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
        private MyBrowser browser = null;
        private List<IHTMLElement> activeElements = new List<IHTMLElement>();
        private string scriptId;

        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            browser = context.CoreBrowser;            
            ScriptObject so = JsonConvert.DeserializeObject<ScriptObject>(expando.GetString("Model"));
            scriptId = so.Id;
            string modelId = so.ModelId;
            //纯脚本执行，没有关联数据模型
            if (string.IsNullOrEmpty(so.ModelId) || modelId == "0")
            {
                Publish(modelId, scriptId, "0");
            }
            else {
                List<SchemaObject> source = ServerApiInvoker.Get_shcema_objects(Identity.UserId, scriptId);
                if (source.Count == 0)
                {
                    context.GoTo(StringHelper.Domain + "/portal/thread/create/" + modelId, "_blank");
                }
                else
                {
                    SchemaObjectSelect selectDialog = new SchemaObjectSelect(modelId, source);
                    selectDialog.Context = context;
                    string schemaObjectId = "0";
                    bool appendLink = false;
                    if (selectDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        schemaObjectId = selectDialog.SchemaObjectId;
                        appendLink = selectDialog.AppendOutLink;
                    }
                    if (schemaObjectId == "0") return new EmptyResult();

                    if (Identity.MockUser == null)
                    {
                        System.Windows.Forms.MessageBox.Show("请先登录");
                    }
                    else
                    {
                        Publish(modelId, scriptId, schemaObjectId);
                    }
                }
            }                 
            return new EmptyResult();
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="schemaId"></param>
        /// <param name="scriptId"></param>
        /// <param name="schemaObjectId"></param>
        private void Publish(string schemaId, string scriptId, string schemaObjectId)
        {           
            browser.WBLButtonDown -= new csExWB.HTMLMouseEventHandler(webBrowser_WBLButtonDown);
            browser.WBLButtonDown += new csExWB.HTMLMouseEventHandler(webBrowser_WBLButtonDown);

            ThreadRepository rep = new ThreadRepository(schemaId, scriptId, schemaObjectId);
            var model = new ModelSet(rep);
            
            var currentWorker = new ActionRunnable(model);
            currentWorker.Run(WaitComplete, true);

         
        }

        /// <summary>
        /// 执行用户手动调整的脚本
        /// </summary>
        /// <param name="result"></param>
        private void WaitComplete(AsyncResult result) {
            browser.WBLButtonDown -= new csExWB.HTMLMouseEventHandler(webBrowser_WBLButtonDown);
            string customScript = ServerApiInvoker.Get_record_mark(Identity.UserId, scriptId);
            if (!string.IsNullOrEmpty(customScript) && customScript.Length > 10) {                
                LocalScriptRepository rep = new LocalScriptRepository(customScript);
                var model = new ModelSet(rep);
                
                var currentWorker = new ActionRunnable(model);
                currentWorker.Run(null, true);
                     
            }
        }        

        void webBrowser_WBLButtonDown(object sender, csExWB.HTMLMouseEventArgs e)
        {
            IHTMLElement el = e.SrcElement as mshtml.IHTMLElement;
            var attr = el.getAttribute("__recordmark", 0);       
            if (attr != null && attr.ToString() == "1")
            {
                if (!string.IsNullOrEmpty(scriptId))
                {
                    Write(activeElements);                   
                }
            }
            else
            {
                if (!activeElements.Contains(el))
                {
                    activeElements.Add(el);
                }
            }
        }

        private void Write(List<IHTMLElement> activeElements)
        {
            if (activeElements.Count == 0) return;
            IAppContext context = AppContext.Current;
            ModelSet model = new ModelSet(new CustomScriptWriteRepository(Identity.UserId, scriptId));
            foreach (var o in activeElements)
            {

                var type = ActionElementBase.TagStringToElementType(o);
                switch (type)
                {
                    case ElementTypes.TextField:
                        model.AddAction(context, ActionEnum.ActionTypeText.ToString(),
                            new ActionTypingParameter()
                            {
                                Element = o,
                                XPathFinder = false                               
                            });
                        break;
                    case ElementTypes.RadioButton:
                        model.AddAction(context, ActionEnum.ActionRadio.ToString(), new ActionClickParameter()
                        {
                            Element = o,                            
                        });
                        break;
                    case ElementTypes.CheckBox:
                        model.AddAction(context, ActionEnum.ActionCheckbox.ToString(), new ActionClickParameter()
                        {
                            Element = o,                            
                        });
                        break;
                    case ElementTypes.SelectList:
                        model.AddAction(context, ActionEnum.ActionSelectList.ToString(),
                        new Core.ActionBuilder.SelectParameter() { ByValue = true, Element = o });                        
                        break;
                    default:
                        break;
                }
            }
            activeElements.Clear();
            model.Save();

        }
       
    }
}
