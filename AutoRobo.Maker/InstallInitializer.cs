using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AutoRobo.Core;
using AutoRobo.Core.IO;

namespace AutoRobo.Makers
{
    public class InstallInitializer
    {
        public static void Configure() {
            //string drive = GetDrive();
            //string root = drive + "bitrun";
            string mydoc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "bitrun");
            if (!Directory.Exists(mydoc))
            {
                Directory.CreateDirectory(mydoc);
            }
            AppSettings.Instance.LibraryPath = mydoc;
            AppSettings.Instance.Save();            
        }

        static public void ConfigSample() {
       
            string google = "采集google财经数据";
            string cnboker = "表格数据采集";
            string stock = "上市公司财务报表";
            
            string rootDir = "";
            string dataDir="";
            string scriptDir="";
            string dbFile="";
            
            ProjectHelper.Initialize(google, out rootDir, out scriptDir, out dataDir);
            WriteFile(Path.Combine(scriptDir,"google财经数据(ajax数据提取).bit"), "AutoRobo.Makers.Sample.google财经数据(ajax数据提取).bit");

            ProjectHelper.Initialize(cnboker, out rootDir, out scriptDir, out dataDir);
            WriteFile(Path.Combine(scriptDir, "提取表格数据.bit"), "AutoRobo.Makers.Sample.提取表格数据.bit");

            ProjectHelper.Initialize(stock, out rootDir, out scriptDir, out dataDir);
            WriteFile(Path.Combine(scriptDir, "提取上市公司3大报表数据.bit"), "AutoRobo.Makers.Sample.提取上市公司3大报表数据.bit");
        }

        static private void WriteFile(string fileName, string resourcePath)
        {
            Stream stream = System.Reflection.Assembly.GetExecutingAssembly().
                 GetManifestResourceStream(resourcePath);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write(sr.ReadToEnd());
            sr.Close();
            sw.Close();
        }

        /// <summary>
        /// 如果有D盘，则返回D盘盘符
        /// </summary>
        /// <returns></returns>
        static private string GetDrive() {
            string sysDrive = Path.GetPathRoot(Environment.SystemDirectory);
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            List<DriveInfo> avadrives = new List<DriveInfo>();
            for (int i = 0; i <= drives.Length - 1; i++)
            {
                if (drives[i].DriveType == DriveType.Fixed && drives[i].Name != sysDrive) {
                    avadrives.Add(drives[i]);
                }
            }
            if (avadrives.Count > 0) {
                return avadrives[0].Name;
            }
            return sysDrive;
        }
    }
}
