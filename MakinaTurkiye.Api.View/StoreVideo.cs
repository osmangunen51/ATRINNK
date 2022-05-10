using System.Collections.Generic;
using System;

namespace MakinaTurkiye.Api.View
{
    public class StoreVideoItem
    {
        public int VideoId { get; set; } = 0;
        public int Order { get; set; } = 0;
        public string Title { get; set; }
        public string File { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string VideoPath { get; set; }
        public long ViewCount { get; set; }
        public DateTime RecordDate { get; set; }
       
    }

    public class StoreVideo
    {
        public List<StoreVideoItem> List { get; set; } = new List<StoreVideoItem>();
        public int MainPartyId { get; set; } = 0;
    }
}