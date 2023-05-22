namespace NeoSistem.Trinnk.Core.Sitemap
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9", DataType = "", IsNullable = true)]
    public class SitemapXml : ISitemap
    {
        public SitemapXml()
        {
            this.items = new List<SitemapNode>();
        }

        [XmlElement(ElementName = "url")]
        public List<SitemapNode> items { get; set; }
    }
}
