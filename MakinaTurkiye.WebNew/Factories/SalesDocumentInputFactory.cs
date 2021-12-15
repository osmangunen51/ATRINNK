using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var phones = _phoneService.GetPhonesByMainPartyId(storeId);
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeId);
            var member = _memberService.GetMemberByMainPartyId(memberStore.MemberMainPartyId.Value);
            var packet = _packetService.GetPacketByPacketId(order.PacketId);
            var goldPacket = _packetService.GetPacketByPacketId(29);

            var orderInstallments = _orderInstallmentService.GetOrderInstallmentsByOrderId(orderId);

            datas.Add("{companyName}", store.StoreName);
            datas.Add("{address}", address.GetFullAddress());
            var phone = new Phone();

            if (phones.Count() > 0)
            {
                phone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Phone);
                if (phone == null)
                {
                    phone = phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneTypeEnum.Gsm);
                }
                datas.Add("{phoneNumber}", phone.PhoneCulture + phone.PhoneAreaCode + " " + phone.PhoneNumber);
            }
            else
            {
                datas.Add("{phoneNumber}", "");
            }
            datas.Add("{email}", member.MemberEmail);
            datas.Add("{vergiNo}", store.TaxNumber);
            datas.Add("{alinanPaket}", packet.PacketName);
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
            datas.Add("{packetDay}", packet.PacketDay.ToString());
            datas.Add("{standartPacketPrice}", goldPacket.PacketPrice.ToString("N2"));
            datas.Add(" {paymentDate}", order.PayDate.HasValue ? order.PayDate.Value.ToString("dd/MM/yyyy") : "");

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