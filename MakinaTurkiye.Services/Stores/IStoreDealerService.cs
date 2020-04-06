using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreDealerService : ICachingSupported
    {
        IList<StoreDealer> GetStoreDealersByMainPartyId(int mainPartyId, DealerTypeEnum dealerType);
        void InsertStoreDealer(StoreDealer storeDealer);
    }
}
