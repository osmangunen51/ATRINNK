using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
    public class MTStorePagingModel
    {
        public MTStorePagingModel()
        {
            this.PageUrls = new Dictionary<int, string>();
        }

        public int FirstPage { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int LastPage { get; set; }

        public string FirstPageUrl { get; set; }
        public string LastPageUrl { get; set; }

        public IDictionary<int,string> PageUrls { get; set; }
    }

}