using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.WebApi.Entities
{
    [Serializable]
    public class WorkObjectBase
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 录制脚本ID
        /// </summary>
        public string ScriptId { get; set; }
        /// <summary>
        /// 录制脚本内容
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// 实体当前状态
        /// </summary>
        public ResultStatue Statue { get; set; }
     
    }
}
