using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
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