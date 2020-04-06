﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class AllSectorProductsModel
    {
        public AllSectorProductsModel()
        {
            this.Sectors=new List<MTCategoryItemModel>();
            this.Products = new List<MTAllSectorProductItemModel>();
        }
        public List<MTCategoryItemModel> Sectors { get; set; }
        public List<MTAllSectorProductItemModel> Products { get; set; }//yeterli class tüm paremetreleri içeriyor
      
    }
}