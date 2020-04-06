namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class SiteMapCategoryResult
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public byte CategoryType { get; set; }

        public int? CategoryParentId { get; set; }

        public string CategoryContentTitle { get; set; }

        public string TopCategoryName { get; set; }

        public int? TopCategoryParentId { get; set; }

        public string TopCategoryContentTitle { get; set; }

        public string ProductGroupName { get; set; }

        public string BrandName { get; set; }

        public string FirstCategoryName { get; set; }

    }
}
