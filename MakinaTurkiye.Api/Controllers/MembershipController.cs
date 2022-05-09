using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Authentication;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Packets;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using MakinaTurkiye.Api.ExtentionsMethod;
using ProcessResult = MakinaTurkiye.Api.View.ProcessResult;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Core;
using System.Collections.Generic;

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
        private readonly IPacketService _packetService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;

        public MembershipController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _messageService = EngineContext.Current.Resolve<IMessageService>();
            _loginLogService = EngineContext.Current.Resolve<ILoginLogService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _packetService = EngineContext.Current.Resolve<IPacketService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _storeActivityTypeService = EngineContext.Current.Resolve<IStoreActivityTypeService>();
        }

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

        public HttpResponseMessage FastToInstitutional(CorporateInfoModelInput Model)
        {
            ProcessResult processStatus = new ProcessResult();
            using (System.Transactions.TransactionScope Transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, TimeSpan.FromMinutes(30)))
            {
                try
                {
                    var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                    var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                    if (loginmember != null)
                    {
                        string StoreLogo = "";
                        int? StoreEstablishment = Convert.ToInt32(Convert.ToDateTime(Model.storeEstDate).Year);

                        if (StoreEstablishment == null)
                        {
                            StoreEstablishment = 0;
                        }

                        var member = _memberService.GetMemberByMainPartyId(loginmember.MainPartyId);
                        if (member != null)
                        {
                            member.MemberName = Model.name;
                            member.MemberSurname = Model.surname;
                            member.BirthDate = Convert.ToDateTime(member.BirthDate);
                            member.MemberTitleType = (byte)MemberType.Enterprise;
                            member.MemberType = (byte)MemberType.Enterprise;
                            member.FastMemberShipType = (byte)FastMembershipType.Normal;
                            _memberService.UpdateMember(member);
                        }

                        var storeMainParty = new MainParty
                        {
                            Active = false,
                            MainPartyType = (byte)MainPartyType.Firm,
                            MainPartyRecordDate = DateTime.Now,
                            MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Model.storeName.ToLower()),
                        };

                        _memberService.InsertMainParty(storeMainParty);


                        var packet = _packetService.GetPacketByIsStandart(true);



                        var store = new Store
                        {
                            MainPartyId = storeMainParty.MainPartyId,
                            PacketId = packet.PacketId,
                            StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Model.storeName.ToLower()),
                            StoreEMail = member.MemberEmail,
                            StoreWeb = Model.storeWeb,
                            StoreLogo = StoreLogo,
                            StoreActiveType = (byte)PacketStatu.Inceleniyor,
                            StorePacketBeginDate = DateTime.Now,
                            StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                            StoreAbout = Model.storeAbout,
                            StoreRecordDate = DateTime.Now,
                            StoreEstablishmentDate = StoreEstablishment,
                            StoreCapital = (byte)Model.storeCapID,
                            StoreEmployeesCount = (byte)Model.storeEmpCountID,
                            StoreEndorsement = (byte)Model.storeEndorseID,
                            StoreType = (byte)Model.storeTypeID,
                            TaxOffice = Model.storeTaxAuth,
                            TaxNumber = Model.storeTaxNo,
                            StoreUrlName = Model.storeUrl,
                            StoreShortName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Model.storeName.ToLower()),
                            PurchasingDepartmentEmail = member.MemberEmail,
                            PurchasingDepartmentName = "Satın Alma",
                            FounderText = string.Empty,
                            GeneralText = string.Empty,
                            HistoryText = string.Empty,
                            PhilosophyText = string.Empty,
                            ViewCount = 0,
                            SingularViewCount = 0,
                            StoreShowcase = false,
                        };
                        string storeNo = "###";
                        for (int i = 0; i < 6 - storeMainParty.MainPartyId.ToString().Length; i++)
                        {
                            storeNo = storeNo + "0";
                        }
                        storeNo = storeNo + storeMainParty.MainPartyId;
                        store.StoreNo = storeNo;
                        _storeService.InsertStore(store);

                        var storeMainPartyId = store.MainPartyId;
                        var address = _addressService.GetFisrtAddressByMainPartyId(member.MainPartyId);

                        if (address != null)
                        {
                            address.MainPartyId = storeMainPartyId;
                            address.Street = Model.sokak;
                            address.CityId = Model.selectedCityID;
                            address.CountryId = Model.selectedCountryID;
                            address.LocalityId = Model.selectedLocalityID;
                            address.TownId = Model.selectedTownID;
                            address.Avenue = Model.cadde;
                            address.PostCode = Model.posta;
                            _addressService.UpdateAddress(address);
                        }

                        var phones = _phoneService.GetPhonesByMainPartyId(member.MainPartyId);
                        foreach (var phoneItem in phones)
                        {
                            phoneItem.MainPartyId = storeMainPartyId;
                            _phoneService.UpdatePhone(phoneItem);
                        }

                        foreach (var ActivityId in Model.storeActivitySelected)
                        {
                            var storeActivityType = new StoreActivityType
                            {
                                StoreId = storeMainPartyId,
                                ActivityTypeId = (byte)ActivityId
                            };
                            _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                        }

                        var memberStore = new MemberStore
                        {
                            MemberMainPartyId = member.MainPartyId,
                            StoreMainPartyId = storeMainPartyId,
                            MemberStoreType = (byte)MemberStoreType.Owner
                        };

                        _memberStoreService.InsertMemberStore(memberStore);

                        #region bireyseldenkurumsalagecis

                        //var settings = ConfigurationManager.AppSettings;
                        MailMessage mail = new MailMessage();
                        MessagesMT mailT = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");
                        mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                        string template = mailT.MessagesMTPropertie;
                        template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword).Replace("#firmaadi#", store.StoreName);
                        mail.Body = template;                                                            //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        this.SendMail(mail);
                        #endregion
                        #region bilgimakina

                        MailMessage mailb = new MailMessage();
                        MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                        mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                        mailb.To.Add("bilgi@makinaturkiye.com");
                        mailb.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                        //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                        //templatet = messagesmttemplate.MessagesMTPropertie;
                        string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                        bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Firma Üyelik");
                        mailb.Body = bilgimakinaicin;
                        mailb.IsBodyHtml = true;
                        mailb.Priority = MailPriority.Normal;
                        this.SendMail(mailb);
                        #endregion

                        string Logo = Model.logoBase64;
                        if (!string.IsNullOrEmpty(store.StoreName))
                        {
                            // Gelen Base64String CVonvert Edilerek Kayıt Edilecek...
                            string Uzanti = Logo.GetUzanti();
                            if (!string.IsNullOrEmpty(Uzanti))
                            {
                                bool IslemDurum = false;
                                string ServerImageUrl = "";
                                if (Uzanti == "jpg")
                                {
                                    IslemDurum = true;
                                }
                                if (Uzanti == "png")
                                {
                                    IslemDurum = true;
                                }

                                if (IslemDurum)
                                {
                                    string storeLogoFolder = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.StoreLogoFolder);
                                    string resizeStoreFolder = System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.ResizeStoreLogoFolder);
                                    string storeLogoThumbSize = AppSettings.StoreLogoThumbSizes;
                                    List<string> thumbSizesForStoreLogo = new List<string>();
                                    thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                                    var di = System.IO.Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, store.MainPartyId.ToString()));
                                    di.CreateSubdirectory("thumbs");

                                    string newStoreLogoImageFilePath = resizeStoreFolder + store.MainPartyId.ToString() + "\\";
                                    string newStoreLogoImageFileName = store.StoreName.ToImageFileName() + "_logo." + Uzanti;

                                    ServerImageUrl = $"~{AppSettings.StoreLogoFolder}/{store.StoreName.ToImageFileName()}_logo.{Uzanti}";
                                    string ServerFile = System.Web.Hosting.HostingEnvironment.MapPath(ServerImageUrl);
                                    System.Drawing.Image Img = Logo.ToImage();
                                    if (Uzanti == "png")
                                    {
                                        Img.Save(ServerFile, System.Drawing.Imaging.ImageFormat.Png);
                                    }
                                    if (Uzanti == "jpg")
                                    {
                                        Img.Save(ServerFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }
                                    store.StoreLogo = ServerImageUrl;
                                    _storeService.UpdateStore(store);
                                    bool thumbResult = ImageProcessHelper.ImageResize(ServerFile, newStoreLogoImageFilePath + "thumbs\\" + store.StoreName.ToImageFileName(), thumbSizesForStoreLogo);
                                }
                                //processStatus.ActiveResultRowCount = 1;
                                //processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                //processStatus.Message.Header = "Store İşlemleri";
                                //processStatus.Message.Text = "Başarılı";
                                //processStatus.Status = true;
                            }
                            else
                            {
                                //processStatus.ActiveResultRowCount = 1;
                                //processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                                //processStatus.Message.Header = "Store İşlemleri";
                                //processStatus.Message.Text = "Resim Uzantısı Hatalı";
                                //processStatus.Status = false;
                            }
                        }
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "İşlem başarılı";
                        processStatus.Status = true;
                        processStatus.Result = null;
                        Transaction.Complete();
                    }
                    else
                    {
                        processStatus.Message.Header = "Store İşlemleri";
                        processStatus.Message.Text = "Member Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                    }
                }
                catch (Exception ex)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "İşlem başarısız";
                    processStatus.Status = false;
                    processStatus.Result = ex.Message;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}