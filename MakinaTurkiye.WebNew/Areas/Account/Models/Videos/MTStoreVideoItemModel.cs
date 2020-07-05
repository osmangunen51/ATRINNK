using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos
{
    public class MTStoreVideoItemModel
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
        public string VideoPath { get; set; }
        public string ImagePath { get; set; }
        public long ViewCount { get; set; }
        public DateTime RecordDate { get; set; }
        public byte ? Order { get; set; }
       
 
    }
}