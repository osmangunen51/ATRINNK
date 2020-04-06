using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class CertificateTypeProductMap:EntityTypeConfiguration<CertificateTypeProduct>
    {
        public CertificateTypeProductMap()
        {
            this.ToTable("CertificateTypeProduct");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.CertificateTypeProductId);
        }

    }
}
