using Trinnk.Core;
using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IStorePackagePurchaseRequestService
    {
        void InsertStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        void DeleteStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        void UpdateStorePackagePurchaseRequest(StorePackagePurchaseRequest storePackagePurchaseRequest);
        StorePackagePurchaseRequest GetStorePackagePurchaseRequestWithDate(int MainMartyId, System.DateTime date);
        List<StorePackagePurchaseRequest> GetAll();
        IPagedList<StorePackagePurchaseRequest> GetStorePackagePurchaseRequest(int page, int pageSize);
    }
}
