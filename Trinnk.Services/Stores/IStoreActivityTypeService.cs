using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IStoreActivityTypeService : ICachingSupported
    {

        void InsertStoreActivityType(StoreActivityType storeActivityType);
        void DeleteStoreActivityType(StoreActivityType storeActivityType);
        IList<StoreActivityType> GetStoreActivityTypesByStoreId(int storeId);
        StoreActivityType GetActivityTypeById(int StoreActivityTypeId);
    }
}
