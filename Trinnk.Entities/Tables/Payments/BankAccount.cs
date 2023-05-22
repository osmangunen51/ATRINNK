namespace Trinnk.Entities.Tables.Payments
{
    public class BankAccount : BaseEntity
    {
        public byte AccountId { get; set; }
        public string AccountName { get; set; }

        public string AccountNo { get; set; }

        public string BranchName { get; set; }

        public string BranchCode { get; set; }

        public string BankName { get; set; }

        public string IbanNo { get; set; }

        public bool Active { get; set; }
    }
}
