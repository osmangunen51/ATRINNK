using NeoSistem.MakinaTurkiye.Web.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoItemViewModel
    {
        public MTVideoItemViewModel()
        {
            this.MTStoreAndProductDetailModel = new MTStoreAndProductDetailModel();
            this.MTOtherVideosModel = new MTOtherVideosModel();
            this.MTVideoCategoryItemModel = new MTVideoCategoryItemModel();
        }
        public string VideoPath { get; set; }
        public int VideoId { get; set; }
        public MTStoreAndProductDetailModel MTStoreAndProductDetailModel { get; set; }
        public MTOtherVideosModel MTOtherVideosModel { get; set; }
        public MTVideoCategoryItemModel MTVideoCategoryItemModel { get; set; }

    }
}