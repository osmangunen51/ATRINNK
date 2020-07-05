using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class CertificateType:BaseEntity
    {
        public int CertificateTypeId { get; set; }
        public int? InsertedStoreMainPartyId { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Order { get; set; }

        public bool Active { get; set; }

    }
}
