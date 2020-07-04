using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Help
{
    public class MTHelpMenuModel
    {
        public int CategoryId { get; set; }
        public int CategoryParentId { get; set; }
        public string CategoryName { get; set; }
        public string HelpUrl { get; set; }

    }
}