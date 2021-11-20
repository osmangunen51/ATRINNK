using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;

namespace NeoSistem.MakinaTurkiye.Web.Factories
{
    public class SalesDocumentInputFactory : ISalesDocumentInputFactory
    {
        IStoreService _storeService;
        IOrderService _orderService;
        IAddressService _addressService;
        IPhoneService _phoneService;
        IMemberStoreService _memberStoreService;
        IMemberService _memberService;
        IPacketService _packetService;
        IOrderInstallmentService _orderInstallmentService;

        public SalesDocumentInputFactory(IStoreService storeService,
            IOrderService orderService,
            IAddressService addressService,
            IPhoneService phoneService,
            IMemberStoreService memberStoreService,
            IMemberService memberService,
            IPacketService packetService,
            IOrderInstallmentService orderInstallmentService)
        {
            this._storeService = storeService;
            this._orderService = orderService;
            this._addressService = addressService;
            this._phoneService = phoneService;
            this._memberStoreService = memberStoreService;
            this._memberService = memberService;
            this._packetService = packetService;
            this._orderInstallmentService = orderInstallmentService;
        }

        public Dictionary<string, string> getPayload(int storeId, int orderId)
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();

            var store = _storeService.GetStoreByMainPartyId(storeId);
            var order = _orderService.GetOrderByOrderId(orderId);
            var address = _addressService.GetAddressesByMainPartyId(storeId).FirstOrDefault();
            var phone = _phoneService.GetPhonesByMainPartyId(storeId).FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Phone);
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeId);
            var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
            var packet = _packetService.GetPacketByPacketId(order.PacketId);

            var orderInstallments = _orderInstallmentService.GetOrderInstallmentsByOrderId(orderId);

            datas.Add("{companyName}", store.StoreName);
            datas.Add("{address}", address.GetFullAddress());
            datas.Add("{phoneNumber}", phone.PhoneCulture + phone.PhoneAreaCode + " " + phone.PhoneNumber);
            datas.Add("{email}", member.MemberEmail);
            datas.Add("{vergiNo}", store.TaxNumber);
            datas.Add("{alinanPaket}",packet.PacketName);
            datas.Add("{paketBaslangic}", order.RecordDate.ToString("dd/MM/yyyy"));
            if (order.PacketDay.HasValue)
            {
                DateTime orderPacketEndDate = order.PacketStartDate.Value.AddDays(order.PacketDay.Value);
                datas.Add("{paketBitis}", orderPacketEndDate.ToString("dd/MM/yyyy"));
            }
            datas.Add("{fiyat}", order.OrderPrice.ToString("N2"));
            datas.Add("{faturaAdres}", address.GetFullAddress());
            datas.Add("{vergiDaire}", store.TaxOffice);
            datas.Add("{vergiSicilNo}", "");
            datas.Add("{sozlesmeTarih}", order.RecordDate.ToString("dd/MM/yyyy"));
            datas.Add("{orderType}", getOrderType(order.OrderType));

            if (getOrderType(order.OrderType).Contains("Taksit"))
                datas.Add("{peşin}", "Taksit");
            else
                datas.Add("{peşin}", "Peşin");
            string orderInstallmentText = "";
            int counter = 0;
            foreach (var orderInstallment in orderInstallments)
            {
                string orderInsText = String.Format("{0} tarihli {1}. taksit tutarı : {2} TL<br>", orderInstallment.PayDate.ToString("dd/MM/yyyy"), ++counter, orderInstallment.Amunt.ToString("N2"));
                orderInstallmentText += orderInsText;
            }
            datas.Add("{taksit}", orderInstallmentText);
            
            return datas;
        }
        

        private string getOrderType(int orderType)
        {
      
            switch (orderType)
            {
                case (int)Models.Ordertype.Havale:
                    return "Havale";
               
                case (int)Models.Ordertype.KrediKarti:
                   return "Kredi Kartı";
                case 3:
                    return "Kredi Kartı Vade";
                case 4:
                    return "Havale Taksit";
                case 5:
                    return "Kredi Kartı Taksit";
                default:
                    break;
            }
            return "";
        }

    }
}