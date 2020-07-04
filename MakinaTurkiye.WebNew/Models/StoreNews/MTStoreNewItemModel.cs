using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreNews
{
    public class MTStoreNewItemModel
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string NewUrl { get; set; }
        public string DateString { get; set; }
        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
    }
}