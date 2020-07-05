using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class SortOptionModel
    {
        public int SortId { get; set; }
        public string SortOptionUrl { get; set; }
        public string SortOptionName { get; set; }
        public bool Selected { get; set; }
    }
}