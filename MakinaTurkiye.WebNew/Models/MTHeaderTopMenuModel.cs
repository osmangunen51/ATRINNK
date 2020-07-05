using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models.Home;
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