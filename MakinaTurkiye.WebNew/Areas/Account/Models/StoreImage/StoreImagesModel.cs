using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreImage
{
    public class StoreImagesModel
    {
        public LeftMenuModel LeftMenu { get; set; }
        public List<StoreImageItem> StoreImageItems { get; set; }

        public StoreImagesModel()
        {
            this.StoreImageItems = new List<StoreImageItem>();
        }

    }

}