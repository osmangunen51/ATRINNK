namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;

    public class ProductViewModel
  {
    public SearchModel<ProductModel> ProductItems { get; set; }

    public LeftMenuModel LeftMenu { get; set; }

  }
}