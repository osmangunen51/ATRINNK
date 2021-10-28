
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Utilities.HttpHelpers;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Messages;
using NeoSistem.MakinaTurkiye.Web.Controllers;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using NoeSistemHelpers = NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{

    public class MessageController : BaseAccountController
    {
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberService _memberService;
        private readonly IMessageService _messageService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly IMobileMessageService _mobileMessageService;
        private readonly IMessagesMTService _messagesMTService;

        public MessageController(IMemberStoreService memberStoreService,
            IMemberService memberService,
            IMessageService messageService, IAddressService addressService,
            IPhoneService phoneService, IProductService productService,
            IStoreService storeService, IMobileMessageService mobileMessageService, IMessagesMTService messagesMTService)
        {
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._messageService = messageService;
            this._addressService = addressService;
            this._phoneService = phoneService;
            this._productService = productService;
            this._storeService = storeService;
            this._mobileMessageService = mobileMessageService;
            this._messagesMTService = messagesMTService;

            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._phoneService.CachingGetOrSetOperationEnabled = false;
            this._productService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
        }

        public ActionResult Index()
        {

            //geri alma işlemi
            var mPageType = (MessagePageType)byte.Parse(Request.QueryString["MessagePageType"]);


            int sendmainparty = 0;
            int memberNo = 0;
            int productNo = 0;
            var subject = "";
            int messageid = 0;
            if (Request.QueryString["Mainparty"] != null)
            {
                sendmainparty = int.Parse(Request.QueryString["Mainparty"]);
                var memberReceiver = _memberService.GetMemberByMainPartyId(sendmainparty);
                ViewData["receiverMember"] = memberReceiver;
                if (Request.QueryString["messageid"] != null)
                {
                    messageid = int.Parse(Request.QueryString["messageid"]);
                    var message = _messageService.GetSendMessageErrorByMessageId(messageid);
                    if (message != null)
                        subject = message.MessageSubject;
                }

            }

            string ad = "";
            if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual)
            {
                if (Request.QueryString["MessagePageType"].ToString() == "1")
                {


                    int mainPartyId = (int)AuthenticationUser.Membership.MainPartyId;

                    if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
                    {
                        mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
                    }
                    var adressData = _addressService.GetAddressesByMainPartyId(mainPartyId);
                    if (adressData.First().CountryId != 246 && adressData.First().CountryId != null)
                    {


                    }
                    else
                    {


                        if (AuthenticationUser.Membership.MemberType != (byte)MemberType.Enterprise)//eğer firmaysa mesaj gönderebilir onaya gerek yoktur.
                        {
                            if (adressData != null)
                            {
                                Address address = adressData.First();
                                if (address.CountryId == null || address.CityId == null || address.LocalityId == null)
                                    return RedirectToAction("ChangeAddress", "Personal", new { urunNo = Request.QueryString["UrunNo"], uyeNo = Request.QueryString["UyeNo"], mtypePage = Request.QueryString["MessagePageType"], gelenSayfa = "Teklif" });
                            }
                            else
                            {
                                return RedirectToAction("ChangeAddress", "Personal", new { urunNo = Request.QueryString["UrunNo"], uyeNo = Request.QueryString["UyeNo"], mtypePage = Request.QueryString["MessagePageType"], gelenSayfa = "Teklif" });
                            }

                            var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(mainPartyId, PhoneTypeEnum.Gsm);
                            if (phone != null)
                            {
                                if (phone.active == null || phone.active == 0)
                                {
                                    return RedirectToAction("ChangeAddress", "Personal", new
                                    {
                                        urunNo = Request.QueryString["UrunNo"],
                                        uyeNo = Request.QueryString["UyeNo"],
                                        mtypePage = Request.QueryString["MessagePageType"],
                                        gelenSayfa = "Teklif",
                                        error = "PhoneActive"
                                    });

                                }
                            }
                        }
                    }

                }


                var member = new Member();
                var store = new Store();
                var memberFullName = "";
                string productName = "";
                string productUrl = "";

                var model = new MessageViewModel();

                if (Request.QueryString["UrunNo"] != null)
                {
                    productNo = int.Parse(Request.QueryString["UrunNo"]);
                    var product = _productService.GetProductByProductId(productNo);
                    productName = product.ProductName;
                    productUrl = NoeSistemHelpers.ProductUrl(product.ProductId, product.ProductName);

                    model.Product = product;
                }

                if (Request.QueryString["UyeNo"] != null)
                {
                    memberNo = int.Parse(Request.QueryString["UyeNo"]);
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberNo);
                    if (memberStore != null)
                    {
                        store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                        memberFullName = store != null ? store.StoreName : "";
                    }
                    else
                    {
                        member = _memberService.GetMemberByMainPartyId(memberNo);
                        memberFullName = member != null ? (member.MemberName + " " + member.MemberSurname) : "";
                    }
                }

                var data = new Data.Message();
                var memberStore1 = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                string mainPartyIdsPar = AuthenticationUser.CurrentUser.Membership.MainPartyId.ToString();
                if (memberStore1 != null)
                {
                    var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore1.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();
                    mainPartyIdsPar = String.Join(", ", mainPartyIds); ;
                }



                switch (mPageType)
                {
                    case MessagePageType.Inbox:
                        //gelen kutusu.
                        model.MessageItems = data.GetItemsByMainPartyIds(mainPartyIdsPar, (byte)MessageType.Inbox).AsCollection<MessageModel>();
                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyMessage, (byte)LeftMenuConstants.MyMessage.IncomingMessages);

                        break;
                    case MessagePageType.Send:

                        #region

                        if (sendmainparty != 0)
                        {
                            var sendermember = _memberService.GetMemberByMainPartyId(sendmainparty);
                            model.Message = new MessageModel();
                            model.MemberMessageDetail.Member = new Member();
                            model.MemberMessageDetail.Member.MainPartyId = sendmainparty;
                            ad = sendermember.MemberName + " " + sendermember.MemberSurname;
                            model.Message.MainPartyFullName = ad.ToString();
                            var mesagge = _messageService.GetMessageByMesssageId(messageid);
                            model.Message.Subject = "RE: " + mesagge.MessageSubject;
                            model.Message.Content = mesagge.MessageDate + " " + ad + " yazdı." + Environment.NewLine + mesagge.MessageContent;
                            int proid = 0;
                            if (mesagge.ProductId > 0)
                            {
                                proid = mesagge.ProductId;
                                model.Message.ProductId = proid;
                            }

                        }

                        #endregion

                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyMessage, (byte)LeftMenuConstants.MyMessage.SendMessage);
                        model.Message = new MessageModel();
                        model.MemberMessageDetail.Member = new Member();
                        model.Message.Subject = subject;
                        model.Message.MainPartyFullName = memberFullName;
                        model.MemberMessageDetail.Member.MainPartyId = memberNo;
                        model.Message.ProductId = productNo;
                        model.ProductName = productName;
                        model.ProductUrl = productUrl;


                        break;
                    case MessagePageType.Outbox:

                        model.MessageItems = data.GetItemsByMainPartyIds(mainPartyIdsPar, (byte)MessageType.Outbox).AsCollection<MessageModel>();
                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyMessage, (byte)LeftMenuConstants.MyMessage.OutgoingMessages);
                        ViewData["message"] = Request.QueryString["messages"];
                        break;
                    case MessagePageType.Banned:
                        break;
                    case MessagePageType.RecyleBin:
                        model.MessageItems = data.GetItemsByMainPartyIds(mainPartyIdsPar, (byte)MessageType.RecyleBin).AsCollection<MessageModel>();
                        model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyMessage, (byte)LeftMenuConstants.MyMessage.DeletedMessages);
                        break;
                    default:
                        break;
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Individual", "MemberType", new { urunNo = Request.QueryString["UrunNo"], uyeNo = Request.QueryString["UyeNo"], mtypePage = Request.QueryString["MessagePageType"], gelenSayfa = "Teklif", memberType = "hizli" });
            }
        }
        [HttpPost]
        public ActionResult DeleteMessage(int MessageId, int messagetype)
        {
            try
            {
                var usermessage = _messageService.GetMessageMainPartyByMessageIdWithMessageType(MessageId, (MessageTypeEnum)messagetype);
                if (usermessage != null)
                {
                    usermessage.MessageType = (byte)MessageType.RecyleBin;
                    _messageService.UpdateMessageMainParty(usermessage);
                }

                var deletecheck = _messageService.GetMessageCheckByMessageId(MessageId);
                if (deletecheck != null)
                {
                    _messageService.DeleteMessageCheck(deletecheck);
                }

            }
            catch (Exception)
            {

                throw;
            }

            return Json(true);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(MessageViewModel model)
        {
            //Active = true,
            // MessageContent = model.Message.Content,
            // MessageDate = DateTime.Now,
            // MessageRead = false,
            // MessageSubject = model.Message.Subject,
            // ProductId=model.Message.ProductId
            var message = new Message
            {
                Active = true,
                MessageContent = model.Message.Content,
                MessageSubject = model.Message.Subject,
                MessageDate = DateTime.Now,
                MessageRead = false,
                ProductId = model.Message.ProductId,
                MessageFile = model.Message.FileName
            };
            _messageService.InsertMessage(message);

            int messageId = message.MessageId;
            int mainPartyId = model.Message.MainPartyId;
            var messageMainParty = new MessageMainParty
            {
                MainPartyId = AuthenticationUser.Membership.MainPartyId,
                MessageId = messageId,
                InOutMainPartyId = mainPartyId,
                MessageType = (byte)MessageType.Outbox,
            };
            _messageService.InsertMessageMainParty(messageMainParty);

            var curMessageMainParty = new MessageMainParty
            {
                InOutMainPartyId = AuthenticationUser.Membership.MainPartyId,
                MessageId = messageId,
                MainPartyId = mainPartyId,
                MessageType = (byte)MessageType.Inbox,
            };
            _messageService.InsertMessageMainParty(curMessageMainParty);




            var receiverUser = _memberService.GetMemberByMainPartyId(mainPartyId);
            if (receiverUser.FastMemberShipType == (byte)FastMembershipType.Phone)
            {



                var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(receiverUser.MainPartyId, PhoneTypeEnum.Gsm);
                if (phone != null)
                {
                    SmsHelper sms = new SmsHelper();
                    string onlyUsePaswword = sms.CreateOnlyUsePassword();
                    string phoneNumber = phone.PhoneCulture + phone.PhoneAreaCode + phone.PhoneNumber;
                    var product = _productService.GetProductByProductId(model.Message.ProductId);
                    MobileMessage mobileTemp = _mobileMessageService.GetMobileMessageByMessageName("gelenmesaj");
                    string messageMobile = mobileTemp.MessageContent;

                    messageMobile = messageMobile.Replace("#isimsoyisim#", receiverUser.MemberName + " " + receiverUser.MemberSurname).Replace("#urunadi#", product.ProductName).Replace("#aktivasyonkodu#", onlyUsePaswword);
                    sms.SendSmsOnlyPassword(phoneNumber, messageMobile);
                    receiverUser.MemberPassword = onlyUsePaswword;

                    _memberService.UpdateMember(receiverUser);
                }



                //if (model.Message.ProductId != 0) //tek kullanımlık şifre ile ilgili template hazırlanıcak
                //{
                //    #region messageissendbilgilendirme
                //    var kullaniciemail = entities.Members.Where(c => c.MainPartyId == model.Message.MainPartyId).SingleOrDefault();
                //    string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                //    string productName = product.ProductName.ToString();
                //    var productno = entities.Products.Where(c => c.ProductId == model.Message.ProductId).SingleOrDefault().ProductNo;
                //    var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                //    var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
                //    var categorymodelname = entities.Categories.Where(c => c.CategoryId == product.ModelId).SingleOrDefault().CategoryName;
                //    var categorybrandname = entities.Categories.Where(c => c.CategoryId == product.BrandId).SingleOrDefault().CategoryName;
                //    string productnosub = productName + " " + categorybrandname + " " + categorymodelname + " İlan no:" + productno;
                //    string productUrl = "http://www.makinaturkiye.com/" + Helpers.ToUrl(groupname) + "/" + product.ProductId + "/" + Helpers.ToUrl(categoryname) + "/" + Helpers.ToUrl(productName);
                //    MailMessage mail = new MailMessage();
                //    mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye"); //Mailin kimden gittiğini belirtiyoruz
                //    mail.To.Add(mailadresifirma);                                                              //Mailin kime gideceğini belirtiyoruz
                //    mail.Subject = productnosub;                                              //Mail konusu
                //    string templatet = Resources.Email.mesajınızvar;
                //    templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", productno);
                //    mail.Body = templatet;                                                            //Mailin içeriği
                //    mail.IsBodyHtml = true;
                //    mail.Priority = MailPriority.Normal;
                //    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                //    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                //    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                //    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                //    sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777"); //Gmail hesap kontrolü için bilgilerimizi girdi
                //    sc.Send(mail);
                //}
                //    #endregion

            }
            else
            {

                #region messageissendbilgilendirme

                if (model.Message.ProductId != 0)
                {
                    var product = _productService.GetProductByProductId(model.Message.ProductId);
                    var kullaniciemail = _memberService.GetMemberByMainPartyId(model.Message.MainPartyId);
                    string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                    string productName = product.ProductName.ToString();
                    //var productno = entities.Products.Where(c => c.ProductId == model.Message.ProductId).SingleOrDefault().ProductNo;
                    //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                    //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
                    string categoryModelName = "";
                    string brandName = "";
                    var categoryModel = product.Model;
                    if (categoryModel != null)
                        categoryModelName = categoryModel.CategoryName;
                    var categorBrand = product.Brand;
                    if (categorBrand != null)
                        brandName = categorBrand.CategoryName;

                    string productnosub = productName + " " + brandName + " " + categoryModelName + " İlan no:" + product.ProductNo;
                    string productUrl = UrlBuilder.GetProductUrl(product.ProductId, productName);

                    LinkHelper linkHelper = new LinkHelper();
                    string encValue = linkHelper.Encrypt(model.Message.MainPartyId.ToString());
                    string messageLink = "/Account/Message/Detail/" + messageId + "?RedirectMessageType=0";
                    string loginauto = "https://www.makinaturkiye.com/MemberShip/LogonAuto?validateId=" + encValue + "&returnUrl=" + messageLink;

                    MailMessage mail = new MailMessage();
                    string mailTemplateName = "mesajinizvarkullanici";

                    if (product.MainPartyId == mainPartyId)
                        mailTemplateName = "mesajınızvar";

                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName(mailTemplateName);
                    mail.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                    mail.To.Add(mailadresifirma);                                                              //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = productnosub;                                              //Mail konusu
                    string templatet = mailTemplate.MessagesMTPropertie;
                    templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo).Replace("#producturl#", productUrl).Replace("#messagecontent#", model.Message.Content).Replace("#loginautolink#", loginauto);
                    mail.Body = templatet;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mailTemplate.Mail, mailTemplate.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                }

                #endregion
            }
            //mail gönderilmesi yapılabilir.
            return RedirectToAction("Index", "Message", new { MessagePageType = (byte)MessagePageType.Outbox });
        }

        [HttpPost]
        public JsonResult FindMainPartyFullName(string searchText)
        {
            var dataMember = new Data.Member();
            return Json(dataMember.MemberGetItemsByMainPartyFullName(searchText).AsCollection<MessageModel>());
        }

        public ActionResult Detail(int id)
        {


            var model = new MessageViewModel();
            model.EntitiesMessage = _messageService.GetMessageByMesssageId(id);
            model.Product = _productService.GetProductByProductId(model.EntitiesMessage.ProductId);
            model.ProductUrl = NoeSistemHelpers.ProductUrl(model.Product.ProductId, model.Product.ProductName);
            MessageTypeEnum redirectMessageType = (MessageTypeEnum)byte.Parse(Request.QueryString["RedirectMessageType"]);

            //gelen kutusundan gidiyorsa
            int memberMainPartyId = 0;
            var messageMainParty = _messageService.GetMessageMainPartyByMessageIdWithMessageType(id, redirectMessageType);
            if (Request.QueryString["RedirectMessageType"].ToString() == "0")
            {
                memberMainPartyId = messageMainParty.InOutMainPartyId;
            }
            else
            {
                memberMainPartyId = messageMainParty.MainPartyId;

            }


            //}
            var storeMainparty = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);

            if (storeMainparty != null)
            {
                var store = _storeService.GetStoreByMainPartyId(storeMainparty.StoreMainPartyId.Value);
                model.Store = store;
            }

            var member = _memberService.GetMemberByMainPartyId(memberMainPartyId);
            MemberMessageDetailModel memberMessageDetail = new MemberMessageDetailModel();
            memberMessageDetail.Member = member;
            int mainPartyId = member.MainPartyId;
            if (member.MemberType == (byte)MemberType.Enterprise)
            {
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
                mainPartyId = memberStore.StoreMainPartyId.Value;
            }
            memberMessageDetail.Address = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
            memberMessageDetail.Phones = _phoneService.GetPhonesByMainPartyId(mainPartyId).ToList();
            model.MemberMessageDetail = memberMessageDetail;
            var checkmainparty = _messageService.GetMessageCheckByMessageId(id);
            if (checkmainparty == null && redirectMessageType == 0)
            {
                var checkmain = new MessageCheck
                {
                    Check = 1,
                    MainPartyId = AuthenticationUser.Membership.MainPartyId,
                    MessageId = id
                };
                _messageService.InsertMessageCheck(checkmain);
            }

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyMessage, (byte)LeftMenuConstants.MyMessage.SendMessage);

            return View(model);
        }

        [HttpPost]
        public ActionResult DeletePicture(int MessageId)
        {
            var model = new MessageViewModel();
            var data = new Data.Message();

            var messageMainParty = _messageService.GetFirstMessageMainPartyByMessageId(MessageId);
            messageMainParty.MessageType = (byte)MessageType.RecyleBin;
            _messageService.UpdateMessageMainParty(messageMainParty);

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();
            string mainPartyIdsPar = String.Join(", ", mainPartyIds);
            model.MessageItems = data.GetItemsByMainPartyIds(mainPartyIdsPar, (byte)MessageType.Inbox).AsCollection<MessageModel>();
            return View("InboxList", model);
        }

        [HttpPost]
        public ActionResult SaveMessage(MessageViewModel model)
        {
            string productNo = "#" + model.Message.ProductId;
            string memberNo = "##" + model.Message.MainPartyId;

            var brandname = "";
            var modelname = "";
            var title = "";
            var productId = 0;
            var product = _productService.GetProductByProductId(model.Message.ProductId);
            if (product != null)
            {
                productId = product.ProductId;
                if (product.Brand != null)
                {
                    brandname = product.Brand.CategoryName;
                }
                else
                {
                    brandname = "";
                }
                if (product.Model != null)
                {
                    modelname = product.Model.CategoryName;
                }
                else
                {
                    modelname = "";
                }
                title = product.ProductName + " " + brandname + " " + modelname + " İlan no:" + productNo;
            }
            else
                title = model.Message.Subject;

            //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
            var mainPartyId = model.Message.MainPartyId;//entities.Members.SingleOrDefault(c => c.MemberNo == memberNo).MainPartyId;
            var Eposta = _memberService.GetMemberByMainPartyId(mainPartyId);
            //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;

            var message = new Message
            {
                Active = true,
                MessageContent = model.Message.Content,
                MessageSubject = title,
                MessageDate = DateTime.Now,
                MessageRead = false,
                ProductId = productId,
                MessageFile = model.Message.FileName
            };
            _messageService.InsertMessage(message);

            int messageId = message.MessageId;


            var messageMainParty = new MessageMainParty
            {
                MainPartyId = AuthenticationUser.Membership.MainPartyId,
                MessageId = messageId,
                InOutMainPartyId = mainPartyId,
                MessageType = (byte)MessageType.Outbox,
            };
            _messageService.InsertMessageMainParty(messageMainParty);

            var curMessageMainParty = new MessageMainParty
            {
                InOutMainPartyId = AuthenticationUser.Membership.MainPartyId,
                MessageId = messageId,
                MainPartyId = mainPartyId,
                MessageType = (byte)MessageType.Inbox,
            };
            _messageService.InsertMessageMainParty(curMessageMainParty);


            #region messageissendbilgilendirme

            if (model.Message.ProductId != 0)
            {
                var kullaniciemail = _memberService.GetMemberByMainPartyId(model.Message.MainPartyId);
                string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                string productName = product.ProductName.ToString();
                //var productno = entities.Products.Where(c => c.ProductId == model.Message.ProductId).SingleOrDefault().ProductNo;
                //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
                //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
                string categoryModelName = "";
                string brandName = "";
                var categoryModel = product.Model;
                if (categoryModel != null)
                    categoryModelName = categoryModel.CategoryName;
                var categorBrand = product.Brand;
                if (categorBrand != null)
                    brandName = categorBrand.CategoryName;

                string productnosub = productName + " " + brandName + " " + categoryModelName + " İlan no:" + product.ProductNo;
                string productUrl = UrlBuilder.GetProductUrl(product.ProductId, productName);

                LinkHelper linkHelper = new LinkHelper();
                string encValue = linkHelper.Encrypt(model.Message.MainPartyId.ToString());
                string messageLink = "/Account/Message/Detail/" + messageId + "?RedirectMessageType=0";
                string loginauto = "https://www.makinaturkiye.com/MemberShip/LogonAuto?validateId=" + encValue + "&returnUrl=" + messageLink;

                MailMessage mail = new MailMessage();
                string mailTemplateName = "mesajinizvarkullanici";

                if (product.MainPartyId == mainPartyId)
                    mailTemplateName = "mesajınızvar";

                MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName(mailTemplateName);
                mail.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(mailadresifirma);                                                              //Mailin kime gideceğini belirtiyoruz
                mail.Subject = productnosub;                                              //Mail konusu
                string templatet = mailTemplate.MessagesMTPropertie;
                templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo).Replace("#producturl#", productUrl).Replace("#messagecontent#", model.Message.Content).Replace("#loginautolink#", loginauto);
                mail.Body = templatet;                                                            //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                sc.Credentials = new NetworkCredential(mailTemplate.Mail, mailTemplate.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                sc.Send(mail);
            }
            #endregion

            //return Json(true);
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Message", new { MessagePageType = (byte)MessagePageType.Outbox });
        }

    }
}
