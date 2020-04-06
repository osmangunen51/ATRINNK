using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
  public class MTStoreAddressFilterItemModel
  {
    public string FilterItemId { get; set; }
    public string FilterItemName { get; set; }
    public string FilterUrl { get; set; }
    public bool Filtered { get; set; }
    public int FilterItemStoreCount { get; set; }
  }
}