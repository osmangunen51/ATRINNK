using Trinnk.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.ViewModels
{
    public class ProductSalesListViewModel
    {
        public IList<Product> ProductSalesBuyer { get; set; }
        public IList<Product> ProductSalesDealer { get; set; }
    }
}