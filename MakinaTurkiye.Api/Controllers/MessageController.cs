using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace MakinaTurkiye.Api.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IMessageService _messageService;
        private readonly IPhoneService _phoneService;
        private readonly IProductService _productService;
        private readonly IMobileMessageService _mobileMessageService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;

        public MessageController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _messageService = EngineContext.Current.Resolve<IMessageService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _productService = EngineContext.Current.Resolve<IProductService>();
            _mobileMessageService = EngineContext.Current.Resolve<IMobileMessageService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
        }

        //public MessageController(IMemberService memberService,
        //                          IMessageService messageService,
        //                          IPhoneService phoneService,
        //                          IProductService productService,
        //                          IMobileMessageService mobileMessageService,
        //                          IMessagesMTService messagesMTService)
        //{
        //    this._memberService = memberService;
        //    this._messageService = messageService;
        //    this._phoneService = phoneService;
        //    this._productService = productService;
        //    this._mobileMessageService = mobileMessageService;
        //    this._messagesMTService = messagesMTService;
        //}

        public HttpResponseMessage SendPrivateMessage(MessageViewModel model)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    if (member.MainPartyId != model.TargetMainPartyId)
                    {
                        var message = new Entities.Tables.Messages.Message
                        {
                            Active = true,
                            MessageContent = model.Content,
                            MessageSubject = model.Subject,
                            MessageDate = DateTime.Now,
                            MessageRead = false,
                            ProductId = model.ProductId,
                            MessageFile = model.FileName
                        };
                        _messageService.InsertMessage(message);

                        int messageId = message.MessageId;
                        int mainPartyId = model.TargetMainPartyId;
                        var messageMainParty = new Entities.Tables.Messages.MessageMainParty
                        {
                            MainPartyId = member.MainPartyId,
                            MessageId = messageId,
                            InOutMainPartyId = mainPartyId,
                            MessageType = (byte)MessageType.Outbox,
                        };
                        _messageService.InsertMessageMainParty(messageMainParty);

                        var curMessageMainParty = new Entities.Tables.Messages.MessageMainParty
                        {
                            InOutMainPartyId = member.MainPartyId,
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
                                var product = _productService.GetProductByProductId(model.ProductId);
                                MobileMessage mobileTemp = _mobileMessageService.GetMobileMessageByMessageName("gelenmesaj");
                                string messageMobile = mobileTemp.MessageContent;

                                messageMobile = messageMobile.Replace("#isimsoyisim#", receiverUser.MemberName + " " + receiverUser.MemberSurname).Replace("#urunadi#", product.ProductName).Replace("#aktivasyonkodu#", onlyUsePaswword);
                                sms.SendSmsOnlyPassword(phoneNumber, messageMobile);
                                receiverUser.MemberPassword = onlyUsePaswword;

                                _memberService.UpdateMember(receiverUser);
                            }
                        }
                        else
                        {
                            #region messageissendbilgilendirme

                            if (model.ProductId != 0)
                            {
                                var product = _productService.GetProductByProductId(model.ProductId);
                                var kullaniciemail = _memberService.GetMemberByMainPartyId(model.TargetMainPartyId);
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
                                string encValue = linkHelper.Encrypt(model.TargetMainPartyId.ToString());
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
                                templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo).Replace("#producturl#", productUrl).Replace("#messagecontent#", model.Content).Replace("#loginautolink#", loginauto);
                                mail.Body = templatet;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                this.SendMail(mail);
                            }

                            #endregion messageissendbilgilendirme
                        }

                        processStatus.Result = "Mesajınız başarıyla gönderildi";
                        processStatus.Message.Header = "Send Private Message";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Message.Header = "Send Private Message";
                        processStatus.Message.Text = "İşlem başarısız.";
                        processStatus.Status = false;
                        processStatus.Result = "Kendinize mesaj gönderemezsiniz!";
                    }
                }
                else
                {
                    processStatus.Message.Header = "Send Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Send Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeletePrivateMessage(List<int> messageIdList)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    foreach (var messageId in messageIdList)
                    {
                        var message = _messageService.GetMessageByMesssageId(messageId);
                        var messageMainParty = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.Inbox).ToList();
                        var messageMainParty2 = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.Outbox).ToList();
                        messageMainParty.AddRange(messageMainParty2);

                        var usermessage = messageMainParty.Where(x => x.MessageId == messageId && x.MainPartyId == member.MainPartyId).Single();
                        if (usermessage != null)
                        {
                            usermessage.MessageType = (byte)MessageType.RecyleBin;
                            _messageService.UpdateMessageMainParty(usermessage);
                        }

                        var deletecheck = _messageService.GetMessageCheckByMessageId(messageId);
                        if (deletecheck != null)
                        {
                            _messageService.DeleteMessageCheck(deletecheck);
                        }
                    }
                    processStatus.Result = "Mesajınız başarıyla silindi";
                    processStatus.Message.Header = "Delete Private Message";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Delete Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Delete Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

       
        public HttpResponseMessage GetAllInboxPrivateMessage()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var privateMessageViewList = new List<Object>();
                    var allMessageMainPartyForLogginMamber = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageTypeEnum.Inbox).ToList();
                    foreach (var messageMainPartyForLogginMamber in allMessageMainPartyForLogginMamber)
                    {
                        var messageData = _messageService.GetMessageByMesssageId(messageMainPartyForLogginMamber.MessageId);
                        var senderUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.InOutMainPartyId);
                        var targetUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.MainPartyId);
                        var tmpproductresult = _productService.GetProductByProductId(messageData.ProductId);
                        if (tmpproductresult != null)
                        {
                            var Currency = tmpproductresult.GetCurrency();
                            View.Result.ProductSearchResult ProductSearchResult = new View.Result.ProductSearchResult
                            {
                                ProductId = tmpproductresult.ProductId,
                                CurrencyCodeName = Currency,
                                ProductName = tmpproductresult.ProductName,
                                BrandName = tmpproductresult.Brand.CategoryName,
                                ModelName = tmpproductresult.Model.CategoryName,
                                MainPicture = "",
                                StoreName = "",
                                MainPartyId = (int)tmpproductresult.MainPartyId,
                                ProductPrice = (tmpproductresult.ProductPrice ?? 0),
                                ProductPriceType = (byte)tmpproductresult.ProductPriceType,
                                ProductPriceLast = (tmpproductresult.ProductPriceLast ?? 0),
                                ProductPriceBegin = (tmpproductresult.ProductPriceBegin ?? 0),
                                HasVideo = tmpproductresult.HasVideo,
                            };
                            string picturePath = "";
                            var picture = _pictureService.GetFirstPictureByProductId(ProductSearchResult.ProductId);
                            if (picture != null)
                                picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(ProductSearchResult.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                            var privateMessage = new
                            {
                                TargetMainPartyId = targetUser.MainPartyId,
                                TargetNameSurname = targetUser.MemberName + " " + targetUser.MemberSurname,
                                SenderMainPartyId = senderUser.MainPartyId,
                                SenderNameSurname = senderUser.MemberName + " " + senderUser.MemberSurname,
                                MessageId = messageMainPartyForLogginMamber.MessageId,
                                MessageType = messageMainPartyForLogginMamber.MessageType,
                                MessageDate = messageData.MessageDate,
                                MessageFile = messageData.MessageFile,
                                MessageRead = messageData.MessageRead,
                                MessageSeenAdmin = messageData.MessageSeenAdmin,
                                MessageSubject = messageData.MessageSubject,
                                ProductId = messageData.ProductId,
                                MessageContent = messageData.MessageContent,
                                Product = ProductSearchResult
                            };
                            privateMessageViewList.Add(privateMessage);
                        }
                    }
                    processStatus.Result = privateMessageViewList;
                    processStatus.TotolRowCount = privateMessageViewList.Count;
                    processStatus.Message.Header = "Inbox Private Message";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Inbox Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Inbox Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetInboxPrivateMessageContent(int messageId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var messageMainPartyForLogginMamber = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageTypeEnum.Inbox).Where(x => x.MessageId == messageId).Single();

                    var messageData = _messageService.GetMessageByMesssageId(messageMainPartyForLogginMamber.MessageId);
                    messageData.MessageRead = true;
                    _messageService.UpdateMessage(messageData);

                    var senderUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.InOutMainPartyId);
                    var targetUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.MainPartyId);

                    var privateMessageView = new
                    {
                        TargetMainPartyId = targetUser.MainPartyId,
                        TargetNameSurname = targetUser.MemberName + " " + targetUser.MemberSurname,
                        SenderMainPartyId = senderUser.MainPartyId,
                        SenderNameSurname = senderUser.MemberName + " " + senderUser.MemberSurname,
                        MessageId = messageMainPartyForLogginMamber.MessageId,
                        MessageType = messageMainPartyForLogginMamber.MessageType,
                        MessageContent = messageData.MessageContent,
                        MessageDate = messageData.MessageDate,
                        MessageFile = messageData.MessageFile,
                        MessageRead = messageData.MessageRead,
                        MessageSeenAdmin = messageData.MessageSeenAdmin,
                        MessageSubject = messageData.MessageSubject,
                        ProductId = messageData.ProductId
                    };

                    processStatus.Result = privateMessageView;
                    processStatus.Message.Header = "Inbox Private Message";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Inbox Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Inbox Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllSendPrivateMessage()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var privateMessageViewList = new List<Object>();
                    var allMessageMainPartyForLogginMamber = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageTypeEnum.Send).ToList();
                    foreach (var messageMainPartyForLogginMamber in allMessageMainPartyForLogginMamber)
                    {
                        var messageData = _messageService.GetMessageByMesssageId(messageMainPartyForLogginMamber.MessageId);
                        var senderUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.MainPartyId);
                        var targetUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.InOutMainPartyId);

                        var privateMessageView = new
                        {
                            TargetMainPartyId = targetUser.MainPartyId,
                            TargetNameSurname = targetUser.MemberName + " " + targetUser.MemberSurname,
                            SenderMainPartyId = senderUser.MainPartyId,
                            SenderNameSurname = senderUser.MemberName + " " + senderUser.MemberSurname,
                            MessageId = messageMainPartyForLogginMamber.MessageId,
                            MessageType = messageMainPartyForLogginMamber.MessageType,
                            MessageContent = messageData.MessageContent,
                            MessageDate = messageData.MessageDate,
                            MessageFile = messageData.MessageFile,
                            MessageRead = messageData.MessageRead,
                            MessageSeenAdmin = messageData.MessageSeenAdmin,
                            MessageSubject = messageData.MessageSubject,
                            ProductId = messageData.ProductId
                        };
                        privateMessageViewList.Add(privateMessageView);
                    }

                    processStatus.Result = privateMessageViewList;
                    processStatus.TotolRowCount = privateMessageViewList.Count();
                    processStatus.Message.Header = "Outbox Private Message";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Outbox Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Outbox Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllDeletedPrivateMessage()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (member != null)
                {
                    var privateMessageViewList = new List<Object>();
                    var allMessageMainPartyForLogginMamber = _messageService.GetAllMessageMainParty(member.MainPartyId, (byte)MessageType.RecyleBin).ToList();

                    foreach (var messageMainPartyForLogginMamber in allMessageMainPartyForLogginMamber)
                    {
                        var messageData = _messageService.GetMessageByMesssageId(messageMainPartyForLogginMamber.MessageId);
                        var senderUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.InOutMainPartyId);
                        var targetUser = _memberService.GetMemberByMainPartyId(messageMainPartyForLogginMamber.MainPartyId);

                        var privateMessageView = new
                        {
                            TargetMainPartyId = targetUser.MainPartyId,
                            TargetNameSurname = targetUser.MemberName + " " + targetUser.MemberSurname,
                            SenderMainPartyId = senderUser.MainPartyId,
                            SenderNameSurname = senderUser.MemberName + " " + senderUser.MemberSurname,
                            MessageId = messageMainPartyForLogginMamber.MessageId,
                            MessageType = messageMainPartyForLogginMamber.MessageType,
                            MessageContent = messageData.MessageContent,
                            MessageDate = messageData.MessageDate,
                            MessageFile = messageData.MessageFile,
                            MessageRead = messageData.MessageRead,
                            MessageSeenAdmin = messageData.MessageSeenAdmin,
                            MessageSubject = messageData.MessageSubject,
                            ProductId = messageData.ProductId
                        };

                        privateMessageViewList.Add(privateMessageView);
                    }

                    processStatus.Result = privateMessageViewList;
                    processStatus.TotolRowCount = privateMessageViewList.Count();
                    processStatus.Message.Header = "Deleted Private Message";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Deleted Private Message";
                    processStatus.Message.Text = "İşlem başarısız.";
                    processStatus.Status = false;
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Deleted Private Message";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAllMembersInfoForPrivateMessage()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var results = _memberService.GetAllMembers().Where(x => x.MainPartyId != member.MainPartyId && x.MainPartyId != 0 && x.Active == true).ToList();
                    var memberInfoList = new List<MemberInfoForPrivateMessage>();
                    foreach (var result in results)
                    {
                        var memberInfo = new MemberInfoForPrivateMessage()
                        {
                            Active = result.Active,
                            MainPartyId = result.MainPartyId,
                            MemberEmail = result.MemberEmail,
                            MemberName = result.MemberName,
                            MemberSurname = result.MemberSurname,
                        };

                        memberInfoList.Add(memberInfo);
                    }

                    //foreach (var item in Result)
                    //{
                    //    item.StoreLogo = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300);
                    //}
                    processStatus.Result = memberInfoList;
                    processStatus.TotolRowCount = memberInfoList.Count;
                    processStatus.Message.Header = "Kullanıcı İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Kullanıcı İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Login kullanıcı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetMessages()
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var PhoneNumber = "";
                    var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(member.MainPartyId, PhoneTypeEnum.Gsm);
                    if (phone != null)
                    {
                        PhoneNumber = $"{phone.PhoneCulture}{phone.PhoneAreaCode}{phone.PhoneNumber}";
                    }
                    MessageViewMemberItem From = new MessageViewMemberItem() 
                    { 
                        MainPartyId=member.MainPartyId,
                        FirtName=member.MemberName,
                        LastName=member.MemberSurname,
                        Email=member.MemberEmail,
                        Telefon=PhoneNumber,
                    };

                    var SendMessages = _messageService.GetAllMessageMainParty(member.MainPartyId,(byte)MessagePageType.Send);
                    var InboxMessages = _messageService.GetAllMessageMainParty(member.MainPartyId,(byte)MessagePageType.Inbox);
                    List<MessageViewItem> result = new List<MessageViewItem>();
                    result.Clear();

                    List<MakinaTurkiye.Entities.Tables.Catalog.Product> ProductList = new List<MakinaTurkiye.Entities.Tables.Catalog.Product>();
                    List<MakinaTurkiye.Entities.Tables.Members.Member> MemberList = new List<Entities.Tables.Members.Member>();
                    List<MakinaTurkiye.Entities.Tables.Messages.Message> MessageListesi = new List<Entities.Tables.Messages.Message>();
                    if (InboxMessages.Count > 0)
                    {
                        MessageListesi = _messageService.GetMessageByMessageIds(InboxMessages.Select(x => x.MessageId).Distinct().ToList()).ToList();
                        ProductList = _productService.GetProductByProductsIds(MessageListesi.Select(x => x.ProductId).Distinct().ToList()).ToList();
                        MemberList = _memberService.GetMembersByMainPartyIds(InboxMessages.Select(x => x.InOutMainPartyId).Cast<int?>().ToList()).ToList();
                    }

                    foreach (var Msg in InboxMessages)
                    {

                        var Message = _messageService.GetMessageByMesssageId(Msg.MessageId);
                        if (Message != null)
                        {
                            if (result.FirstOrDefault(x => x.MessageId == Message.MessageId) == null)
                            {
                                if (result.FirstOrDefault(x => x.ProductId == Message.ProductId && (
                                (x.From.MainPartyId == Msg.MainPartyId && x.To.MainPartyId == Msg.InOutMainPartyId)
                                ||
                                (x.To.MainPartyId == Msg.MainPartyId && x.From.MainPartyId == Msg.InOutMainPartyId)
                                )) == null)
                                {
                                    MessageViewMemberItem To = new MessageViewMemberItem()
                                {
                                    MainPartyId = 0,
                                    FirtName = "",
                                    LastName = "",
                                    Email = "",
                                };

                                var tomember = MemberList.FirstOrDefault(x => x.MainPartyId == Msg.InOutMainPartyId);
                                if (tomember != null)
                                {
                                    var ToPhoneNumber = "";
                                    var Tophone = _phoneService.GetPhonesByMainPartyIdByPhoneType(member.MainPartyId, PhoneTypeEnum.Gsm);
                                    if (Tophone != null)
                                    {
                                        ToPhoneNumber = $"{Tophone.PhoneCulture}{Tophone.PhoneAreaCode}{Tophone.PhoneNumber}";
                                    }

                                    To = new MessageViewMemberItem()
                                    {
                                        MainPartyId = tomember.MainPartyId,
                                        FirtName = tomember.MemberName,
                                        LastName = tomember.MemberSurname,
                                        Email = tomember.MemberEmail,
                                        Telefon = ToPhoneNumber
                                    };
                                }

                                string picturePath = "";
                                var product = ProductList.FirstOrDefault(x => x.ProductId == (Message.ProductId != null ? Message.ProductId : 0));
                                if (product != null)
                                {
                                    var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
                                    if (picture != null)
                                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                                }

                                MessageViewItem MessageViewItem = new MessageViewItem
                                {
                                    MessageId = Msg.MessageId,
                                    Content = Message.MessageContent,
                                    Subject = Message.MessageSubject,
                                    Date = Message.MessageDate,
                                    From = From,
                                    To = To,
                                    MessageTypeEnum = (MessageTypeEnum)Msg.MessageType,
                                    ProductId = (product != null ? product.ProductId : 0),
                                    ProductName = (product != null ? product.ProductName : ""),
                                    ProductNo = (product != null ? product.ProductNo : ""),
                                    ProductResim = picturePath
                                };
                                result.Add(MessageViewItem);
                            }
                            }
                        }
                    }


                    ProductList = new List<MakinaTurkiye.Entities.Tables.Catalog.Product>();
                    MemberList = new List<Entities.Tables.Members.Member>();
                    MessageListesi = new List<Entities.Tables.Messages.Message>();
                    if (SendMessages.Count > 0)
                    {
                        MessageListesi = _messageService.GetMessageByMessageIds(SendMessages.Select(x => x.MessageId).Distinct().ToList()).ToList();
                        ProductList = _productService.GetProductByProductsIds(MessageListesi.Select(x => x.ProductId).Distinct().ToList()).ToList();
                        MemberList = _memberService.GetMembersByMainPartyIds(SendMessages.Select(x => x.InOutMainPartyId).Cast<int?>().ToList()).ToList();
                    }

                    foreach (var Msg in SendMessages)
                    {
                        var Message = _messageService.GetMessageByMesssageId(Msg.MessageId);
                        if (Message != null)
                        {
                            if (result.FirstOrDefault(x => x.MessageId == Message.MessageId) == null)
                            {
                                if (result.FirstOrDefault(x => x.ProductId == Message.ProductId && (
                                (x.From.MainPartyId == Msg.MainPartyId && x.To.MainPartyId == Msg.InOutMainPartyId)
                                ||
                                (x.To.MainPartyId == Msg.MainPartyId && x.From.MainPartyId == Msg.InOutMainPartyId)
                                )) == null)
                                {

                                    MessageViewMemberItem To = new MessageViewMemberItem()
                                {
                                    MainPartyId = 0,
                                    FirtName = "",
                                    LastName = "",
                                    Email = "",
                                };
                                var tomember = MemberList.FirstOrDefault(x => x.MainPartyId == Msg.InOutMainPartyId);
                                if (tomember != null)
                                {
                                    var ToPhoneNumber = "";
                                    var Tophone = _phoneService.GetPhonesByMainPartyIdByPhoneType(member.MainPartyId, PhoneTypeEnum.Gsm);
                                    if (Tophone != null)
                                    {
                                        ToPhoneNumber = $"{Tophone.PhoneCulture}{Tophone.PhoneAreaCode}{Tophone.PhoneNumber}";
                                    }

                                    To = new MessageViewMemberItem()
                                    {
                                        MainPartyId = tomember.MainPartyId,
                                        FirtName = tomember.MemberName,
                                        LastName = tomember.MemberSurname,
                                        Email = tomember.MemberEmail,
                                        Telefon = ToPhoneNumber,
                                    };
                                }

                                string picturePath = "";
                                var product = ProductList.FirstOrDefault(x => x.ProductId == (Message.ProductId != null ? Message.ProductId : 0));
                                if (product != null)
                                {
                                    var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
                                    if (picture != null)
                                        picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                                }

                                MessageViewItem MessageViewItem = new MessageViewItem
                                {
                                    MessageId = Msg.MessageId,
                                    Content = Message.MessageContent,
                                    Subject = Message.MessageSubject,
                                    Date = Message.MessageDate,
                                    From = From,
                                    To = To,
                                    MessageTypeEnum = (MessageTypeEnum)Msg.MessageType,
                                    ProductId = (product != null ? product.ProductId : 0),
                                    ProductName = (product != null ? product.ProductName : ""),
                                    ProductNo = (product != null ? product.ProductNo : ""),
                                    ProductResim = picturePath
                                };
                                result.Add(MessageViewItem);
                            }
                        }
                        }
                    }

                    result = result.Distinct().ToList();
                    processStatus.Result = result;
                    processStatus.TotolRowCount = result.Count;
                    processStatus.Message.Header = "Kullanıcı İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Kullanıcı İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Login kullanıcı bulunamadı";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

         public HttpResponseMessage GetMessageHistory(int MessageId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var MainMessage = _messageService.GetFirstMessageMainPartyByMessageId(MessageId);
                if (MainMessage!=null)
                {
                    MakinaTurkiye.Entities.Tables.Messages.Message MMessage = _messageService.GetMessageByMesssageId(MainMessage.MessageId);

                    List<MessageMainParty> MessageHistoryList = new List<MessageMainParty>();
                    MessageHistoryList.AddRange(_messageService.GetMessageMainPartyByFromAndTo(MainMessage.MainPartyId, MainMessage.InOutMainPartyId));
                    MessageHistoryList.AddRange(_messageService.GetMessageMainPartyByFromAndTo(MainMessage.InOutMainPartyId,MainMessage.MainPartyId));


                    //MessageHistoryList = MessageHistoryList.Where(x => x.MessageId != MainMessage.MessageId).ToList();
                    List<MessageViewItem> result = new List<MessageViewItem>();
                    result.Clear();
                    List<MakinaTurkiye.Entities.Tables.Catalog.Product> ProductList = new List<MakinaTurkiye.Entities.Tables.Catalog.Product>();
                    List<MakinaTurkiye.Entities.Tables.Members.Member> MemberList = new List<Entities.Tables.Members.Member>();
                    List<MakinaTurkiye.Entities.Tables.Messages.Message> MessageListesi = new List<Entities.Tables.Messages.Message>();
                    if (MessageHistoryList.Count > 0)
                    {
                        
                        MessageListesi = _messageService.GetMessageByMessageIds(MessageHistoryList.Select(x => x.MessageId).Distinct().ToList()).Where(x=>x.ProductId==MMessage.ProductId).ToList();
                        MessageListesi=MessageListesi.Where(x=>x.MessageId!=MainMessage.MessageId).ToList();
                        ProductList = _productService.GetProductByProductsIds(MessageListesi.Select(x => x.ProductId).Distinct().ToList()).ToList();
                        MemberList = _memberService.GetMembersByMainPartyIds(MessageHistoryList.Select(x => x.InOutMainPartyId).Cast<int?>().ToList()).ToList();
                    }


                    foreach (var Message in MessageListesi)
                    {
                        var Msg=_messageService.GetFirstMessageMainPartyByMessageId(Message.MessageId);
                        MessageViewMemberItem From = new MessageViewMemberItem()
                        {
                            MainPartyId = 0,
                            FirtName = "",
                            LastName = "",
                            Email = "",
                        };
                        var frommember = MemberList.FirstOrDefault(x => x.MainPartyId == Msg.MainPartyId);
                        if (frommember != null)
                        {
                            From = new MessageViewMemberItem()
                            {
                                MainPartyId = frommember.MainPartyId,
                                FirtName = frommember.MemberName,
                                LastName = frommember.MemberSurname,
                                Email = frommember.MemberEmail
                            };
                        }


                        MessageViewMemberItem To = new MessageViewMemberItem()
                        {
                            MainPartyId = 0,
                            FirtName = "",
                            LastName = "",
                            Email = "",
                        };
                        var tomember = MemberList.FirstOrDefault(x => x.MainPartyId == Msg.InOutMainPartyId);
                        if (tomember != null)
                        {
                            To = new MessageViewMemberItem()
                            {
                                MainPartyId = tomember.MainPartyId,
                                FirtName = tomember.MemberName,
                                LastName = tomember.MemberSurname,
                                Email = tomember.MemberEmail
                            };
                        }
                        string picturePath = "";
                        var product = ProductList.FirstOrDefault(x => x.ProductId == (Message.ProductId != null ? Message.ProductId : 0));
                        var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
                        if (picture != null)
                            picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;

                        MessageViewItem MessageViewItem = new MessageViewItem
                        {
                            MessageId = Msg.MessageId,
                            Content = Message.MessageContent,
                            Subject = Message.MessageSubject,
                            Date = Message.MessageDate,
                            From = (From.MainPartyId == MainMessage.MainPartyId ? From : To),
                            To = (From.MainPartyId == MainMessage.MainPartyId ? To : From),
                            MessageTypeEnum = (MessageTypeEnum)Msg.MessageType,
                            ProductId = (product != null ? product.ProductId : 0),
                            ProductName = (product != null ? product.ProductName : ""),
                            ProductNo = (product != null ? product.ProductNo : ""),
                            ProductResim = picturePath,
                        };
                        result.Add(MessageViewItem);
                    }

                    processStatus.Result = result;
                    processStatus.TotolRowCount = result.Count;
                    processStatus.Message.Header = "Kullanıcı İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Message.Header = "Message İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "ana mesaj bulunamadı";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Kullanıcı İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


    }
}