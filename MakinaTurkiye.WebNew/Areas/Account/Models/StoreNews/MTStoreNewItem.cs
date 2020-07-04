using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews
{
    public class MTStoreNewItem
    {
        public int StoreNewId { get; set; }
        public string Title { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Active { get; set; }
        public string ImagePath { get; set; }
        public string NewUrl { get; set; }
        public long ViewCount { get; set; }
        

    }
}