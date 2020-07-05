using System;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class ReturnInvoice:BaseEntity
    {
        public int ReturnInvoiceId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
