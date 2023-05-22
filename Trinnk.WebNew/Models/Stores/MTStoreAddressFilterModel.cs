using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Stores
{
    public class MTStoreAddressFilterModel
    {
        public MTStoreAddressFilterModel()
        {
            this.CityFilterItemModels = new List<MTStoreAddressFilterItemModel>();
            this.LocalityFilterItemModels = new List<MTStoreAddressFilterItemModel>();
        }

        public IList<MTStoreAddressFilterItemModel> CityFilterItemModels { get; set; }
        public IList<MTStoreAddressFilterItemModel> LocalityFilterItemModels { get; set; }
    }
}