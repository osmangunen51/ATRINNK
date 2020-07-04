using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTBannerModel
    {
        public int BannerId { get; set; }
        public int? CategoryId { get; set; }
        public string BannerResource { get; set; }
        public string BannerDescription { get; set; }
        public byte? BannerType { get; set; }
        public string BannerLink { get; set; }
       public string BannerAltTag { get; set; }
    }
}