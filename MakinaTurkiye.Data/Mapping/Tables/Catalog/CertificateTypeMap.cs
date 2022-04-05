using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class CertificateTypeMap : EntityTypeConfiguration<CertificateType>
    {
        public CertificateTypeMap()
        {
            this.ToTable("CertificateType");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.CertificateTypeId);
        }

    }
}
