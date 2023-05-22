namespace NeoSistem.Trinnk.Core.Sitemap
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "sitemap")]
    public class SitemapIndexNode
    {
        public SitemapIndexNode()
        {
        }

        [XmlElement(ElementName = "loc")]
        public string location { get; set; }

        [XmlIgnore]
        public DateTime lastmodified { get; set; }

        [XmlElement]
        public string lastmod
        {
            get { return lastmodified.ToString("yyyy-MM-dd"); }
            set { lastmodified = DateTime.Parse(value); }
        }
    }
}
