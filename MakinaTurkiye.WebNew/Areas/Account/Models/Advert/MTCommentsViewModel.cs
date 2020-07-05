using NeoSistem.MakinaTurkiye.Web.Models.Products;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class MTCommentsViewModel
    {
        public MTCommentsViewModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.ProductCommentStoreItems = new SearchModel<MTProductCommentStoreItem>();
        }
        public LeftMenuModel LeftMenu { get; set; }
        public SearchModel<MTProductCommentStoreItem> ProductCommentStoreItems { get; set; }
    }
}