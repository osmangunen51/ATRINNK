using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores
{
    public class MTCatologItem
    {
        public int CatologId { get; set; }
        public string Name { get; set; }
        public int ? FileOrder { get; set; }
        public string FilePath { get; set; }
    }
}