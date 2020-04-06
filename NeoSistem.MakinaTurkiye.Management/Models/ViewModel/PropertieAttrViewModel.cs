using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class PropertieAttrViewModel
    {
        public PropertieAttrViewModel()
        {
            this.PropertieAttrs = new List<PropertieAttrModel>();
        }
        public int PropertieId { get; set; }
        public string PropertieName { get; set; }
        public string PropertieAttrValue { get; set; }
        public int Order { get; set; }
        public List<PropertieAttrModel> PropertieAttrs { get; set; }
              
    }
  
}