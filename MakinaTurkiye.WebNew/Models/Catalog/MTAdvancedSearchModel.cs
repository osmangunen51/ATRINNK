using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTAdvancedSearchModel
    {
        public MTAdvancedSearchModel()
        {
            this.SectorList = new List<MTCategoryItemModel>();
            this.FilterItems = new List<AdvancedSearchFilterItem>();
        }
        public List<AdvancedSearchFilterItem> FilterItems { get; set; }
        public List<MTCategoryItemModel> SectorList { get; set; }
    }
    public class AdvancedSearchFilterItem
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
        
        
}