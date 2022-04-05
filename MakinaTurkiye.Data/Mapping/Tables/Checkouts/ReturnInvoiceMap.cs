using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Checkouts
{
    public class ReturnInvoiceMap : EntityTypeConfiguration<ReturnInvoice>
    {
        public ReturnInvoiceMap()
        {
            this.ToTable("ReturnInvoice");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ReturnInvoiceId);

        }

    }
}
