using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTCategoryProductModel
    {
        //test zorunlu

        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? SeriesId { get; set; }
        public int? ModelId { get; set; }
        public int? MainPartyId { get; set; }
        public int? ModelYear { get; set; }
        public string PicturePath { get; set; }
        //public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        //public string SeriesName { get; set; }
        public string StoreName { get; set; }
        public string BriefDetailText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductSalesTypeText { get; set; }
        public bool Doping { get; set; }
        public string ProductDescription { get; set; }
        public string CurrencyCss { get; set; }
        public decimal ProductPiceDesc { get; set; }
        public string CurrencySymbol { get; set; }
        public byte? ProductPriceType { get;set;}
       // public int? StoreId { get; set; }
        public string StoreConnectUrl { get; set; }
        public bool HasVideo { get; set; }
        public string StoreUrl { get; set; }
        public int? StoreMainPartyId { get; set;}
        public string ProductContactUrl { get; set; }
        public string KdvOrFobText { get; set; }
        public int? FavoriteProductId { get; set; }
        public string ProductPriceWithDiscount { get; set; }
        
    }
}