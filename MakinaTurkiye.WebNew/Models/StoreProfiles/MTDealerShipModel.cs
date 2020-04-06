
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTDealerShipModel
    {
        public MTDealerShipModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.DealerBrands = new List<DealerBrand>();
        }
        public byte StoreActiveType { get; set; }   
        public int MainPartyId { get; set; }
        public IList<DealerBrand> DealerBrands{get; set;}
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    }
}