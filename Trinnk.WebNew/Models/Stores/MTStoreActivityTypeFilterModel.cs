using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Stores
{
    public class MTStoreActivityTypeFilterModel
    {
        public MTStoreActivityTypeFilterModel()
        {
            ActivityTypeFilterItemModels = new List<MTStoreActivityTypeFilterItemModel>();
            SelectedActivityTypeFilterName = string.Empty;
        }
        public IList<MTStoreActivityTypeFilterItemModel> ActivityTypeFilterItemModels { get; set; }
        public string SelectedActivityTypeFilterName { get; set; }


    }
}