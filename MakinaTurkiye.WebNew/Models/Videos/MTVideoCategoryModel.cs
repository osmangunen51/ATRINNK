using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoCategoryModel
    {
        public MTVideoCategoryModel()
        {
            this.VideoCategoryItemModels = new List<MTVideoCategoryItemModel>();
            this.VideoTopCategoryItemModels = new List<MTVideoCategoryItemModel>();
            this.VideoTopSubCategoryItemModels = new List<MTVideoCategoryItemModel>();
        }

        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }

        public IList<MTVideoCategoryItemModel> VideoCategoryItemModels { get; set; }

        public IList<MTVideoCategoryItemModel> VideoTopCategoryItemModels { get; set; }
        public IList<MTVideoCategoryItemModel> VideoTopSubCategoryItemModels { get; set; }
    }
}