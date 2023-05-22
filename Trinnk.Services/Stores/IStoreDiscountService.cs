using Trinnk.Entities.Tables.Stores;

namespace Trinnk.Services.Stores
{
    public interface IStoreDiscountService
    {
        void InsertStoreDiscount(StoreDiscount storeDiscount);
        void UpdateStoreDiscount(StoreDiscount storeDiscount);
        StoreDiscount GetStoreDiscountByOrderId(int orderId);


    }
}
