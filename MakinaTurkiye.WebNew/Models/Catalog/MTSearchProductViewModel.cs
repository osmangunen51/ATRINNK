using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakinaTurkiye.Utilities.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTSearchProductViewModel : BaseMakinaTurkiyeModel
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