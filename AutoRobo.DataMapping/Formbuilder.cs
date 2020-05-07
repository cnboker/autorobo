using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace AutoRobo.DataMapping
{
  	 /**
	* @abstract This class is the server-side component that handles interaction with
	* the jquery formbuilder plugin.
	* @package jquery.Formbuilder*/
    public class Formbuilder
    {
        public Formbuilder() { 
        
        }

        public DataMap ToDataMap(string forms) {
            NameValueCollection kv = Parse(forms);
            DataMap map = new DataMap();
            Dictionary<string, DataMapField> dict = new Dictionary<string, DataMapField>();
            for (int i = 0; i < kv.Count; i++) {
                var key = kv.Keys[i];
                var dim = key.Substring(0, key.IndexOf("."));
               
                var _token = key.Substring(key.IndexOf(".") + 1);
                DataMapField field = null;
                if (dict.ContainsKey(dim))
                {
                     field = dict[dim];                    
                }
                else {
                    field = CreateDataMapField(_token, kv[key]);
                    dict.Add(dim, field);
                }
                field.AddAttribute(_token, kv[i]);
            }
            foreach (var o in dict.Values) {
                map.Fields.Add(o);
            }
            return map;
        }

        private DataMapField CreateDataMapField(string key, string value)
        {
            DataMapField field = null;
            if (key != "cssClass") return null;
            switch (value)
            { 
                case "input_text":
                    field = new InputDataMapField();
                    break;
                case "checkbox":
                    field = new CheckBoxDataMapField();
                    break;
                case "select":
                    field = new SelectDataMapField();
                    break;
                case "radio":
                    field = new RadioDataMapField();
                    break;
                case "textarea":
                    field = new TextAreaDataMapField();
                    break;
                case "input_file":
                    field = new FileInputDataMapField();
                    break;
                default:
                    break;
            }
            return field;
        }

        private NameValueCollection Parse(string forms)
        {
            string[] arr = forms.Split("&".ToCharArray());
            NameValueCollection kv = new NameValueCollection();
            foreach (var o in arr) {
                var _key = o.Split("=".ToCharArray());
                Console.Write(_key[0]);
                if (string.IsNullOrEmpty(_key[0])) continue;
                var _t = _key[0].Replace("][", ".").Replace("[", ".").Replace("]", "");
                kv.Add(_t.Substring(_t.IndexOf(".") + 1), _key[1]);
            }
            return kv;
        }

     
    }

}
