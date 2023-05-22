namespace NeoSistem.Trinnk.Web.Models
{
    using Trinnk.Web.Models.ViewModels;
    using NeoSistem.Trinnk.Web.Areas.Account.Models;

    public class FavoriteProductModel
    {
        public int FavoriteProductId { get; set; }

        public int MainPartyId { get; set; }

        public int ProductId { get; set; }

        public SearchModel<ProductModel> GetProduct { get; set; }

        public LeftMenuModel LeftMenu { get; set; }

    }
}