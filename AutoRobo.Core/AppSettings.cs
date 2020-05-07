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
    /// ���������Ż�����������
    /// </summary>
    public enum EngineNetWorkEnum { 
        /// <summary>
        /// ���������Ż�����ʹ�ô���
        /// </summary>
        None,
        /// <summary>
        /// ʹ��ϵͳ����
        /// </summary>
        System,
        /// <summary>
        /// ͨ��ADSL���²��Ż�ȡ��ͬIP
        /// </summary>
        ADSL
    }

    public class EngineSettings {
      
        public EngineNetWorkEnum Networking { get; set; }
        /// <summary>
        /// ADSL����
        /// </summary>
        public string ADSLName { get; set; }
        /// <summary>
        /// �������ڣ� ȱʡ5�������²���
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
        /// �Ƿ�����������Ƶ
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
       /// ���ݲɼ���ʱdb�ļ�Ŀ¼,��d:/bitrun/www.cnboker.com/data/db.scf
        /// </summary>
       //public string TempDbFile { get; set; }

        /// <summary>
        /// �Ƿ�������������
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
        /// �Ƿ���������activex
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
        /// �Ƿ���������flash
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
        /// �Ƿ���������ͼƬ
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
        /// �Ƿ�Ϊ����ģʽ
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
        /// �Ƿ��������Ի���
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
        /// �Ƿ��������ű�����
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
        /// �Ƿ��ǽű�����ģʽ
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
        /// ���ļ�·��,��d:/bitrun
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
        /// ��ǰ���л���Ŀ¼,��d:/bitrun/www.cnboker.com/
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
        /// �����-��������ӵ������Ƿ����е�����ʾ��
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
        /// ���������ж����е�ʱ�򱣴�
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
