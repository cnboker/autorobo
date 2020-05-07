using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Models
{
    public class ActionSchemaSet
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string SchemaValue { get; set; }

        public ActionSchemaSet() { }
        public ActionSchemaSet(string name, string schema) {
            this.Name = name;
            this.Schema = schema;
        }

        public ActionSchemaSet(string name, string schema, string value)
        {
            this.Name = name;
            this.Schema = schema;
            this.SchemaValue = value;
        }
    }
}
