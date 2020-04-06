using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Help
{
    public class MTHelpTopModel
    {
        public MTHelpTopModel()
        {
            this.MenuItemModels = new List<MTHelpMenuModel>();
        }

        public IList<MTHelpMenuModel> MenuItemModels { get; set; }
        public MTHelpMenuModel CurrentMenuModel { get; set; }

    }
}