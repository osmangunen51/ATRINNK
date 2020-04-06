using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.Catolog
{
    public class HomeSectorProductModel
    {
        public HomeSectorProductModel()
        {
            this.HomeSectorProductItemsFilter = new FilterModel<HomeSectorProductItem>();
            this.Sectors = new List<SelectListItem>();

        }
        public FilterModel<HomeSectorProductItem> HomeSectorProductItemsFilter { get; set; }
        public List<SelectListItem> Sectors { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}