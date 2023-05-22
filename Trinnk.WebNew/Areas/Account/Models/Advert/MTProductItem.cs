namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Advert
{
    public class MTProductItem
    {
        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string SeriesName { get; set; }
        public string ProductPrice { get; set; }
        public string ModelYear { get; set; }
        public long ViewCount { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductStatusText { get; set; }
        public string BriefDetail { get; set; }
        public string SalesTypeText { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public byte? ProductActiveType { get; set; }
        public bool ProductActive { get; set; }
        public byte? ProductPriceType { get; set; }
        public byte CurrencyId { get; set; }
        public string CurrencyCssText { get; set; }
        public bool Doping { get; set; }
        public int Sort { get; set; }
        public decimal? ProductPriceDecimal { get; set; }
        public string ProductPriceWithDiscount { get; set; }
        public decimal? ProductPriceWithDiscountDecimal { get; set; }
        public bool IsKdv { get; set; }
        public bool ShowDopingForm { get; set; }
    }
}