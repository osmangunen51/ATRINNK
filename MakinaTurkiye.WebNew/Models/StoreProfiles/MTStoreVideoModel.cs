using NeoSistem.MakinaTurkiye.Web.Models.Videos;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreVideoModel
    {

        public MTStoreVideoModel()
        {

            MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.MTVideoModels = new List<MTVideoModel>();
        }
        public string StoreName { get; set; }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<MTVideoModel> MTVideoModels { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    }
}