using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreActivityTypeService : ICachingSupported
    {

        void InsertStoreActivityType(StoreActivityType storeActivityType);

        void DeleteStoreActivityType(StoreActivityType storeActivityType);

        IList<StoreActivityType> GetStoreActivityTypesByStoreId(int storeId);

    }
}
