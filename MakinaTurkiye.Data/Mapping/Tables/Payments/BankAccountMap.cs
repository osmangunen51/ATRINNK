using MakinaTurkiye.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Payments
{
    public class BankAccountMap: EntityTypeConfiguration<BankAccount>
    {
        public BankAccountMap()
        {
            this.ToTable("Account");
            this.Ignore(a => a.Id);
            this.HasKey(a => a.AccountId);
        }
    }
}
