namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using MakinaTurkiye.Web.Models.ViewModels;
    using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;

  public class FavoriteProductModel
  {
    public int FavoriteProductId { get; set; }

    public int MainPartyId { get; set; }

    public int ProductId { get; set; }

    public SearchModel<ProductModel> GetProduct { get; set; }

    public LeftMenuModel LeftMenu { get; set; }

  }
}