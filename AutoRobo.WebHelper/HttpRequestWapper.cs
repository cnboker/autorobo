using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;
using Util;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Util
{
    public class HttpRequestWapper
    {
        delegate string AsyncHttpWebRequestDelegate(string url);
        static log4net.ILog logger = log4net.LogManager.GetLogger("HttpRequestWapper");
        
        static string GetToken()
        {
            return HttpUtility.UrlEncode(RijndaelHelper.Encrypt("jdsd_#@a543jk*9" + DateTime.Now.Ticks.ToString()));
        }

        static string GetSafeUrl(string url) {
            var ch = url.IndexOf("?") > 0 ? '&' : '?';
            url += ch + "token=" + GetToken();
            return url;
        }

        /// <summary>
        /// 通用调用方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JArray GetJsonArray(string url)
        {
            string val = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(val)) return null;
            return JsonConvert.DeserializeObject(val) as JArray;
        }

        /// <summary>
        /// 通用调用方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JObject GetJsonObject(string url)
        {
            string val = HttpRequestWapper.GetData(url);
            if (string.IsNullOrEmpty(val)) return null;
            return JsonConvert.DeserializeObject(val) as JObject;
        }

        public static bool GetBoolean(string url, string ticket)
        {
            string val = HttpRequestWapper.GetData(url, ticket);
            return val.ToLower() == "true";
        }

        static public string Post(string url, Dictionary<string, string> keyvalues)
        {
            return Post(url, keyvalues, "");
        }

        static public string Post(string url, Dictionary<string, string> keyvalues, string ticket)
        {
            try
            {
                if (keyvalues == null)
                {
                    keyvalues = new Dictionary<string, string>();
                }
                url = GetSafeUrl(url);
                StringBuilder postString = new StringBuilder();
                foreach (var o in keyvalues)
                {
                    postString.AppendFormat("{0}={1}", o.Key, o.Value);
                    postString.Append("&");
                }
                if (postString.Length > 0)
                {
                    postString.Length = postString.Length - 1;
                }
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString.ToString());

                // set up request object
                HttpWebRequest request;
                try
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                    if (!string.IsNullOrEmpty(ticket))
                    {
                        request.Headers.Add("Cookie", FormsAuthentication.FormsCookieName + "=" + ticket);
                    }
                }
                catch (UriFormatException)
                {
                    request = null;
                }
                if (request == null)
                    throw new ApplicationException("Invalid URL: " + url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;

                // add post data to request
                Stream postStream = request.GetRequestStream();
                postStream.Write(postBytes, 0, postBytes.Length);
                postStream.Close();

                //Get response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //Output response to a string            
                String result = "";
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    return result;
                }
            }
            catch (Exception e1)
            {
                logger.Fatal(e1);
                return null;
            }
         
        }

        static public void Post_Data(string url, Dictionary<string, string> keyvalues)
        {
            Post_Data(url, keyvalues, "");
        }
        static public void Post_Data(string url, Dictionary<string, string> keyvalues, string ticket)
        {
            if (keyvalues == null) {
                keyvalues = new Dictionary<string, string>();
            }
            url = GetSafeUrl(url);
            new Thread(delegate()
           {
               try
               {
                   StringBuilder postString = new StringBuilder();
                   foreach (var o in keyvalues)
                   {
                       postString.AppendFormat("{0}={1}", o.Key, o.Value);
                       postString.Append("&");
                   }
                   if (postString.Length > 0)
                   {
                       postString.Length = postString.Length - 1;
                   }
                   byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postString.ToString());

                   // set up request object
                   HttpWebRequest request;
                   try
                   {
                       request = WebRequest.Create(url) as HttpWebRequest;
                       if (!string.IsNullOrEmpty(ticket))
                       {
                           request.Headers.Add("Cookie", FormsAuthentication.FormsCookieName + "=" + ticket);
                       }
                   }
                   catch (UriFormatException)
                   {
                       request = null;
                   }
                   if (request == null)
                       throw new ApplicationException("Invalid URL: " + url);
                   request.Method = "POST";
                   request.ContentType = "application/x-www-form-urlencoded";
                   request.ContentLength = postBytes.Length;

                   // add post data to request
                   Stream postStream = request.GetRequestStream();
                   postStream.Write(postBytes, 0, postBytes.Length);
                   postStream.Close();
               }
               catch (Exception e1)
               {
                   logger.Fatal(e1);
               }
           }).Start();
        }

        public static string GetData(string url)
        {
            return GetData(url, "");
        }

        public static string GetData(string url, string ticket) {
            //logger.Info("request url:" + url);
            //单元测试写log
            System.Diagnostics.Debug.WriteLine(url);
            if (string.IsNullOrEmpty(ticket) && url.IndexOf(StringHelper.Domain) != -1)
            {
                url = GetSafeUrl(url);
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            if (!string.IsNullOrEmpty(ticket))
            {
                request.Headers.Add("Cookie", FormsAuthentication.FormsCookieName + "=" + ticket);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return DecodeEncodedNonAsciiCharacters(reader.ReadToEnd());
            }
        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }

        public static string AsyncGetJsonString(string url)
        {
            url = GetSafeUrl(url);
            logger.Info("begin to execute AsyncGetJsonString, url:" + url);
            DateTime now = DateTime.Now;

            AsyncHttpWebRequestDelegate caller = new AsyncHttpWebRequestDelegate(GetData);
            IAsyncResult result = caller.BeginInvoke(url, null, null);
            while (!result.IsCompleted)
            {
                Thread.Sleep(250);
            }
            logger.Info("execute AsyncGetJsonString end, elapse time:" + DateTime.Now.Subtract(now).TotalMilliseconds.ToString());
            return caller.EndInvoke(result);
        }
    }
}
