using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            this.HasKey(i => i.InvoiceId);
            this.Ignore(i => i.Id);

        }
    }
}
