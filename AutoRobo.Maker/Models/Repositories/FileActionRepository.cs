using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core.Models;
using AutoRobo.Core;
using System.Data;
using AutoRobo.Core.IO;
using Util;
using System.Linq;

namespace AutoRobo.Makers.Models.Repositories
{
    public class FileActionRepository : IActionRepository
    {
        private string fileName = "";

        public FileActionRepository()
        {

        }

        public FileActionRepository(string fileName)
        {
            this.fileName = fileName;
            ProjectHelper.Use(fileName);
        }

        public ActionXmlSet Read()
        {
            if (string.IsNullOrEmpty(fileName)) return null;
            ActionXmlSet xmlset = new ActionXmlSet();
            StreamReader sr = new StreamReader(fileName);
            try
            {
                xmlset.XmlAction = sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
            }
            xmlset.IsLoadIdentiy = false;
            return xmlset;
        }

        public void Write(string actionxml)
        {
            StreamWriter sw = null;
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    SaveAs(actionxml);
                }
                else
                {
                    sw = new StreamWriter(fileName);
                    sw.Write(actionxml);
                }
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public void SaveAs(string xmlContent)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "bit文件 (*.bit)|*.bit";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = false;
                dialog.InitialDirectory = AppSettings.Instance.LibraryPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    // Can use dialog.FileName
                    using (Stream stream = dialog.OpenFile())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(xmlContent);
                        // Save data
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }
                }
            }
        }

        public ActionXmlSet GetModulerModel(string modulerId)
        {
            throw new NotImplementedException();
        }


        public string[] GetActionModulers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新建工程        
        /// </summary>
        /// <param name="startUrl"></param>
        public void New(string projectName)
        {
            string rootDir = "";
            string dataDir = "";
            string scriptDir = "";
            ProjectHelper.Initialize(projectName, out rootDir, out scriptDir, out dataDir);
            fileName = Path.Combine(scriptDir, GetNewName(Directory.GetFiles(scriptDir)) + ".bit");
            //AppSettings.Instance.TempDbFile = dbFile;
        }

        static private string GetNewName(string[] existNames)
        {
            string prefixName = "脚本";
            int index = 1;
            while (existNames.Any(c => c == (prefixName + index.ToString())))
            {
                index++;
            }
            return prefixName + index.ToString();
        }
    }
}
