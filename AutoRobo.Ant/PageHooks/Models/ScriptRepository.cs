using System;
using System.Linq;
using AutoRobo.Protocol;
using AutoRobo.WebApi;
using AutoRobo.WebApi.Entities;
using AutoRobo.Core.Models;
using Util;
using System.Web.Caching;
using System.Collections.Generic;

namespace AutoRobo.ClientLib.PageHooks.Models
{

    public class ScriptRepository : IActionReadRepository
    {
        private string  url;
        private ActionXmlSet xmlset;
        private List<ScriptObject> scripts = null;
        private ScriptObject currentScriptObject;
        /// <summary>
        /// 调用read方法之前需要先设置读取脚本类型
        /// </summary>
        public ScriptTypeEnum ReadScriptType { get; set; }

        public ScriptRepository(string url)
        {
            this.url = url;
        }   

        public ActionXmlSet Read()
        {
            if (scripts == null) {
                scripts = ServerApiInvoker.GetScriptObjectByUrl(url);
            }
            currentScriptObject = scripts.FirstOrDefault(c => c.ScriptType == ((int)ReadScriptType).ToString());
            xmlset = CreateActionXmlSet(currentScriptObject);
            return xmlset;
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
                actionXmlSet.SchemaSet = new ActionSchemaSet(schema.DisplayName, schema.JsonObject);               
            }
            return actionXmlSet;
        }

      
    }
}
