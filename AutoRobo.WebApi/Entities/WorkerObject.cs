using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{
    [Serializable]
    public class WorkerObject : WorkObjectBase
    {        
        /// <summary>
        /// 虚拟账户ID
        /// </summary>
        public string MockId { get; set; }
        /// <summary>
        /// 结果信息实体ID， 该指是通过WEB创建Result结果表实体创建的
        /// </summary>
        public string ResultId { get; set; }   
    }
}
