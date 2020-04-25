using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.Orders
{
    public class OrderCountItemModel
    {
        public string Username { get; set; }
        public int Count { get; set; }
        public string TotalAmount { get; set; }
        public string StoreNames { get; set; }
    }
}