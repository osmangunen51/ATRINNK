using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Home
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