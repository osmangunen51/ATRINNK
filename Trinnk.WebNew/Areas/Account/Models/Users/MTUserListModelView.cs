using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Users
{
    public class MTUserListModelView
    {
        public MTUserListModelView()
        {
            this.MTUserItems = new List<MTUserItemModel>();
            this.LeftMenu = new LeftMenuModel();
        }
        public List<MTUserItemModel> MTUserItems { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        public bool IsAllowedToSee { get; set; }
    }
}