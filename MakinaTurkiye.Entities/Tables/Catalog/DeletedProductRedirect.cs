namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class DeletedProductRedirect : BaseEntity
    {
        public int DeletedProductRedirectId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
