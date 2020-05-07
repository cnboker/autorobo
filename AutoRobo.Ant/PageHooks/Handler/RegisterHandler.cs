using AutoRobo.ClientLib.PageHooks.Models;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.ClientLib.PageServices;
using AutoRobo.Core;
using AutoRobo.Core.Models;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    /// <summary>
    /// 展示注册view, 执行注册脚本
    /// </summary>
    public class RegisterHandler : IRequestProcessor
    {
        log4net.ILog logger = log4net.LogManager.GetLogger("RegisterHandler");
        FakeHttpContext context = null;
        public virtual ICustomIdentity Identity { get { return ServiceLocator.Instance.GetService<ICustomIdentity>(); } }
    
        /// <summary>
        /// 执行注册操作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="expando"></param>
        /// <returns></returns>
        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {            
            this.context = context;
            context.EnableTracing = false;
            ScriptRepository rep = new ScriptRepository(context.PageUrl);
            rep.ReadScriptType = ScriptTypeEnum.Register;
            var model = new ModelSet(rep);
            
            var currentWorker = new ActionRunnable(model);
            currentWorker.Run(null, true);

            return new RegisterResultView() { Model = expando.GetString("Model")};
        }
            
    }
}
