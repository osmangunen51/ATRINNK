using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Users
{
    public class MTUserItemModel
    {
        public int MainPartyId { get; set; }
        public string Email { get; set;}
        public string Password { get; set; }
        public string NameSurname { get; set; }
        public DateTime RecordDate { get;set; }
        public bool? Active { get; set; }

    }
}