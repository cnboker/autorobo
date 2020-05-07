
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutoRobo.Protocol
{
    
    public class ThreadProtocol : MockUserProtocol
    {
        /// <summary>
        /// 是否记录用户运行脚本前对脚本的活动值的调整
        /// </summary>
        public bool RecordMark { get; set; }
        /// <summary>
        /// 是否为手动提交信息
        /// </summary>
        public bool ManualSubmit { get; set; }
        ///// <summary>
        ///// 发帖内容
        ///// </summary>
        public string SchemaObjectId { get; set; }
        public string ScriptId { get; set; }
    }
}
