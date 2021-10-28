using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Adverts
{
    public class MTBrandViewModel
    {
        public MTBrandViewModel()
        {
            this.BrandItems = new List<MTCategoryModel>();
        }

        public int CategoryId { get; set; }
        public int CategoryIdSession { get; set; }

        public List<MTCategoryModel> BrandItems { get; set; }
        public LeftMenuModel LeftMenu { get; set; }

    }
}