using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MTHeaderTopMenuItem
    {
        public int HelpCategoryId { get; set; }
        public string HelpCategoryName { get; set; }
        public int HelpCategoryType { get; set; }
        public string Url { get; set; }
    }
}