using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using MakinaTurkiye.Entities.Tables.Checkouts;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Packets;
using System;
using System.Collections.Generic;
using System.Web;
using MakinaTurkiye.Core;
namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
    public class IyzicoPayment
    {


        public Order Order { get; set; }
        public global::MakinaTurkiye.Entities.Tables.Common.Address Address { get; set; }
        public Packet Packet { get; set; }
        public Member Member { get; set; }
        public string Amount { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardNameSurname { get; set; }
        public string Cvv2 { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Installment { get; set; }
        public int? DopingDay { get; set; }
        public string CallBakUrl { get; set; }

        public Phone Phone { get; set; }

        public IyzicoPayment(Order order, Member member, global::MakinaTurkiye.Entities.Tables.Common.Address address, Packet packet,
            string amount, string creditCardNumber, string cardNameSurname, string cvv2, string expireMont, string expireYear, int? dopingDay, string callBackUrl, Phone phone, string installment)
        {
            amount = amount.Trim();
            this.Order = order;
            this.Member = member;
            this.Address = address;
            this.Packet = packet;
            this.Amount = amount;
            this.CreditCardNumber = creditCardNumber;
            this.CardNameSurname = cardNameSurname;
            this.Cvv2 = cvv2;
            this.ExpireMonth = expireMont;
            this.ExpireYear = expireYear;
            this.DopingDay = dopingDay;
            this.CallBakUrl = callBackUrl;
            this.Phone = phone;
            this.Installment = installment;

        }
        public IyzicoResultModel CreatePaymentRequest()
        {
            this.Amount = this.Amount.Trim();
            Options options = new Options();
            options.ApiKey = AppSettings.IyzicoApiKey;
            options.SecretKey = AppSettings.IyzicoSecureKey;
            options.BaseUrl = AppSettings.IyzicoApiUrl;
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Order.OrderId.ToString();
            request.Price = this.Amount;
            request.PaidPrice = this.Amount;
            request.Currency = Iyzipay.Model.Currency.TRY.ToString();
            if (!string.IsNullOrEmpty(this.Installment))
                request.Installment = Convert.ToInt32(this.Installment);
            else
                request.Installment = 1;

            request.BasketId = Order.OrderId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = AppSettings.SiteUrlWithoutLastSlash + this.CallBakUrl;
            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = CardNameSurname;
            paymentCard.CardNumber = this.CreditCardNumber;
            paymentCard.ExpireMonth = this.ExpireMonth;
            paymentCard.ExpireYear = this.ExpireYear;
            paymentCard.Cvc = Cvv2;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            string addressData = "";
            if (Address != null)
                addressData = Address.GetFullAddress();
            else
                addressData = Order.Address;
            request.Buyer = GetBuyer(addressData);
            request.ShippingAddress = GetShippingAddress(addressData);
            request.BillingAddress = GetBillingAddress(addressData);

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = Order.OrderId.ToString();

            if (Order.ProductId.HasValue)
            {
                firstBasketItem.Name = Packet.PacketName.Replace("30", this.DopingDay.ToString());
            }
            else
            {
                firstBasketItem.Name = Packet.PacketName;
            }

            firstBasketItem.Category1 = "Firma Paketi";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = this.Amount;
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;
            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);
            IyzicoResultModel result = new IyzicoResultModel();

            //var status = payment.Status;
            var errorCode = threedsInitialize.ErrorCode;
            var errMessage = threedsInitialize.ErrorMessage;
            string htmlContent = threedsInitialize.HtmlContent;
            result.Status = threedsInitialize.Status;
            result.HtmlContent = htmlContent;
            result.ErrorCode = errorCode;
            result.ErrorMessage = errMessage;
            return result;

        }
        private Iyzipay.Model.Address GetShippingAddress(string addressData)
        {
            Iyzipay.Model.Address shippingAddress = new Iyzipay.Model.Address();
            shippingAddress.ContactName = Member.MemberName + " " + Member.MemberSurname;
            shippingAddress.City = "İstanbul";
            shippingAddress.Country = "Turkiye";
            shippingAddress.Description = addressData;
            shippingAddress.ZipCode = "";
            return shippingAddress;

        }
        private Iyzipay.Model.Address GetBillingAddress(string addressData)
        {
            Iyzipay.Model.Address billingAddress = new Iyzipay.Model.Address();
            billingAddress.ContactName = Member.MemberName + " " + Member.MemberSurname;
            billingAddress.City = "İstanbul";
            billingAddress.Country = "Türkiye";
            billingAddress.Description = addressData;
            billingAddress.ZipCode = "";
            return billingAddress;

        }

        private Buyer GetBuyer(string address)
        {
            Buyer buyer = new Buyer();

            buyer.Id = Member.MainPartyId.ToString();
            buyer.Name = Member.MemberName;
            buyer.Surname = !string.IsNullOrEmpty(Member.MemberSurname) ? Member.MemberSurname : Member.MemberName;
            if (Phone == null)
                buyer.GsmNumber = "+9005326508841";
            else
                buyer.GsmNumber = Phone.PhoneCulture + Phone.PhoneAreaCode + Phone.PhoneNumber;

            buyer.Email = Member.MemberEmail;
            buyer.IdentityNumber = "46887452314";
            buyer.LastLoginDate = "";
            buyer.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationAddress = address;
            buyer.Ip = HttpContext.Current.Request.UserHostAddress.ToString();
            buyer.City = "İstanbul";
            buyer.Country = "Türkiye";
            buyer.ZipCode = "";
            return buyer;
        }
    }
    public class IyzicoResultModel
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string HtmlContent { get; set; }
        public string Status { get; set; }
    }
}