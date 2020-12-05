using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTHomeCategoryModel
    {
        public MTHomeCategoryModel()
        {
            this.SubCategoryModels = new List<MTHomeCategoryModel>();
        }
        public int? CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryIcon { get; set; }
        public string CategoryUrlName { get; set; }
        public List<MTHomeCategoryModel> SubCategoryModels { get; set; }
    }
}