using System;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class OrderInstallment : BaseEntity
    {
        public int OrderInstallmentId { get; set; }
        public int OrderId { get; set; }
        public int? PaymentId { get; set; }
        public decimal Amunt { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime RecordDate { get; set; }
        public bool? IsPaid { get; set; }


    }
}
