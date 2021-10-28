using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Stores
{
    public class MTStoreFilteringModel
    {
        public MTStoreFilteringModel()
        {
            this.SortOptionModels = new List<SortOptionModel>();
            this.StoreAddressFilterModel = new MTStoreAddressFilterModel();
            MtStoreActivityTypeFilterModel = new MTStoreActivityTypeFilterModel();
        }
        public IList<SortOptionModel> SortOptionModels { get; set; }
        public MTStoreAddressFilterModel StoreAddressFilterModel { get; set; }
        public MTStoreActivityTypeFilterModel MtStoreActivityTypeFilterModel { get; set; }

        public int TotalItemCount { get; set; }
    }
}