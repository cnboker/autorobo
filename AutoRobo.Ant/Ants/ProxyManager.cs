namespace AutoRobo.ClientLib.Ants
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Timers;
    using IfacesEnumsStructsClasses;
    using AutoRobo.Core;
   
    public class ProxyManager
    {
        private bool authRequired;
        private int currentProxyIndex;
        private const int INTERNET_OPEN_TYPE_PROXY = 3;
        private const int INTERNET_OPTION_PROXY = 0x26;
        private const int INTERNET_OPTION_PROXY_PASSWORD = 0x2c;
        private const int INTERNET_OPTION_PROXY_USERNAME = 0x2b;
        private const int INTERNET_OPTION_REFRESH = 0x25;
        private const int INTERNET_OPTION_SETTINGS_CHANGED = 0x27;
        private System.Timers.Timer proxyTimer;
        private string pwd;
        private string username;
        private log4net.ILog logger = log4net.LogManager.GetLogger("ProxyManager");
        public static bool CheckProxy(PROXY_DATA proxyData)
        {
            bool flag = false;
            try
            {
                WebRequest request = WebRequest.Create("http://www.baidu.com");
                if (!proxyData.address.ToLower().StartsWith("http"))
                {
                    proxyData.address = "http://" + proxyData.address;
                }
                WebProxy proxy = new WebProxy {
                   
                    Address = new Uri(proxyData.address)
                };
                if (proxyData.username != null)
                {
                    proxy.Credentials = new NetworkCredential(proxyData.username, proxyData.password);
                }
                request.Proxy = proxy;
                request.Timeout = 5000;
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                flag = false;
            }
            catch (Exception)
            {
            }
            return flag;
        }

        private void ClearProxy()
        {
            INTERNET_PROXY_INFO structure = new INTERNET_PROXY_INFO();
            try
            {
                structure.dwAccessType = 1;
                IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
                Marshal.StructureToPtr(structure, ptr, true);
                InternetSetOption(IntPtr.Zero, 0x26, ptr, Marshal.SizeOf(structure));
            }
            catch (Exception)
            {
            }
        }

        private string GetAuthHeader()
        {
            string str = null;
            if (this.authRequired)
            {
                string s = this.username + ":" + this.pwd;
                string str3 = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
                str = string.Format("Proxy-Authorization: Basic {0}{1}", str3, Environment.NewLine);
            }
            return str;
        }

        [DllImport("wininet.dll", SetLastError=true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        private void proxyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool flag = false;
            ArrayList proxyDataList = AppSettings.Instance.ProxyDataList;
            int currentProxyIndex = this.currentProxyIndex;
            do
            {
                PROXY_DATA proxyData = BrowserSettings.GetProxyData((string)proxyDataList[currentProxyIndex]);
                logger.Info("proxy ip:" + proxyData.address);
                currentProxyIndex++;
                if (currentProxyIndex >= proxyDataList.Count)
                {
                    currentProxyIndex = 0;
                }
                if (this.SetProxy(proxyData))
                {
                    flag = true;
                    this.currentProxyIndex = currentProxyIndex;
                }
                else
                {                  
                    if (currentProxyIndex == this.currentProxyIndex)
                    {
                        this.Stop();
                        return;
                    }
                }
                
            }
            while (!flag);
        }

        public void SetCredentials()
        {
            if (this.authRequired)
            {
                InternetSetOption(IntPtr.Zero, 0x2b, Marshal.StringToHGlobalAnsi(this.username), this.username.Length + 1);
                InternetSetOption(IntPtr.Zero, 0x2c, Marshal.StringToHGlobalAnsi(this.pwd), this.pwd.Length + 1);
                InternetSetOption(IntPtr.Zero, 0x27, IntPtr.Zero, 0);
                InternetSetOption(IntPtr.Zero, 0x25, IntPtr.Zero, 0);
            }
        }

        private bool SetProxy(PROXY_DATA proxyData)
        {
            bool flag = false;
            try
            {
                INTERNET_PROXY_INFO internet_proxy_info;
                if (!CheckProxy(proxyData))
                {
                    return flag;
                }
                internet_proxy_info.dwAccessType = 3;
                internet_proxy_info.lpszProxy = Marshal.StringToHGlobalAnsi(proxyData.address);
                internet_proxy_info.lpszProxyBypass = Marshal.StringToHGlobalAnsi("local");
                IntPtr ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(internet_proxy_info));
                Marshal.StructureToPtr(internet_proxy_info, ptr, true);
                flag = InternetSetOption(IntPtr.Zero, 0x26, ptr, Marshal.SizeOf(internet_proxy_info));
                if ((proxyData.username != null) && (proxyData.password != null))
                {
                    this.authRequired = true;
                    this.username = proxyData.username;
                    this.pwd = proxyData.password;
                }
                else
                {
                    this.authRequired = false;
                }
                InternetSetOption(IntPtr.Zero, 0x27, IntPtr.Zero, 0);
                InternetSetOption(IntPtr.Zero, 0x25, IntPtr.Zero, 0);
            }
            catch (Exception)
            {
            }
            return flag;
        }

        public void Start()
        {
            
            if (AppSettings.Instance.ProxyEnabled)
            {
                if (!AppSettings.Instance.ProxyRotate)
                {
                    PROXY_DATA proxyData = BrowserSettings.GetProxyData((string)AppSettings.Instance.ProxyDataList[0]);
                    this.SetProxy(proxyData);
                }
                else
                {
                    this.proxyTimer = new System.Timers.Timer();
                    this.proxyTimer.Elapsed += new ElapsedEventHandler(this.proxyTimer_Elapsed);
                    this.proxyTimer.Interval = (AppSettings.Instance.ProxyRotateInterval * 60) * 1000;
                    this.proxyTimer_Elapsed(this, null);
                    this.proxyTimer.Start();
                }
            }
        }

        public void Stop()
        {
            if (AppSettings.Instance.ProxyEnabled)
            {
                logger.Info("proxy stop");
                if (AppSettings.Instance.ProxyRotate)
                {
                    this.proxyTimer.Stop();
                }
                this.ClearProxy();
            }
        }

        public string AuthHeader
        {
            get
            {
                return this.GetAuthHeader();
            }
        }

        public bool AuthRequired
        {
            get
            {
                return this.authRequired;
            }
        }
    }
}

