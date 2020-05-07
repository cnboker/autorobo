using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRobo.Protocol
{
    public enum ProtocolTypeEnum
    {
        Recorder = 1,
        Register,
        Thread,
        /// <summary>
        /// 搜索引擎关键字优化
        /// </summary>
        KeywordPlan,
        /// <summary>
        /// 直接运行脚本
        /// </summary>
        RunnerScript
    }
}
