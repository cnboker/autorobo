using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Actions
{
    public enum StringMode
    {
        None,
        Javascript,
        /// <summary>
        /// 表达式
        /// </summary>
        Regex,
        Replace
    }
}
