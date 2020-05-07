using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRobo.Core.Actions;

namespace AutoRobo.Core
{
    public class AsyncResult
    {
        /// <summary>
        /// 是否已经处理完成
        /// </summary>
        public bool IsComplete;
        public StatusIndicators Status;
        /// <summary>
        /// 回调过程
        /// </summary>
        public FunCallback CallBack;
        /// <summary>
        /// 如果有异常，包含异常信息
        /// </summary>
        public Exception Exception;
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data;
    }

}
