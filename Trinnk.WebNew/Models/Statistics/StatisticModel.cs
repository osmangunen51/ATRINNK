using Trinnk.Entities.Tables.Catalog;
using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Statistics
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