using MakinaTurkiye.Entities.Tables.Stores;

namespace MakinaTurkiye.Services.Stores
{
    public interface IStoreDiscountService
    {
        void InsertStoreDiscount(StoreDiscount storeDiscount);
        void UpdateStoreDiscount(StoreDiscount storeDiscount);
        StoreDiscount GetStoreDiscountByOrderId(int orderId);


    }
}
