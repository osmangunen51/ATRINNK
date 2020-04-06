using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoSearchViewModel
    {
        public MTVideoSearchViewModel()
        {
            this.VideoModels = new List<MTVideoModel>();
            this.PopularVideoModels = new List<MTPopularVideoModel>();
            this.VideoCategoryModel = new MTVideoCategoryModel();
            this.VideoPagingModel = new MTVideoPagingModel();
            this.SimilarVideos = new List<MTVideoModel>();
            this.Store = new MTStoreAndProductDetailModel();

        }

        public IList<MTVideoModel> VideoModels { get; set; }
        public IList<MTVideoModel> SimilarVideos { get; set; }
        public IList<MTPopularVideoModel> PopularVideoModels { get; set; }
        public MTVideoCategoryModel VideoCategoryModel { get; set; }
        public MTVideoPagingModel VideoPagingModel { get; set; }
        public MTStoreAndProductDetailModel Store{get;set;}

        public string SearchText{get;set;}
    }
}