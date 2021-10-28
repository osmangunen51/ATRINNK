using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;

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