using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTCategoryStoreModel
    {
        public MTCategoryStoreModel()
        {
            this.StoreItemModes = new List<MTCategoryStoreItemModel>();
        }
        //test zorunlu

        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
        public string SelectedCity { get; set; }
        public byte SelectedCategoryType { get; set; }

        public string StoreCategoryUrl { get; set; }

        public IList<MTCategoryStoreItemModel> StoreItemModes { get; set; }
    }
}