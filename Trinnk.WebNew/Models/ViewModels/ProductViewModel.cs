namespace NeoSistem.Trinnk.Web.Models.ViewModels
{
    using NeoSistem.Trinnk.Web.Areas.Account.Models;

    public class ProductViewModel
    {
        public SearchModel<ProductModel> ProductItems { get; set; }

        public LeftMenuModel LeftMenu { get; set; }

    }
}