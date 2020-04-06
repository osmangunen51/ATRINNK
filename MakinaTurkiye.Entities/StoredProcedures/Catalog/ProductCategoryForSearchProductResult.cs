namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class ProductCategoryForSearchProductResult:BaseEntity
    {
        public int CategoryId { get; set; }
        public int CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public string CategoryIcon{get;set;}
        public byte? CategoryType { get; set; }
        public string CategoryContentTitle { get; set; }
    }
}
