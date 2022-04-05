using MakinaTurkiye.Entities.StoredProcedures.Orders;
using MakinaTurkiye.Entities.Tables.Checkouts;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Checkouts
{
    public interface IOrderService
    {
        Order GetOrderByOrderId(int orderId);
        IList<Order> GetOrdersByMainPartyId(int StoreMainPartyId);

        int GetNewOrderId();

        void InsertOrder(Order order);
        void UpdateOrder(Order order);

        IList<Order> GetOrdersWithNullInvoiceNumber();
        IList<Order> GetAllOrders();

        IList<OrderReportResultModel> GetOrderReports(int pageDimension, int page, string where, string orderName, string order, out int totalRecord, out int totalPrice, out int totalPaid);


        void InsertPayment(Payment payment);
        void DeletePayment(Payment payment);
        void UpdatePayment(Payment payment);

        IList<Payment> GetAllPayments();
        List<Payment> GetPaymentsByOrderId(int orderId);
        Payment GetPaymentByPaymentId(int PaymentId);


        void InsertOrderDescription(OrderDescription orderDescription);
        void DeleteOrderDescription(OrderDescription orderDescription);
        List<OrderDescription> GetOrderDescriptionsByOrderId(int orderId);
        OrderDescription GetOrderDescriptionByOrderDescriptionId(int orderDescriptionId);

        void InsertReturnInvoice(ReturnInvoice returnInvoice);
        void DelteReturnInvoice(ReturnInvoice returnInvoice);
        void UpdateReturnInvoice(ReturnInvoice returnInvoice);

        List<ReturnInvoice> GetReturnInvoicesByOrderId(int orderId);
        ReturnInvoice GetReturnInvoiceByReturnInvoiceId(int invoiceId);


        void InsertOrderConfirmation(OrderConfirmation orderConfirmation);

        OrderConfirmation GetOrderConfirmationByOrderId(int orderId);

    }
}
