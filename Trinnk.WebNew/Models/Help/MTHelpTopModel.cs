using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Help
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