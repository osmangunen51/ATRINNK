using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTCatologModel
    {
        public MTCatologModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.MTStoreCatologItems = new List<MTStoreCatologItem>();
        }
        public byte StoreActiveType { get; set; }
        public int MainPartyId { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public List<MTStoreCatologItem> MTStoreCatologItems { get; set; }
    }
}