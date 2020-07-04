using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreProfileBranchModel
    {
        public MTStoreProfileBranchModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.Phones = new List<Phone>();
            this.BranchAddresses = new List<Address>();
            this.StoreBranchs = new List<StoreDealer>();
            this.AdressEdits = new List<AddressShow>();
        }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<Phone> Phones{get;set;}
        public List<Address> BranchAddresses { get; set; }
        public List<AddressShow> AdressEdits {get;set; }

        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public List<StoreDealer> StoreBranchs { get; set; }

    }


}