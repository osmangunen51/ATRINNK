namespace NeoSistem.MakinaTurkiye.Core.Sitemap
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9", DataType = "", IsNullable = true)]
    public class SitemapIndexXml : ISitemap
    {
        public SitemapIndexXml()
        {
            this.items = new List<SitemapIndexNode>();
        }

        [XmlElement(ElementName = "sitemap")]
        public List<SitemapIndexNode> items { get; set; }
    }
}

/*<?xml version="1.0" encoding="utf-8"?>
<sitemapindex xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
<sitemap><loc>http://www.asd.com/categories_sitemap.xml</loc><lastmod>2013-12-13</lastmod></sitemap>
<sitemap><loc>http://www.qwe.com/brand_Foto_-_Kamera_1.xml</loc><lastmod>2013-12-13</lastmod></sitemap>
</sitemapindex>*/
