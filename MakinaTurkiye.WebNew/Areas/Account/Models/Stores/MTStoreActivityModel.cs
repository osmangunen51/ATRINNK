using NeoSistem.MakinaTurkiye.Web.Models.Home;
using NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores
{
    public class MTStoreActivityModel
    {
        public MTStoreActivityModel()
        {
            this.Categories = new List<SelectListItem>();
            this.StoreActivityCategories = new Dictionary<int, string>();

        }
        public LeftMenuModel LeftMenu { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public Dictionary<int,string> StoreActivityCategories { get; set; }
    }
}