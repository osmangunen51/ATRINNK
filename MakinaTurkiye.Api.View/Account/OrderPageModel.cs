using System.Collections.Generic;

namespace MakinaTurkiye.Api.View.Account
{
    public class OrderPageModel
    {
        public OrderPageModel()
        {
            this.OrderListItems = new List<OrderListItem>();
        }

        public List<OrderListItem> OrderListItems { get; set; }

    }
}
