using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTStoreOtherProductModel
    {
        public MTStoreOtherProductModel()
        {
            this.ProductItemModels = new List<MTStoreOtherProductItemModel>();
        }

        public string AllStoreOtherProductUrl { get; set; }

        public List<MTStoreOtherProductItemModel> ProductItemModels { get; set; }
    }
}