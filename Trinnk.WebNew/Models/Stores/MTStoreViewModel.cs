using Trinnk.Utilities.Mvc;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Stores
{
    public class MTStoreViewModel : BaseTrinnkModel
    {
        public MTStoreViewModel()
        {
            this.StoreCategoryModel = new MTStoreCategoryModel();
            this.StoreModels = new List<MTStoreModel>();
            this.FilteringContext = new MTStoreFilteringModel();
            this.StorePagingModel = new MTStorePagingModel();
        }

        public MTStoreFilteringModel FilteringContext { get; set; }
        public MTStoreCategoryModel StoreCategoryModel { get; set; }
        public IList<MTStoreModel> StoreModels { get; set; }
        public MTStorePagingModel StorePagingModel { get; set; }
        public string FAGFSearch { get; set; }
        public string NextPage { get; set; }
        public string PrevPage { get; set; }
        public string CanonicalUrl { get; set; }
        public string SeoContent { get; set; }
        public string RedirectUrl { get; set; }
        public bool ShowFilters { get; set; } = true;
    }
}