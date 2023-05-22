namespace Trinnk.Entities.Tables.Catalog
{
    public class CertificateTypeProduct : BaseEntity
    {
        public int CertificateTypeProductId { get; set; }
        public int? ProductId { get; set; }
        public int? StoreCertificateId { get; set; }
        public int CertificateTypeId { get; set; }


    }
}
