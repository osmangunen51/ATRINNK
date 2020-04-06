namespace NeoSistem.MakinaTurkiye.Core.Sitemap
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "url")]
    public class SitemapNode
    {
        public SitemapNode()
        {
            this.changefrequency = ChangeFrequency.weekly;
            this.priority = 0.5f;
        }

        [XmlElement(ElementName = "loc")]
        public string location { get; set; }

        [XmlIgnore]
        public DateTime lastmodified { get; set; }

        [XmlElement]
        public string lastmod
        {
            get
            {
                if (lastmodified > DateTime.MinValue)
                    return lastmodified.ToString("yyyy-MM-dd");
                else
                    return null;
            }
            set { lastmodified = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "changefreq")]
        public ChangeFrequency changefrequency { get; set; }

        [XmlElement(ElementName = "priority")]
        public float priority { get; set; }
    }
}
