using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreInfoNumberShowService : ICachingSupported
    {
        IList<StoreInfoNumberShow> GetAllStoreInfoNumberShow();

        StoreInfoNumberShow GetStoreInfoNumberShowByStoreInfoNumberShowId(int storeInfoNumberShowID);

        StoreInfoNumberShow GetStoreInfoNumberShowByStoreMainPartyId(int storeMainPartyID);

        void InsertStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow);

        void UpdateStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow);

        void DeleteStoreInfoNumberShow(StoreInfoNumberShow storeInfoNumberShow);
    }
}
