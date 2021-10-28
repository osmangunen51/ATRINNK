using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
    public class MTStoreCategoryModel
    {
        public MTStoreCategoryModel()
        {
            this.StoreCategoryItemModels = new List<MTStoreCategoryItemModel>();
            this.StoreTopCategoryItemModels = new List<MTStoreCategoryItemModel>();
        }

        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }

        public IList<MTStoreCategoryItemModel> StoreCategoryItemModels { get; set; }

        public IList<MTStoreCategoryItemModel> StoreTopCategoryItemModels { get; set; }


    }
}