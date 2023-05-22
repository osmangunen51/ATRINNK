using NeoSistem.Trinnk.Web.Controllers;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Catalog
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