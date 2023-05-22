using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class BrowseDescModel
    {
        public BrowseDescModel()
        {
            this.BaseMemberDescriptionModelItems = new List<BaseMemberDescriptionModelItem>();
            this.Users = new List<SelectListItem>();
        }
        public List<BaseMemberDescriptionModelItem> BaseMemberDescriptionModelItems { get; set; }
        public List<SelectListItem> Users { get; set; }
        public string MemberNameSurname { get; set; }
        public string StoreName { get; set; }
        public int? AuthorizedId { get; set; }
        public int MemberMainPartyId { get; set; }
        public string AuthName { get; set; }
        public int StoreMainPartyId { get; set; }
        public int PreRegistrationStoreId { get; set; }
        public byte RegistrationType { get; set; }
        public bool? IsProductAdded { get; set; }


    }
}