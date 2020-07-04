using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreNews
{
    public class MTNewStoreModel
    {
        public MTNewStoreModel()
        {
            this.Phones = new List<string>();
        }
        public string StoreName { get; set; }
        public string StoreLogoPath { get; set; }
        public string StoreUrl { get; set; }
        public List<string> Phones { get; set; }
     
    }
}