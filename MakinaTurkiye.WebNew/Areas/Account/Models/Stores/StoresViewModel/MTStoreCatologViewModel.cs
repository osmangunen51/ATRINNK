using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel
{
    public class MTStoreCatologViewModel
    {
        public MTStoreCatologViewModel()
        {
            this.LeftMenuModel = new LeftMenuModel();
            this.MTCatologItems = new List<MTCatologItem>();
        }
        public List<MTCatologItem> MTCatologItems { get; set; }
        public LeftMenuModel LeftMenuModel { get; set; }
    }
}