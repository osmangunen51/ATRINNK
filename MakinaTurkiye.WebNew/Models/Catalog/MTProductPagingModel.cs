using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTProductPagingModel
    {
        public MTProductPagingModel()
        {
            this.PageUrls = new Dictionary<int, string>();
        }

        public int TotalPageCount { get; set; }
        public int CurrentPageIndex { get; set; }

        public string FirstPageUrl { get; set; }
        public string LastPageUrl { get; set; }

        public IDictionary<int,string> PageUrls { get; set; }

        public int PageSize { get; set; }
    }
}