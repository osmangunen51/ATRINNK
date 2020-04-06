namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System.Collections.Generic;

    public class BannerModel {
        public bool SaveMessage { get; set; }

        public string Url { get; set; }
        public int BannerType { get; set; }
        public int CategoryId { get; set; }

        public string Banner1Desc { get; set; }
        public string Banner1Link { get; set; }
        public string Banner1Rsc { get; set; }

        public string Banner2Desc { get; set; }
        public string Banner2Link { get; set; }
        public string Banner2Rsc { get; set; }

        public string Banner3Desc { get; set; }
        public string Banner3Link { get; set; }
        public string Banner3Rsc { get; set; }

        public string Banner4Desc { get; set; }
        public string Banner4Link { get; set; }
        public string Banner4Rsc { get; set; }

        public string Banner5Desc { get; set; }
        public string Banner5Link { get; set; }
        public string Banner5Rsc { get; set; }

        public string Banner6Desc { get; set; }
        public string Banner6Link { get; set; }
        public string Banner6Rsc { get; set; }

        public string Banner7Desc { get; set; }
        public string Banner7Link { get; set; }
        public string Banner7Rsc { get; set; }
        public string Banner7Order { get; set; }
        public string Banner7Id { get; set; }
        public string Banner7Type { get; set; }


        public IList<Banner> BannerItems { get; set; }
    }

}