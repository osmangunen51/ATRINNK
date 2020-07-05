using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreProfileProductsModel
    {
        public MTStoreProfileProductsModel()
        {
            this.MTStoreProfileHeaderModel = new MTStoreProfileHeaderModel();
            this.MTProductsProductListModel = new MTProductsProductListModel();
            this.MTCategoryModel = new MTCategoryModel();
            this.CustomFilterModels = new List<CustomFilterModel>();
        }

        public byte GroupType { get; set; }
        public int CategoryId { get; set; }
        public byte StoreActiveType { get; set; }
     

        public MTProductsProductListModel MTProductsProductListModel { get; set; }
        public MTStoreProfileHeaderModel MTStoreProfileHeaderModel { get; set; }
        public MTCategoryModel MTCategoryModel { get; set; }
        public List<CustomFilterModel> CustomFilterModels { get; set; }
    }
}