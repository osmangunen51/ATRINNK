using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Footer
{
    public class MTFooterModel
    {
        public List<MTFooterParentModel> FooterParentModels { get; set; }

        public string ConstantTitle { get; set; }
        public string ConstantProperty { get; set; }
        public MTFooterModel()
        {
            this.FooterParentModels = new List<MTFooterParentModel>();
        }
    }
}