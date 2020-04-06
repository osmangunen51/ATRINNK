using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTHomeLeftCategoriesModel
    {
        public MTHomeLeftCategoriesModel()
        {
            this.HomeLeftAllSectors = new List<MTHomeCategoryModel>();
            this.HomeLeftChoicedCategories = new List<MTHomeCategoryModel>();
        }
        public IList<MTHomeCategoryModel> HomeLeftChoicedCategories { get; set; }
        public IList<MTHomeCategoryModel> HomeLeftAllSectors { get; set; }
    }
}