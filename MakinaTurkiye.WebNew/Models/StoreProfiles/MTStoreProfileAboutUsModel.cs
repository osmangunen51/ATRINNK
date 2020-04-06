using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreProfileAboutUsModel
    {
        public MTStoreProfileAboutUsModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
        }
        public byte StoreActiveType { get; set; }
        public int MainPartyId { get; set; }
        public string GeneralText { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
    }
}