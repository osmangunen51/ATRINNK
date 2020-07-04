using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTProductsPageProductList
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MainPicture { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ProductPrice { get; set; }
        public string Currency { get; set; }
        public string ProductUrl { get; set; }
        public decimal? productrate { get; set; }
        public string ProductNo { get; set; }
        public string ProductImagePath { get; set; }
        public int ModelYear { get; set; }
        public string BriefDetailText { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductSalesTypeText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductDescription { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public int? FavoriteProductId { get; set; }
        public bool HasVideo { get; set; }
        public string ProductPriceDiscount { get; set; }
    }
}