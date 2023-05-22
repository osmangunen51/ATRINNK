using Trinnk.Utilities.Mvc;

namespace NeoSistem.Trinnk.Web.Models.Catalog
{
    public class MTCategorySearchModel : BaseTrinnkModel
    {
        public MTCategorySearchModel()
        {
            this.CategoryModel = new MTProductCategoryModel();
        }
        public MTProductCategoryModel CategoryModel { get; set; }
        public string SearchText { get; set; }
        public int TotalCount { get; set; }


    }
}