using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTMayLikeProductModel
    {
        public MTMayLikeProductModel()
        {
            this.Products = new List<MTMayLikeProductItem>();
        }
        public List<MTMayLikeProductItem> Products { get; set; }
    }
}