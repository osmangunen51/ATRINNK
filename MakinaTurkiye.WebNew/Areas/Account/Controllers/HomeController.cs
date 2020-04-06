using MakinaTurkiye.Services.Checkouts;
using NeoSistem.MakinaTurkiye.Web.Models;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Utilities.HttpHelpers;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.MailHelpers;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Entities.Tables.Messages;

using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Models.AccountModels;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using NeoSistem.MakinaTurkiye.Web.Models.Home;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;

using NeoSistem.EnterpriseEntity.Business;
using NeoSistem.EnterpriseEntity.Extensions.Data;

using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using MakinaTurkiye.Utilities.Controllers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{

    public class HomeController : BaseAccountController
    {
        #region Fields
        //private static ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        private readonly IOrderService _orderService;
        private readonly IPacketService _packetService;
        private readonly IMemberService _memberService;
        private readonly IStoreService _storeService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly ICategoryService _categoryService;
        private readonly IMessageService _messageService;
        private readonly IProductService _productService;
        private readonly IProductCommentService _productCommentService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;

        #endregion

        #region Ctor
        public HomeController(IOrderService orderService, IProductCommentService productCommentService, 
            IPacketService packetService, IMemberService memberService,
            IStoreService storeService, IMemberStoreService memberStoreService, 
            IAddressService addressService, IPhoneService phoneService, ICategoryService categoryService, 
            IMessageService messageService, IProductService productService,
            IMessagesMTService messagesMTService, ICategoryPlaceChoiceService categoryPlaceChoiceService)
        {
            this._orderService = orderService;
            this._packetService = packetService;
            this._memberService = memberService;
            this._storeService = storeService;
            this._memberStoreService = memberStoreService;
            this._addressService = addressService;
            this._phoneService = phoneService;
            this._categoryService = categoryService;
            this._messageService = messageService;
            this._productService = productService;
            this._productCommentService = productCommentService;
            this._messagesMTService = messagesMTService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;

            this._packetService.CachingGetOrSetOperationEnabled=false;
            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._categoryService.CachingGetOrSetOperationEnabled = false;
            this._productService.CachingGetOrSetOperationEnabled = false;
            this._productCommentService.CachingGetOrSetOperationEnabled = false;
            this._categoryPlaceChoiceService.CachingGetOrSetOperationEnabled = false;
        }
        #endregion

        public ActionResult Index(string gelenSayfa, string memberType)
        {
            var model = new MyAccountHomeModel();
            var dataMessage = new Data.Message();
            var messageErrors = _messageService.GetSendMessageErrorsBySenderId(AuthenticationUser.Membership.MainPartyId);
            if (messageErrors.ToList().Count > 0)//gönderilmeyen tüm mailleri gönder
            {
                foreach (var messageItem in messageErrors)
                {
                    var message = new Message
                    {
                        Active = true,
                        MessageContent = messageItem.MessageContent,
                        MessageSubject = messageItem.MessageSubject,
                        MessageDate = DateTime.Now,
                        MessageRead = false,
                        ProductId = messageItem.ProductID,
                    };
                    _messageService.InsertMessage(message);

                    int messageId = message.MessageId;
                    int mainPartyId = Convert.ToInt32(messageItem.ReceiverID);
                    var messageMainParty = new MessageMainParty
                    {
                        MainPartyId = (int)messageItem.SenderID,
                        MessageId = messageId,
                        InOutMainPartyId = mainPartyId,
                        MessageType = (byte)MessageType.Outbox,
                    };
                    _messageService.InsertMessageMainParty(messageMainParty);

                    var curMessageMainParty = new MessageMainParty
                    {
                        InOutMainPartyId = (int)messageItem.SenderID,
                        MessageId = messageId,
                        MainPartyId = mainPartyId,
                        MessageType = (byte)MessageType.Inbox,
                    };
                    _messageService.InsertMessageMainParty(curMessageMainParty);

                    //var receiverUser = _memberService.GetMemberByMainPartyId(mainPartyId);
                    if (messageItem.ProductID != 0)
                    {
                        #region messageissendbilgilendirme

                        var product = _productService.GetProductByProductId(messageItem.ProductID);
                        var kullaniciemail = _memberService.GetMemberByMainPartyId(messageItem.ReceiverID);
                        string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                        string productName = product.ProductName.ToString();
                        //var productno = entities.Products.Where(c => c.ProductId == messageItem.ProductID).SingleOrDefault().ProductNo;
                        //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                        //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
                        //var categorymodelname = entities.Categories.Where(c => c.CategoryId == product.ModelId).SingleOrDefault().CategoryName;
                        //var categorybrandname = entities.Categories.Where(c => c.CategoryId == product.BrandId).SingleOrDefault().CategoryName;
                        string productnosub = productName + " " + product.Brand.CategoryName + " " + product.Model.CategoryName + " İlan no:" + product.ProductNo;
                        string productUrl = Core.Web.Helpers.Helpers.ProductUrl(product.ProductId, productName);

                        MailMessage mail = new MailMessage();
                        MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("mesajınızvar");
                        mail.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(mailadresifirma);                                                              //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = productnosub;                                              //Mail konusu
                        string templatet = mailTemplate.MessagesMTPropertie;
                        templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo).Replace("#messagecontent#", message.MessageContent);
                        mail.Body = templatet;                                                            //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential(mailTemplate.Mail, mailTemplate.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail);

                        #endregion

                        _messageService.DeleteSendMessageError(messageItem);
                    }
                }

                return RedirectToAction("Index", "Message", new { MessagePageType = 2, messages = "true" });

            }
            if (gelenSayfa == "bireyselUyelikOnay")
            {
                ViewData["gelenSayfa"] = "bireyselUyelikOnay";

            }
            else if (gelenSayfa == "KurumsalOnay") ViewData["gelenSayfa"] = "KurumsalOnay";

            model.MemberType = memberType;
            var memberStore1 = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore1.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();
            string mainPartyIdsPar = String.Join(", ", mainPartyIds);
            model.InboxMessageCount = dataMessage.GetItemsByMainPartyIds(mainPartyIdsPar, (byte)MessageType.Inbox).Rows.Count;

            model.ProductCount =  _productService.GetNumberOfProductsByMainPartyId(AuthenticationUser.Membership.MainPartyId);

            var viewCount = _productService.GetViewOfProductsByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            model.ProductTotalViewCount = viewCount;



            bool packetStatu = false;
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                //int memberStoreId = entities.MemberStores.SingleOrDefault(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).StoreMainPartyId.Value;
                var store = _storeService.GetStoreByMainPartyId(AuthenticationUser.Membership.MainPartyId);
                if (store != null)
                {
                    var orderList = _orderService.GetOrdersByMainPartyId(store.MainPartyId);
                    if (orderList.Count > 0)
                    {
                        model.OrderPacketEndDate = store.StorePacketEndDate;
                    }

                    //paket özellikleri getir

                    var packet1 = _packetService.GetPacketByPacketId(Convert.ToInt32(store.PacketId));

                    var showPacketFeatureTypeItems = new List<int>();
                    showPacketFeatureTypeItems.Add(3);
                    showPacketFeatureTypeItems.Add(4);
                    showPacketFeatureTypeItems.Add(5);
                    showPacketFeatureTypeItems.Add(6);
                    showPacketFeatureTypeItems.Add(7);
                    var packetFeatures = packet1.PacketFeatures.Where(p => showPacketFeatureTypeItems.Contains(p.PacketFeatureTypeId));

                    List<PacketFeaturesViewModel> packetFeaturesView = new List<PacketFeaturesViewModel>();
                    List<string> packetFeatureTypeNames = new List<string>();

                    foreach (var packetFeatureItem in packetFeatures)
                    {
                        var packetFeatureType = _packetService.GetPacketFeatureTypeByPacketFeatureTypeId(packetFeatureItem.PacketFeatureTypeId);
                        packetFeatureTypeNames.Add(packetFeatureType.PacketFeatureTypeName);
                        packetFeaturesView.Add(new PacketFeaturesViewModel { PacketFeatureId = packetFeatureItem.PacketFeatureId, FeatureActive = packetFeatureItem.FeatureActive, FeatureContent = packetFeatureItem.FeatureContent, FeatureProcessCount = packetFeatureItem.FeatureProcessCount, PacketFeatureTypeName = packetFeatureType.PacketFeatureTypeName, FeatureType = packetFeatureItem.FeatureType });

                    }

                    var packet = _packetService.GetPacketByPacketId(store.PacketId);

                    model.PacketFeatureTypeNames = packetFeatureTypeNames;
                    model.PacketFeatures = packetFeaturesView;
                    model.PacketDescription = packet.PacketDescription;
                    model.PacketColor = packet.PacketColor;

                    model.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName); ;
                    if (packet.IsOnset.Value || packet.IsStandart.Value || store.StorePacketEndDate.Value < DateTime.Now)
                    {
                        packetStatu = false;
                    }
                    else
                    {
                        packetStatu = true;
                    }
                }
            }
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
            {

            }
            model.hasPacket = packetStatu;
            model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId
                (AuthenticationUser.Membership.MainPartyId);
            var store1 = new global::MakinaTurkiye.Entities.Tables.Stores.Store();
            int curMainPartyId = 0;
            if (memberStore != null)
            {
                store1 = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));

            }
            if (store1.StoreName != null)
                curMainPartyId = store1.MainPartyId;
            else curMainPartyId = Convert.ToInt32(AuthenticationUser.Membership.MainPartyId);
            ProfileFillRateCalculaterBase profileRate = new ProfileFillRateCalculaterBase(member, store1);
            var address = new global::MakinaTurkiye.Entities.Tables.Common.Address();
            address = _addressService.GetFisrtAddressByMainPartyId(Convert.ToInt32(curMainPartyId));

            var phones = _phoneService.GetPhonesByMainPartyId(Convert.ToInt32(curMainPartyId));
            model.ProfileFillRate = profileRate.GetStoreProfileFillPercent(address, phones);

            var helpCategories = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.AccountHome, false);
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
                model.HelpList.Add(new MTHelpModeltem { HelpCategoryId = item.CategoryId, HelpCategoryName = item.Category.CategoryName, Url = url });

            }

            return View(model);


        }
        public ActionResult Index2(string gelenSayfa, string memberType)
        {
            MTAccountHomeModel model = new MTAccountHomeModel();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var messageSended = MessageErrorSend();
            if (gelenSayfa == "bireyselUyelikOnay")
            {
                model.AccountHomeCenterCenterModel.LastPage = "bireyselUyelikOnay";


               // return RedirectToAction("Index", "Message", new { MessagePageType = 2, messages = "true" });

            }
            else if (gelenSayfa == "KurumsalOnay")
                model.AccountHomeCenterCenterModel.LastPage = "KurumsalOnay";

            model.AccountHomeCenterCenterModel.MessageSended = messageSended;

            List<int?> mainPartyIds = new List<int?>();
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            if (memberStore != null)
            {
                mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();

            }
            else
            {
                mainPartyIds.Add(AuthenticationUser.Membership.MainPartyId);
            }
            model.AccountHomeCenterCenterModel.InboxMessageCount = _messageService.SP_GetAllMessageByMainPartyIdsByMessageType(String.Join(", ", mainPartyIds), (byte)MessageType.Inbox).ToList().Count;

            //model.InboxMessageCount = dataMessage.GetItemsByMainPartyId(AuthenticationUser.Membership.MainPartyId, (byte)MessageType.Inbox).Rows.Count;
            var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds);
            model.AccountHomeCenterCenterModel.TotalProductCount = products.Count;
            model.AccountHomeCenterCenterModel.CheckingProductCount = products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Inceleniyor).ToList().Count;
            model.AccountHomeCenterCenterModel.CheckedProductCount = products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylandi).ToList().Count;
            model.AccountHomeCenterCenterModel.NotCheckedProductCount = products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylanmadi).ToList().Count;
            model.AccountHomeCenterCenterModel.DeletedProductCount = products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Silindi).ToList().Count;
            model.AccountHomeCenterCenterModel.ActiveProductCount = products.Where(x => x.ProductActive == true).ToList().Count;
            model.AccountHomeCenterCenterModel.PasiveProductCount = products.Where(x => x.ProductActive == false).ToList().Count;

            var viewCount = products.Sum(p => p.ViewCount);
            var singularViewCountProduct = products.Sum(p => p.SingularViewCount);
            if (viewCount.HasValue)
                model.AccountHomeCenterCenterModel.ViewProductMultipleCount = viewCount.Value;
            else
                model.AccountHomeCenterCenterModel.ViewProductMultipleCount = 0;
            if (singularViewCountProduct != null) model.AccountHomeCenterCenterModel.ViewProductSingularCount = singularViewCountProduct.Value;
            else model.AccountHomeCenterCenterModel.ViewProductSingularCount = 0;
            model.AccountHomeCenterCenterModel.MemberType = (byte)AuthenticationUser.Membership.MemberType;
            model.AccountHomeCenterCenterModel.MemberName = AuthenticationUser.Membership.MemberName;
            model.AccountHomeCenterCenterModel.MemberSurname = AuthenticationUser.Membership.MemberSurname;

            bool packetStatu = false;
            if (model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.Enterprise)
            {



            }
            if (model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.Individual)
            {

            }

            model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            var store1 = new global::MakinaTurkiye.Entities.Tables.Stores.Store();
            int curMainPartyId = 0;
            if (memberStore != null)
            {
                store1 = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));
                if (store1 != null)
                {
                    model.AccountHomeCenterCenterModel.ViewSingularCount = store1.SingularViewCount.Value;
                    model.AccountHomeCenterCenterModel.ViewMultipleCount = store1.ViewCount.Value;
                    curMainPartyId = store1.MainPartyId;
                    var orderList = _orderService.GetOrdersByMainPartyId(store1.MainPartyId);
                    if (orderList.Count > 0)
                    {
                        model.OrderPacketEndDate = store1.StorePacketEndDate;
                        var dateSubstrac = store1.StorePacketEndDate.Value.Subtract(DateTime.Now);

                        model.PacketFinishDay = dateSubstrac.Days;
                    }
                    //paket özellikleri getir
                    var packet1 = _packetService.GetPacketByPacketId(Convert.ToInt32(store1.PacketId));
                    var showPacketFeatureTypeItems = new List<int>();
                    showPacketFeatureTypeItems.Add(3);
                    showPacketFeatureTypeItems.Add(4);
                    showPacketFeatureTypeItems.Add(5);
                    showPacketFeatureTypeItems.Add(6);
                    showPacketFeatureTypeItems.Add(7);
                    var packetFeatures = packet1.PacketFeatures.Where(p => showPacketFeatureTypeItems.Contains(p.PacketFeatureTypeId));
                    List<PacketFeaturesViewModel> packetFeaturesView = new List<PacketFeaturesViewModel>();
                    List<string> packetFeatureTypeNames = new List<string>();

                    foreach (var packetFeatureItem in packetFeatures)
                    {
                        var packetFeatureType = _packetService.GetPacketFeatureTypeByPacketFeatureTypeId(packetFeatureItem.PacketFeatureTypeId);
                        packetFeatureTypeNames.Add(packetFeatureType.PacketFeatureTypeName);
                        packetFeaturesView.Add(new PacketFeaturesViewModel { PacketFeatureId = packetFeatureItem.PacketFeatureId, FeatureActive = packetFeatureItem.FeatureActive, FeatureContent = packetFeatureItem.FeatureContent, FeatureProcessCount = packetFeatureItem.FeatureProcessCount, PacketFeatureTypeName = packetFeatureType.PacketFeatureTypeName, FeatureType = packetFeatureItem.FeatureType });

                    }
                    model.PacketFeatureTypeNames = packetFeatureTypeNames;
                    model.PacketFeatures = packetFeaturesView;
                    var storePacket = _packetService.GetPacketByPacketId(store1.PacketId);
                    model.PacketDescription = storePacket.PacketDescription;
                    model.PacketColor = storePacket.PacketColor;
                    model.StoreUrl = UrlBuilder.GetStoreProfileUrl(store1.MainPartyId, store1.StoreName, store1.StoreUrlName);
                    if (storePacket.PacketPrice == 0)
                    {
                        packetStatu = false;
                        model.OrderPacketEndDate = null;
                    }
                    else
                    {
                        packetStatu = true;
                    }
                    model.AccountHomeCenterCenterModel.HasPacket = packetStatu;
                }
                else
                {
                    curMainPartyId = Convert.ToInt32(mainPartyId);
                }
                if (store1 != null)
                {
                    var order = _orderService.GetOrdersByMainPartyId(store1.MainPartyId).LastOrDefault();
                    if (order != null)
                    {
                        if (order.PriceCheck != true)
                        {
                            model.AccountHomeCenterCenterModel.OrderPriceCheck = false;
                            model.AccountHomeCenterCenterModel.OrderPrice = order.OrderPrice;

                        }
                    }
                }
            }
            ProfileFillRateCalculaterBase profileRate = new ProfileFillRateCalculaterBase(member, store1);
            var address = new global::MakinaTurkiye.Entities.Tables.Common.Address();
            address = _addressService.GetFisrtAddressByMainPartyId(Convert.ToInt32(curMainPartyId));
            var phones = _phoneService.GetPhonesByMainPartyId(Convert.ToInt32(curMainPartyId));
            model.ProfileFillRate = profileRate.GetStoreProfileFillPercent(address, phones);
            var helpCategories = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.AccountHome, false);
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
                model.AccountHomeCenterCenterModel.HelpList.Add(new MTHelpModeltem { HelpCategoryId = item.CategoryId, HelpCategoryName = item.Category.CategoryName, Url = url });

            }

            #region comments
            var myComments = _productCommentService.GetProductCommentsByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            SearchModel<MTProductCommentStoreItem> commentModel = new SearchModel<MTProductCommentStoreItem>();
            int page = 1;
            int pD = 10;
            commentModel.CurrentPage = page;
            commentModel.PageDimension = pD;
            commentModel.TotalRecord = myComments.Count();
            myComments = myComments.OrderByDescending(x => x.ProductCommentId).Skip(page * pD - pD).Take(pD).ToList();
            List<MTProductCommentStoreItem> commentList = new List<MTProductCommentStoreItem>();
            foreach (var item in myComments.ToList())
            {
                var memberComment = _memberService.GetMemberByMainPartyId(item.MemberMainPartyId);
                var product = _productService.GetProductByProductId(item.ProductId);
                if (memberComment != null)
                {
                    commentList.Add(new MTProductCommentStoreItem
                    {
                        CommentText = item.CommentText,
                        MemberNameSurname = memberComment.MemberName + " " + memberComment.MemberSurname,
                        Rate = item.Rate.Value,
                        RecordDate = item.RecordDate,
                        ProductCommentId = item.ProductCommentId,
                        ProductName = product.ProductName,
                        Status = item.Status,
                        Reported = item.Reported,

                        ProductNo = product.ProductNo,
                        ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, product.ProductName)
                    });
                }
   
            }
            commentModel.Source = commentList;
            model.AccountHomeCenterCenterModel.ProductComments = commentModel;
            #endregion


            return View(model);

        }
        public bool MessageErrorSend()
        {
            bool messageSended = false;
            var messageErrors = _messageService.GetSendMessageErrorsBySenderId(AuthenticationUser.CurrentUser.Membership.MainPartyId);

            if (messageErrors.ToList().Count > 0)//gönderilmeyen tüm mailleri gönder
            {
                foreach (var messageItem in messageErrors)
                {
                    using (TransactionUI tran = new TransactionUI())
                    {

                        var message = new Message
                        {
                            Active = true,
                            MessageContent = messageItem.MessageContent,
                            MessageSubject = messageItem.MessageSubject,
                            MessageDate = DateTime.Now,
                            MessageRead = false,
                            ProductId = messageItem.ProductID,
                            MessageSeenAdmin = false
                        };
                        _messageService.InsertMessage(message);
                        int messageId = message.MessageId;
                        int mainPartyId = Convert.ToInt32(messageItem.ReceiverID);

                        var messageMainParty = new MessageMainParty
                        {
                            MainPartyId = (int)messageItem.SenderID,
                            MessageId = messageId,
                            InOutMainPartyId = mainPartyId,
                            MessageType = (byte)MessageType.Outbox
                        };
                        _messageService.InsertMessageMainParty(messageMainParty);

                        var curMessageMainParty = new MessageMainParty
                        {
                            InOutMainPartyId = (int)messageItem.SenderID,
                            MessageId = messageId,
                            MainPartyId = mainPartyId,
                            MessageType = (byte)MessageType.Inbox,
                        };
                        _messageService.InsertMessageMainParty(curMessageMainParty);

                        //var receiverUser = _memberService.GetMemberByMainPartyId(mainPartyId);

                        if (messageItem.ProductID != 0)
                        {
                            #region messageissendbilgilendirme

                            var product = _productService.GetProductByProductId(messageItem.ProductID);
                            var kullaniciemail = _memberService.GetMemberByMainPartyId(messageItem.ReceiverID);
                            string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                            string productName = product.ProductName.ToString();

                            //var productno = entities.Products.Where(c => c.ProductId == messageItem.ProductID).SingleOrDefault().ProductNo;
                            //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                            //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;

                            var categorymodelname = "";

                            if (product.Model!=null)
                            {
                                categorymodelname = product.Model.CategoryName;

                            }
                            string categorybrandname = "";
                            if (product.Brand!=null)
                            {
                                categorybrandname = product.Brand.CategoryName;
                            }

                            DecyptHelper decyptHelper = new DecyptHelper();
                            var enciprtText = decyptHelper.Encrypt(mainPartyId.ToString());
                            var returnurl = "/Account/Message/Detail/" + messageId + "?RedirectMessageType=0";
                            string loginAutoLink = string.Format("/membership/LogonAuto?validateId={0}=&returnUrl={1}", enciprtText, returnurl);
                            string productnosub = productName + " " + categorybrandname + " " + categorymodelname + " İlan no:" + product.ProductNo;
                            string productUrl = "http://www.makinaturkiye.com" + Core.Web.Helpers.Helpers.ProductUrl(product.ProductId, productName);
                            try
                            {

                                MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("mesajınızvar");
                                //Mail konusu
                                string templatet = mailTemplate.MessagesMTPropertie;
                                templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo).Replace("#messagecontent#", messageItem.MessageContent).Replace("#loginautolink#", loginAutoLink);
                                MailHelper mailHelper = new MailHelper(productnosub, templatet, mailTemplate.Mail, mailadresifirma, mailTemplate.MailPassword, mailTemplate.MailSendFromName);
                                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(messageItem.ReceiverID);
                                if (memberStore != null)
                                {
                                    var memberMainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Where(x => x.MemberMainPartyId != AuthenticationUser.CurrentUser.Membership.MainPartyId).Select(x => x.MemberMainPartyId).ToList();
                                    var members = _memberService.GetMembersByMainPartyIds(memberMainPartyIds).Select(x => x.MemberEmail).ToList();
                                    members.ForEach(x => mailHelper.ToMails.Add(x));
                                }
                                mailHelper.Send();
                            }
                            catch (Exception ex)
                            {

                                //log.Error(AuthenticationUser.CurrentUser.MainPartyId.ToString() + " üye id ile " + productnosub + " ürünü  mesajı için firmaya mail gönderilemedi. Hata Mesajı:" + ex.Message);
                            }

                            #endregion

                            _messageService.DeleteSendMessageError(messageItem);
                            messageSended = true;

                        }
                    }

                }
            }
            return messageSended;
        }
        public ActionResult _HeaderMainMenu()
        {
            List<MTHomeCategoryModel> model = new List<MTHomeCategoryModel>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var mainCategories = _categoryService.GetMainCategories();
            foreach (var item in mainCategories)
            {
                MTHomeCategoryModel mainCategoryModel = new MTHomeCategoryModel();
                mainCategoryModel.CategoryName = item.CategoryName;
                mainCategoryModel.CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, item.CategoryName, null, string.Empty);
                mainCategoryModel.ProductCount = item.ProductCount.Value;
                var subCategories = _categoryService.GetCategoriesByCategoryParentId(item.CategoryId);
                foreach (var subItem in subCategories)
                {
                    mainCategoryModel.SubCategoryModels.Add(new MTHomeCategoryModel
                    {
                        CategoryName = subItem.CategoryName,
                        ProductCount = subItem.ProductCount.Value,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, subItem.CategoryName, null, string.Empty)
                    });
                }

                model.Add(mainCategoryModel);
            }
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CommentDelete(int productCommentId)
        {
            var productComment = _productCommentService.GetProductCommentByProductCommentId(productCommentId);
            _productCommentService.DeleteProductComment(productComment);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult commentpaging(int page)
        {
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var myComments = member.ProductComments;
            SearchModel<MTProductCommentStoreItem> commentModel = new SearchModel<MTProductCommentStoreItem>();

            int pD = 10;
            commentModel.CurrentPage = page;
            commentModel.PageDimension = pD;
            commentModel.TotalRecord = myComments.Count();
            myComments = myComments.OrderByDescending(x => x.ProductCommentId).Skip(page * pD - pD).Take(pD).ToList();
            List<MTProductCommentStoreItem> commentList = new List<MTProductCommentStoreItem>();
            foreach (var item in myComments.ToList())
            {
                commentList.Add(new MTProductCommentStoreItem
                {
                    CommentText = item.CommentText,
                    MemberNameSurname = item.Member.MemberName + " " + item.Member.MemberSurname,
                    Rate = item.Rate.Value,
                    RecordDate = item.RecordDate,
                    ProductCommentId = item.ProductCommentId,
                    ProductName = item.Product.ProductName,
                    Status = item.Status,
                    Reported = item.Reported,

                    ProductNo = item.Product.ProductNo,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.Product.ProductName)
                });
            }
            commentModel.Source = commentList;
            return PartialView("_CommentList", commentModel);
        }
    }
}
