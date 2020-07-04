using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Stores;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
  public class ProductSalesViewModel
  {
    //public ProductDetailInfo ProductDetailInfo { get; set; }

    public Product Product { get; set; }

    public Store Store { get; set; }

    public PacketModel PacketModel { get; set; }

  }
}