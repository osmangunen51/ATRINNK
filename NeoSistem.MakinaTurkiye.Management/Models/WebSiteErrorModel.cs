using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class WebSiteErrorModel
    {
        public WebSiteErrorModel()
        {
            this.Users = new List<SelectListItem>();
            this.ProblemTypes = new List<SelectListItem>();
            this.WebSiteErrorList = new List<WebSiteErrorListItem>();
        }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> ProblemTypes { get; set; }
        public List<WebSiteErrorListItem> WebSiteErrorList { get; set; }
        public string Type { get; set; }
    }
}