using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Data;

namespace AutoRobo.DataMapping
{

    public class DataMap : IMapAttribute
    {
        public string Name
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }
        public bool DataAvailable
        {
            get;
            set;
        }

        
        public List<DataMapField> Fields
        {
            get;
            set;
        }

        public DataMap() {
            Fields = new List<DataMapField>();
        }

        public DataMap(string name) {
            this.Name = name;
            Fields = new List<DataMapField>();
        }

        /// <summary>
        /// 转化为表格数据
        /// </summary>
        /// <returns></returns>
        public DataTable ToDataTable() {
            DataTable table = new DataTable(Name);
            foreach (DataMapField field in Fields) {
                table.Columns.Add(field.DisplayName);
            }
            DataRow dr = table.NewRow();
            bool hasData = false;
            foreach (DataMapField field in Fields)
            {
                hasData |= !string.IsNullOrEmpty(field.Value);
                dr[field.DisplayName] = field.Value;
            }
            if (hasData)
            {
                table.Rows.Add(dr);
            }
            return table;
        }
        public string ToJson() {
            return JsonConvert.SerializeObject(Fields);            
        }

        public void Init(string json)
        {
            if (string.IsNullOrEmpty(json)) return;
            JsonDataMapField[] df = JsonConvert.DeserializeObject<JsonDataMapField[]>(json);
            foreach (var o in df) {
                Fields.Add(DataMapFieldConvertor.Convert(o));
            }
        }

        public string DesignHtmlRender()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (var o in Fields) {
                o.DesignHtmlRender();
                sb.Append(o.GetDesignHtml());
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// 为字段赋值
        /// </summary>
        /// <param name="val"></param>
        public void UpdateValues(string val) {
            if (string.IsNullOrEmpty(val)) return;
            var arr = val.Split("&".ToCharArray());
            foreach (var kv in arr) {
                if (string.IsNullOrEmpty(kv)) continue;
                var kvs = kv.Split("=".ToCharArray());
                SetValue(kvs[0], kvs[1]);
            }
        }

        private void SetValue(string key, string val) {
            foreach (var o in Fields) {
                if (o.DisplayName == System.Web.HttpUtility.UrlDecode(key)) {
                    o.Value = val;
                    break;
                }
            }
        }
        /// <summary>
        /// 生成html
        /// </summary>
        /// <returns></returns>
        public string TableHtmlRender()
        {            
            StringBuilder sb = new StringBuilder();            
            foreach (var o in Fields)
            {
                o.TableHtmlRender();
                sb.Append(o.GetTableHtml());
            }            
            return sb.ToString();
        }

        public void Merge(IMapAttribute map)
        {
            var newFields = map.Fields;
            foreach (var o in newFields) {
                if (!Contain(o))
                {
                    Fields.Add(o);
                }
                else {
                    FieldMerge(map, o);
                }               
            }
        }

        private void FieldMerge(IMapAttribute map, DataMapField newField)
        {
            foreach (var o in Fields)
            {
                if (o.DisplayName == newField.DisplayName)
                {
                    if (!string.IsNullOrEmpty(newField.Value)) {
                        o.Value = newField.Value;
                    }
                }
            }
        }
        private bool Contain(DataMapField field) {
            foreach (var o in Fields) {
                if (o.DisplayName == field.DisplayName) {
                    return true;
                }
            }
            return false;
        }

    
        public string Get(string displayName)
        {
            foreach (var o in Fields) {
                if (o.DisplayName == displayName) {
                    return o.Value;
                }
            }
            return "";
        }

        public void Set(string displayName, string val)
        {
            foreach (var o in Fields) {
                if (o.DisplayName == displayName) {
                    o.Value = val;
                    break;
                }
            }
        }

   
    }
}
