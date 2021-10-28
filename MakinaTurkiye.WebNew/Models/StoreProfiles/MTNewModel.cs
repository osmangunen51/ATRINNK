using NeoSistem.MakinaTurkiye.Web.Models.StoreNews;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTNewModel
    {
        public MTNewModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.StoreNewItems = new List<MTStoreNewItemModel>();

        }
        public byte StoreActiveType { get; set; }
        public int MainPartyId { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public List<MTStoreNewItemModel> StoreNewItems { get; set; }
        //public List<MTStoreCatologItem> MTStoreCatologItems { get; set; }
    }
}