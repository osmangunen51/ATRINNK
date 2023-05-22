using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IStoreActivityCategoryService
    {
        StoreActivityCategory GetStoreActivityCategoryByStoreActivityCategoryId(int storeActivityCategoryId);
        List<StoreActivityCategory> GetStoreActivityCategoriesByMainPartyId(int mainPartyId);
        void InsertStoreActivityCategory(StoreActivityCategory storeActivtyCategory);
        void DeleteStoreActivityCategory(StoreActivityCategory storeActivtyCategory);
        void UpdateStoreActivityCategory(StoreActivityCategory storeActivtyCategory);


    }
}
