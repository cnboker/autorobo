using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    public enum ActionStatue
    {
        /// <summary>
        /// 开始状态
        /// </summary>
        Origin,
        /// <summary>
        /// 新增Action
        /// </summary>
        New,
        /// <summary>
        /// 编辑Action
        /// </summary>
        Edit,
        /// <summary>
        /// 脏数据
        /// </summary>
        Dirty
    }
}
