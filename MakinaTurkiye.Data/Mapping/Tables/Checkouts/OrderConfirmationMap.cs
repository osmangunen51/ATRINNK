using MakinaTurkiye.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
