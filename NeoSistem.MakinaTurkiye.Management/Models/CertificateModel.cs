using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class CertificateModel
    {
        public CertificateModel()
        {
            this.CertificateItems = new List<CertificateItemModel>();
            this.CertificateItemModel = new CertificateItemModel();
        }
        public CertificateItemModel CertificateItemModel { get; set; }
       
        public List<CertificateItemModel> CertificateItems { get; set; }
    }

    public class CertificateItemModel
    {
        public int CertificateTypeId { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
    }
}