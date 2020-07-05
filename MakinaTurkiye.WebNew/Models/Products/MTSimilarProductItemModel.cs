using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
  public class MTSimilarProductItemModel
  {
    public int ProductId { get; set; }
    public int Index { get; set; }
    public string ProductName { get; set; }
    public string SmallPicturePah { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string Price { get; set; }
    public string ProductUrl { get; set; }
    public string SmallPictureName { get; set; }
    public string CurrencyCss { get; set; }
    public string ProductContactUrl { get; set; }
    public long? ViewCount { get; set; }

  }
}