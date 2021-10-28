using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
    public class MTStoreModel
    {
        public MTStoreModel()
        {
            this.ProductModels = new List<ProductModel>();
        }

        #region Properties

        public string StoreLogoPath { get; set; }
        public string StoreName { get; set; }
        public string TruncateStoreName { get; set; }
        public string FullActivityTypeName { get; set; }

        public string BrandUrlForStoreProfile { get; set; }
        public string WebSiteUrl { get; set; }
        public string StoreProfileUrl { get; set; }
        public string ProductUrlForStoreProfile { get; set; }

        public string SelectedCategoryName { get; set; }
        public string SelectedCategoryProductUrlForStoreProfile { get; set; }
        public int SelectedCategoryProductCount { get; set; }
        public string StoreShortName { get; set; }
        public string StoreShowName { get; set; }
        public string StoreAbout { get; set; }
        public string SelectedCategoryContentTitle { get; set; }


        public IList<ProductModel> ProductModels { get; set; }

        #endregion


        #region Nested Classes

        public class ProductModel
        {
            public string SmallPicturePath { get; set; }
            public string LargePicturePath { get; set; }
            public string BrandName { get; set; }
            public string ModelName { get; set; }
            public string ProductUrl { get; set; }
            public string SimilarUrl { get; set; }
            public string ProductName { get; set; }
        }

        #endregion
    }
}