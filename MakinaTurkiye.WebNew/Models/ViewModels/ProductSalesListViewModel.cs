using System.Collections.Generic;
using MakinaTurkiye.Entities.Tables.Catalog;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    public class ProductSalesListViewModel
    {
        public IList<Product> ProductSalesBuyer { get; set; }
        public IList<Product> ProductSalesDealer { get; set; }
    }
}