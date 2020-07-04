namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using MakinaTurkiye.Web.Models.ViewModels;
    using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
  public class FavoriteStoreModel
  {
    public int FavoriteMainPartyId { get; set; }

    public int MemberMainPartyId { get; set; }

    public int StoreMainPartyId { get; set; }

    public SearchModel<StoreModel> GetStore { get; set; }

    public LeftMenuModel LeftMenu { get; set; }


  }
}