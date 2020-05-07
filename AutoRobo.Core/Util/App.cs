using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading;

namespace AutoRobo.Core
{
    public class App
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();


        static public void MemoryRelease()
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        [DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
        private static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);
        const int URLMON_OPTION_USERAGENT = 0x10000001;

        static public void ChangeUserAgent(String Agent)
        {
            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, Agent, Agent.Length, 0);
        }

        /// <summary>
        /// 设置浏览器代理, 地址为空字符串直接访问
        /// </summary>
        /// <param name="ProxyAddress"></param>
        /// <param name="BypassList"></param>
        static public void SetSessionProxy(string ProxyAddress, string BypassList)
        {
            uint dwAccessType = 0x3;
            if (string.IsNullOrEmpty(ProxyAddress)) {
                dwAccessType = 0x1;
            }
            var proxyInfo = new INTERNET_PROXY_INFO
            {
                dwAccessType = dwAccessType,
                lpszProxy = ProxyAddress,
                lpszProxyBypass = BypassList
            };
            int structSize = Marshal.SizeOf(proxyInfo);
            const uint SetProxy = 0x26;

            if (UrlMkSetSessionOption(SetProxy, proxyInfo, (uint)structSize, 0) != 0)
                throw new Win32Exception();
        }

        [StructLayout(LayoutKind.Sequential)]
        private class INTERNET_PROXY_INFO
        {
            public uint dwAccessType;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpszProxy;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpszProxyBypass;
        }

        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int UrlMkSetSessionOption(uint dwOption, INTERNET_PROXY_INFO structNewProxy, uint dwLen, uint dwZero);


        /// <summary>
        /// 等待满足条件返回， 30秒超时
        /// </summary>
        /// <param name="condition"></param>
        public static void Wait(Func<bool> condition)
        {
            var timeout = DateTime.UtcNow + TimeSpan.FromSeconds(30);

            while (!condition() && DateTime.UtcNow < timeout)
            {
                Pause();
            }

            Pause();
        }

        private static void Pause(double ms = 50)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(ms));
        }

        public static bool Ping(string ip)
        {
            bool result = false;
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                result = true;
            }
            return result;
        }

        static public Thread Invoke(Action action, bool ansync)
        {
            Thread thread = null;
            if (ansync)
            {
                thread = new Thread(() =>
                {
                    action();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else {
                action();
            }
            return thread;
        }
    }
}
