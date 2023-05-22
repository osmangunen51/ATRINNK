using NeoSistem.Trinnk.Web.Models.ViewModels;

namespace NeoSistem.Trinnk.Web.Models.StoreNews
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