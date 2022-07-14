using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStorePackagePurchaseRequestService
    {
        void InsertStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        void DeleteStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        void UpdateStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        StorePackagePurchaseRequest GetStorePackagePurchaseRequestWithDate(System.DateTime date);
        IPagedList<StorePackagePurchaseRequest> GetStorePackagePurchaseRequest(int page, int pageSize);
    }
}
