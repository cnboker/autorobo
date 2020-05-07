using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Models;

namespace AutoRobo.ClientLib.PageHooks.Models
{
    public class LocalScriptRepository : IActionReadRepository
    {
        private string script;

        public LocalScriptRepository(string script) {
            this.script = script;
        }
        public ActionXmlSet Read()
        {
            return new Core.Models.ActionXmlSet()
            {
                XmlAction = script                
            };
        }
    }

}
