using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Models
{
    public class ActionXmlSet 
    {
        /// <summary>
        /// 是否加载账户变量信息，离线状态不加载， 在线状态需要加载
        /// </summary>
        public bool IsLoadIdentiy { get; set; }
        public string StartUrl { get; set; }
        /// <summary>
        /// 活动集脚本
        /// </summary>
        public string XmlAction{ get; set; }
        /// <summary>
        /// 活动集Schema
        /// </summary>
        public ActionSchemaSet SchemaSet { get; set; }     
        /// <summary>
        /// 活动集名称即脚本名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动集标识即脚本ID
        /// </summary>
        public object Key
        {
            get;
            set;
        }

        public ActionXmlSet() {
            SchemaSet = new ActionSchemaSet();
            IsLoadIdentiy = true;
        }
    }
}
