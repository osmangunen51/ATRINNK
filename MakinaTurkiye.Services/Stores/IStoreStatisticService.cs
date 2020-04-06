using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreStatisticService
    {
        StoreStatistic GetStoreStatisticByIpAndStoreIdAndRecordDate(int storeId, string ipAdress, DateTime recordDate);
        void DeleteStoreStatistic(StoreStatistic storeProductStatistic);
        void UpdateStoreStatistic(StoreStatistic storeProductStatistic);
        void InsertStoreStatistic(StoreStatistic storeProductStatistic);
        List<StoreStatistic> GetStoreStatisticsByStoreIdAndDates(int storeId, DateTime startDate, DateTime LastDate);

    }
}
