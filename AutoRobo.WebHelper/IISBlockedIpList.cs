using System;
using System.Collections.Generic;

using System.Text;

using System.DirectoryServices;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net;
using System.Security.Principal;

namespace Util
{
    /// <summary>
    /// Allows reading and setting the IIS Blocked IP List for a given Web site
    /// 
    /// Note:
    /// These routines only handle simple IP addressing rules. It handles
    /// the IPDeny list only and works only with full IP Addresses or
    /// Subnet blocked wildcard IP Addresses. If you use domain name
    /// lookups don't use these routines to update!
    /// 
    /// These routines require ADMIN or System level permissions in
    /// order to set IIS configuration settings.
    /// 
    /// Disclaimer:
    /// Use at your own risk. These routines modify the IIS metabase
    /// and therefore have the potential to muck up your configuration.
    /// YOU ARE FULLY RESPONSIBLE FOR ANY PROBLEMS THAT THESE ROUTINES
    /// MIGHT CAUSE TO YOUR CONFIGURATION!
    /// </summary>
    public class IISBlockedIpList : IDisposable
    {

        public string MetaBasePath
        {
            get { return _MetaBasePath; }
            set { _MetaBasePath = value; }
        }
        private string _MetaBasePath = "IIS://localhost/W3SVC/1/ROOT";

        private DirectoryEntry IIS;

        public IISBlockedIpList(string metaBasePath)
        {
            if (!string.IsNullOrEmpty(metaBasePath))
                this.MetaBasePath = metaBasePath;


        }


        private void Open()
        {
            this.Open(this.MetaBasePath);
        }
        private void Open(string IISMetaPath)
        {
            if (this.IIS == null)
                this.IIS = new DirectoryEntry(IISMetaPath);
        }
        private void Close()
        {
            if (IIS != null)
            {
                this.IIS.Close();
                this.IIS = null;

            }
        }

        /// <summary>
        /// Returns a list of Ips as a plain string returning just the
        /// IP Addresses, leaving out the subnet mask values.
        /// 
        /// Any wildcarded IP Addresses will return .0 for the
        /// wild card characters.
        /// </summary>
        /// <returns></returns>
        public string[] GetIpList()
        {
            this.Open();

            // *** Grab the IP List
            object IPSecurity = IIS.Properties["IPSecurity"].Value;

            // retrieve the IPDeny list from the IPSecurity object. Note: Strings as objects
            //Array origIPDenyList = (Array)wwUtils.GetPropertyCom(IPSecurity, "IPDeny");

            Array origIPDenyList = (Array)
             IPSecurity.GetType().InvokeMember("IPDeny",
                    BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.GetProperty,
                    null, IPSecurity, null);

            this.Close();

            // *** Format and Extract into a string list
            List<string> Ips = new List<string>();
            foreach (string IP in origIPDenyList)
            {
                // *** Strip off the subnet-mask - we'll use .0 or .* to represent
                string TIP = IP.Substring(0, IP.IndexOf(",")); //wwUtils.ExtractString(IP, "", ",");
                Ips.Add(TIP);
            }


            return Ips.ToArray();
        }

        /// <summary>
        /// Allows you to pass an array of strings that contain the IP Addresses
        /// to block. 
        /// 
        /// Wildcard IPs should use .* or .0 to indicate blocks.
        /// 
        /// Note this string list should contain ALL IP addresses to block
        /// not just new and added ones (ie. use GetList first and then
        /// add to the list.
        /// </summary>
        /// <param name="IPStrings"></param>
        public void SetIpList(string[] IPStrings)
        {
            this.Open();

            object IPSecurity = IIS.Properties["IPSecurity"].Value;

            // *** IMPORTANT: This list MUST be object or COM call will fail!
            List<object> newIpList = new List<object>();

            foreach (string Ip in IPStrings)
            {
                string newIp;

                if (Ip.EndsWith(".*.*.*") || Ip.EndsWith(".0.0.0"))
                    newIp = Ip.Replace(".*", ".0") + ",255.0.0.0";
                else if (Ip.EndsWith(".*.*") || Ip.EndsWith(".0.0"))
                    newIp = Ip.Replace(".*", ".0") + ",255.255.0.0";
                else if (Ip.EndsWith(".*") || Ip.EndsWith(".0"))
                {
                    // *** Wildcard requires different IP Mask
                    newIp = Ip.Replace(".*", ".0") + ",255.255.255.0";
                }
                else
                    newIp = Ip + ", 255.255.255.255";


                // *** Check for dupes - nasty but required because
                // *** object -> string comparison can't do BinarySearch
                bool found = false;
                foreach (string tempIp in newIpList)
                {
                    if (newIp == tempIp)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    newIpList.Add(newIp);
            }

            //wwUtils.SetPropertyCom(this.IPSecurity, "GrantByDefault", true);

            IPSecurity.GetType().InvokeMember("GrantByDefault",
                    BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.SetProperty,
                    null, IPSecurity, new object[] { true });


            object[] ipList = newIpList.ToArray();

            // *** Apply the new list
            //wwUtils.SetPropertyCom(this.IPSecurity, "IPDeny",ipList);

            IPSecurity.GetType().InvokeMember("IPDeny",
                     BindingFlags.Public |
                     BindingFlags.Instance | BindingFlags.SetProperty,
                     null, IPSecurity, new object[] { ipList });


            IIS.Properties["IPSecurity"].Value = IPSecurity;
            IIS.CommitChanges();
            IIS.RefreshCache();

            this.Close();
        }


        /// <summary>
        /// Adds IP Addresses to the existing IP Address list
        /// </summary>
        /// <param name="IPString"></param>
        public void AddIpList(string[] newIps)
        {
            string[] origIps = this.GetIpList();

            List<string> Ips = new List<string>(origIps);
            foreach (string ip in newIps)
            {
                Ips.Add(ip);
            }

            this.SetIpList(Ips.ToArray());
        }

        /// <summary>
        /// Returns a list of all IIS Sites on the server
        /// </summary>
        /// <returns></returns>
        public IIsWebSite[] GetIIsWebSites()
        {
            // *** IIS://Localhost/W3SVC/
            string iisPath = this.MetaBasePath.Substring(0, this.MetaBasePath.ToLower().IndexOf("/w3svc/")) + "/W3SVC";
            DirectoryEntry root = new DirectoryEntry(iisPath);

            List<IIsWebSite> Sites = new List<IIsWebSite>();
            foreach (DirectoryEntry Entry in root.Children)
            {
                System.DirectoryServices.PropertyCollection Properties = Entry.Properties;

                try
                {
                    IIsWebSite Site = new IIsWebSite();
                    Site.SiteName = (string)Properties["ServerComment"].Value;

                    // *** Skip over non site entries
                    if (Site.SiteName == null || Site.SiteName == "")
                        continue;

                    Site.IISPath = Entry.Path;
                    Sites.Add(Site);
                }
                catch { ; }
            }

            root.Close();

            return Sites.ToArray();
        }


        #region IDisposable Members
        public void Dispose()
        {
            if (this.IIS != null)
                IIS.Close();
        }
        #endregion
    }

    /// <summary>
    /// Container class that holds information about an IIS Web site
    /// </summary>
    public class IIsWebSite
    {

        /// <summary>
        /// The display name of the Web site
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        private string _SiteName = "";

        /// <summary>
        /// The IIS Metabase path
        /// </summary>
        public string IISPath
        {
            get { return _IISPath; }
            set { _IISPath = value; }
        }
        private string _IISPath = "";

    }
}
