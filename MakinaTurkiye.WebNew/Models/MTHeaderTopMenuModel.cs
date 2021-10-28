using System.Collections.Generic;
namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MTHeaderTopMenuModel
    {
        public MTHeaderTopMenuModel()
        {
            this.HeaderTopMenuForHelp = new List<MTHeaderTopMenuItem>();

        }
        public List<MTHeaderTopMenuItem> HeaderTopMenuForHelp { get; set; }

    }
}