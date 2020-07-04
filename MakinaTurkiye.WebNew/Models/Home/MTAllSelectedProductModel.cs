using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTAllSelectedProductModel
    {
        public MTAllSelectedProductModel()
        {
            this.CategoryModel = new List<MTHomeCategoryModel>();
            this.Products = new List<MTHomeAdModel>();
        }
        public List<MTHomeCategoryModel> CategoryModel { get; set; }
        public List<MTHomeAdModel> Products { get; set; }
        public int Index { get; set; }
        public string BackgrounCss { get; set; }
        public string TabBackgroundCss { get; set; }
        
    }
}