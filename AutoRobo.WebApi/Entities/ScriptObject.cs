using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{
    /// <summary>
    /// 网站脚本信息
    /// </summary>
    public class ScriptObject
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string Title { get; set; }
        public string Script { get; set; }
        public string BeginUrl { get; set; }
        /// <summary>
        /// schemaId, 该脚本对应的数据表单
        /// </summary>
        public string ModelId { get; set; }
        /// <summary>
        /// 是否是部件
        /// </summary>
        public bool? Part { get; set; }
        /// <summary>
        /// 是否允许弹出提示框
        /// </summary>
        public bool? AlertAllow { get; set; }
        /// <summary>
        /// 页面识别类型
        /// </summary>
        public string PageRecognize { get; set; }
        /// <summary>
        /// 页面识别类型值
        /// </summary>
        public string Similarity { get; set; }
        /// <summary>
        /// 脚本类型
        /// </summary>
        public string ScriptType { get; set; }
    }
}
