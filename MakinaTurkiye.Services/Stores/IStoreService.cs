using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreService : ICachingSupported
    {
        IList<Store> GetAllStores(StoreActiveTypeEnum? storeActiveType=null);

        IPagedList<WebSearchStoreResult> SPWebSearch(out IList<int> filterableCityIds, out IList<int> filterableLocalityIds, 
            out IList<int> filterableActivityIds,int categoryId = 0, int modelId = 0, int brandId = 0, 
            int cityId = 0, IList<int> localityIds = null, string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "");

        CategoryStoresResult GetCategoryStores(int categoryId = 0, int modelId = 0, int brandId = 0,
            int cityId = 0, IList<int> localityIds = null, string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "");

        IList<StoreForCategoryResult> GetSPStoresForCategoryByCategoryId(int categoryId);

        IList<StoreForCategoryResult> GetSPGetStoreForCategoryByCategoryIdAndBrandId(int categoryId = 0, int brandId = 0);

        IList<StoreForCategoryResult> GetSPStoreForCategorySearch(int categoryId, int brandId = 0, int countryId = 0, int cityId = 0, int localityId = 0);
        IList<Store> GetStoresByMainPartyIds(List<int> mainPartyIds);

        Store GetStoreByMainPartyId(int mainPartyId);

        Store GetStoreByStoreEmail(string storeEmail);

        List<Store> GetStoresByMainPartyIds(List<int?> mainPartyIds);

        Store GetStoreByStoreUrlName(string storeUrlName);

        void InsertStore(Store store);
        void UpdateStore(Store store);

        Store GetStoreForVideoSearch(string searchText);

        IList<Store> GetHomeStores(int pageSize = int.MaxValue);

        IList<Store> GetStoreSearchByStoreName(string storeName);

        Store GetStoreByStoreNo(string storeNo);

        List<Store> SP_GetStoresForAutoMail(int packetId, int constandId, int pageDimension, int pageIndex);

        void InsertStoreCertificate(StoreCertificate storeCertificate);
        void UpdateStoreCertificate(StoreCertificate storeCertificate);
        void DeleteStoreCertificate(StoreCertificate storeCertificate);

        StoreCertificate GetStoreCertificateByStoreCertificateId(int storeCertificateId);
        

        IList<StoreCertificate> GetStoreCertificatesByMainPartyId(int mainPartyId);

        void UpdateStoreUpdated(StoreUpdated storeUpdated);
        void InsertStoreUpdated(StoreUpdated storeUpdated);
        StoreUpdated GetStoreUpdatedByMainPartyId(int mainPartyId);

    }
}
