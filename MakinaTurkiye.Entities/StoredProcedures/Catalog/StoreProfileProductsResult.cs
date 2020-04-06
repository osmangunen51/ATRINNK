namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class StoreProfileProductsResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MainPicture { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public decimal? ProductPrice { get; set; }
        public byte? ProductPriceType { get; set; }
        public decimal? ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }
        public string CurrencyCodeName { get; set; }
        public byte? CurrencyId { get; set; }
        public decimal? productrate { get; set; }
        public string ProductNo { get; set; }

        public int? ModelYear { get; set; }
        public string BriefDetailText { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductSalesTypeText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductDescription { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public int? Sort { get; set; }
        public int? FavoriteProductId { get; set; }
        public bool HasVideo { get; set; }
        public byte? DiscountType { get; set; }
        public decimal? ProductPriceWithDiscount { get; set; }

    }
}
