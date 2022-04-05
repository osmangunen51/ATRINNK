using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class OrderDescriptionMap : EntityTypeConfiguration<OrderDescription>
    {
        public OrderDescriptionMap()
        {
            this.ToTable("OrderDescription");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.OrderDescriptionId);

        }
    }
}
