using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace AutoRobo.Makers.Views
{
    public class MenuBuilder
    {
       
        public static ToolStripItem[] CreateMenu()
        {
            
            List<ToolStripItem> list = new List<ToolStripItem>();
            var doc = new XmlDocument();
            Stream stream = System.Reflection.Assembly.GetEntryAssembly().
                GetManifestResourceStream(string.Format("AutoRobo.Makers.Menu.xml"));
            StreamReader sr = new StreamReader(stream);
            doc.LoadXml(sr.ReadToEnd());
            sr.Close();
            XmlNode root = doc.DocumentElement;
            if (root == null) return list.ToArray();

            XmlNodeList nodeList = root.SelectNodes("MenuItem");
            LoadMenus(list, nodeList);
            return list.ToArray();
        }

        static private void LoadMenus(List<ToolStripItem> list, XmlNodeList nodeList)
        {
            if (nodeList == null) return;            
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType != XmlNodeType.Comment)
                {
                    list.Add(Create(node));
                }
            }
        }

        static ToolStripItem Create(XmlNode node)
        {            
            ToolStripItem item = null;
            var text = node.Attributes["Text"].Value;
            if (text == "-")
            {
                item = new ToolStripSeparator();
            }
            else
            {
                item = new ToolStripMenuItem();
                item.Name = node.Attributes["Name"].Value;
                item.Text = node.Attributes["Text"].Value;
                item.Tag = node.Attributes["Action"] == null ? "" : node.Attributes["Action"].Value;
                item.Enabled = node.Attributes["Enabled"] == null ? true : Convert.ToBoolean(node.Attributes["Enabled"]);
                
                if (node.ChildNodes.Count > 0)
                {
                    List<ToolStripItem> list = new List<ToolStripItem>();
                    LoadMenus(list, node.ChildNodes[0].ChildNodes);
                    ((ToolStripMenuItem)item).DropDownItems.AddRange(list.ToArray());
                }
            }                   
            return item;
        }

      
    }
}
