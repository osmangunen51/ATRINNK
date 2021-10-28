using System;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreNews
{
    public class MTNewDetailModel
    {
        public MTNewDetailModel()
        {
            this.NewOthers = new List<MTNewOtherItem>();
        }

        public int NewId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }


        public MTNewStoreModel NewStoreModel { get; set; }
        public List<MTNewOtherItem> NewOthers { get; set; }

    }
}