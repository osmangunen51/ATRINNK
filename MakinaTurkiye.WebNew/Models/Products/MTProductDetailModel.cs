using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductDetailModel
    {
      

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string BrandName { get; set; }
        public string BrandUrl { get; set; }
        public string ModelName { get; set; }
        public string ModelUrl { get; set; }
        public string ModelYear { get; set; }
        public string ProductType { get; set; }
        public string ProductStatus { get; set; }
      public string ProductCurrencySymbol { get; set; }
        //public byte? ProductActiveType { get; set; }
        //public bool? ProductActive { get; set; }
        public string MenseiName { get; set; }
        public string DeliveryStatus { get; set; }
        public string AdvertBeginDate { get; set; }//dd.MM.yyyy
        public string AdvertEndDate { get; set; }//dd.MM.yyyy
        public string Location { get; set; }
        public string BriefDetail { get; set; }
        public string WarriantyPerriod { get; set; }
        public string SalesType { get; set; }
        public string Price { get; set; }
        public string ProductPriceWithDiscount { get; set; }
        public byte? ProductPriceType { get; set; }
        public decimal ? PriceDec { get; set; }
        public string PriceWithoutCurrency { get; set; }
        public string Currency { get; set; }
        public string ButtonDeliveryStatusText { get; set; }
        public bool IsActive { get; set; }
        public int ProductStatuConstantId { get; set; }
        public string ProductContactUrl { get; set; }
        public string UnitTypeText { get; set; }
        public bool Kdv { get; set; }
        public bool Fob { get; set; }
        public long ProductViewCount { get; set; }
        public string StoreName { get; set; }
        public bool IsAllowProductSellUrl { get; set; }
        public string ProductSellUrl { get; set; }
        public bool ProductActive { get; set; }
        public string Certificates { get; set; }

        public bool IsFavoriteProduct { get; set; }

        public byte? MinumumAmount { get; set; }
        public MTProductComplainModel ProductComplainModel { get; set; }
    }
}