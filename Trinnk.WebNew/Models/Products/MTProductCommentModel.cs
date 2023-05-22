using NeoSistem.Trinnk.Web.Models.ViewModels;

namespace NeoSistem.Trinnk.Web.Models.Products
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