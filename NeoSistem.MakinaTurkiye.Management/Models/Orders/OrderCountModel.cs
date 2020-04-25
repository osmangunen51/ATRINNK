using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.Orders
{
    public class OrderCountModel
    {
        public OrderCountModel()
        {
            this.Months = new List<SelectListItem>();
            this.OrderCountItems = new List<OrderCountItemModel>();
        }
        public List<SelectListItem> Months { get; set; }
        public List<OrderCountItemModel> OrderCountItems { get; set; }

    }
}