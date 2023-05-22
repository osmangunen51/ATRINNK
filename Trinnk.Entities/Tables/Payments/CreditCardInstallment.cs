namespace Trinnk.Entities.Tables.Payments
{
    public class CreditCardInstallment : BaseEntity
    {
        public short CreditCardInstallmentId { get; set; }
        public byte CreditCardId { get; set; }
        public decimal CreditCardValue { get; set; }
        public byte CreditCardCount { get; set; }

    }
}
