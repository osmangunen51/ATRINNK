using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos
{
    public class MTStoreVideoModel
    {
        public MTStoreVideoModel()
        {
            this.StoreVideoItemModels = new List<MTStoreVideoItemModel>();
            this.LeftMenu = new LeftMenuModel();
        }
        public List<MTStoreVideoItemModel> StoreVideoItemModels { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}