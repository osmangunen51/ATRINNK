using Trinnk.Core;
using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
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
