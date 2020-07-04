using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores
{
    public class CreateStoreCertificateModel
    {
        public CreateStoreCertificateModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.CertificateTypes = new List<SelectListItem>();
        }
        public string CeritificateName { get; set; }
        public int Order { get; set; }
        public int CertificateTypeId { get; set; }
        public List<SelectListItem> CertificateTypes { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}