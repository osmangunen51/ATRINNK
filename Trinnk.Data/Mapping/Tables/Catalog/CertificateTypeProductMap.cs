using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class CertificateTypeProductMap : EntityTypeConfiguration<CertificateTypeProduct>
    {
        public CertificateTypeProductMap()
        {
            this.ToTable("CertificateTypeProduct");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.CertificateTypeProductId);
        }

    }
}
