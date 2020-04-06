using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreImageAndVideosModel
    {
        public MTStoreImageAndVideosModel()
        {
            this.StoreImages = new List<string>();
            this.MTCompanyProfileVideosModels = new List<MTCompanyProfileVideosModel>();
        }
        public string StoreName { get; set; }
        public IList<string> StoreImages { get; set; }
        public int TotalVideoPage { get; set; }
        public IList<MTCompanyProfileVideosModel> MTCompanyProfileVideosModels { get; set; }

    }
}