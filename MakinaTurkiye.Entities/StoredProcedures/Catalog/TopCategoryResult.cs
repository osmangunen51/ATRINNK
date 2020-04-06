namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class TopCategoryResult
    {
        public int CategoryOrderId { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public byte CategoryType { get; set; }
        public int CategoryId { get; set; }
        public int? ProductCount { get; set; }
        public string CategoryContentTitle { get; set; }
        public string StorePageTitle { get; set; }
        public string CategoryUrl { get; set; }
    }
}
