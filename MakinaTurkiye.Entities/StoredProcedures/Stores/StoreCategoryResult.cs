namespace MakinaTurkiye.Entities.StoredProcedures.Stores
{
    public class StoreCategoryResult
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public byte CategoryType { get; set; }
        public string CategoryContentTitle { get; set; }
        public int StoreCount { get; set; }
        public string StorePageTitle { get; set; }
    }
}
