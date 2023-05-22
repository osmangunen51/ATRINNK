using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Checkouts
{
    public class OrderPageModel
    {
        public OrderPageModel()
        {
            this.OrderListItems = new List<OrderListItem>();
            this.LeftMenu = new LeftMenuModel();
        }

        public List<OrderListItem> OrderListItems { get; set; }
        public LeftMenuModel LeftMenu { get; set; }

    }
}