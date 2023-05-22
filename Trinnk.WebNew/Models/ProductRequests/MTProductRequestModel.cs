using NeoSistem.Trinnk.Web.Models.Catalog;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.ProductRequests
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