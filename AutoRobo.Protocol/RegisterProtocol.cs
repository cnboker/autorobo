
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AutoRobo.Protocol
{
    
    public class RegisterProtocol : MockUserProtocol
    {
        /// <summary>
        /// 执行脚本ID
        /// </summary>
        public string ScriptId { get; set; }
    }
}
