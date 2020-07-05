using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTHomeProductsRelatedCategoryModel
    {
        public MTHomeProductsRelatedCategoryModel()
        {
            this.Categories = new List<MTHomeCategoryModel>();
            this.Products = new List<MTHomeProductRelatedItem>();
            this.SubCategories = new List<MTHomeCategoryModel>();
        }
        public List<MTHomeCategoryModel> Categories { get; set; }
        public List<MTHomeProductRelatedItem> Products { get; set; }
        public List<MTHomeCategoryModel> SubCategories { get; set; }
    }
}