using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Home
{
    public class MTHomeModel
    {

        public MTHomeModel()
        {
            this.CategoryModels = new List<MTHomeCategoryModel>();
            this.PopularVideoModels = new List<MTHomeVideoModel>();
            this.SliderBannerMoldes = new List<MTHomeBannerModel>();
            this.ShowCaseProducts = new List<MTHomeAdModel>();
            this.StoreModels = new List<MTHomeStoreModel>();
            this.HomeLeftCategoriesModel = new MTHomeLeftCategoriesModel();
            this.HomeProductsRelatedCategoryModel = new MTHomeProductsRelatedCategoryModel();
            this.MTMayLikeProductModel = new MTMayLikeProductModel();
            this.StoreNewItems = new List<MTStoreNewItem>();
            //this.SuccessStories = new List<MTStoreNewItem>();
            this.HomeSectorItems = new List<MTHomeSectorItem>();
            this.MTAllSelectedProduct = new List<MTAllSelectedProductModel>();
        }


        public List<MTHomeCategoryModel> CategoryModels { get; set; }
        public List<MTHomeVideoModel> PopularVideoModels { get; set; }
        public List<MTHomeBannerModel> SliderBannerMoldes { get; set; }
        public List<MTHomeAdModel> ShowCaseProducts { get; set; }
        public List<MTHomeStoreModel> StoreModels { get; set; }
        public MTHomeLeftCategoriesModel HomeLeftCategoriesModel { get; set; }
        public MTHomeProductsRelatedCategoryModel HomeProductsRelatedCategoryModel { get; set; }
        public MTMayLikeProductModel MTMayLikeProductModel { get; set; }
        public List<MTStoreNewItem> StoreNewItems { get; set; }
        //public List<MTStoreNewItem> SuccessStories { get; set; }
        public List<MTHomeSectorItem> HomeSectorItems { get; set; }
        public List<MTAllSelectedProductModel> MTAllSelectedProduct { get; set; }
        public List<MTHomeAdModel> PopularAdModels { get; set; } = new List<MTHomeAdModel>();
        public List<MTHomeAdModel> NewsAdModels { get; set; } = new List<MTHomeAdModel>();

        public string ConstantTitle { get; set; }
        public string ConstantProperty { get; set; }

    }
}