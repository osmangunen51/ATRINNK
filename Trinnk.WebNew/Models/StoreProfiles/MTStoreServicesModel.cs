using Trinnk.Entities.Tables.Common;
using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTStoreServicesModel
    {
        public MTStoreServicesModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.Phones = new List<Phone>();
            this.ServicesAddresses = new List<Address>();
            this.StoreServices = new List<StoreDealer>();
            this.AdressEdits = new List<AddressShow>();
        }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Address> ServicesAddresses { get; set; }
        public List<AddressShow> AdressEdits { get; set; }

        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public List<StoreDealer> StoreServices { get; set; }
    }
}