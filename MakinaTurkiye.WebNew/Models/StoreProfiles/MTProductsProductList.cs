using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models.Helpers;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTProductsProductListModel
    {
         public MTProductsProductListModel()
        {
            this.MTProductsPageProductLists = new PagingModel<MTProductsPageProductList>();
        }
        public int StoreMainPartyId { get; set; }
        public byte ViewType { get; set; }
        public int CategoryId { get; set; }
        public PagingModel<MTProductsPageProductList> MTProductsPageProductLists { get; set; }
    }
}