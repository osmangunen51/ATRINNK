using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreCertificateMap:EntityTypeConfiguration<StoreCertificate>
    {
        public StoreCertificateMap()
        {
            this.ToTable("StoreCertificate");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreCertificateId);

            //this.HasRequired(x => x.Store).WithMany(x => x.StoreCertificates).HasForeignKey(x => x.MainPartyId);
        }
    }
}
