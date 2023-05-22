namespace NeoSistem.Trinnk.Web.Models
{
    using Trinnk.Web.Models.ViewModels;
    using NeoSistem.Trinnk.Web.Areas.Account.Models;
    public class FavoriteStoreModel
    {
        public int FavoriteMainPartyId { get; set; }

        public int MemberMainPartyId { get; set; }

        public int StoreMainPartyId { get; set; }

        public SearchModel<StoreModel> GetStore { get; set; }

        public LeftMenuModel LeftMenu { get; set; }


    }
}