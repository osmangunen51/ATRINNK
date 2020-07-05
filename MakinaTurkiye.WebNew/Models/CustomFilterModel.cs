using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class CustomFilterModel
    {
        public int FilterId { get; set; }
        public string FilterUrl { get; set; }
        public string FilterName { get; set; }
        public int ProductCount { get; set; }
        public bool Selected { get; set; }
    }
}