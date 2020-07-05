using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.ProductRequests
{
    public class MTProductRequestModel
    {
        public MTProductRequestModel()
        {
            this.SectorList = new List<MTCategoryItemModel>();
            this.MTProductRequestForm = new MTProductRequestForm();
        }
        public int MainPartyId { get; set; }
        public List<MTCategoryItemModel> SectorList { get; set; }
        public MTProductRequestForm MTProductRequestForm { get; set; }

    }
}