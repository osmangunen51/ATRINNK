namespace NeoSistem.MakinaTurkiye.Core.Helpers
{
    using System.IO;
    using System.Xml.Serialization;

    public static class XmlExtension
    {
        public static string ToXmlString(this object o)
        {
            XmlSerializer xmls = new XmlSerializer(o.GetType());
            StringWriter strWriter = new StringWriter();
            xmls.Serialize(strWriter, o);
            return strWriter.ToString();
        }
    }
}