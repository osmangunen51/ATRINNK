using NeoSistem.MakinaTurkiye.Web.Models.Home;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class MTBaseMenuModel
    {
        public MTBaseMenuModel()
        {
            this.CategoryModels = new List<MTHomeCategoryModel>();
        }

        public string BaseMenuName { get; set; }
        public int BaseMenuId { get; set; }
        public List<MTHomeCategoryModel> CategoryModels { get; set; }
    }
}