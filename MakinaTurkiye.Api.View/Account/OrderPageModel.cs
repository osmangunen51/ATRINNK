using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
