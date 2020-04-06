namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class WebSearchProductResult
    {
        public int ProductSearchId { get; set; }
        public int ProductId { get; set; }
        public string FullSearxhName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public bool? ProductActive { get; set; }
        public string StoreName { get; set; }
        public string BriefDetailText { get; set; }
        public string ProductStatuText { get; set; }
        public string ProductTypeText { get; set; }
        public string ProductSalesTypeText { get; set; }
        public int? ModelYear { get; set; }
        public string CategoryProductGroupName { get; set; }
        public string CategoryName { get; set; }
        public string MainPicture { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string CurrencyName { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public string ProductDescription { get; set; }
        public byte? ProductActiveType { get; set; }
        public int? CategoryId { get; set; }
        public decimal? productrate { get; set; }
        public string CategoryTreeName { get; set; }
        public int? BrandId { get; set; }
        public int? SeriesId { get; set; }
        public int? ModelId { get; set; }
    }
}
