using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.FavoriteProducts
{
    public class FavoriteProductViewModel
    {
        public FavoriteProductViewModel()
        {
            this.MTCategoryProductModels = new PagingModel<MTCategoryProductModel>();
            this.LeftMenuModel = new LeftMenuModel();
        }
        public PagingModel<MTCategoryProductModel> MTCategoryProductModels { get; set; }
        public LeftMenuModel LeftMenuModel { get; set; }
    }
}