namespace Trinnk.Entities.Tables.Checkouts
{
    public class Invoice : BaseEntity
    {
        public int InvoiceId { get; set; }
        public int StoreId { get; set; }
        public int OrderId { get; set; }
    }
}
