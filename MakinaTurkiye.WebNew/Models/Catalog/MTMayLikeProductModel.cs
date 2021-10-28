using System.Collections.Generic;

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