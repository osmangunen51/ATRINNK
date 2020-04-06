using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews
{
    public class MTStoreNewModel
    {
        public MTStoreNewModel()
        {
            this.MTStoreNews = new List<MTStoreNewItem>();
            this.LeftMenu = new LeftMenuModel();
        }

        public int NewType { get; set; }
        public string PageTitle { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        public List<MTStoreNewItem> MTStoreNews { get; set; }
    }
}