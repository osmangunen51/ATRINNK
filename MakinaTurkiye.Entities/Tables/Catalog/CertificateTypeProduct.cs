using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class CertificateTypeProduct : BaseEntity
    {
        public int CertificateTypeProductId { get; set; }
        public int? ProductId { get; set; }
        public int? StoreCertificateId { get; set; }
        public int CertificateTypeId { get; set; }


    }
}
