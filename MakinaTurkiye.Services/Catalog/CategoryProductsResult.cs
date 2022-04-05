using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public class CategoryProductsResult
    {
        public CategoryProductsResult()
        {
            this.Products = new List<WebCategoryProductResult>();
            this.FilterableCategoryIds = new List<FilterableCategoriesResult>();
            this.FilterableBrandIds = new List<int>();
            this.FilterableCityIds = new List<int>();
            this.FilterableCountryIds = new List<int>();
            this.FilterableLocalityIds = new List<int>();
            this.FilterableModelIds = new List<int>();
            this.FilterableSeriesIds = new List<int>();
        }

        public IList<WebCategoryProductResult> Products { get; set; }

        public IList<int> FilterableCountryIds { get; set; }
        public IList<int> FilterableCityIds { get; set; }
        public IList<int> FilterableLocalityIds { get; set; }
        public IList<FilterableCategoriesResult> FilterableCategoryIds { get; set; }
        public IList<int> FilterableBrandIds { get; set; }
        public IList<int> FilterableModelIds { get; set; }
        public IList<int> FilterableSeriesIds { get; set; }

        public int NewProductCount { get; set; }
        public int UsedProductCount { get; set; }
        public int ServicesProductCount { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}
