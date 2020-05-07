using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.ClientLib.PageHooks.Views;
using AutoRobo.Core;
using WatiN.Core;

namespace AutoRobo.ClientLib.PageHooks.Handler
{
    public class LogRequestHandler : IRequestHandler, IRequestProcessor
    {
        log4net.ILog logger = log4net.LogManager.GetLogger("LogRequestHandler");

        public IResult HandleRequest(IRequest req)
        {           
            string script = @"
               var logger = {
                     info : function(message){
                        //alert(message);
                        window.external.Call('LogRequestHandler',{'level':'Info', 'log':message});     
                    },
                    error : function(message){
                        window.external.Call('LogRequestHandler',{'level':'fatal', 'log':message});     
                    }
                }
            ";
            req.HttpContext.Browser.InjectScript(script);    
            return new EmptyResult();
        }

        public IResult Process(FakeHttpContext context, System.Runtime.InteropServices.Expando.IExpando expando)
        {
            string level = expando.GetString("level");
            string log = expando.GetString("log");
            switch (level) {
                case "Info":
                    logger.Info(log);
                    break;
                case "fatal":
                    logger.Fatal(log);
                    break;
                default:
                    break;
            }
            System.Diagnostics.Debug.WriteLine(log);
            return new EmptyResult();
        }
    }
}
