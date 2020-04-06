using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Users
{
    public class MTUserFormModelView
    {
        public MTUserFormModelView()
        {
            this.MTUserFormModel = new MTUserFormModel();
        }
        public MTUserFormModel MTUserFormModel { get; set; }
        public LeftMenuModel LeftMenu { get; set; }

    }
}