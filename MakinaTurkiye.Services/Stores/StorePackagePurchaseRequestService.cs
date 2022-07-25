using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
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

        public StorePackagePurchaseRequest GetStorePackagePurchaseRequestWithDate(int MainMartyId,System.DateTime date)
        {
            if (date == default)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            var query = _StorePackagePurchaseRequestRepository.Table;
            query = query.Where(s => s.Date>=date.Date && s.MainPartyId==MainMartyId);
            return query.FirstOrDefault();
        }
        public IPagedList<StorePackagePurchaseRequest> GetStorePackagePurchaseRequest(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<StorePackagePurchaseRequest> GetAll()
        {
            List<StorePackagePurchaseRequest> result = new List<StorePackagePurchaseRequest>();
            var queryStorePackagePurchaseRequest = _StorePackagePurchaseRequestRepository.Table;
            var queryStore = _StoreRepository.Table;
            if (queryStorePackagePurchaseRequest != null)
            {
              var tmp = queryStorePackagePurchaseRequest.Join(
                      queryStore, 
                      StorePackagePurchaseRequest => StorePackagePurchaseRequest.MainPartyId,
                      Store => Store.MainPartyId,
                      (StorePackagePurchaseRequest, Store) => new
                      {
                          Id = StorePackagePurchaseRequest.Id,
                          Desciption = StorePackagePurchaseRequest.Desciption,
                          Date = StorePackagePurchaseRequest.Date,
                          FirstName = StorePackagePurchaseRequest.FirstName,
                          LastName = StorePackagePurchaseRequest.LastName,
                          MainPartyId = StorePackagePurchaseRequest.MainPartyId,
                          Phone = StorePackagePurchaseRequest.Phone,
                          ProductQuantity = StorePackagePurchaseRequest.ProductQuantity,
                          StoreName = Store.StoreName,
                      }).OrderByDescending(x => x.Date).ToList();
                result= tmp.Select(x => new StorePackagePurchaseRequest {
                    Id = x.Id,
                    Desciption = x.Desciption,
                    Date = x.Date,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MainPartyId = x.MainPartyId,
                    Phone = x.Phone,
                    ProductQuantity = x.ProductQuantity,
                    StoreName = x.StoreName,

                }).ToList();
            }
            return result;
        }
    }
}
