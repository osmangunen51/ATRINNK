using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Footer
{
    public class MTFooterParentModel
    {
        public MTFooterParentModel()
        {
            this.FooterContents = new List<MTFooterContentModel>();
        }
        public int FooterParentId { get; set; }
        public string FooterParentName { get; set; }
        public IList<MTFooterContentModel> FooterContents { get; set; }
    }
}