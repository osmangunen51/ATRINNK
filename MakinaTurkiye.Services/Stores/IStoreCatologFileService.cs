using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreCatologFileService : ICachingSupported
    {
        List<StoreCatologFile> StoreCatologFilesByStoreMainPartyId(int storeMainPartyId);
        void InsertStoreCatologFile(StoreCatologFile storeCatologFile);
        void DeleteStoreCatologFile(StoreCatologFile storeCatologFile);
        void UpdateStoreCatologFile(StoreCatologFile storeCatologFile);
        StoreCatologFile GetStoreCatologFileByCatologId(int categologFileId);

    }
}
