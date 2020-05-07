using System;
using System.Linq;
using AutoRobo.Protocol;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using AutoRobo.Core.Models;
using Util;

namespace AutoRobo.Makers.Models.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private RecorderProtocol identity;
        private ActionXmlSet xmlset;

        public ActionRepository(RecorderProtocol identity)
        {
            this.identity = identity;
        }

        public string[] GetActionModulers()
        {
            return ServerApiInvoker.Get_SubModules(identity.WebsiteId)
                .Where(c => c.Title != xmlset.Name).Select(c => c.Title).ToArray();
        }

        public ActionXmlSet GetModulerModel(string modulerId)
        {
            return CreateActionXmlSet(ServerApiInvoker.Get_Script(identity.WebsiteId, modulerId));
        }

        public ActionXmlSet Read()
        {
            RecorderProtocol protocol = identity as RecorderProtocol;
            if (protocol == null) throw new ArgumentException();
            ScriptObject scriptObject = ServerApiInvoker.Get_Script(protocol.ScriptId);
            xmlset = CreateActionXmlSet(scriptObject);
            return xmlset;
        }

        public void Write(string actionxml)
        {
            ServerApiInvoker.Post_Script(identity.ScriptId, actionxml);
        }

        private ActionXmlSet CreateActionXmlSet(ScriptObject scriptObject)
        {
            var actionXmlSet = new Core.Models.ActionXmlSet()
            {
                Key = scriptObject.WebSiteId,
                Name = scriptObject.Title,
                XmlAction = scriptObject.Script,
                StartUrl = scriptObject.BeginUrl
            };

            if (!string.IsNullOrEmpty(scriptObject.ModelId) && scriptObject.ModelId != "0")
            {
                Schema schema = ServerApiInvoker.GetSchema(scriptObject.ModelId);
                if (schema == null)
                {
                    throw new ApplicationException("对应数据表单不存在，请重新配置表单模型");
                }
                actionXmlSet.SchemaSet.Schema = schema.JsonObject;
                actionXmlSet.SchemaSet.Name = schema.DisplayName;
            }
            return actionXmlSet;
        }


        public void New(string projectName)
        {
            
        }

      
    }
}
