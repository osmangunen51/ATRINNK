
using Trinnk.Entities.Tables.Catalog;
using NeoSistem.Trinnk.Web.Models.Videos;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Catalog
{
    public class MTCategoryProductViewModel
    {
        public MTCategoryProductViewModel()
        {
            this.CategoryModel = new MTProductCategoryModel();
            this.FilteringContext = new MTProductFilteringModel();
            this.PagingModel = new MTProductPagingModel();
            this.CategoryProductModels = new List<MTCategoryProductModel>();
            this.StoreModel = new MTCategoryStoreModel();
            this.BannerModels = new List<MTBannerModel>();
            this.VideoModels = new List<MTPopularVideoModel>();
            this.MostViewedProductModel = new MTMostViewedProductModel();
            this.SeoModel = new MTCategorySeoModel();
            this.RandomProducts = new List<MTRandomProductItemModel>();
            this.MTCategoSliderItems = new List<MTCategorySliderItem>();
        }

        public MTProductPagingModel PagingModel { get; set; }
        public MTProductFilteringModel FilteringContext { get; set; }
        public MTProductCategoryModel CategoryModel { get; set; }
        public MTCategoryStoreModel StoreModel { get; set; }
        public List<MTPopularVideoModel> VideoModels { get; set; }
        public List<MTBannerModel> BannerModels { get; set; }
        public MTMostViewedProductModel MostViewedProductModel { get; set; }
        public MTCategorySeoModel SeoModel { get; set; }
        public List<MTCategorySliderItem> MTCategoSliderItems { get; set; }
        public int TotalItemCount { get; set; }
        public string ContentSide { get; set; }
        public string ContentBottomCenter { get; set; }
        public bool NoIndex { get; set; }
        public string SpesificBrandName { get; set; }
        public string NextPageUrl { get; set; }
        public string PrevPageUrl { get; set; }
        public string SameCategoryH1 { get; set; }
        public string SpesificCategoryNameForModelH1 { get; set; }
        public string SearchText { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public int LocalityId { get; set; }
        public string AllCategoryUrl { get; set; }
        public string ConstantTitle { get; set; }
        public string ConstantProperty { get; set; }
        public IList<MTRandomProductItemModel> RandomProducts { get; set; }
        public IList<MTCategoryProductModel> CategoryProductModels { get; set; }
        public IList<Category> ParentCategoryItems { get; set; }
        public IList<Category> ProductGroupParentItems { get; set; }
        public IList<Category> ProductGroupFirstCategoryParentItems { get; set; }

    }
}