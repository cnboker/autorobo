
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Util;
using System.Web.UI;
using System.Web;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;



namespace AutoRobo.Protocol
{
    
    public class ProtocolObject
    {
        static private log4net.ILog logger = log4net.LogManager.GetLogger("protocolObject");

        public ProtocolTypeEnum ProtocolType { get; set; }

        public string CurrentUserId { get; set; }
       
        public bool IsAuthentication { get; set; }
        /// <summary>
        ///富文本是否增加自然链接
        /// </summary>
        public bool RichTextAppendNaturalLink { get; set; }
        /// <summary>
        /// _blank, _top
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 输入参数字符串， 格式为name,value;name1,value1字符串
        /// </summary>
        public string InputParameters 
        { 
            get 
            { 
                StringBuilder sb = new StringBuilder();
                foreach(var o in kvs){
                    sb.AppendFormat("{0},{1};",o.Key,o.Value);
                }
                if(sb.Length > 0){
                    return sb.ToString().Substring(0,sb.Length-1);
                }
      
                return sb.ToString();
            }
            set {
                string val = value;
                if (string.IsNullOrEmpty(val)) return;
                var arr = val.Split(";".ToCharArray());
                foreach (var o in arr) { 
                    var t = o.Split(",".ToCharArray());
                    kvs.Add(new KeyValuePair<string,string>(t[0], t[1]));
                }
            }
        }

        private List<KeyValuePair<string, string>> kvs = new List<KeyValuePair<string, string>>();

        
        public List<KeyValuePair<string, string>> GetDynamicParameters()
        {
            return kvs;
        }

        public virtual string ProtocolName {
            get {
                return "autoRobo";
            }
        }

        /// <summary>
        /// 添加动态输入参数，主要为WEB应用提供外部接口，可以将动态参数传递给脚本引擎执行
        /// 比如用户在WEB端选择城市，那就可以将城市的这个参数（city=深圳)通过这里传递进来给脚本引擎，脚本引擎
        /// 在通过这个参数名称(city)来绑定值进行操作
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParameter(string name, string value) {            
            foreach (var o in kvs) {
                if (o.Key == name) {
                    throw new ApplicationException(string.Format("参数{0}已经存在", name));
                }
            }
            kvs.Add(new KeyValuePair<string,string>(name, value));
        }

        protected bool SecurityCheck()
        {
            return true;
        }

        public virtual string GetProtocolUrl()
        {
            string json = SerializeObject();
            json = RijndaelHelper.Encrypt(json);
 
            return string.Format("{0}://{1}", ProtocolName, json);
        }

        protected string SerializeObject()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ProtocolObject DeserializeObject(string encryptJson)
        {
            int index = encryptJson.IndexOf(":") + 3;
            string json = encryptJson.Substring(index);
            //json = System.Web.HttpUtility.UrlDecode(json);
            try
            {
                json = RijndaelHelper.Decrypt(json);
            }
            catch {
                if (json[json.Length - 1] == '/')
                {
                    json = json.Substring(0, json.Length - 1);
                }
                json = RijndaelHelper.Decrypt(json);
            }
            logger.Info(json);
            JObject jso = JsonConvert.DeserializeObject(json) as JObject;

            return ProtocolObjectConvertor.ConvertTo(jso);
        }


    }
}
