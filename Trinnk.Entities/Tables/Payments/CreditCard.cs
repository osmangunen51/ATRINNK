namespace Trinnk.Entities.Tables.Payments
{
    public class CreditCard : BaseEntity
    {
        public byte CreditCardId { get; set; }
        public string CreditCardName { get; set; }

        public byte VirtualPosId { get; set; }

        public bool Active { get; set; }
    }
}
