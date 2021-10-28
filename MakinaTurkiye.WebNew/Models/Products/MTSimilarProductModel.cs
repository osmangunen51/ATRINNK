using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTSimilarProductModel
    {
        public MTSimilarProductModel()
        {
            this.ProductItemModels = new List<MTSimilarProductItemModel>();
        }

        public string AllSimilarProductUrl { get; set; }
        public List<MTSimilarProductItemModel> ProductItemModels { get; set; }
    }
}