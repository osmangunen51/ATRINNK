using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreDealerService : ICachingSupported
    {
        IList<StoreDealer> GetStoreDealersByMainPartyId(int mainPartyId, DealerTypeEnum dealerType);

        StoreDealer GetStoreDealersByStoreDealerId(int StoreDealer);

        void InsertStoreDealer(StoreDealer storeDealer);

        void DeleteStoreDealer(StoreDealer storeDealer);

        void UpdateStoreDealer(StoreDealer storeDealer);
    }
}