using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.OtherSettings
{
    public class OtherSettingsProductModel
    {
        public OtherSettingsProductModel()
        {
            this.MTProductItems = new SearchModel<MTProductItem>();
            this.LeftMenuModel = new LeftMenuModel();
        }
        public SearchModel<MTProductItem> MTProductItems { get; set; }
        public LeftMenuModel LeftMenuModel { get; set; }
    }
}