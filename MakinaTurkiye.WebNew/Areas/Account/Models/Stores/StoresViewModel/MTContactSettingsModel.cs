using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel
{
    public class MTContactSettingsModel
    {
        public MTContactSettingsModel()
        {
            this.MTSettingItems = new List<MTSettingItemModel>();
            this.LeftMenu = new LeftMenuModel();
        }
        public List<MTSettingItemModel> MTSettingItems { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}