using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    /// <summary>
    /// status indicators for an action
    /// </summary>
    public enum StatusIndicators
    {
        /// <summary>
        /// 初始状态
        /// </summary>
        Default,
        /// <summary>
        /// 操作正常
        /// </summary>
        StepContinue,
        /// <summary>
        /// 操作跳出当前ModelSet上下文，进入父级上下文
        /// </summary>
        StepBreak,
        /// <summary>
        /// 操作停止运行
        /// </summary>
        StepFailure,
        /// <summary>
        /// 退出运行
        /// </summary>
        Exit,    
    }
}
