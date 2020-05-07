using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using AutoRobo.Core.Actions;
using AutoRobo.DataMapping;
using AutoRobo.WebHelper;

namespace AutoRobo.Core.Models
{
    public class ModelSerilizer
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger("ModelSerilizer");        
      
        public string Serilize(ModelSet ModelSet) {
            StringWriterWithEncoding sw = new StringWriterWithEncoding(Encoding.UTF8);

            var writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented };
            writer.WriteStartDocument();

            writer.WriteStartElement("AutoScript");
            if (!string.IsNullOrEmpty(ModelSet.StartUrl)) {
                writer.WriteAttributeString("StartUrl", ModelSet.StartUrl);
            }
            if (!string.IsNullOrEmpty(ModelSet.Target)) {
                writer.WriteAttributeString("Target", ModelSet.Target);
            }
            writer.WriteAttributeString("DataMethod", ModelSet.DataMethod.ToString());
            ModelSet.MainActionModel.SaveXml("MainAction", writer);
            ModelSet.SubActionModel.SaveXml("FunctionAction", writer);
            ModelSet.VariableActionModel.SaveXml("VariableAction", writer);
       
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            return sw.ToString();
        }

        public void SerializeToFile(ModelSet ModelSet, string filename)
        {
            StreamWriter fs = new StreamWriter(filename, false, Encoding.UTF8);
            fs.Write(Serilize(ModelSet));
            fs.Close();
        }

        public void Deserialize(string xmlContent)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            var context = AppContext.Current;
            LoadScript(context, doc);
        }

        private void LoadScript(IAppContext context, XmlDocument doc)
        {              
            XmlNode root = doc.DocumentElement;
            if (root == null) return;
            XmlAttribute att = root.Attributes["StartUrl"];
            if (att != null) {
                context.ActionModel.StartUrl = att.Value;
            }
            
            att = root.Attributes["Target"];
            if (att != null)
            {
                context.ActionModel.Target = att.Value;
            }

            att = root.Attributes["DataMethod"];
            if (att != null)
            {
                context.ActionModel.DataMethod = (DataMethod)Enum.Parse(typeof(DataMethod),att.Value);
            }
            XmlNodeList nodeList = root.SelectNodes("MainAction");
            LoadModel(context, context.ActionModel.MainActionModel, nodeList);
            //解决版本兼容问题
            nodeList = root.SelectNodes("Test");
            LoadModel(context, context.ActionModel.MainActionModel, nodeList);
            nodeList = root.SelectNodes("ActionList");
            LoadModel(context, context.ActionModel.MainActionModel, nodeList);
            //加载活动块
            nodeList = root.SelectNodes("FunctionAction");
            LoadModel(context, context.ActionModel.SubActionModel, nodeList);

            //加载全局变量
            nodeList = root.SelectNodes("VariableAction");
            LoadModel(context, context.ActionModel.VariableActionModel, nodeList);
           
        }

        private void LoadModel(IAppContext context, ActionList actionList, XmlNodeList nodeList)
        {
            if (nodeList == null) return;

            foreach (XmlNode node in nodeList)
            {
                actionList.LoadScript(context, node);
            }
        }
        /// <summary>
        /// Loads code from a file, must be in native format
        /// </summary>
        /// <param name="Filename">Filename to load from</param>
        public void DeserilizeFromFile(IAppContext context, string Filename)
        {
            if (!File.Exists(Filename))
            {
                MessageBox.Show(string.Format(Properties.Resources.FileDoesNotExist, Filename), Properties.Resources.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
            try
            {
                var doc = new XmlDocument();
                doc.Load(Filename);
                LoadScript(context, doc);
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
