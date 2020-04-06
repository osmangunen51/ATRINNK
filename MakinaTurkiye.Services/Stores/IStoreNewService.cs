using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreNewService : ICachingSupported
    {
        IList<StoreNew> GetAllStoreNews(byte newType);
        IPagedList<StoreNew> GetAllStoreNews(int pageDimension, int page, byte newType);
        IList<StoreNew> GetStoreNewsTop(int top, byte newType);
        IList<StoreNew> GetStoreNewsByStoreMainPartyId(int storeMainPartyId, StoreNewTypeEnum newType);
        StoreNew GetStoreNewByStoreNewId(int storeNewId);
        void InsertStoreNew(StoreNew storeNew);
        void UpdateStoreNew(StoreNew storeNew);
        void DeleteStoreNew(StoreNew storeNew);

    }
}
