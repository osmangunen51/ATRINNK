namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class CategoryForStoreProfileResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte CategoryType { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryContentTitle { get; set; }
    }
}
