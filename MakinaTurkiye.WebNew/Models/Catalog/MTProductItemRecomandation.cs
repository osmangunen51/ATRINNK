using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTProductItemRecomandation
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? ModelId { get; set; }
    }
}