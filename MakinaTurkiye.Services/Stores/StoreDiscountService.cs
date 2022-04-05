using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreDiscountService : IStoreDiscountService
    {
        IRepository<StoreDiscount> _storeDiscountRepository;

        public StoreDiscountService(IRepository<StoreDiscount> storeDiscountRepository)
        {
            this._storeDiscountRepository = storeDiscountRepository;
        }

        public StoreDiscount GetStoreDiscountByOrderId(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentNullException("orderId");
            var query = _storeDiscountRepository.Table;
            return query.FirstOrDefault(x => x.OrderId == orderId);
        }

        public void InsertStoreDiscount(StoreDiscount storeDiscount)
        {
            if (storeDiscount == null)
                throw new ArgumentNullException("storeDiscount");
            _storeDiscountRepository.Insert(storeDiscount);
        }

        public void UpdateStoreDiscount(StoreDiscount storeDiscount)
        {
            if (storeDiscount == null)
                throw new ArgumentNullException("storeDiscount");
            _storeDiscountRepository.Update(storeDiscount);
        }
    }
}
