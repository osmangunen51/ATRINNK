using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores
{
    public class MTStoreCertificateItemModel
    {
        public MTStoreCertificateItemModel()
        {
            this.PhotoPaths = new List<string>();
        }
        public string CertificateName { get; set; }
        public int StoreCertificateId { get; set; }
        public List<string> PhotoPaths { get; set; }

    }
}