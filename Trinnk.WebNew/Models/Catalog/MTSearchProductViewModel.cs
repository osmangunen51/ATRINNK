using Trinnk.Utilities.Mvc;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Catalog
{
    public class MTSearchProductViewModel : BaseTrinnkModel
    {
        public MTSearchProductViewModel()
        {
            this.CategoryModel = new MTProductCategoryModel();
            this.SearchProductModels = new List<MTSearchProductModel>();
            this.PagingModel = new MTProductPagingModel();
            this.FilteringContext = new MTProductFilteringModel();
        }

        public MTProductCategoryModel CategoryModel { get; set; }
        public IList<MTSearchProductModel> SearchProductModels { get; set; }
        public MTProductPagingModel PagingModel { get; set; }
        public MTProductFilteringModel FilteringContext { get; set; }

        public string SearchText { get; set; }
    }
}