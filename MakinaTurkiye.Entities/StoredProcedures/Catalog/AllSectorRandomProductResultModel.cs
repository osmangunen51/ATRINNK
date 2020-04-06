namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class AllSectorRandomProductResultModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public decimal? ProductPriceBegin { get; set; }
        public decimal? ProductPriceLast { get; set; }
        public string CurrencyName { get; set; }
        public string MainPicture { get; set; } 
    }
}
