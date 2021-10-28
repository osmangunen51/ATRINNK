using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductTabModel
    {

        public class CertificatesItem
        {
            public string Name { get; set; } = "";
            public string File { get; set; } = "";
        }

        public MTProductTabModel()
        {
            this.VideoModels = new Dictionary<int, List<MTProductVideoModel>>();
            this.MTProductTechnicalInfoItems = new List<MTProductTechnicalInfoItem>();
            this.MTProductCatologs = new List<MTProductCatologItem>();
            this.MTProductComment = new MTProductCommentModel();
            // this.ProductKeywords = new List<MTProductKeywordItem>();
            this.Certificates = new List<CertificatesItem>();
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
        public IList<CertificatesItem> Certificates { get; set; }
    }
}