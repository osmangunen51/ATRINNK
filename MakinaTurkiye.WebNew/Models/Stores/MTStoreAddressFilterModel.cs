using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
  public class MTStoreAddressFilterModel
  {
    public MTStoreAddressFilterModel()
    {
      this.CityFilterItemModels = new List<MTStoreAddressFilterItemModel>();
      this.LocalityFilterItemModels = new List<MTStoreAddressFilterItemModel>();
    }

    public IList<MTStoreAddressFilterItemModel> CityFilterItemModels { get; set; }
    public IList<MTStoreAddressFilterItemModel> LocalityFilterItemModels { get; set; }
  }
}