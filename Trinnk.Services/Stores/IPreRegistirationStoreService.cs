using Trinnk.Core;
using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IPreRegistirationStoreService
    {
        void InsertPreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void DeletePreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void UpdatePreRegistrationStore(PreRegistrationStore preRegistirationStore);

        IPagedList<PreRegistrationStoreResponse> GetPreRegistirationStores(int page, int pageSize, string storeName, string email, string city = "",string user="", bool notcalling = false);

        IList<Store> GetPreRegistrationStoreSearchByPhone(params string[] Phones);

        PreRegistrationStore GetPreRegistirationStoreByPreRegistrationStoreId(int preRegistraionStoreId);

        IList<PreRegistrationStore> GetPreRegistrationStores();

        IList<PreRegistrationStore> GetPreRegistrationStoreSearchByName(string storeName);
    }
}
