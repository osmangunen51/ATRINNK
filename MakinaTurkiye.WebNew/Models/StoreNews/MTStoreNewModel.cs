using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreNews
{
    public class MTStoreNewModel
    {
        public MTStoreNewModel()
        {
            this.MTStoreNews = new SearchModel<MTStoreNewItemModel>();
        }
        public SearchModel<MTStoreNewItemModel> MTStoreNews { get; set; }
    }
}