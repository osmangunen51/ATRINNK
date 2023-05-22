namespace NeoSistem.Trinnk.Web.Models.Catalog
{
    public class MTProductCategoryItemModel
    {
        public int CategoryId { get; set; }

        public byte CategoryType { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public string CategoryUrl { get; set; }
        public string TruncatedCategoryName { get; set; }
        public string CategoryContentTitle { get; set; }
    }
}