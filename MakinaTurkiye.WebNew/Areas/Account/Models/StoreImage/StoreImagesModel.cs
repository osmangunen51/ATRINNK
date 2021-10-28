using System.Collections.Generic;

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