using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
    public class MTStoreCategoryItemModel
    {
        public MTStoreCategoryItemModel()
        {
            this.SubStoreCategoryItemModes = new List<MTStoreCategoryItemModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryName { get; set; }
        public int StoreCount { get; set; }
        public byte CategoryType { get; set; }
        public string DefaultCategoryName { get; set; }
        public string CategoryContentTitle { get; set; }

        public IList<MTStoreCategoryItemModel> SubStoreCategoryItemModes { get; set; }
    }
}