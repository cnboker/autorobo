using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{
    /// <summary>
    /// 表单定义
    /// </summary>
    public class Schema
    {
        public string Id { get; set; }
        public string WebsiteId { get; set; }
        public string DisplayName { get; set; }
        public string JsonObject { get; set; }
    }
}
