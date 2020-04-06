using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTBrandModel
    {
        public MTBrandModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.StoreBrands = new List<StoreBrand>();
        }
        public byte StoreActiveType { get;set; }
        public int MainPartyId { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public IList<StoreBrand> StoreBrands { get; set; }
    }
}