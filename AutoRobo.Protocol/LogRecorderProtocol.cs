using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutoRobo.Protocol
{
    /// <summary>
    /// 日志分析协议， 通过该对象可以指定录制脚本那一行有问题， recorder可以通过ActiveIndex指出错误行
    /// </summary>
  
    public class LogRecorderProtocol : RecorderProtocol
    {
         
         public int ActiveIndex { get; set; }
          
    }
}
