namespace Trinnk.Entities.StoredProcedures.Stores
{
    public class ProductForStoreResult
    {
        public int ProductId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string MainPicturePath { get; set; }
        public int CategoryId { get; set; }
        public string ProductGroupName { get; set; }

    }
}
