using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoPagingModel
    {
        public int FirstPage { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
    }
}