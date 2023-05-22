using Trinnk.Entities.Tables.Common;
using NeoSistem.Trinnk.Web.Models.Catalog;
using NeoSistem.Trinnk.Web.Models.Products;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTConnectionModel
    {

        public MTConnectionModel()
        {
            this.Phones = new List<MTStorePhoneModel>();
            this.StoreAddress = new Address();
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            MtJsonLdModel = new MTJsonLdModel();
        }
        public int MainPartyId { get; set; }
        public byte StoreActiveType { get; set; }
        public string AuthorizedNameSurname { get; set; }
        public string StoreName { get; set; }
        public Address StoreAddress { get; set; }
        public string AddressMap { get; set; }
        public IList<MTStorePhoneModel> Phones { get; set; }
        public string StoreWebUrl { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public MTJsonLdModel MtJsonLdModel { get; set; }

    }
}