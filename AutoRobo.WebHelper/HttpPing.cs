using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net;
using System.IO;

namespace Util
{
    public class HttpPing
    {
        public static bool Ping(string url) {
            if (string.IsNullOrEmpty(url)) return false;
            if (url.IndexOf("http") == -1) {
                url = "http://" + url;
            }
            int speed = 0;
            return Ping(url, out speed);
        }

      

        public static bool Ping(string url, out int speed) { 
            // Create a new 'HttpWebRequest' Object.
            DateTime sTime = DateTime.Now;
            bool haveResponse = false;
            HttpWebRequest request= null;
            WebResponse response = null;
            speed = 0;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();                
                haveResponse =  request.HaveResponse;
                DateTime eTime = DateTime.Now;
                speed = eTime.Subtract(sTime).Milliseconds;                
            }
            catch 
            {
            }
            finally {
                if (response != null) {
                    response.Close();
                }
            }
            return haveResponse;
        }

        public static bool Peer(string url, out int speed)
        {
            HttpWebRequest webRequest = null;
            WebResponse webResponse = null;
            Stream strResponse = null;
            bool haveResponse = false;
            DateTime sTime = DateTime.Now;
            speed = 0;
            using (WebClient wcDownload = new WebClient())
            {
                try
                {
                    // Create a request to the file we are downloading
                    webRequest = (HttpWebRequest)WebRequest.Create(url);
                    // Set default authentication for retrieving the file
                    webRequest.Credentials = CredentialCache.DefaultCredentials;
                    // Retrieve the response from the server
                    webResponse = (HttpWebResponse)webRequest.GetResponse();
                    haveResponse = webRequest.HaveResponse;
                    // Ask the server for the file size and store it
                    Int64 fileSize = webResponse.ContentLength;

                    // Open the URL for download
                    strResponse = wcDownload.OpenRead(url);

                    // It will store the current number of bytes we retrieved from the server
                    int bytesSize = 0;
                    // A buffer for storing and writing the data retrieved from the server
                    byte[] downBuffer = new byte[2048];

                    // Loop through the buffer until the buffer is empty
                    while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                    {
                        break;
                    }
                    DateTime eTime = DateTime.Now;
                    speed = eTime.Subtract(sTime).Milliseconds;
                }
                catch { }
                finally
                {
                    // When the above code has ended, close the streams
                    if(webResponse != null)
                    webResponse.Close();
                    if(strResponse != null)
                    strResponse.Close();
                }
                return haveResponse;
            }
        }
    }
}
