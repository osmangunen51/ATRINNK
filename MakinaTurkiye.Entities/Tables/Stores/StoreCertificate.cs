using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreCertificate:BaseEntity
    {

        public int StoreCertificateId { get; set; }
        public int MainPartyId { get; set; }
        public string CertificateName { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Active { get; set; }
        public int? Order { get; set; }
        public virtual Store Store { get; set; }
    }
}
