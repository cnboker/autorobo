using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core
{
    /// <summary>
    /// 活动过滤界别
    /// </summary>
    public enum ActionFliter
    {
        /// <summary>
        /// 鼠标点击label, div, element table 不增加CLICK事件
        /// </summary>
        High,

        Mediue,
        /// <summary>
        /// 鼠标点击label, div, element table 增加CLICK事件
        /// </summary>
        Lower
    }
}
