namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class SiteMapStoreCategoryResult
    {
        public int CategoryId { get; set; }

        public int? CategoryParentId { get; set; }

        public byte CategoryType { get; set; }

        public string CategoryName { get; set; }

        public string StorePageTitle { get; set; }

        public string CategoryContentTitle { get; set; }
    }
}
