using NeoSistem.Trinnk.Web.Models.ViewModels;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Advert
{
    public class MTCommentsViewModel
    {
        public MTCommentsViewModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.ProductCommentStoreItems = new SearchModel<MTProductCommentStoreItem>();
        }
        public LeftMenuModel LeftMenu { get; set; }
        public SearchModel<MTProductCommentStoreItem> ProductCommentStoreItems { get; set; }
    }
}