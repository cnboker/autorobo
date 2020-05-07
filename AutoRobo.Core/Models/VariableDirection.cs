using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Core.Models
{
    public enum VariableDirection
    {
        /// <summary>
        /// 输出变量
        /// </summary>
        Output = 0,
        /// <summary>
        /// 输入变量
        /// </summary>
        Input,      
        /// <summary>
        /// 输出输入变量，初始化会清除该类型数据
        /// </summary>
        InOut
    }
}
