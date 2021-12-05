using MakinaTurkiye.Entities.Tables.Checkouts;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.Mvc;
using NeoSistem.MakinaTurkiye.Web.Factories;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Help;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class HelpController : BaseController
    {
        #region Fields

        private ICategoryService _categoryService;
        private ISalesDocumentInputFactory _salesDocumentInputFactory;
        private IOrderService _orderService;
        private IMessagesMTService _messageMtService;
        private IMemberStoreService _memberStoreService;
        private IStoreService _storeService;
        private IMemberService _memberService;
        private IPacketService _packetService;



        #endregion

        #region Ctor

        public HelpController(ICategoryService categoryService,
            ISalesDocumentInputFactory salesDocumentInputFactory,
            IOrderService orderService,
            IMemberStoreService memberStoreService,
            IStoreService storeService,
            IMemberService memberService,
            IPacketService packetService,
            IMessagesMTService messagesMTService)
        {
            this._categoryService = categoryService;
            this._salesDocumentInputFactory = salesDocumentInputFactory;
            this._orderService = orderService;
            this._memberService = memberService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._memberStoreService = memberStoreService;
            this._messageMtService = messagesMTService;
        }

        #endregion


        #region Methods

        [Compress]
        public ActionResult Index()
        {
            var request = HttpContext.Request;
            ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
            return View();
        }

        public ActionResult Menu()
        {
            List<MTHelpMenuModel> model = new List<MTHelpMenuModel>();
            var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                model.Add(new MTHelpMenuModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryParentId = item.CategoryParentId.GetValueOrDefault(),
                    HelpUrl = url
                });
            }
            return View(model);

        }
        public ActionResult MenuSub(int categoryId)
        {
            if (categoryId <= 0)
            {
                return RedirectToAction("Index");
            }

            var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
            var currentMenuModel = helpCategories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (currentMenuModel == null)
            {
                return RedirectToAction("Index");
            }

            List<MTHelpMenuModel> menuModels = new List<MTHelpMenuModel>();
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                menuModels.Add(new MTHelpMenuModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryParentId = item.CategoryParentId.GetValueOrDefault(),
                    HelpUrl = url
                });
            }

            MTHelpTopModel model = new MTHelpTopModel();
            model.MenuItemModels = menuModels;
            model.CurrentMenuModel = menuModels.FirstOrDefault(c => c.CategoryId == categoryId);

            return View(model);
        }

        [Compress]
        public ActionResult YardimDetay(int categoryId, string storeId, string orderId)
        {
            if (categoryId <= 0)
            {
                return RedirectToAction("Index");
            }

            var category = _categoryService.GetCategoryByCategoryId(categoryId);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var request = HttpContext.Request;
            string helpUrl = UrlBuilder.GetHelpCategoryUrl(category.CategoryId, category.CategoryName);
            string absUrl = request.Url.AbsolutePath;

            //#if !DEBUG
            //      absUrl=request.Url.AbsoluteUri;
            //#endif
            //if (absUrl != helpUrl)
            //{
            //    return RedirectPermanent(helpUrl);
            //}

            var model = new MTHelpDetailModel();
            try
            {


                var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
                var subHelpCategories = helpCategories.Where(c => c.CategoryParentId == categoryId);
                foreach (var item in subHelpCategories)
                {
                    string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                    model.SubMenuItemModels.Add(new MTHelpMenuModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = item.CategoryParentId.GetValueOrDefault(),
                        HelpUrl = url
                    });
                }

                model.CategoryId = category.CategoryId;
                model.CategoryName = category.CategoryName;
                model.Content = category.Content;
                model.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;

                //SeoPageType = (byte)PageType.HelpCategory;


                if (!string.IsNullOrEmpty(storeId) && !string.IsNullOrEmpty(orderId))
                {
                    var orderConfirmation = _orderService.GetOrderConfirmationByOrderId(Convert.ToInt32(orderId));

                    model.OrderConfirmationForm = new OrderConfirmationFormModel { OrderId = Convert.ToInt32(orderId), StoreMainPartyId = Convert.ToInt32(storeId), ReturnUrl = Request.Url.ToString(),
                    IsConfirmed=orderConfirmation!=null, RecordDate=orderConfirmation!=null ? orderConfirmation.RecordDate : DateTime.Now};

                    var def = _salesDocumentInputFactory.getPayload(Convert.ToInt32(storeId), Convert.ToInt32(orderId));
                    foreach (var item in def)
                    {
                        if(item.Key== "{peşin}" && item.Value!= "Taksit")
                        {
                            model.Content = model.Content.Replace("<strong>Taksit</strong> :", "");
                        }
                        model.Content = model.Content.Replace(item.Key, item.Value);
                    }
                }
            }
            catch
            {
                return Redirect("/yardim");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult YardımDetay(OrderConfirmationFormModel orderConfirm)
        {
            if (orderConfirm.IsConfirmed)
            {
                var orderConfirmationLast = _orderService.GetOrderConfirmationByOrderId(orderConfirm.OrderId);
                if (orderConfirmationLast == null)
                {
                    var order = _orderService.GetOrderByOrderId(orderConfirm.OrderId);

                    OrderConfirmation orderConfirmation = new OrderConfirmation
                    {
                        OrderId = orderConfirm.OrderId,
                        RecordDate = DateTime.Now,
                        StoreMainPartyId = orderConfirm.StoreMainPartyId,
                        IpAddress = Request.UserHostAddress,

                    };

                    TempData["confirmed"] = true;
                    _orderService.InsertOrderConfirmation(orderConfirmation);


                    DateTime orderBeginDate = DateTime.Now;
                    if (order.PacketStartDate.HasValue)
                        orderBeginDate = order.PacketStartDate.Value;

                    //hesap numnarası fiyatı
                    var packet =_packetService.GetPacketByPacketId(order.PacketId);
                    var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    var member = _memberService.GetMemberByMainPartyId(memberMainPartyId);
            
                    #region emailicin
                    var settings = ConfigurationManager.AppSettings;
                    MailMessage mail = new MailMessage();
                    MessagesMT mailT = _messageMtService.GetMessagesMTByMessageMTName("goldenpro");
                    mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz

                    mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                    string template = mailT.MessagesMTPropertie;
                    string dateimiz = orderBeginDate.AddDays(packet.PacketDay).ToString();
                    template = template.Replace("#uyeliktipi#", packet.PacketName).Replace("#uyelikbaslangıctarihi#", orderBeginDate.ToString("D")).Replace("#uyelikbitistarihi#", dateimiz).Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#pakettipi#", packet.PacketName);
                    mail.Body = template;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                    #endregion

                    store.PacketId = packet.PacketId;

                    store.StorePacketBeginDate = orderBeginDate;
                    var packetfeauture = _packetService.GetPacketFeatureByPacketIdAndPacketFeatureTypeId(packet.PacketId, 3);
                    if (packetfeauture.FeatureContent != null)
                    {
                        store.ProductCount = 9999;
                    }
                    else if (packetfeauture.FeatureActive != null)
                    {
                        if (packetfeauture.FeatureActive == true)
                        {
                            store.ProductCount = 3;
                        }
                        else if (packetfeauture.FeatureActive == false)
                        {
                            store.ProductCount = 0;
                        }
                    }
                    else
                    {
                        store.ProductCount = packetfeauture.FeatureProcessCount;
                    }
                    bool isRenewPacket = false;
                    var orderLastList = _orderService.GetOrdersByMainPartyId(store.MainPartyId);

                    foreach (var item in orderLastList.ToList())
                    {
                        item.OrderPacketEndDate = store.StorePacketEndDate;
                        _orderService.UpdateOrder(item);
                    }
                    if (order.PacketDay.HasValue)
                        store.StorePacketEndDate = orderBeginDate.AddDays(order.PacketDay.Value);
                    else
                        store.StorePacketEndDate = orderBeginDate.AddDays(packet.PacketDay);

                    if (!order.ProductId.HasValue || (order.ProductId.HasValue && order.ProductId.Value == 0))
                    {
                        _storeService.UpdateStore(store);
                    }

                    order.PacketStatu = (byte)PacketStatu.Onaylandi;
                    if (order.PacketDay.HasValue)
                        order.OrderPacketEndDate = orderBeginDate.AddDays(order.PacketDay.Value);
                    else
                        order.OrderPacketEndDate = orderBeginDate.AddDays(packet.PacketDay);

                    if (orderLastList.Count > 1)
                    {
                        isRenewPacket = true;
                    }
                    order.IsRenewPacket = isRenewPacket;
                    _orderService.UpdateOrder(order);

                    #region paymentsadd
                    if (order.PriceCheck == true)
                    {
                        var paymentsNew = _orderService.GetPaymentsByOrderId(order.OrderId);
                        var paidPriceSum = paymentsNew.Select(x => x.PaidAmount).Sum();
                        var restAmountPaid = order.OrderPrice - paidPriceSum;
                        var newPayment1 = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
                        newPayment1.OrderId = order.OrderId;
                        newPayment1.PaidAmount = restAmountPaid;
                        newPayment1.RecordDate = DateTime.Now;
                        newPayment1.RestAmount = 0;
                        newPayment1.PaymentType = order.OrderType;
                        _orderService.InsertPayment(newPayment1);

                    }
                    #endregion
                }
            }
 
            return Redirect(orderConfirm.ReturnUrl);
        }

        #endregion

    }
}
