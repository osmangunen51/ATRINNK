using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class StoreCertificateItem
    {
        public int CertificateId { get; set; }
        public string Name { get; set; }
        public List<string> Image { get; set; } = new List<string>() { };
        public int Type { get; set; }
        public int Sira { get; set; }
    }

    public class StoreCertificate
    {
        public List<StoreCertificateItem> List { get; set; } = new List<StoreCertificateItem>();
        public int MainPartyId { get; set; } = 0;
    }
}