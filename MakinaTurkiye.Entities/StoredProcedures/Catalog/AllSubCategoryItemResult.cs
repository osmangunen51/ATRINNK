namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class AllSubCategoryItemResult
    {

        public int CategoryId { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryContentTitle { get; set; }
        public string CategoryName { get; set; }
        public string StorePageTitle { get; set; }
        public string Content { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public byte? CategoryType { get; set; }
        public string Title { get; set; }

    }
}
