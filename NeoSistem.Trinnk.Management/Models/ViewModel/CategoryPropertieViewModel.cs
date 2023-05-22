using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models.ViewModel
{
    public class CategoryPropertieViewModel
    {
        public CategoryPropertieViewModel()
        {
            this.Questions = new List<SelectListItem>();
            this.PropertieModels = new List<PropertieModel>();

        }
        public List<SelectListItem> Questions { get; set; }
        public List<PropertieModel> PropertieModels { get; set; }
        public int PropertieId { get; set; }
        public int CategoryId { get; set; }

    }
}