using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class ProductCreateSettingItem
    {
        public int ProductSettingId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}