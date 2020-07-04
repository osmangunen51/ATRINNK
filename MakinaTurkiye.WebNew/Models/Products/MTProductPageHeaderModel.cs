using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductPageHeaderModel
    {
        public string Navigation { get; set; }
        public string MobileNavigation { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int MainPartyId { get; set; }
        public string MemberEmail { get; set; }
        public bool IsActive { get; set; }
        public string nextProductUrl { get; set; }
        public string preProductUrl { get; set; }

    }
}