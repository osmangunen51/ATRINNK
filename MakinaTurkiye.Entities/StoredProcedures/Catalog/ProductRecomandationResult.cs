namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class ProductRecomandationResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductPicturePath { get; set; }
        public int TotalCount { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ProductNo { get; set; }
    }
}
