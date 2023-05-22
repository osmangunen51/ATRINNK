using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class ProductDopingModel
    {
        public ProductDopingModel()
        {
            this.DopingModels = new List<ProductDopingListModel>();
            this.AvairableCategories = new List<SelectListItem>();
            this.Stores = new List<SelectListItem>();
        }

        public List<ProductDopingListModel> DopingModels { get; set; }
        public List<SelectListItem> AvairableCategories { get; set; }
        public List<SelectListItem> Stores { get; set; }
        public int SelectedCategoryId { get; set; }
        public int StoreMainPartyId { get; set; }

    }
}