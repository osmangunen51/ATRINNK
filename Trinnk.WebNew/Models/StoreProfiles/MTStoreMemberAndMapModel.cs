using Trinnk.Entities.Tables.Common;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTStoreMemberAndMapModel
    {
        public MTStoreMemberAndMapModel()
        {
            this.Phones = new List<Phone>();
            this.StoreAddress = new Address();
        }
        public string AuthorizedNameSurname { get; set; }
        public string StoreName { get; set; }
        public Address StoreAddress { get; set; }
        public string AddressMap { get; set; }
        public IList<Phone> Phones { get; set; }
        public string StoreWebUrl { get; set; }
    }
}