using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductTabModel
    {
        public MTProductTabModel()
        {
            this.VideoModels = new Dictionary<int, List<MTProductVideoModel>>();
            this.MTProductTechnicalInfoItems = new List<MTProductTechnicalInfoItem>();
            this.MTProductCatologs = new List<MTProductCatologItem>();
            this.MTProductComment = new MTProductCommentModel();
           // this.ProductKeywords = new List<MTProductKeywordItem>();
            this.Certificates = new Dictionary<string, string>();
        }

        public string ProductDescription { get; set; }

       // public List<MTProductKeywordItem> ProductKeywords { get; set; }
        public string MapCode { get; set; }
        public long ProductViewCount { get; set; }
        public string ProductName { get; set; }
        public string ProductSeoDescription { get; set; }
        public List<MTProductTechnicalInfoItem> MTProductTechnicalInfoItems { get; set; }
        public Dictionary<int, List<MTProductVideoModel>> VideoModels { get; set; }
        public List<MTProductCatologItem> MTProductCatologs { get; set; }
        public MTProductCommentModel MTProductComment { get; set; }
        public Dictionary<string, string> Certificates { get; set; }
    }
}