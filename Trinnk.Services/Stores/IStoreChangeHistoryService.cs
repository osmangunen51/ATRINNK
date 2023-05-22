using Trinnk.Entities.StoredProcedures.Stores;
using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
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
