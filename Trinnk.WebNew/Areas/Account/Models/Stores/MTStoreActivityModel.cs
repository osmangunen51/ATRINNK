using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Stores
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

        public Dictionary<int, string> StoreActivityCategories { get; set; }
    }
}