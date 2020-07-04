using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models.Statistics
{
    public class MTProductStatisticViewModel
    {
        public MTProductStatisticViewModel()
        {
            this.MTStatisticModel = new MTStatisticModel();
            this.LeftMenu = new LeftMenuModel();
            this.MTStatisticLocationModels = new List<MTStatisticLocationModel>();
            this.ProductItems = new List<MTProductItem>();
            this.FilterItemModels = new List<FilterItemModel>();
            this.FilterDays = new List<SelectListItem>();
        }

        public int TotalViewCount { get; set; }
        public int TotalUserCount { get; set; }
        public long LastTotalViewCount { get; set; }
        public long LastSingularViewCount { get; set; }
     
        public bool TodayData { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        public List<SelectListItem> FilterDays { get; set; }
        public MTStatisticModel MTStatisticModel { get; set; }
        public List<MTProductItem> ProductItems { get; set; }
        public List<FilterItemModel> FilterItemModels { get; set; }
        public List<MTStatisticLocationModel> MTStatisticLocationModels { get; set; }
    }
}