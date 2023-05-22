using Trinnk.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Payments
{
    public class BankAccountMap : EntityTypeConfiguration<BankAccount>
    {
        public BankAccountMap()
        {
            this.ToTable("Account");
            this.Ignore(a => a.Id);
            this.HasKey(a => a.AccountId);
        }
    }
}
