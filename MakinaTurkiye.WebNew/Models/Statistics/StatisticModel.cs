using MakinaTurkiye.Entities.Tables.Catalog;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Statistics
{
    public class StatisticModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<ProductStatisticListItem> StatisticList { get; set; } = new List<ProductStatisticListItem>();

        public StatisticModel()
        {

        }
    }

    public class ProductStatisticListItem
    {
        public ProductStatistic ProductStatistic { get; set; }
        public Product Product { get; set; }
    }
}