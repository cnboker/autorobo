
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoRobo.Protocol
{
    public class ProtocolObjectConvertor
    {
      static  private ProtocolTypeEnum GetProtocolType(object jObject)
        {
            return (ProtocolTypeEnum)Enum.Parse(typeof(ProtocolTypeEnum), jObject.ToString(), true);
        }

      static public ProtocolObject ConvertTo(JObject jso)
      {
          ProtocolObject p = null;
          ProtocolTypeEnum type = GetProtocolType(jso["ProtocolType"]);
          if (type == ProtocolTypeEnum.Thread)
          {
              p = new ThreadProtocol();
              ((ThreadProtocol)p).MockUserId = jso["MockUserId"].ToString();
              //if (jso["PublishUrl"] != null)
              //{
              //    ((ThreadProtocol)p).PublishUrl = jso["PublishUrl"].ToString();
              //}
          }
          else if (type == ProtocolTypeEnum.Register)
          {
              p = new RegisterProtocol();
              ((RegisterProtocol)p).MockUserId = jso["MockUserId"].ToString();
          }
          else if (type == ProtocolTypeEnum.Recorder)
          {
              p = new RecorderProtocol();
              ((RecorderProtocol)p).ScriptId = jso["ScriptId"].ToString();
              ((RecorderProtocol)p).WebsiteId = jso["WebsiteId"].ToString();
          }
          else if (type == ProtocolTypeEnum.KeywordPlan)
          {
              p = new KeywordPlanProtocol();
          }
          else if (type == ProtocolTypeEnum.RunnerScript) {
              p = new ScriptRunnerProtocol();
              ((ScriptRunnerProtocol)p).ScriptId = jso["ScriptId"].ToString();
          }
          else
          {
              p = new ProtocolObject();
          }
          p.CurrentUserId = jso["CurrentUserId"].ToString();
          p.IsAuthentication = Convert.ToBoolean(jso["IsAuthentication"]);
         // if (jso.ContainsKey("Target"))
          {
              p.Target = jso["Target"] == null ? "_blank" : jso["Target"].ToString();
          }
          p.ProtocolType = type;
         // if (jso.ContainsKey("InputParameters"))
          //{
          //    p.InputParameters = jso["InputParameters"].ToString();
          //}
          return p;
      }

    }
}