using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Catalog
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