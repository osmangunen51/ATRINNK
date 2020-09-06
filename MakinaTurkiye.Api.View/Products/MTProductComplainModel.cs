using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View.Products
{
    public class MTProductComplainModel
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserComment { get; set; }

        public List<ProductComplainTypeView> ProductComplainTypeList { get; set; }
    }
}
