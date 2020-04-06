using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
  public class MTStoreFilteringModel
  {
    public MTStoreFilteringModel()
    {
      this.SortOptionModels = new List<SortOptionModel>();
      this.StoreAddressFilterModel = new MTStoreAddressFilterModel();
      MtStoreActivityTypeFilterModel = new MTStoreActivityTypeFilterModel();
    }
    public IList<SortOptionModel> SortOptionModels { get; set; }
    public MTStoreAddressFilterModel StoreAddressFilterModel { get; set; }
    public MTStoreActivityTypeFilterModel MtStoreActivityTypeFilterModel { get; set; }

    public int TotalItemCount { get; set; }
  }
}