namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class WebCategoryProductResult
    {
        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? SeriesId { get; set; }
        public int? ModelId { get; set; }
        public int? MainPartyId { get; set; }
        public int? ModelYear { get; set; }
        public string MainPicture { get; set; }

        public string BrandName { get; set; }
        public string ModelName { get; set; }

        public string CurrencyCodeName { get; set; }

        public string StoreName { get; set; }

        public string BriefDetailText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductSalesTypeText { get; set; }

        public bool Doping { get; set; }

        public bool? HasVideo { get; set; }
        public int? StoreMainPartyId { get; set; }
        public byte? ProductPriceType { get; set; }

        public decimal? ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }
        public bool? Kdv { get; set; }
        public bool? Fob { get; set; }
        public int? FavoriteProductId { get; set; }
        public byte? DiscountType { get; set; }
        public decimal? ProductPriceWithDiscount { get; set; }
        public string UnitType { get; set; }
        public byte? MinumumAmount { get; set; }
        //public string ProductType { get; set; }
        //public string ProductStatu { get; set; }
        //public string BriefDetail { get; set; }
        //public string ProductSalesType { get; set; }
    }
}
