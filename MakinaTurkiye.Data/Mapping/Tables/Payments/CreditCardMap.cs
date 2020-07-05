using MakinaTurkiye.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Payments
{
    public class CreditCardMap : EntityTypeConfiguration<CreditCard>
    {
        public CreditCardMap()
        {
            this.ToTable("CreditCard");
            this.Ignore(cc => cc.Id);
            this.HasKey(cc => cc.CreditCardId);
        }
    }
}
