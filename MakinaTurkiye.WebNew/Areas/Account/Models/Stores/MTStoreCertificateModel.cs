using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores
{
    public class MTStoreCertificateModel
    {
        public MTStoreCertificateModel()
        {
            this.StoreCertificateItemModels = new List<MTStoreCertificateItemModel>();
            this.LeftMenu = new LeftMenuModel();
        }
        public LeftMenuModel LeftMenu { get; set; }
        public List<MTStoreCertificateItemModel> StoreCertificateItemModels { get; set; }
    }
}