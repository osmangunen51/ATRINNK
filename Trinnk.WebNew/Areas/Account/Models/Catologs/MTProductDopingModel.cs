using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Catologs
{
    public class MTProductDopingModel
    {
        public MTProductDopingModel()
        {
            this.Packets = new List<SelectListItem>();
        }
        public int ProductId { get; set; }
        public int PacketId { get; set; }
        public decimal ProductDopingPricePerMonth { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string CategoryName { get; set; }
        public List<SelectListItem> Packets { get; set; }


    }
}