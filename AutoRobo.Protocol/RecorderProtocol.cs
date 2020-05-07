
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace AutoRobo.Protocol
{
  
    public class RecorderProtocol : ProtocolObject
    {       
        public string ScriptId { get; set; }      
        public string WebsiteId { get; set; }
    }
}
