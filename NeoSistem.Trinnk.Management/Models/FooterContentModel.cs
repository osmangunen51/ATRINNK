using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class FooterContentModel
    {
        public FooterContentModel()
        {
            this.FooterParentItems = new List<SelectListItem>();
        }
        public int FooterContentId { get; set; }
        public string FooterContentName { get; set; }
        public string FooterContentUrl { get; set; }
        public int DisplayOrder { get; set; }
        public string FooterParentName { get; set; }

        public string FooterParentId { get; set; }
        public List<SelectListItem> FooterParentItems { get; set; }

        public FooterParentModel FooterParentModel { get; set; }
    }
}