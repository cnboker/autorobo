using System;
using System.Collections.Generic;
using System.Text;
using Util;
using Newtonsoft.Json;


namespace AutoRobo.DataMapping
{

    public class DataMapFieldConvertor 
    {
        static private DataMapField GetDataMapField(JsonDataMapField o)
        {
            string type = o.CssClass;
            DataMapField p = null;
            if (type == "input_text")
            {
                p = new InputDataMapField();
            }
            else if (type == "checkbox")
            {
                p = new CheckBoxDataMapField();
                ((CheckBoxDataMapField)p).Options.AddRange(o.Options);
            }
            else if (type == "select")
            {
                p = new SelectDataMapField();
                ((SelectDataMapField)p).Options.AddRange(o.Options);
            }
            else if (type == "radio")
            {
                p = new RadioDataMapField();
                ((RadioDataMapField)p).Options.AddRange(o.Options);
            }
            else if (type == "textarea")
            {
                p = new TextAreaDataMapField();
            }
            else
            {
                p = new DataMapField();
            }
            p.CssClass = o.CssClass;
            p.Value = o.Value;
            p.Required = o.Required;
            p.DisplayName = o.DisplayName;
            return p;        
        }

        internal static DataMapField Convert(JsonDataMapField o)
        {
            return GetDataMapField(o);
        }
    }

}
