using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Checkouts
{
    public class OrderInstallmentService : IOrderInstallmentService
    {
        IRepository<OrderInstallment> _orderInstallmenRepository;

        public OrderInstallmentService(IRepository<OrderInstallment> orderInstallmenRepository)
        {
            this._orderInstallmenRepository = orderInstallmenRepository;
        }

        public void DeleteOrderInstallment(OrderInstallment orderInstallment)
        {
            if (orderInstallment == null)
                throw new ArgumentNullException("orderInstallment");
            _orderInstallmenRepository.Delete(orderInstallment);
        }

        public OrderInstallment GetOrderInstallmentByOrderInstallmentId(int orderInstallmentId)
        {
            if (orderInstallmentId == 0)
                throw new ArgumentNullException("orderInstallmentId");
            var query = _orderInstallmenRepository.Table;
            return query.FirstOrDefault(x => x.OrderInstallmentId == orderInstallmentId);
        }
        public List<OrderInstallment> GetOrderInstallmentsByOrderId(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentNullException("orderId");
            var query = _orderInstallmenRepository.Table;
            query = query.Where(x => x.OrderId == orderId);
            return query.ToList();
        }
        public void InsertOrderInstallment(OrderInstallment orderInstallment)
        {
            if (orderInstallment == null)
                throw new ArgumentNullException("orderInstallment");
            _orderInstallmenRepository.Insert(orderInstallment);
        }
        public void UpdateOrderInstallment(OrderInstallment orderInstallment)
        {
            if (orderInstallment == null)
                throw new ArgumentNullException("orderInstallment");
            _orderInstallmenRepository.Update(orderInstallment);
        }
    }
}
