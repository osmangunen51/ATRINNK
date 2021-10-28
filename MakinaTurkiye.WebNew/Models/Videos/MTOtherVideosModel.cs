using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTOtherVideosModel
    {
        public MTOtherVideosModel()
        {
            this.MTVideoModels = new List<MTVideoModel>();
            this.VideoAutomaticStatus = true;
        }
        public List<MTVideoModel> MTVideoModels { get; set; }
        public int TotalRecord { get; set; }
        public bool VideoAutomaticStatus { get; set; }
    }
}