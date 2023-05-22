namespace NeoSistem.Trinnk.Web.Models.StoreProfiles
{
    public class MTPopularProductsModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductUrl { get; set; }
        public string ProductNameTruncated { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string PriceText { get; set; }
        public string CurrencyCodeName { get; set; }
        public int? FavoriteProductId { get; set; }
        public bool HasVideo { get; set; }
        public string ProductPriceWithDiscount { get; set; }

    }
}