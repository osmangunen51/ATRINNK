using System.Collections.Generic;
using NeoSistem.MakinaTurkiye.Web.Controllers;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
  public class MTJsonLdModel
  {
    public MTJsonLdModel()
    {
      Navigations = new List<BaseController.Navigation>();
    }
    public IList<BaseController.Navigation> Navigations { get; set; }
    public string JsonLdString { get; set; }

  }
}