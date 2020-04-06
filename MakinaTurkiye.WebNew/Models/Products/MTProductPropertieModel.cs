using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductPropertieModel
    {
        public MTProductPropertieModel()
        {
            this.MTProductProperties = new List<MTProductPropertieItem>();
        }
       public List<MTProductPropertieItem> MTProductProperties { get; set; }
    }
    public class MTProductPropertieItem
    {
        public MTProductPropertieItem()
        {
            this.Attrs = new List<SelectListItem>();
        }
        public string InputName { get; set; }
        public string DisplayName { get; set; }
        public byte Type { get; set; }
        public int PropertieId { get; set; }
        public string Value { get; set; }

        public List<SelectListItem> Attrs { get; set; }

    }
}