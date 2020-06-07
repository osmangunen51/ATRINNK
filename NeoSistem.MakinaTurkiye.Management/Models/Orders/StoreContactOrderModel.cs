using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;

namespace NeoSistem.MakinaTurkiye.Management.Models.Orders
{
    public class StoreContactOrderModel
    {
        public StoreContactOrderModel()
        {
            this.Phones = new List<Phone>();
            this.Address = new Address();
            this.StoreMemberDescriptions = new List<StoreMemberDescriptionItem>();

        }
        public string Email { get; set; }
        public int MemberMainPartyId { get; set; }
        public bool IsWhatsappUsing { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }

        public List<StoreMemberDescriptionItem> StoreMemberDescriptions { get; set; }
    }
}