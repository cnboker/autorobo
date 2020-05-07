using System;
using System.Collections.Generic;
using System.Text;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core.Models
{
    /// <summary>
    /// 变量数据更新参数
    /// </summary>
    public class VariableDataArgs : EventArgs
   {              
        /// <summary>
        /// 变量包含数据
        /// </summary>
        public object DataSource { get; set; }
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VariableName { get; set; }
    }

}
