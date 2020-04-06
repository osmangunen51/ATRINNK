using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Statistics
{
    public class MTProductItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ViewCount { get; set; }
    }
}