﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Help
{
    public class MTHelpDetailModel
    {
        public MTHelpDetailModel()
        {
            SubMenuItemModels = new List<MTHelpMenuModel>();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Content { get; set; }

        public string Canonical { get; set; }


        public IList<MTHelpMenuModel> SubMenuItemModels { get; set; }
    }
}