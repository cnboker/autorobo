using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{
    /// <summary>
    /// 表单填充数据
    /// </summary>
    public class SchemaObject
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SchemaId { get; set; }
        public string Title { get; set; }
        public string JsonValue { get; set; }
        //public DateTime CreateDate { get; set; }
        public bool? IsDefault { get; set; }
    }
}
