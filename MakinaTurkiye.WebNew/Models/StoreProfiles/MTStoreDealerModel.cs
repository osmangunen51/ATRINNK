
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreDealerModel
    {
        public MTStoreDealerModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.Phones = new List<Phone>();
            this.DealerAddresses = new List<Address>();
            this.StoreDealers = new List<StoreDealer>();
            this.AdressEdits = new List<AddressShow>();
        }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Address> DealerAddresses { get; set; }
        public List<AddressShow> AdressEdits { get; set; }

        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public List<StoreDealer> StoreDealers { get; set; }
    }
    public class AddressShow
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
    }
}