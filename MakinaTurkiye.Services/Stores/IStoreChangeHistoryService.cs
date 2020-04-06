using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreChangeHistoryService
    {
        IList<StoreChangeHistory> GetAllStoreChangeHistory();

        StoreChangeHistory GetStoreChangeHistoryByStoreChangeHistoryId(int storeChangeHistoryId);

        void AddStoreChangeHistory(StoreChangeHistory storeChangeHistory);

        void DeleteStoreChangeHistory(StoreChangeHistory storeChangeHistory);

        void AddStoreChangeHistoryForStore(Store store);
    }
}
