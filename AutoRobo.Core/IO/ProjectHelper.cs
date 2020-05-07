using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using AutoRobo.DB;
using Util;

namespace AutoRobo.Core.IO
{
    public class ProjectHelper
    {
        static log4net.ILog logger = log4net.LogManager.GetLogger("ProjectHelper");
      
        /// <summary>
        /// 为脚本文件设置临时数据库文件
        /// </summary>
        /// <param name="bitFile"></param>
        public static void Use(string bitFile) { 
            FileInfo file = new FileInfo(bitFile);
            DirectoryInfo rootDir = file.Directory.Parent;
            AppSettings.Instance.CurrentExecutePath = rootDir.FullName;                 
        }
        /// <summary>
        /// 初始化脚本项目文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="rootDir">url输出根目录</param>
        public static void Initialize(string projectName, out string rootDir,
            out string scriptDir, out string dataDir) {
            
            logger.Info("AppSettings.Instance.LibraryPath:" + AppSettings.Instance.LibraryPath);
            rootDir = Path.Combine(AppSettings.Instance.LibraryPath, projectName);
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            scriptDir = Path.Combine(rootDir, "script");
            if (!Directory.Exists(scriptDir))
            {
                Directory.CreateDirectory(scriptDir);
            }

            dataDir = Path.Combine(rootDir, "data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

        }

    
    }
}
