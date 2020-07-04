using System;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class Payment : BaseEntity
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RestAmount { get; set; }
        public DateTime RecordDate { get; set; }
        public byte PaymentType { get; set; }
        public string SenderNameSurname { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Description { get; set; }
        public int? BankConstantId { get; set; }

        public virtual Order Order { get; set; }

    }
}
