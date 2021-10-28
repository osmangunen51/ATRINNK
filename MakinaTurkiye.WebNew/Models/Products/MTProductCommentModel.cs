using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductCommentModel
    {
        public MTProductCommentModel()
        {
            this.MTProductCommentItems = new SearchModel<MTProductCommentItem>();
        }
        public int TotalProductComment { get; set; }
        public SearchModel<MTProductCommentItem> MTProductCommentItems { get; set; }
    }
}