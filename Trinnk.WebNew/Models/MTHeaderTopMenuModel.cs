using System.Collections.Generic;
namespace NeoSistem.Trinnk.Web.Models
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