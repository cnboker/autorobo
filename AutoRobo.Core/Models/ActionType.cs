using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Models
{
    /// <summary>
    /// 活动类型
    /// </summary>
    public enum StatementType
    {
        /// <summary>
        /// 执行入口
        /// </summary>
        Main,
        /// <summary>
        /// 过程
        /// </summary>
        Sub,
        /// <summary>
        /// 变量
        /// </summary>
        Variable,
        /// <summary>
        /// 模块
        /// </summary>
        Module
    }
}
