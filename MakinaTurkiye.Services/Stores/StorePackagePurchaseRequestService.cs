using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StorePackagePurchaseRequestService : IStorePackagePurchaseRequestService
    {
        IRepository<StorePackagePurchaseRequest> _StorePackagePurchaseRequestRepository;
        IRepository<Store> _StoreRepository;
        public StorePackagePurchaseRequestService(IRepository<StorePackagePurchaseRequest> StorePackagePurchaseRequestRepository, IRepository<Store> StoreRepository)
        {
            this._StorePackagePurchaseRequestRepository = StorePackagePurchaseRequestRepository;
            this._StoreRepository = StoreRepository;
        }


        public void InsertStorePackagePurchaseRequest(StorePackagePurchaseRequest StorePackagePurchaseRequest)
        {
            if (StorePackagePurchaseRequest == null)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            _StorePackagePurchaseRequestRepository.Insert(StorePackagePurchaseRequest);
        }

        public void UpdateStorePackagePurchaseRequest(StorePackagePurchaseRequest StorePackagePurchaseRequest)
        {
            if (StorePackagePurchaseRequest == null)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            _StorePackagePurchaseRequestRepository.Update(StorePackagePurchaseRequest);
        }
        public void DeleteStorePackagePurchaseRequest(StorePackagePurchaseRequest StorePackagePurchaseRequest)
        {
            if (StorePackagePurchaseRequest == null)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            _StorePackagePurchaseRequestRepository.Delete(StorePackagePurchaseRequest);
        }

        public StorePackagePurchaseRequest GetStorePackagePurchaseRequestWithDate(System.DateTime date)
        {
            if (date == default)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            var query = _StorePackagePurchaseRequestRepository.Table;
            query = query.Where(s => s.Date>=date.Date);
            return query.FirstOrDefault();
        }

        public IPagedList<StorePackagePurchaseRequest> GetStorePackagePurchaseRequest(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
