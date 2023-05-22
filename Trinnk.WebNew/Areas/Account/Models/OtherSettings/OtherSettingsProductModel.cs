using NeoSistem.Trinnk.Web.Areas.Account.Models.Advert;
using NeoSistem.Trinnk.Web.Models.ViewModels;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.OtherSettings
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