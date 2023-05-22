using NeoSistem.Trinnk.Web.Models.Catalog;
using NeoSistem.Trinnk.Web.Models.Helpers;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.FavoriteProducts
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