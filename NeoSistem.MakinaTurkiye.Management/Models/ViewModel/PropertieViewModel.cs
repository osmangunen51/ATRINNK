using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class PropertieViewModel
    {
        public PropertieViewModel()
        {
            this.PropertieTypes = new List<SelectListItem>();
            this.Properties = new FilterModel<PropertieModel>();
        }
        public int PropertieId { get; set; }
        public string PropertieName { get; set; }
        public byte PropertieType { get; set; }
        public List<SelectListItem> PropertieTypes {get; set; }
        public FilterModel<PropertieModel> Properties { get; set; }
    }
}