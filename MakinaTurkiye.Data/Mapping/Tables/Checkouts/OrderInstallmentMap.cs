using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class OrderInstallmentMap : EntityTypeConfiguration<OrderInstallment>
    {
        public OrderInstallmentMap()
        {
            this.ToTable("OrderInstallment");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.OrderInstallmentId);
        }

    }
}
