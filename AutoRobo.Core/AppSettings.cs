 using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using AutoRobo.Core.Actions;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Collections;
using System.Runtime.CompilerServices;
using System.CodeDom.Compiler;

namespace AutoRobo.Core
{
    /// <summary>
    /// 搜索引擎优化器代理设置
    /// </summary>
    public enum EngineNetWorkEnum { 
        /// <summary>
        /// 搜索引擎优化器不使用代理
        /// </summary>
        None,
        /// <summary>
        /// 使用系统代理
        /// </summary>
        System,
        /// <summary>
        /// 通过ADSL重新拨号获取不同IP
        /// </summary>
        ADSL
    }

    public class EngineSettings {
      
        public EngineNetWorkEnum Networking { get; set; }
        /// <summary>
        /// ADSL名称
        /// </summary>
        public string ADSLName { get; set; }
        /// <summary>
        /// 拨号周期， 缺省5分钟重新拨号
        /// </summary>
        public int DialTime { get; set; }
    }
    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    public class AppSettings : ApplicationSettingsBase
    {
        private static AppSettings defaultInstance = ((AppSettings)SettingsBase.Synchronized(new AppSettings()));

        public static AppSettings Instance
        {
            get
            {
                return defaultInstance;
            }
        }
        /// <summary>
        /// 是否允许下载视频
        /// </summary>
       [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool DownloadVideo
        {
            get
            {
                return (bool)this["DownloadVideo"];
            }
            set
            {
                this["DownloadVideo"] = value;
            }
        }
        /// <summary>
       /// 数据采集临时db文件目录,如d:/bitrun/www.cnboker.com/data/db.scf
        /// </summary>
       //public string TempDbFile { get; set; }

        /// <summary>
        /// 是否允许下载声音
        /// </summary>
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool DownloadSounds
        {
            get
            {
                return (bool)this["DownloadSounds"];
            }
            set
            {
                this["DownloadSounds"] = value;
            }
        }
       
        /// <summary>
        /// 是否允许下载activex
        /// </summary>        
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool DownloadActiveX
        {
            get
            {
                return (bool)this["DownloadActiveX"];
            }
            set
            {
                this["DownloadActiveX"] = value;
            }
        }
        /// <summary>
        /// 是否允许下载flash
        /// </summary>        
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool DownloadFlash
        {
            get
            {
                return (bool)this["DownloadFlash"];
            }
            set
            {
                this["DownloadFlash"] = value;
            }
        }
        /// <summary>
        /// 是否允许下载图片
        /// </summary>        
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool DownloadImages
        {
            get
            {
                return (bool)this["DownloadImages"];
            }
            set
            {
                this["DownloadImages"] = value;
            }
        }
        /// <summary>
        /// 是否为在线模式
        /// </summary>
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("True"), UserScopedSetting]
        public bool OfflineMode
        {
            get
            {
                return (bool)this["OfflineMode"];
            }
            set
            {
                this["OfflineMode"] = value;
            }
        }
        /// <summary>
        /// 是否允许弹出对话框
        /// </summary>        
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool AllowAlert
        {
            get
            {
                return (bool)this["AllowAlert"];
            }
            set
            {
                this["AllowAlert"] = value;
            }
        }
        /// <summary>
        /// 是否允许弹出脚本错误
        /// </summary>        
        /// 
        [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool AllowScriptError
        {
            get
            {
                return (bool)this["AllowScriptError"];
            }
            set
            {
                this["AllowScriptError"] = value;
            }
        }
        /// <summary>
        /// 是否是脚本调试模式
        /// </summary>        
        /// 
         [DebuggerNonUserCode, DefaultSettingValue("False"), UserScopedSetting]
        public bool Debug
        {
            get
            {
                return (bool)this["Debug"];
            }
            set
            {
                this["Debug"] = value;
            }
        }
        [DefaultSettingValue("1000"), UserScopedSetting, DebuggerNonUserCode]
        public double TypingTime
        {
            get
            {
                return (double)this["TypingTime"];
            }
            set
            {
                this["TypingTime"] = value;
            }
        }
       
        /// <summary>
        /// 库文件路径,如d:/bitrun
        /// </summary>     
        /// 
         [DebuggerNonUserCode, UserScopedSetting]
        public string LibraryPath
        {
            get
            {
                return (string)this["LibraryPath"];
            }
            set
            {
                this["LibraryPath"] = value;
            }
        }
        /// <summary>
        /// 当前运行环境目录,如d:/bitrun/www.cnboker.com/
        /// </summary>
         public string CurrentExecutePath
         {
             get;
             set;
         }
        [DebuggerNonUserCode, UserScopedSetting]
        public ArrayList ProxyDataList
        {
            get
            {
                return (ArrayList)this["ProxyDataList"];
            }
            set
            {
                this["ProxyDataList"] = value;
            }
        }

        [DebuggerNonUserCode, UserScopedSetting, DefaultSettingValue("False")]
        public bool ProxyEnabled
        {
            get
            {
                return (bool)this["ProxyEnabled"];
            }
            set
            {
                this["ProxyEnabled"] = value;
            }
        }

        [DefaultSettingValue("False"), UserScopedSetting, DebuggerNonUserCode]
        public bool ProxyRotate
        {
            get
            {
                return (bool)this["ProxyRotate"];
            }
            set
            {
                this["ProxyRotate"] = value;
            }
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("1")]
        public byte ProxyRotateInterval
        {
            get
            {
                return (byte)this["ProxyRotateInterval"];
            }
            set
            {
                this["ProxyRotateInterval"] = value;
            }
        }

        [DefaultSettingValue("0"), DebuggerNonUserCode, UserScopedSetting]
        public byte ProxyType
        {
            get
            {
                return (byte)this["ProxyType"];
            }
            set
            {
                this["ProxyType"] = value;
            }
        }
        /// <summary>
        /// 活动数据-》多项添加到数据是否运行弹出提示框
        /// </summary>        
        [DefaultSettingValue("True"), UserScopedSetting, DebuggerNonUserCode]
        public bool ListToListAllowPrompt
        {
            get
            {
                return (bool)this["ListToListAllowPrompt"];
            }
            set
            {
                this["ListToListAllowPrompt"] = value;
            }
        }

        public List<FindMethods> FindPattern = new List<FindMethods>();
        public enum CodeLanguages { CSharp, VBNet, PHP, Python, Perl, Ruby }

         [DebuggerNonUserCode, UserScopedSetting]
        public EngineSettings EngineSettings
        {
            get
            {
                return (EngineSettings)this["EngineSettings"];
            }
            set
            {
                this["EngineSettings"] = value;
            }
        }
        /// <summary>
        /// 保存活动数据有多少行的时候保存
        /// </summary>
        [DefaultSettingValue("10"), UserScopedSetting, DebuggerNonUserCode]
        public int MemoryMaxItemsCount
        {
            get
            {
                return (int)this["MemoryMaxItemsCount"];
            }
            set
            {
                this["MemoryMaxItemsCount"] = value;
            }
        }

        private void LoadFindPattern(string PatternSetting)
        {
            string[] arrFindMethod = PatternSetting.Split(",".ToCharArray());
            foreach (string method in arrFindMethod)
            {
                FindPattern.Add((FindMethods) Enum.Parse(typeof(FindMethods), method));
            }
        }

        private string GetFindPattern()
        {
            var builder = new StringBuilder();
            foreach (var method in FindPattern)
            {
                if (builder.Length > 0) builder.Append(",");
                builder.Append(method);
            }
            return builder.ToString();
        }
     
    }
}
