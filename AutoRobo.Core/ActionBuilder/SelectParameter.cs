using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.ActionBuilder
{
    public class SelectParameter : ActionParameter
    {
        /// <summary>
        /// 是否通过值匹配
        /// </summary>
        public bool ByValue { get; set; }
    }
}
