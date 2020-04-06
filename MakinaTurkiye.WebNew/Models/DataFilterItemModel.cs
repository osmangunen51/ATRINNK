using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class DataFilterItemModel
    {
        public int FilterItemId { get; set; }
        public string FilterUrl { get; set; }
        public string FilterName { get; set; }
        public bool Selected { get; set; }
        public int Count { get; set; }
    }
}