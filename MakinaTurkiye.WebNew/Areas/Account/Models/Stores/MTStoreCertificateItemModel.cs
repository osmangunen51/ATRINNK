using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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