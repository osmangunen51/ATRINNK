using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MTPayWithCreditCardModel
    {
        public MTPayWithCreditCardModel()
        {
            this.PacketModel = new PacketModel();
        }

        public PacketModel PacketModel { get; set; }
        
        public bool IsDoping { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DopingDay { get; set; }
        
    }
}