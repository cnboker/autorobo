using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoRobo.Core.Interface
{
    public interface IActionBlock
    {
        /// <summary>
        /// 包含有子活动列表的活动
        /// </summary>
        ActionList Actions { get; set; }
    }
}
