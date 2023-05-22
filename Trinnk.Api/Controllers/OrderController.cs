using Trinnk.Api.Helpers;
using Trinnk.Api.View;
using Trinnk.Api.View.Account;
using Trinnk.Core.Infrastructure;
using Trinnk.Entities.Tables.Checkouts;
using Trinnk.Services.Checkouts;
using Trinnk.Services.Members;
using Trinnk.Services.Packets;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Trinnk.Api.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IOrderService _orderService;
        private readonly IPacketService _packetService;

        public OrderController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _orderService = EngineContext.Current.Resolve<IOrderService>();
            _packetService = EngineContext.Current.Resolve<IPacketService>();
        }

        //public OrderController(IMemberService memberService,
        //                     IMemberStoreService memberStoreService,
        //                     IOrderService orderService,
        //                     IPacketService packetService)
        //{
        //    this._memberService = memberService;
        //    this._memberStoreService = memberStoreService;
        //    this._orderService = orderService;
        //    this._packetService = packetService;
        //}

        public HttpResponseMessage GetMyOrders()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    int memberMainPartyId = member.MainPartyId;
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
                    if (memberStore == null)
                    {
                        processStatus.Result = "MemberStore bulunamadı Bu işlemi yapamazsınız";
                        processStatus.Message.Header = "AddOrderList";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        OrderPageModel orderPageModel = new OrderPageModel();
                        var orders = _orderService.GetOrdersByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId)).Where(x => x.RecordDate.Date.Year > 2017);
                        foreach (var item in orders)
                        {
                            var orderListItem = new OrderListItem
                            {
                                AccountId = item.AccountId,
                                Address = item.Address,
                                CreditCardInstallmentId = item.CreditCardInstallmentId,
                                InvoiceNumber = item.InvoiceNumber,
                                InvoiceStatus = item.InvoiceStatus,
                                MainPartyId = item.MainPartyId,
                                OrderCode = item.OrderCode,
                                OrderDescription = item.OrderDescription,
                                OrderId = item.OrderId,
                                OrderNo = item.OrderNo,
                                OrderPacketEndDate = item.OrderPacketEndDate,
                                OrderPrice = item.OrderPrice,
                                OrderType = item.OrderType,
                                PacketStatu = item.PacketStatu,
                                RecordDate = item.RecordDate,
                                TaxNo = item.TaxNo,
                                TaxOffice = item.TaxOffice
                            };
                            var packet = _packetService.GetPacketByPacketId(item.PacketId);
                            orderListItem.PacketName = packet.PacketName;
                            var orderPayments = _orderService.GetPaymentsByOrderId(item.OrderId);
                            if (item.PriceCheck == true)
                            {
                                var payment = new Payment();
                                payment.OrderId = item.OrderId;
                                payment.PaidAmount = item.OrderPrice;
                                payment.RestAmount = 0;
                                payment.RecordDate = item.RecordDate;
                                payment.PaymentType = item.OrderType;
                                _orderService.InsertPayment(payment);
                                orderPayments.Add(payment);
                            }
                            decimal restAmount = item.OrderPrice;
                            if (orderPayments.Count > 0)
                            {
                                var orderPayment = orderPayments.OrderByDescending(x => x.RecordDate).LastOrDefault();
                                restAmount = orderPayment.RestAmount;
                            }
                            orderListItem.RestAmount = restAmount;
                            orderPageModel.OrderListItems.Add(orderListItem);
                        }
                        processStatus.Result = orderPageModel;
                        processStatus.Message.Header = "AddOrderList";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "AddOrderList";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "AddOrderList";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}