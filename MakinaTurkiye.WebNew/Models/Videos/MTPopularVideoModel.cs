using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTPopularVideoModel
    {
        public string VideoUrl { get; set; }
        public string PicturePath { get; set; }
        public long? SingularViewCount { get; set; }
        public string TruncatetStoreName { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        
    }
}