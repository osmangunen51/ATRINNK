using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class ProductCreateSettingModel
    {
        public ProductCreateSettingModel()
        {
            this.Properties = new List<SelectListItem>();
            this.LeftMenu = new LeftMenuModel();
            this.ProductCreateSettingItems = new List<ProductCreateSettingItem>();
        }
        public List<SelectListItem> Properties;
        public int StoreMainPartyId { get; set; }
        public int PropertyId;
        public LeftMenuModel LeftMenu { get; set; }

        public List<ProductCreateSettingItem> ProductCreateSettingItems { get; set; }
    }
}