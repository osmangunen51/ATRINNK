using MakinaTurkiye.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Payments
{
    public class CreditCardInstallmentMap : EntityTypeConfiguration<CreditCardInstallment>
    {
        public CreditCardInstallmentMap()
        {
            this.ToTable("CreditCardInstallment");
            this.Ignore(ccl => ccl.Id);
            this.HasKey(ccl => ccl.CreditCardInstallmentId);
        }
    }
}
