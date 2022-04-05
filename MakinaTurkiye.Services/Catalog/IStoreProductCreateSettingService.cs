using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IStoreProductCreateSettingService
    {
        void InsertStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting);
        void UpdateStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting);
        void DeleteStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting);

        List<StoreProductCreateSetting> GetStoreProductCreateSettingsByStoreMainPartyId(int storeMainPartyId);

        List<StoreProductCreatePropertie> GetStoreProductCreateProperties();

        StoreProductCreatePropertie GetStoreProductCreatePropertieById(int id);

    }
}
