using NeoSistem.Trinnk.Web.Models.Home;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models
{
    public class MTBaseSubMenuModel
    {
        public MTBaseSubMenuModel()
        {
            this.CategoryModels = new List<MTHomeCategoryModel>();
            this.ImageModels = new Dictionary<string, string>();
        }
        public IDictionary<string, string> ImageModels { get; set; }
        public List<MTHomeCategoryModel> CategoryModels { get; set; }
    }
}