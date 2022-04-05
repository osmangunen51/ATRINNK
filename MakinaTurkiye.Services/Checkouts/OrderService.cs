using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Orders;
using MakinaTurkiye.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Checkouts
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IRepository<Order> _orderRepository;
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<OrderDescription> _orderDescriptionRepository;
        private readonly IRepository<ReturnInvoice> _returnInvoiceRepository;
        private readonly IRepository<OrderConfirmation> _orderConfirmationRepository;

        #endregion

        #region Ctor

        public OrderService(IRepository<Order> orderRepository, IDbContext dbContext,
            IRepository<Payment> paymentRepository,
            IRepository<OrderDescription> orderDescriptionRepository,
            IDataProvider dataProvider, IRepository<ReturnInvoice> returnInvoiceRepository,
            IRepository<OrderConfirmation> orderConfirmationRepository)
        {
            this._orderRepository = orderRepository;
            this._dbContext = dbContext;
            this._paymentRepository = paymentRepository;
            this._orderDescriptionRepository = orderDescriptionRepository;
            this._dataProvider = dataProvider;
            this._returnInvoiceRepository = returnInvoiceRepository;
            this._orderConfirmationRepository = orderConfirmationRepository;
        }

        #endregion

        #region Methods

        public void InsertOrder(Order order)
        {
            _orderRepository.Insert(order);
        }

        public Order GetOrderByOrderId(int orderId)
        {
            if (orderId == 0)
                return new Order();

            var query = _orderRepository.Table;
            return query.FirstOrDefault(o => o.OrderId == orderId);
        }

        public int GetNewOrderId()
        {
            var query = _orderRepository.Table;
            var order = query.OrderByDescending(o => o.OrderId).FirstOrDefault();
            return order.OrderId + 1;
        }

        public void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            _orderRepository.Update(order);

        }

        public IList<Order> GetOrdersByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentException("mainPartyId");

            var orders = _orderRepository.Table.Where(o => o.MainPartyId == mainPartyId);
            return orders.ToList();
        }

        public IList<Order> GetOrdersWithNullInvoiceNumber()
        {
            var query = _orderRepository.Table;
            query = query.Where(x => x.InvoiceNumber == "" || x.InvoiceNumber == null);
            return query.ToList();
        }

        public void InsertPayment(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            _paymentRepository.Insert(payment);

        }

        public void DeletePayment(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            _paymentRepository.Delete(payment);
        }

        public void UpdatePayment(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException("payment");

            _paymentRepository.Update(payment);
        }

        public IList<Payment> GetAllPayments()
        {
            var query = _paymentRepository.Table;
            return query.ToList();
        }

        public Payment GetPaymentByPaymentId(int PaymentId)
        {
            if (PaymentId == 0)
                throw new ArgumentNullException("paymentId");

            var query = _paymentRepository.Table;
            return query.FirstOrDefault(x => x.PaymentId == PaymentId);
        }

        public List<Payment> GetPaymentsByOrderId(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentNullException("orderId");

            var query = _paymentRepository.Table;
            query = query.Where(x => x.OrderId == orderId);
            return query.ToList();
        }

        public void InsertOrderDescription(OrderDescription orderDescription)
        {
            if (orderDescription == null)
                throw new ArgumentNullException("orderDescription");

            _orderDescriptionRepository.Insert(orderDescription);
        }

        public void DeleteOrderDescription(OrderDescription orderDescription)
        {
            if (orderDescription == null)
                throw new ArgumentNullException("orderDescription");

            _orderDescriptionRepository.Delete(orderDescription);
        }

        public List<OrderDescription> GetOrderDescriptionsByOrderId(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentNullException("orderId");

            var query = _orderDescriptionRepository.Table;
            query = query.Where(x => x.OrderId == orderId);
            return query.ToList();
        }

        public OrderDescription GetOrderDescriptionByOrderDescriptionId(int orderDescriptionId)
        {
            if (orderDescriptionId == 0)
                throw new ArgumentNullException("orderDescriptionId");

            var query = _orderDescriptionRepository.Table;
            return query.FirstOrDefault(x => x.OrderDescriptionId == orderDescriptionId);
        }

        public IList<Order> GetAllOrders()
        {
            var query = _orderRepository.Table;
            return query.ToList();
        }

        public IList<OrderReportResultModel> GetOrderReports(int pageDimension, int page, string where, string orderName, string order, out int totalRecord, out int totalPrice, out int totalPaid)
        {
            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;

            var pTotalPrice = _dataProvider.GetParameter();
            pTotalPrice.ParameterName = "TotalPrice";
            pTotalPrice.DbType = DbType.Int32;
            pTotalPrice.Direction = ParameterDirection.Output;

            var pTotalPaid = _dataProvider.GetParameter();
            pTotalPaid.ParameterName = "TotalPaid";
            pTotalPaid.DbType = DbType.Int32;
            pTotalPaid.Direction = ParameterDirection.Output;

            var pPageDimension = _dataProvider.GetParameter();
            pPageDimension.ParameterName = "PageDimension";
            pPageDimension.Value = pageDimension;
            pPageDimension.DbType = DbType.Int32;



            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "Page";
            pPageSize.Value = page;
            pPageSize.DbType = DbType.Int32;


            var pWhere = _dataProvider.GetParameter();
            pWhere.ParameterName = "Where";
            pWhere.Value = where;
            pWhere.DbType = DbType.String;

            var pOrder = _dataProvider.GetParameter();
            pOrder.ParameterName = "Order";
            pOrder.Value = order;
            pOrder.DbType = DbType.String;

            var pOrderName = _dataProvider.GetParameter();
            pOrderName.ParameterName = "OrderName";
            pOrderName.Value = orderName;
            pOrderName.DbType = DbType.String;

            var result = _dbContext.SqlQuery<OrderReportResultModel>("KM_spOrderTahsilat @TotalRecord out, @PageDimension, @Page, @Where, @OrderName, @Order, @TotalPrice out, @TotalPaid out", pTotalRecords, pPageDimension, pPageSize, pWhere, pOrderName, pOrder, pTotalPrice, pTotalPaid).ToList();
            totalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            totalPrice = (pTotalPrice.Value != DBNull.Value) ? Convert.ToInt32(pTotalPrice.Value) : 0;
            totalPaid = (pTotalPaid.Value != DBNull.Value) ? Convert.ToInt32(pTotalPaid.Value) : 0;

            return result;


        }

        public void InsertReturnInvoice(ReturnInvoice returnInvoice)
        {
            if (returnInvoice == null)
                throw new ArgumentNullException("returnInvoice");

            _returnInvoiceRepository.Insert(returnInvoice);
        }


        public void DelteReturnInvoice(ReturnInvoice returnInvoice)
        {
            if (returnInvoice == null)
                throw new ArgumentNullException("returnInvoice");

            _returnInvoiceRepository.Delete(returnInvoice);

        }

        public void UpdateReturnInvoice(ReturnInvoice returnInvoice)
        {
            if (returnInvoice == null)
                throw new ArgumentNullException("returnInvoice");

            _returnInvoiceRepository.Update(returnInvoice);
        }

        public List<ReturnInvoice> GetReturnInvoicesByOrderId(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentNullException("orderId");

            var query = _returnInvoiceRepository.Table;
            query = query.Where(x => x.OrderId == orderId);
            return query.ToList();
        }

        public ReturnInvoice GetReturnInvoiceByReturnInvoiceId(int invoiceId)
        {
            if (invoiceId == 0)
                throw new ArgumentNullException("invoiceId");

            var query = _returnInvoiceRepository.Table;
            return query.FirstOrDefault(x => x.ReturnInvoiceId == invoiceId);
        }

        public void InsertOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            _orderConfirmationRepository.Insert(orderConfirmation);
        }

        public OrderConfirmation GetOrderConfirmationByOrderId(int orderId)
        {
            var query = _orderConfirmationRepository.Table;
            return query.FirstOrDefault(x => x.OrderId == orderId);
        }

        #endregion

    }
}
