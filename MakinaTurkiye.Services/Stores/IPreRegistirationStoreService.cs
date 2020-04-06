using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Stores;

namespace MakinaTurkiye.Services.Stores
{
    public interface IPreRegistirationStoreService
    {
        void InsertPreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void DeletePreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void UpdatePreRegistrationStore(PreRegistrationStore preRegistirationStore);

        IPagedList<PreRegistrationStore> GetPreRegistirationStores(int page, int pageSize,string storeName, string email);
       
        PreRegistrationStore GetPreRegistirationStoreByPreRegistrationStoreId(int preRegistraionStoreId);

    }
}
