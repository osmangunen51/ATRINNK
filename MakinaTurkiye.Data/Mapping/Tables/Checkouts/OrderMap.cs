using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.ToTable("Order");
            this.Ignore(o => o.Id);
            this.HasKey(o => o.OrderId);

            //TODO:İlişkilendirmeler yapılacak

        }
    }
}
