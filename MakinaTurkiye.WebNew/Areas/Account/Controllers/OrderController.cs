using MakinaTurkiye.Entities.Tables.Checkouts;
using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Utilities.Controllers;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Checkouts;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;

using System;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class OrderController : BaseAccountController
    {

        private IOrderService _orderService;
        private IMemberStoreService _memberStoreService;
        private IPacketService _packetService;

        public OrderController(IOrderService orderService, IMemberStoreService memberStoreService, IPacketService packetService)
        {
            this._orderService = orderService;
            this._memberStoreService = memberStoreService;
            this._packetService = packetService;

            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._packetService.CachingGetOrSetOperationEnabled = false;
        }
            
        public ActionResult Index()
        {
            int memberMainPartyId = AuthenticationUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            if(memberStore==null)
            {
                return RedirectToAction("Home");
            }
            else
            {
                OrderPageModel orderPageModel = new OrderPageModel();
                var orders = _orderService.GetOrdersByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId)).Where(x=>x.RecordDate.Date.Year>2017);
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
                    if(item.PriceCheck==true)
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
                    if (orderPayments.Count>0)
                    {
                        var orderPayment = orderPayments.OrderByDescending(x => x.RecordDate).LastOrDefault();
                        restAmount = orderPayment.RestAmount;

                    }
                    orderListItem.RestAmount = restAmount;
                    orderPageModel.OrderListItems.Add(orderListItem);

                }
                orderPageModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Order, (byte)LeftMenuConstants.MyAd.AllAd);

                return View(orderPageModel);
            }
           
        }
    }
}