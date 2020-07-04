using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreChangeHistoryService
    {
        IList<StoreChangeHistory> GetAllStoreChangeHistory();

        StoreChangeHistory GetStoreChangeHistoryByStoreChangeHistoryId(int storeChangeHistoryId);


        List<StoreChangeInfoResult> SP_StoreInfoChange(int pageSize, int pageIndexref, out int totalRecord);

        void AddStoreChangeHistory(StoreChangeHistory storeChangeHistory);

        void DeleteStoreChangeHistory(StoreChangeHistory storeChangeHistory);

        void AddStoreChangeHistoryForStore(Store store);
    }
}
