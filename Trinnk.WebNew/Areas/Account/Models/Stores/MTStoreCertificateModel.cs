using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Stores
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