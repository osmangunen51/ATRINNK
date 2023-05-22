using Trinnk.Entities.Tables.Checkouts;
using System.Collections.Generic;

namespace Trinnk.Services.Checkouts
{
    public interface IOrderInstallmentService
    {
        void InsertOrderInstallment(OrderInstallment orderInstallment);
        void DeleteOrderInstallment(OrderInstallment orderInstallment);
        void UpdateOrderInstallment(OrderInstallment orderInstallment);
        OrderInstallment GetOrderInstallmentByOrderInstallmentId(int orderInstallmentId);
        List<OrderInstallment> GetOrderInstallmentsByOrderId(int orderId);


    }
}
