using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.UrlRedirectModels
{
    public class UrlRedirectItem
    {
        public int UrlRedirectId { get; set; }
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}