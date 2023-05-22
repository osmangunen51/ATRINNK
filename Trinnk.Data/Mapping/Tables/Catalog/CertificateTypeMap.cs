using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
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
