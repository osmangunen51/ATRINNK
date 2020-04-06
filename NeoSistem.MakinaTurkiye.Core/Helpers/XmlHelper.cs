namespace NeoSistem.MakinaTurkiye.Core.Helpers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public partial class XmlHelper
    {
        public static XmlDocument SerializeToXml(object oValue, Encoding encEncoding)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Encoding = encEncoding,
                CheckCharacters = true
            };

            StringBuilder sStringBuilder = new StringBuilder();

            XmlWriter xmlWriter = XmlWriter.Create(sStringBuilder, xmlWriterSettings);

            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            //xmlSerializerNamespaces.Add(string.Empty, string.Empty);
            xmlSerializerNamespaces.Add(string.Empty, "http://www.sitemaps.org/schemas/sitemap/0.9");

            XmlSerializer xmlSerializer = new XmlSerializer(oValue.GetType());
            xmlSerializer.Serialize(xmlWriter, oValue, xmlSerializerNamespaces);

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(sStringBuilder.ToString());

            if (encEncoding != null)
            {
                XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration(@"1.0", encEncoding.BodyName, null);
                XmlElement xmlElementRoot = xmlDocument.DocumentElement;
                xmlDocument.InsertBefore(xmlDeclaration, xmlElementRoot);
            }

            return xmlDocument;
        }

        public static string SerializeToString(object oValue, Encoding encEncoding)
        {

            string sSerializeToXml = SerializeToXml(oValue, encEncoding).OuterXml;
            return !string.IsNullOrEmpty(sSerializeToXml) ? sSerializeToXml : string.Empty;
        }

        public static object DeserializeFromXml(XmlDocument xmlDocument, Type type)
        {
            using (StringReader stringReader = new StringReader(xmlDocument.OuterXml))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                return xmlSerializer.Deserialize(stringReader);
            }
        }

        public static object DeserializeFromString(string sValue, Type type)
        {
            if (!string.IsNullOrEmpty(sValue))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(sValue);

                return DeserializeFromXml(xmlDocument, type);
            }

            return null;
        }
    }
}