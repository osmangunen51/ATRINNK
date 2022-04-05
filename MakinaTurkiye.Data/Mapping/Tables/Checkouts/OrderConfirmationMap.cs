using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class OrderConfirmationMap : EntityTypeConfiguration<OrderConfirmation>
    {
        public OrderConfirmationMap()
        {
            this.ToTable("OrderConfirmation");

            this.HasKey(i => i.OrderConfirmationId);
            this.Ignore(i => i.Id);
        }
    }
}
