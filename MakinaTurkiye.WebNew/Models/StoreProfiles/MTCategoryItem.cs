using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTCategoryItem
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryType { get; set; }
        public int CategoryParentId { get; set; }
        public string CategoryUrl { get; set; }

    }
}