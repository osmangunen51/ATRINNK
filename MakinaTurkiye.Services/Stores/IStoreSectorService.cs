using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreSectorService
    {
        StoreSector GetStoreSectorByStoreMainPartyIdAndCategoryId(int mainPartyId, int categoryId);
        StoreSector GetStoreSectorByStoreSectorId(int storeSectorId);
        void InsertStoreSector(StoreSector storeSector);
        void DeleteStoreSector(StoreSector storeSector);
        List<StoreSector> GetStoreSectorsByMainPartyId(int mainPartyId);

    }
}
