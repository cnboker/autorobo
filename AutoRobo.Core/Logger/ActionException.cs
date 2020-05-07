using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Logger
{
    public class ActionException
    {
        /// <summary>
        /// 活动标签
        /// </summary>
        public string ActionLable { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public string ActionType { get; set; }
        /// <summary>
        /// 活动标识
        /// </summary>
        public string ActionId { get; set; }
        /// <summary>
        /// 活动在活动组索引
        /// </summary>
        public int ActionIndex { get; set; }
        /// <summary>
        /// 活动异常信息
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// 脚本标识
        /// </summary>
        public string ScriptId { get; set; }
    }
}
