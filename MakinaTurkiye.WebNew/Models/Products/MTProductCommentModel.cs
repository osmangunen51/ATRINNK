using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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