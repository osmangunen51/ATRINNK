using NeoSistem.Trinnk.Web.Models.Videos;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
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