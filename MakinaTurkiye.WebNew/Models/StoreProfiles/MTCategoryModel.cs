﻿using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTCategoryModel
    {
        public MTCategoryModel()
        {
            this.MTCategoryItems = new List<MTCategoryItem>();
            this.MTTopCategoryItems = new List<MTCategoryItem>();
        }
        public int CurrentCategoryId { get; set; }
        public Category ActiveCategory { get; set; }
        public List<MTCategoryItem> MTCategoryItems { get; set; }
        public List<MTCategoryItem> MTTopCategoryItems { get; set; }
    }
}