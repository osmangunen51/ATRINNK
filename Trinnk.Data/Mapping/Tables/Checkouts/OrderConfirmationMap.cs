using Trinnk.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Checkouts
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
