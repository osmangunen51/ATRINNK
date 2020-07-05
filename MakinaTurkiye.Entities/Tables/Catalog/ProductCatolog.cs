namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductCatolog:BaseEntity
    {
        public int ProductCatologId { get; set; }
        public int ProductId { get; set; }
        public string FileName { get; set; }

        //public virtual Product Product { get; set; }
    }
}
