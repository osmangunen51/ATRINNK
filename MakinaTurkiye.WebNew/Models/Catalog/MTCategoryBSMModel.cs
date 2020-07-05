using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTCategoryBSMModel
    {
        public int  CategoryId { get; set; }
        public byte CategoryType { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public bool Selected { get; set; }
        public string Url { get; set; }

    }
}