namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class PopularProductResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductPicturePath { get; set; }
    }
}
