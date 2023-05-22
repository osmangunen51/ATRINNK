using Trinnk.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Payments
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
