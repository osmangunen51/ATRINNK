using MakinaTurkiye.Utilities.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTCategorySearchModel:BaseMakinaTurkiyeModel
    {
        public MTCategorySearchModel()
        {
            this.CategoryModel = new MTProductCategoryModel();
        }
        public MTProductCategoryModel CategoryModel { get; set; }
        public string SearchText { get; set; }
        public int TotalCount { get; set; }


    }
}