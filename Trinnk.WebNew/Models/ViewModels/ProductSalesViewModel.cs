using Trinnk.Entities.Tables.Catalog;
using Trinnk.Entities.Tables.Stores;

namespace NeoSistem.Trinnk.Web.Models.ViewModels
{
    public class ProductSalesViewModel
    {
        //public ProductDetailInfo ProductDetailInfo { get; set; }

        public Product Product { get; set; }

        public Store Store { get; set; }

        public PacketModel PacketModel { get; set; }

    }
}