using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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