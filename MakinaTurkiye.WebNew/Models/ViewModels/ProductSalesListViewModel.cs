using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    public class ProductSalesListViewModel
    {
        public IList<Product> ProductSalesBuyer { get; set; }
        public IList<Product> ProductSalesDealer { get; set; }
    }
}