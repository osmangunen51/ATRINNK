using NeoSistem.MakinaTurkiye.Web.Models.Videos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreProfileVideoModel
    {
        public MTStoreProfileVideoModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.VideoCategoryModel = new MTVideoCategoryModel();
            this.VideosPage = new List<MTVideoModel>();
        }
        public string StoreName { get; set; }
        public byte StoreActiveType { get; set; }
        public int MainPartyId { get; set; }
        public MTVideoCategoryModel VideoCategoryModel { get; set; }
        public List<MTVideoModel> VideosPage { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    }
}