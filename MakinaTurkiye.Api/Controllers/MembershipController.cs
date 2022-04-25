using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Authentication;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using ProcessResult = MakinaTurkiye.Api.View.ProcessResult;

namespace MakinaTurkiye.Api.Controllers
{
    public class MembershipController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly IMessageService _messageService;
        private readonly ILoginLogService _loginLogService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IStoreService _storeService;

        public MembershipController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _messageService = EngineContext.Current.Resolve<IMessageService>();
            _loginLogService = EngineContext.Current.Resolve<ILoginLogService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
        }

        //public MembershipController(IMemberService memberService,
        //                         IMessagesMTService messagesMTService,
        //                         IMessageService messageService,
        //                         ILoginLogService loginLogService,
        //                         IMemberStoreService memberStoreService,
        //                         IAuthenticationService authenticationService,
        //                         IStoreService storeService
        //                         )
        //{
        //    this._memberService = memberService;
        //    this._messagesMTService = messagesMTService;
        //    this._messageService = messageService;
        //    this._memberStoreService = memberStoreService;
        //    this._loginLogService = loginLogService;
        //    this._authenticationService = authenticationService;
        //    this._storeService = storeService;
        //}

        public HttpResponseMessage FastMembership([FromBody] UserRegister Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                if (Model.IsContractChecked)
                {
                    if (Model.MemberEmail.IsValidEmail())
                    {
                        string activCode = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 6);
                        var anyUser = _memberService.GetMemberByMemberEmail(Model.MemberEmail);
                        if (anyUser == null)
                        {
                            InsertMember(Model);

                            processStatus.Message.Header = "Fast Membership";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                            processStatus.Result = "Email adresinize gönderilen Üyelik Aktivasyon maili ile lütfen hesabınızı onaylayın";
                        }
                        else
                        {
                            processStatus.Message.Header = "Fast Membership";
                            processStatus.Message.Text = "Başarısız";
                            processStatus.Status = false;
                            processStatus.Result = "Belirtmiş olduğunuz e-posta adresi kullanılmaktadır!";
                        }
                    }
                    else
                    {
                        processStatus.Message.Header = "Fast Membership";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                        processStatus.Result = "Geçersiz mail adresi.";
                    }
                }
                else
                {
                    processStatus.Message.Header = "Fast Membership";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "KVKKK ve diğer Tüm sözleşmeleri Kabul ediniz!";
                }
            }
            catch (Exception ex)
            {
                processStatus.Status = false;
                processStatus.Error = ex;
                processStatus.Message = processStatus.Message = new View.Message()
                {
                    Header = "Fast Membership",
                    Text = string.Format("Hata Oluştu : {0}", ex.Message)
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage LogOn(MemberEmailPassword model)
        {
            ProcessResult processStatus = new ProcessResult();

            var member = _memberService.GetMemberByMemberEmail(model.MemberEmail);
            if (member != null)
            {
                if (member.MemberPassword == model.MemberPassword)
                {
                    //throw new InvalidOperationException("exception");
                    var sendErrorMessage = _messageService.GetSendMessageErrorsBySenderId(member.MainPartyId);
                    if (member.FastMemberShipType == (byte)FastMembershipType.Phone && sendErrorMessage == null)
                    {
                        member.MemberPassword = null;
                        member.Active = false;
                    }
                }
                else
                {
                    processStatus.Message.Header = "User Login";
                    processStatus.Message.Text = "Giriş işlemi başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Email adresi veya parolanız yanlıştır.";
                }
                if (member.Active.HasValue && member.Active.Value)
                {
                    // _authenticationService.SignIn(member, model.Remember);

                    if (member.MemberPassword == model.MemberPassword)
                    {
                        _authenticationService.SignIn(member, true);

                        if (member.MemberType == (byte)MemberType.Enterprise)
                        {
                            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                            LoginLog loginLog = new LoginLog();
                            loginLog.StoreMainPartyId = Convert.ToInt32(memberStore.StoreMainPartyId);
                            loginLog.LoginDate = DateTime.Now;
                            loginLog.IpAddress = HttpContext.Current.Request.UserHostAddress;
                            _loginLogService.InsertLoginLog(loginLog);
                        }

                        processStatus.Message.Header = "User Login";
                        processStatus.Message.Text = "Giriş işlemi başarılı";
                        processStatus.Status = true;
                        var accesToken = GetMemberAccessToken(model);
                        var result = new
                        {
                            member.MainPartyId,
                            Key = "makinaturkiye",
                            MemberNameSurname = member.MemberName + " " + member.MemberSurname,
                            Token = accesToken,
                        };

                        processStatus.Result = result;

                        return Request.CreateResponse(HttpStatusCode.OK, processStatus);
                    }
                    else
                    {
                        processStatus.Message.Header = "User Login";
                        processStatus.Message.Text = "Giriş işlemi başarısız";
                        processStatus.Status = false;
                        processStatus.Result = "Email adresi veya parolanız yanlıştır.";
                    }
                }
                else
                {
                    processStatus.Message.Header = "User Login";
                    processStatus.Message.Text = "Giriş işlemi başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Hesabınız Aktif Değil. Lütfen mail adresinizi kontrol ediniz.";
                }
            }
            else
            {
                processStatus.Message.Header = "User Login";
                processStatus.Message.Text = "Giriş işlemi başarısız";
                processStatus.Status = false;
                processStatus.Result = "Email adresi veya parolanız yanlıştır.";
            }

            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //public HttpResponseMessage ForgettedPassowrd(MemberEmailPassword model, string passwordCode)

        [System.Web.Http.HttpGet]
        public HttpResponseMessage ForgotPassword(string userEmail)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                string[] memberHeader = new string[2];
                if (!string.IsNullOrEmpty(userEmail))
                {
                    var memberInfo = _memberService.GetMemberByMemberEmail(userEmail);
                    if (memberInfo != null)
                    {
                        string code = DateTime.Now.Ticks.ToString();
                        memberInfo.ForgetPasswodCode = code;
                        _memberService.UpdateMember(memberInfo);
                        memberHeader[0] = memberInfo.MemberName;
                        memberHeader[1] = memberInfo.MemberSurname;
                        ReCreateLinkSend(memberInfo.MemberEmail, memberHeader, false, code);

                        processStatus.Message.Header = "Forgotten Password";
                        processStatus.Message.Text = "İşlem başarılı";
                        processStatus.Status = true;
                        processStatus.Result = "Şifre yenileme işlemi başarılı şekilde tamamlanmıştır. Lütfen mail adresinize gönderilen linki kullanarak şifrenizi sıfırlayabilirsiniz.";
                    }
                    else
                    {
                        processStatus.Message.Header = "Forgotten Password";
                        processStatus.Message.Text = "İşlem başarısız";
                        processStatus.Status = false;
                        processStatus.Result = "Sistemde kayıtlı email bulunamadı.";
                    }
                }
                else
                {
                    processStatus.Message.Header = "Forgotten Password";
                    processStatus.Message.Text = "İşlem başarısız";
                    processStatus.Status = false;
                    processStatus.Result = "Şifre yenileme işlemi başarısız.";
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Forgotten Password";
                processStatus.Message.Text = "İşlem başarısız";
                processStatus.Status = false;
                processStatus.Result = "Şifre yenileme işlemi başarısız.";
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage Logout()
        {
            ProcessResult ProcessStatus = new ProcessResult();

            try
            {
                var loginMemberEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                _authenticationService.SignOut();
                ProcessStatus.Status = true;
                ProcessStatus.Message = ProcessStatus.Message = new View.Message()
                {
                    Header = "Logout",
                    Text = "Başarıyla çıkış yapıldı"
                };

                var TxtToken = CheckClaims.GetDefaultAccessToken();

                var Snc = new
                {
                    KullaniciAd = "makinaturkiye",
                    Key = "makinaturkiye",
                    AdSoyad = "makinaturkiye",
                    Token = TxtToken
                };
                ProcessStatus.Result = Snc;
            }
            catch (Exception ex)
            {
                ProcessStatus.Status = false;
                ProcessStatus.Error = ex;
                ProcessStatus.Message = ProcessStatus.Message = new View.Message()
                {
                    Header = "Logout",
                    Text = string.Format("Hata Oluştu : {0}", ex.Message)
                };
            }

            return Request.CreateResponse(HttpStatusCode.OK, ProcessStatus);
        }


        private void InsertMember(UserRegister model)
        {
            try
            {
                string activCode = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 6);
                var anyUser = _memberService.GetMemberByMemberEmail(model.MemberEmail);
                if (anyUser == null)
                {
                    var mainParty = new global::MakinaTurkiye.Entities.Tables.Members.MainParty
                    {
                        Active = false,
                        MainPartyType = (byte)MainPartyType.FastMember,
                        MainPartyRecordDate = DateTime.Now,
                        MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower() + " " + model.Surname.ToLower())
                    };
                    _memberService.InsertMainParty(mainParty);
                    var member = new global::MakinaTurkiye.Entities.Tables.Members.Member
                    {
                        MainPartyId = mainParty.MainPartyId,
                        MemberEmail = model.MemberEmail,
                        MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name.ToLower()),
                        MemberPassword = model.MemberPassword,
                        MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Surname.ToLower()),
                        MemberType = (byte)MemberType.FastIndividual,
                        Active = false,
                        ActivationCode = activCode,
                        FastMemberShipType = (byte)FastMembershipType.Normal,
                    };
                    string memberNo = "##";
                    for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                    {
                        memberNo = memberNo + "0";
                    }
                    memberNo = memberNo + mainParty.MainPartyId;
                    member.MemberNo = memberNo;
                    _memberService.InsertMember(member);

                    ActivationCodeSend(model.MemberEmail, activCode, model.Name + " " + model.Surname);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ActivationCodeSend(string Email, string activationCode, string nameSurname)
        {
            try
            {
                string siteUrl = ConfigurationManager.AppSettings["SiteUrl"].ToString();
                MailMessage mail = new MailMessage();
                MessagesMT mailMessage = _messagesMTService.GetMessagesMTByMessageMTName("Aktivasyon");

                mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName);
                mail.To.Add(Email);
                mail.Subject = mailMessage.MessagesMTTitle;
                string actLink = siteUrl + "Uyelik/Aktivasyon/" + activationCode;
                string template = mailMessage.MessagesMTPropertie;
                template = template.Replace("#OnayKodu#", activationCode).Replace("#OnayLink#", actLink).Replace("#uyeadisoyadi#", nameSurname);
                mail.Body = template;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
                //ExceptionHandler.HandleException(ex);
            }
        }

        private void ReCreateLinkSend(string Email, string[] userNameSurname, bool isInfo, string code)
        {
            try
            {
                string siteUrl = ConfigurationManager.AppSettings["SiteUrl"].ToString();

                string actLink = siteUrl + "Uyelik/SifremiUnuttum/" + code;
                if (!isInfo)
                {
                    MailMessage mail = new MailMessage();
                    MessagesMT mailTemp = _messagesMTService.GetMessagesMTByMessageMTName("sifremiunuttum");
                    mail.From = new MailAddress(mailTemp.Mail, mailTemp.MailSendFromName);
                    mail.To.Add(Email);
                    mail.Subject = mailTemp.MessagesMTTitle;
                    string template = mailTemp.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", userNameSurname[0]).Replace("#userSurname#", userNameSurname[1]).Replace("#createdLink#", actLink).Replace("#userMail#", Email);
                    mail.Body = template;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    this.SendMail(mail);
                }
                else
                {
                    MailMessage mail = new MailMessage();
                    MessagesMT mailTemp = _messagesMTService.GetMessagesMTByMessageMTName("sifreyenileme");
                    mail.From = new MailAddress(mailTemp.Mail, mailTemp.MailSendFromName);
                    mail.To.Add(Email);
                    mail.Subject = mailTemp.MessagesMTTitle;
                    string template = mailTemp.MessagesMTPropertie;
                    template = template.Replace("#uyeadisoyadi#", userNameSurname[0] + " " + userNameSurname[1]);
                    mail.Body = template;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    this.SendMail(mail);
                }
            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ex);
            }
        }

        private string GetMemberAccessToken(MemberEmailPassword model)
        {
            string TxtToken = "";
            try
            {
                var member = _memberService.GetMemberByMemberEmail(model.MemberEmail);
                if (member != null)
                {
                    if (member.MemberPassword == model.MemberPassword)
                    {
                        string Key = ConfigurationManager.AppSettings["Token:Sifre-Key"].ToString();
                        LoginInfoFromToken token = new LoginInfoFromToken()
                        {
                            Key = "makinaturkiye",
                            PrivateAnahtar = "makinaturkiye",
                            LoginMemberEmail = member.MemberEmail,
                            LoginMemberNameSurname = member.MemberName + " " + member.MemberSurname,
                            //EndDate =  DateTime.Now.AddDays(365),
                            LoginMemberMainPartyId = member.MainPartyId
                        };
                        TxtToken = Newtonsoft.Json.JsonConvert.SerializeObject(token, Newtonsoft.Json.Formatting.None).Sifrele(Key);
                    };
                }

                return TxtToken;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage CheckEmailForNewMember(string email)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var member = _memberService.GetMemberByMemberEmail(email);

                var anyUser = member != null;
                processStatus.Message.Header = "CheckEmailForNewMember";
                processStatus.Message.Text = "İşlem başarılı";
                processStatus.Status = true;
                processStatus.Result = anyUser;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "CheckEmailForNewMember";
                processStatus.Message.Text = "İşlem başarısız";
                processStatus.Status = false;
                processStatus.Result = "Şifre yenileme işlemi başarısız.";
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage CheckMailForStore(string email)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var itemMember = _memberService.GetMemberByMemberEmail(email);
                    var itemStore = _storeService.GetStoreByStoreEmail(email);
                    if (itemMember != null || itemStore != null)
                    {
                        processStatus.Message.Header = "CheckMailForStore";
                        processStatus.Message.Text = "İşlem başarılı";
                        processStatus.Status = true;
                        processStatus.Result = false;
                    }
                    if (email == LoginUserEmail)
                    {
                        processStatus.Message.Header = "CheckMailForStore";
                        processStatus.Message.Text = "İşlem başarılı";
                        processStatus.Status = true;
                        processStatus.Result = true;
                    }

                    processStatus.Message.Header = "CheckMailForStore";
                    processStatus.Message.Text = "İşlem başarılı";
                    processStatus.Status = true;
                    processStatus.Result = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "CheckMailForStore";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "CheckMailForStore";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage FastToInstitutional(CorporateInfoModelInput corporateInfoModelInput)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var memberMainParty = new MainParty
                    {
                        Active = false,
                        MainPartyType = (byte)MainPartyType.Member,
                        MainPartyRecordDate = DateTime.Now,
                        MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(member.MemberName.ToLower() + " " + member.MemberSurname.ToLower())
                    };
                    _memberService.InsertMainParty(memberMainParty);

                    if (!string.IsNullOrEmpty(corporateInfoModelInput.birthDate))
                    {
                        var bithDate = new Convert.ToDateTime(corporateInfoModelInput.birthDate.ToString());
                        member.BirthDate = bithDate;
                    }
                }
                
                var model = SessionMembershipViewModel.MembershipViewModel;
                if (string.IsNullOrWhiteSpace(model.MembershipModel.MemberName))
                {
                    Session["TimeOut"] = true;
                    return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
                }
                


               
               

                int memberMainPartyId = memberMainParty.MainPartyId;
                var member = new Member
                {
                    MainPartyId = memberMainPartyId,
                    BirthDate = model.MembershipModel.BirthDate,
                    Gender = model.MembershipModel.Gender,
                    MemberEmail = model.MembershipModel.MemberEmail,
                    MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower()),
                    MemberPassword = model.MembershipModel.MemberPassword,
                    Active = false,
                    MemberType = (byte)MemberType.Enterprise,
                    MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberSurname.ToLower()),
                    MemberTitleType = model.MembershipModel.StoreAuthorizedTitleType,
                    ActivationCode = activCode,
                    ReceiveEmail = model.MembershipModel.ReceiveEmail
                };
                

                string memberNo = "##";
                for (int i = 0; i < 7 - memberMainPartyId.ToString().Length; i++)
                {
                    memberNo = memberNo + "0";
                }
                memberNo = memberNo + memberMainPartyId;
                member.MemberNo = memberNo;

                _memberService.InsertMember(member);

                var storeMainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)MainPartyType.Firm,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                };
                _memberService.InsertMainParty(storeMainParty);

                var storeMainPartyId = storeMainParty.MainPartyId;


                var packet = _packetService.GetPacketByIsStandart(true);
                var store = new Store
                {
                    MainPartyId = storeMainPartyId,
                    PacketId = packet.PacketId,
                    StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.StoreName.ToLower()),
                    StoreEMail = model.MembershipModel.MemberEmail,
                    StoreWeb = model.MembershipModel.StoreWeb,
                    StoreLogo = model.MembershipModel.StoreLogo,
                    StoreActiveType = (byte)PacketStatu.Inceleniyor,
                    StorePacketBeginDate = DateTime.Now,
                    StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                    StoreAbout = model.MembershipModel.StoreAbout,
                    StoreRecordDate = DateTime.Now,
                    StoreEstablishmentDate = model.MembershipModel.StoreEstablishmentDate.HasValue ? model.MembershipModel.StoreEstablishmentDate.Value : 0,
                    StoreCapital = model.MembershipModel.StoreCapital,
                    StoreEmployeesCount = model.MembershipModel.StoreEmployeesCount,
                    StoreEndorsement = model.MembershipModel.StoreEndorsement,
                    StoreType = model.MembershipModel.StoreType,
                    TaxOffice = model.MembershipModel.TaxOffice,
                    TaxNumber = model.MembershipModel.TaxNumber,
                    PurchasingDepartmentEmail = model.MembershipModel.PurchasingDepartmentEmail,
                    PurchasingDepartmentName = model.MembershipModel.PurchasingDepartmentName,
                    FounderText = string.Empty,
                    GeneralText = string.Empty,
                    HistoryText = string.Empty,
                    PhilosophyText = string.Empty,
                    ViewCount = 0,
                    SingularViewCount = 0,
                    StoreShowcase = false,
                };

                string storeNo = "###";
                for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                {
                    storeNo = storeNo + "0";
                }
                storeNo = storeNo + storeMainPartyId;
                store.StoreNo = storeNo;

                _storeService.InsertStore(store);

                var address = new Address
                {
                    MainPartyId = storeMainPartyId,
                    Avenue = model.MembershipModel.Avenue,
                    Street = model.MembershipModel.Street,
                    DoorNo = model.MembershipModel.DoorNo,
                    ApartmentNo = model.MembershipModel.ApartmentNo,
                    AddressDefault = true,
                };
                if (model.MembershipModel.CountryId > 0)
                    address.CountryId = model.MembershipModel.CountryId;
                else
                    address.CountryId = null;
                if (model.MembershipModel.AddressTypeId > 0)
                    address.AddressTypeId = model.MembershipModel.AddressTypeId;
                else
                    address.AddressTypeId = null;
                if (model.MembershipModel.CityId > 0)
                    address.CityId = model.MembershipModel.CityId;
                else
                    address.CityId = null;
                if (model.MembershipModel.LocalityId > 0)
                    address.LocalityId = model.MembershipModel.LocalityId;
                else
                    address.LocalityId = null;
                if (model.MembershipModel.TownId > 0)
                    address.TownId = model.MembershipModel.TownId;
                else
                    address.TownId = null;

                _addressService.InsertAdress(address);

                if (model.MembershipModel.InstitutionalPhoneNumber != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalPhoneNumber))
                {
                    var phone1 = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneAreaCode = model.MembershipModel.InstitutionalPhoneAreaCode,
                        PhoneCulture = model.MembershipModel.InstitutionalPhoneCulture,
                        PhoneNumber = model.MembershipModel.InstitutionalPhoneNumber,
                        PhoneType = (byte)PhoneType.Phone,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phone1);
                }
                if (model.MembershipModel.InstitutionalPhoneNumber2 != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalPhoneNumber2))
                {
                    var phone2 = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneAreaCode = model.MembershipModel.InstitutionalPhoneAreaCode2,
                        PhoneCulture = model.MembershipModel.InstitutionalPhoneCulture2,
                        PhoneNumber = model.MembershipModel.InstitutionalPhoneNumber2,
                        PhoneType = (byte)PhoneType.Phone,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phone2);
                }
                if (model.MembershipModel.InstitutionalGSMNumber != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalGSMNumber))
                {
                    var phoneGsm = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode,
                        PhoneCulture = model.MembershipModel.InstitutionalGSMCulture,
                        PhoneNumber = model.MembershipModel.InstitutionalGSMNumber,
                        PhoneType = (byte)PhoneType.Gsm,
                        GsmType = model.MembershipModel.GsmType
                    };
                    _phoneService.InsertPhone(phoneGsm);
                }
                if (model.MembershipModel.InstitutionalGSMNumber2 != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalGSMNumber2))
                {
                    var phoneGsm2 = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode2,
                        PhoneCulture = model.MembershipModel.InstitutionalGSMCulture2,
                        PhoneNumber = model.MembershipModel.InstitutionalGSMNumber2,
                        PhoneType = (byte)PhoneType.Gsm,
                        GsmType = model.MembershipModel.GsmType2
                    };
                    _phoneService.InsertPhone(phoneGsm2);
                }
                if (model.MembershipModel.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalFaxNumber))
                {
                    var phoneFax = new Phone
                    {
                        MainPartyId = storeMainPartyId,
                        PhoneAreaCode = model.MembershipModel.InstitutionalFaxAreaCode,
                        PhoneCulture = model.MembershipModel.InstitutionalFaxCulture,
                        PhoneNumber = model.MembershipModel.InstitutionalFaxNumber,
                        PhoneType = (byte)PhoneType.Fax,
                        GsmType = null
                    };
                    _phoneService.InsertPhone(phoneFax);
                }
                if (model.MembershipModel.ActivityName != null)
                {
                    for (int i = 0; i < model.MembershipModel.ActivityName.Length; i++)
                    {
                        if (model.MembershipModel.ActivityName.GetValue(i).ToString() != "false")
                        {
                            var storeActivityType = new StoreActivityType
                            {
                                StoreId = storeMainPartyId,
                                ActivityTypeId = Convert.ToByte(model.MembershipModel.ActivityName.GetValue(i))
                            };
                            _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                        }
                    }
                }
                if (model.MembershipModel.StoreActivityCategory != null)
                {
                    var relCategory = model.MembershipModel.StoreActivityCategory.Where(c => c != "false").ToList();
                    for (int i = 0; i < relCategory.Count(); i++)
                    {
                        var storeActivityCategory = new StoreActivityCategory
                        {
                            MainPartyId = storeMainPartyId,
                            CategoryId = int.Parse(relCategory[i])
                        };
                        _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);
                    }
                }
                var memberStore = new MemberStore
                {
                    MemberMainPartyId = memberMainPartyId,
                    StoreMainPartyId = storeMainPartyId,
                    MemberStoreType = 1
                };
                _memberStoreService.InsertMemberStore(memberStore);

                // firma  logo biçimendirme.
                if (!string.IsNullOrEmpty(store.StoreName))
                {
                    string storeLogoFolder = this.Server.MapPath(AppSettings.StoreLogoFolder);
                    string resizeStoreFolder = this.Server.MapPath(AppSettings.ResizeStoreLogoFolder);
                    string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                    List<string> thumbSizesForStoreLogo = new List<string>();
                    thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                    var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, store.MainPartyId.ToString()));
                    di.CreateSubdirectory("thumbs");
                    string oldStoreLogoImageFilePath = string.Format("{0}{1}", storeLogoFolder, store.StoreLogo);
                    if (System.IO.File.Exists(oldStoreLogoImageFilePath))
                    {
                        // eski logoyu kopyala, varsa ustune yaz
                        string newStoreLogoImageFilePath = resizeStoreFolder + store.MainPartyId.ToString() + "\\";
                        string newStoreLogoImageFileName = store.StoreName.ToImageFileName() + "_logo.jpg";
                        System.IO.File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);
                        bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                        newStoreLogoImageFilePath + "thumbs\\" + store.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                        store.StoreLogo = newStoreLogoImageFileName;
                        _storeService.UpdateStore(store);
                    }
                }
                ActivationCodeSend(model.MembershipModel.MemberEmail, activCode, model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname);


                processStatus.Message.Header = "CheckEmailForNewMember";
                processStatus.Message.Text = "İşlem başarılı";
                processStatus.Status = true;
                processStatus.Result = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "CheckEmailForNewMember";
                processStatus.Message.Text = "İşlem başarısız";
                processStatus.Status = false;
                processStatus.Result = "Şifre yenileme işlemi başarısız.";
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

    }
}