using MakinaTurkiye.Api.View.Result;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using System.Collections.Generic;
using static MakinaTurkiye.Api.View.StoreCategoryItemModel;

namespace MakinaTurkiye.Api.View
{
    public class StoreResult
    {
        public List<StoreListItem> StoreListesi { get; set; } = new List<StoreListItem>();
        public List<StoreCategoryItemModel> TopCategories { get; set; } = new List<StoreCategoryItemModel>();
        public List<StoreCategoryItemModel> Categories { get; set; } = new List<StoreCategoryItemModel>();
        public int SelectedCategoryId { get; set; } = 0;
        public string SelectedCategoryName { get; set; } = "";

        public IList<SortOptionModel> SortOptionModels { get; set; }=new List<SortOptionModel>();
        public StoreAddressFilterModel StoreAddressFilterModel { get; set; } = new StoreAddressFilterModel();
        public StoreActivityTypeFilterModel StoreActivityTypeFilterModel { get; set; }=new StoreActivityTypeFilterModel();

        public int TotalItemCount { get; set; }
    }

    public class StoreAddressFilterModel
    {
        public StoreAddressFilterModel()
        {
            this.CityFilterItemModels = new List<StoreAddressFilterItemModel>();
            this.LocalityFilterItemModels = new List<StoreAddressFilterItemModel>();
        }

        public IList<StoreAddressFilterItemModel> CityFilterItemModels { get; set; }
        public IList<StoreAddressFilterItemModel> LocalityFilterItemModels { get; set; }
    }

    public class StoreActivityTypeFilterModel
    {
        public StoreActivityTypeFilterModel()
        {
            ActivityTypeFilterItemModels = new List<StoreActivityTypeFilterItemModel>();
            SelectedActivityTypeFilterName = string.Empty;
        }

        public IList<StoreActivityTypeFilterItemModel> ActivityTypeFilterItemModels { get; set; }
        public string SelectedActivityTypeFilterName { get; set; }
    }

    public class StoreAddressFilterItemModel
    {
        public string FilterItemId { get; set; }
        public string FilterItemName { get; set; }
        public string FilterUrl { get; set; }
        public bool Filtered { get; set; }
        public int FilterItemStoreCount { get; set; }
    }

    public class StoreListItem
    {
        public WebSearchStoreResult Store { get; set; } = new WebSearchStoreResult();
        public List<ProductSearchResult> Products { get; set; } = new List<ProductSearchResult>();
    }

    public class StoreCategoryItemModel
    {
        public StoreCategoryItemModel()
        {
            this.SubStoreCategoryItemModes = new List<StoreCategoryItemModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryUrl { get; set; }
        public string CategoryName { get; set; }
        public int StoreCount { get; set; }
        public byte CategoryType { get; set; }
        public string DefaultCategoryName { get; set; }
        public string CategoryContentTitle { get; set; }

        public IList<StoreCategoryItemModel> SubStoreCategoryItemModes { get; set; }

        public class SortOptionModel
        {
            public int SortId { get; set; }
            public string SortOptionUrl { get; set; }
            public string SortOptionName { get; set; }
            public bool Selected { get; set; }
        }
    }
}