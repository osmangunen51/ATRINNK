using System.Web;
using System.Xml;

namespace MakinaTurkiye.Localization
{
    public class Resource
    {
        /// <summary>
        /// GetXmlResource all items (ex: tr_TR.xml)
        ///
        /// Very simple xml structure
        ///     <lang>
	    ///         <item id="homepage">Homepage</item>
        ///     </lang>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="applicationName"></param>
        public static void GetXmlResource(string path, string applicationName)
        {
            var doc = new XmlTextReader(path);
            var xml = new XmlDocument();
            xml.Load(doc);
            var nodes = xml.DocumentElement.SelectSingleNode("//lang");
            for (var nod = 0; nod <= nodes.ChildNodes.Count - 1; nod++)
            {
                var itemId = nodes.ChildNodes.Item(nod).Attributes.Item(0).InnerText;
                var itemValue = nodes.ChildNodes.Item(nod).InnerText;
                HttpContext.Current.Application[applicationName + itemId] = itemValue;
            }
            xml.Clone();
            doc.Close();
        }

        public static void SetXmlResource(string path, string id, string Value)
        {
            var doc = new XmlTextReader(path);
            var xml = new XmlDocument();
            xml.Load(doc);
            var nodes = xml.DocumentElement.SelectSingleNode("//lang");
            XmlElement Nd = xml.CreateElement("item");
            Nd.SetAttribute("id", id);
            Nd.InnerText = Value;
            nodes.AppendChild(Nd);
            doc.Close();
            xml.Save(path);
        }
    }
}