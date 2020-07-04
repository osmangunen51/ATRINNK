using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTMostViewedProductModel
    {
        public MTMostViewedProductModel()
        {
            this.ProductItemModels = new List<MTMostViewedProductItemModel>();
        }
        public string SelectedCategoryName { get; set; }
        public List<MTMostViewedProductItemModel> ProductItemModels { get; set; }
    }
}