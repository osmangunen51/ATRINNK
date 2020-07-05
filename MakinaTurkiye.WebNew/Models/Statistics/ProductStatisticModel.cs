using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Statistics
{
    public class ProductStatisticModel
    {
        public ProductStatisticModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.ProductItems = new SearchModel<ProductModel>();
            this.MTStatisticModel = new MTStatisticModel();
        }
     
        public SearchModel<ProductModel> ProductItems { get; set; }

        public LeftMenuModel LeftMenu { get; set; }
        public MTStatisticModel MTStatisticModel { get; set; }
    }
}