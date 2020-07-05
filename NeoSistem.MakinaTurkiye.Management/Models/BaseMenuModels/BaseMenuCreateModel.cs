using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels
{
    public class BaseMenuCreateModel
    {
        public BaseMenuCreateModel()
        {
            this.SectorCategories = new List<SelectListItem>();
            this.BaseMenuImages = new Dictionary<int, string>();
        }
        public List<SelectListItem> SectorCategories { get; set; }
        public string BaseMenuName { get; set; }
        public int BaseMenuId { get; set; }
        public bool Active { get; set; }
        public byte Order { get; set; }
        public string ImageUrl { get; set; }
        public byte? HomePageOrder { get; set; }
        public string BackgroundCss { get; set; }
        public string TabBackgroundCss { get; set; }
        public IDictionary<int,string> BaseMenuImages { get; set; }

    }
}