using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class PaymentMap : EntityTypeConfiguration<Payment>
    {
        public PaymentMap()
        {
            this.ToTable("Payment");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.PaymentId);

            this.HasRequired(x => x.Order).WithMany(y => y.Payments).HasForeignKey(x => x.OrderId);

        }
    }
}
