using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Models;
using AutoRobo.WebApi.Entities;
using AutoRobo.WebApi;

namespace AutoRobo.ClientLib.PageHooks.Models
{
    public class ThreadRepository : IActionReadRepository
    {
        private string schemaId;
        private string scriptId;
        private string schemaObjectId;

        public ThreadRepository(string schemaId, string scriptId, string schemaObjectId) {
            this.schemaId = schemaId;
            this.scriptId = scriptId;
            this.schemaObjectId = schemaObjectId;
        }
 
        public ActionXmlSet Read()
        {         
            ScriptObject script = ServerApiInvoker.Get_Script(scriptId);
        
            ActionXmlSet xmlset = new ActionXmlSet()
            {
                XmlAction = script.Script,
                SchemaSet = new ActionSchemaSet()
            };

            Schema schema = null;
            if (Convert.ToInt32(schemaId) > 0)
            {
                schema = ServerApiInvoker.GetSchema(schemaId);
            }

            SchemaObject sco = null;
            if (Convert.ToInt32(schemaObjectId) > 0)
            {
                sco = ServerApiInvoker.GetSchemaObject(schemaObjectId);
            }
            if (schema != null)
            {
                xmlset.SchemaSet = new ActionSchemaSet(schema.DisplayName, schema.JsonObject, sco.JsonValue);
            }
            return xmlset;
        }

    }
}
