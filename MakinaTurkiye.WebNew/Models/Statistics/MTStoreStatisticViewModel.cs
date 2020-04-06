using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Statistics
{
    public class MTStoreStatisticViewModel
    {
        public MTStoreStatisticViewModel()
        {
            this.MTStoreStatisticModel = new MTStatisticModel();
            this.LeftMenu = new LeftMenuModel();
            this.MTStatisticLocationModels = new List<MTStatisticLocationModel>();
      
        }
        public MTStatisticModel MTStoreStatisticModel { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        public int TotalViewCount { get; set; }
        public int TotalUserCount { get; set; }
        public long LastTotalViewCount { get; set; }
        public long LastSingularViewCount { get; set; }

        public List<MTStatisticLocationModel> MTStatisticLocationModels { get; set; }


    }
}