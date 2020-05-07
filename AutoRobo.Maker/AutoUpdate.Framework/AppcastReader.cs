using System;
using System.Collections.Generic;
using System.Xml;

namespace AutoRobo.Makers.AutoUpdate.Framework
{
    public class AppcastReader : IUpdateFeedSource
    {
        public List<Update> Read(string url)
        {
            var updates = new List<Update>();

            var document = new XmlDocument();
            try
            {
                document.Load(url);
            }
            catch (Exception ex) {
                return new List<Update>();
            }
            var nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace("appcast", "http://www.adobe.com/xml-namespaces/appcast/1.0");

            var nodes = document.SelectNodes("/rss/channel/item");

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes.Item(i);

                var titleNode = node.SelectSingleNode("title");
                var versionNode = node.SelectSingleNode("appcast:version", nsManager);
                var enclosureNode = node.SelectSingleNode("enclosure");
                var fileUrlNode = enclosureNode.Attributes["url"];
                //var fileLengthNode = enclosureNode.Attributes["length"];

                var update = new Update
                {
                    Title = titleNode.InnerText,
                    Version = new Version(versionNode.InnerText),
                    FileUrl = fileUrlNode.Value,
                    //FileLength = Convert.ToInt64(fileLengthNode.Value)
                };

                updates.Add(update);
            }
            return updates;
        }
    }
}