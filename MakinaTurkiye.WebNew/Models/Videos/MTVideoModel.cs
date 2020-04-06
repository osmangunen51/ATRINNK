using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoModel
    {
        public int VideoId { get; set; }
        public string PicturePath { get; set; }
        public string StoreUrl { get; set; }
        public string VideoUrl { get; set; }
        public string ProductUrl { get; set; }
        public byte VideoMinute { get; set; }
        public byte VideoSecond { get; set; }
        public string TruncateProductName { get; set; }
        public string TruncateStoreName { get; set; }
        public string StoreName { get; set; }
        public long SingularViewCount { get; set; }
        public string VideoRecordDate { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryName { get; set; }
        public string VideoPath { get; set; }
        public string VideoTitle { get; set; }

        public MTVideoModel _mtVideoModel { get; set; }
    }
}