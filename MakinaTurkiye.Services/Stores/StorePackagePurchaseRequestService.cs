
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Entities.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StorePackagePurchaseRequestService : IStorePackagePurchaseRequestService
    {
        IRepository<StorePackagePurchaseRequest> _StorePackagePurchaseRequestRepository;
        IRepository<Store> _StoreRepository;

        public StorePackagePurchaseRequestService(IRepository<StorePackagePurchaseRequest> storePackagePurchaseRequestRepository, IRepository<Store> storeRepository)
        {
            _StorePackagePurchaseRequestRepository = storePackagePurchaseRequestRepository;
            _StoreRepository = storeRepository;
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

        public StorePackagePurchaseRequest GetStorePackagePurchaseRequestWithDate(int MainMartyId, System.DateTime date)
        {
            if (date == default)
                throw new ArgumentNullException("StorePackagePurchaseRequest");
            var query = _StorePackagePurchaseRequestRepository.Table;
            query = query.Where(s => s.Date >= date.Date && s.MainPartyId == MainMartyId);
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
                            AuthorizedId = Store.AuthorizedId,
                            PortfoyUserId = Store.PortfoyUserId,
                        }).OrderByDescending(x => x.Date).ToList();


                foreach (var item in tmp)
                {
                    var StorePackagePurchaseRequest = new StorePackagePurchaseRequest
                    {
                        Id = item.Id,
                        Desciption = item.Desciption,
                        Date = item.Date,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        MainPartyId = item.MainPartyId,
                        Phone = item.Phone,
                        ProductQuantity = item.ProductQuantity,
                        StoreName = item.StoreName,
                        AuthorizedId = item.AuthorizedId,
                        PortfoyUserId = item.PortfoyUserId,
                    };
                    result.Add(StorePackagePurchaseRequest);
                };
            }
            return result;
        }
    }
}



