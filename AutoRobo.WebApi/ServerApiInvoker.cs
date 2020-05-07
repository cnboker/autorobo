using System.Collections.Generic;
using System.Configuration;
using AutoRobo.WebApi.Entities;
using Newtonsoft.Json;
using Util;


namespace AutoRobo.WebApi
{  
    public class ServerApiInvoker
    {
        static private log4net.ILog logger = log4net.LogManager.GetLogger("ServerApiInvoker");

        static public string APIRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["root"] + "/jsonApi/";
            }
        }

        static public string Domain
        {
            get
            {
                return ConfigurationManager.AppSettings["root"] + "/";
            }
        }

        
        static public Schema GetSchema(string schemaId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/schemaApi/get_schema/" + schemaId;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Schema>(json);
        }

        static public SchemaObject GetSchemaObject(string schemaobjectId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/schemaApi/get_schema_value/" + schemaobjectId;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<SchemaObject>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">script id</param>
        /// <param name="script"></param>
        static public void Post_Script(string id, string script) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_script";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("id", id);
            keyvalues.Add("script", System.Web.HttpUtility.UrlEncode(script));
            HttpRequestWapper.Post_Data(url, keyvalues);         
        }

        static public void Post_register_result(string mockid, string resultId, ResultStatue statue)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_register_result";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("id", resultId);
            keyvalues.Add("mockid", mockid);    
            keyvalues.Add("statue", ((int)statue).ToString());
            HttpRequestWapper.Post_Data(url, keyvalues);       
        }  

        /// <summary>
        /// 是否还能继续群注册
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool IsRegister(string userid)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/is_register/" + userid;
            string json = HttpRequestWapper.GetData(url);
            return json.ToLower() == "true";
        }

        /// <summary>
        /// 是否还能继续群发贴
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool IsThread(string userid)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/is_thread/" + userid;
            string json = HttpRequestWapper.GetData(url);
            return json.ToLower() == "true";
        }

        /// <summary>
        /// 获取虚拟用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public MockUser Get_MockUser(string id) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_mockuser/" + id;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<MockUser>(json);
        }

        /// <summary>
        /// 获取虚拟用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public List<WebSite> Get_TopWebsite()
        {
            var url = APIRoot + "get_top_websites";
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<List<WebSite>>(json);
        }

        /// <summary>
        /// 切换虚拟帐号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mockUserId"></param>
        static public void Shift_MockUserAccount(string newmockUserId)
        {
            var url = APIRoot + "shift_account";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();            
            keyvalues.Add("newmockUserId", newmockUserId);           
            HttpRequestWapper.Post_Data(url, keyvalues);
        }
        /// <summary>
        /// 获取虚拟用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public List<MockUser> Get_MockUsers(string userId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_mockusers/" + userId;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<List<MockUser>>(json);
        }

        static public ScriptObject Get_Script(string websiteId,string title)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_script_byTitle/" + websiteId + "/" + title;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<ScriptObject>(json);
        }

        static public ScriptObject Get_Script(string id)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_script/" + id;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<ScriptObject>(json);
        }

        static public ScriptObject Get_ScriptByNo(string no)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_scriptByNo/" + no;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<ScriptObject>(json);
        }
        /// <summary>
        /// 获取网站子模块脚本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public List<ScriptObject> Get_SubModules(string websiteId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_subModules/" + websiteId;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<List<ScriptObject>>(json);
        }

        /// <summary>
        /// 获取需要优化的关键字列表
        /// </summary>
        /// <returns></returns>
        static public List<KeywordPlan> Get_KeywordPlan(string userid) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_keywordPlan/" + userid;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json)) return null;
            return JsonConvert.DeserializeObject<List<KeywordPlan>>(json);        
        }

        /// <summary>
        /// 更新优化后关键字老化时间
        /// </summary>
        /// <param name="id"></param>
        static public void Post_keywordPlan(string userid, string engineKeyId) { 
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_keyword_plan/" + userid + "/" + engineKeyId;
            logger.Info("Post_keywordPlan url:" + url);
            HttpRequestWapper.Post_Data(url, null);               
        }

        public static void Post_thread(string ticket, string mockUserId, string scriptId, string schemaObjectId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/tasks/ThreadResult";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("mockUserId", mockUserId);
            keyvalues.Add("scriptId", scriptId);
            keyvalues.Add("schemaObjectId", schemaObjectId);
            HttpRequestWapper.Post_Data(url, keyvalues, ticket);
        }

        public static void Post_thread_result(string mockid, string resultId, ResultStatue resultStatue)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_thread_result";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("mockId", mockid);
            keyvalues.Add("resultId", resultId);           
            keyvalues.Add("statue", ((int)resultStatue).ToString());
            HttpRequestWapper.Post_Data(url, keyvalues);       
        }

        /// <summary>
        /// 通过虚拟账户获取执行脚本
        /// </summary>
        /// <param name="mockid"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static List<RegisterObject> Get_Register_Worker_Object(string mockid)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_register_script/" +
              mockid;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<RegisterObject>();
            }
            return JsonConvert.DeserializeObject<List<RegisterObject>>(json);
        }

        public static List<ThreadObject> Get_Thread_Worker_Object(string mockid)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_thread_script/" +
             mockid;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ThreadObject>();
            }
            return JsonConvert.DeserializeObject<List<ThreadObject>>(json);
        }

        public static ThreadObject Get_Thread_Worker_Object(string mockid, string scriptId, string schemaObjectId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_thread_script_1/" +
                string.Format("{0}/{1}/{2}", mockid, scriptId, schemaObjectId);
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ThreadObject>(json);
        }
        /// <summary>
        /// 通过虚拟账户和目录列表获取执行脚本
        /// </summary>
        /// <param name="mockid"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static List<ScriptObject> Get_register_script_by_categories(string mockid, string categories)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_register_script_by_categories/" +
              mockid + "/" + categories;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ScriptObject>();
            }
            return JsonConvert.DeserializeObject<List<ScriptObject>>(json);
        }

        /// <summary>
        /// 通过虚拟账户和站点列表获取执行脚本
        /// </summary>
        /// <param name="mockid"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static List<ScriptObject> Get_register_script_by_websites(string mockid, string websites)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_register_script_by_websites/" +
              mockid + "/" + websites;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ScriptObject>();
            }
            return JsonConvert.DeserializeObject<List<ScriptObject>>(json);
        }

        public static List<ScriptObject> Get_thread_script_by_websites(string mockid, string schemaObjectId, string websites)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_thread_script_by_websites/" +
              mockid + "/" + schemaObjectId + "/" + websites;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ScriptObject>();
            }
            return JsonConvert.DeserializeObject<List<ScriptObject>>(json);
        }

        public static List<ScriptObject> Get_thread_script_by_categories(string mockid, string schemaObjectId, string categories)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_thread_script_by_categories/" +
              mockid + "/" + schemaObjectId + "/" + categories;
            string json = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(json))
            {
                return new List<ScriptObject>();
            }
            return JsonConvert.DeserializeObject<List<ScriptObject>>(json);
        }

        public static List<SchemaObject> Get_shcema_objects(string userId, string scriptId) {
           string json = HttpRequestWapper.GetData(ServerApiInvoker.APIRoot + string.Format("get_shcema_objects/{0}/{1}", userId, scriptId));
           if (string.IsNullOrEmpty(json))
           {
               return new List<SchemaObject>();
           }
           return JsonConvert.DeserializeObject<List<SchemaObject>>(json);
        }

        /// <summary>
        /// 客户端异常数据报告
        /// </summary>
        /// <param name="scriptId">脚本ID</param>
        /// <param name="actionId">活动ID</param>
        /// <param name="message">异常信息</param>
        /// <param name="actionIndex">活动行索引</param>
        /// <param name="actionType">活动类型</param>
        /// <param name="labelText">活动标签</param>
        public static void ExceptionReport(string scriptId, string actionId, string message, string actionIndex, 
            string actionType, string labelText) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_exception";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("scriptId", scriptId);
            keyvalues.Add("actionId", actionId);
            keyvalues.Add("message", message);
            keyvalues.Add("actionIndex", actionIndex);
            keyvalues.Add("actionType", actionType);
            keyvalues.Add("labelText", string.IsNullOrEmpty(labelText) ? "" : labelText);
            HttpRequestWapper.Post_Data(url, keyvalues);
        }


        /// <summary>
        /// 提交用户发帖修改痕迹
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="url">发布页面地址</param>
        /// <param name="script">用户操作痕迹生成的脚本内容</param>
        public static void PostRecordMark(string userid, string scriptId, string script)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_record_mark";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("scriptId", scriptId);
            keyvalues.Add("userId", userid.ToString());
            keyvalues.Add("script", System.Web.HttpUtility.UrlEncode(script));
           
            HttpRequestWapper.Post_Data(url, keyvalues);
        }

        /// <summary>
        /// 获取用户发帖修改痕迹
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="scriptId">同一个脚本保存上一次操作痕迹</param>
        /// <returns></returns>
        public static string Get_record_mark(string userid, string scriptId)
        {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_record_mark/" + userid.ToString() + "/" + scriptId;
            logger.Info("Get_record_mark url :" + url);  
            return HttpRequestWapper.GetData(url);         
        }

        public static string Sql_get_value(string sql) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/sql_get_value/";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("sql", System.Web.HttpUtility.UrlEncode(sql));
            return HttpRequestWapper.Post(url, keyvalues);  
        }

        public static string Get_UserId() {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/get_userid";            
            return HttpRequestWapper.GetData(url);      
        }

        public static bool HasTask(string website) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/has_task?url=" + website;
            
            string val = HttpRequestWapper.GetData(url);
            logger.Info("hastask return result:" + val);
            return val != "";  
        }

        /// <summary>
        /// 通过URL获取发布类型脚本类型，包含近似度数据，不包含script内容信息
        /// </summary>
        /// <param name="domain">二级域名</param>
        /// <returns></returns>
        public static List<ScriptObject> GetScriptObjectByUrl(string domain)
        {
            string json = HttpRequestWapper.GetData(ServerApiInvoker.APIRoot + string.Format("get_script_by_domain?domain={0}", domain));
            return JsonConvert.DeserializeObject <List<ScriptObject>>(json);
        }
        /// <summary>
        /// 更新近似度字符串
        /// </summary>
        /// <param name="scriptId"></param>
        /// <param name="similarity"></param>
        public static void PostSimilarity(string scriptId, string similarity) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_similarity/";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("scriptId", scriptId);
            keyvalues.Add("similarity", similarity);
            HttpRequestWapper.Post_Data(url, keyvalues);  
        }

        public static void PostEmailRegisterStatue(string mockuserId, bool isActive) {
            var url = ConfigurationManager.AppSettings["root"] + "/jsonApi/post_email_statue/";
            Dictionary<string, string> keyvalues = new Dictionary<string, string>();
            keyvalues.Add("id", mockuserId);
            keyvalues.Add("IsActiveMail", isActive ? "1" : "0");
            HttpRequestWapper.Post_Data(url, keyvalues); 
            
        }
    }

}
