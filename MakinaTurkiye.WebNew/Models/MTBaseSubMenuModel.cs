﻿using NeoSistem.MakinaTurkiye.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MTBaseSubMenuModel
    {
        public MTBaseSubMenuModel()
        {
            this.CategoryModels = new List<MTHomeCategoryModel>();
            this.ImageModels = new Dictionary<string, string>();
        }
        public IDictionary<string,string> ImageModels { get; set; }
        public List<MTHomeCategoryModel> CategoryModels { get; set; }
    }
}