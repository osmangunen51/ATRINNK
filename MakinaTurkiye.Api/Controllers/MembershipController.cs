using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Authentication;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
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

        public MembershipController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _messageService = EngineContext.Current.Resolve<IMessageService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _loginLogService = EngineContext.Current.Resolve<ILoginLogService>();
            _authenticationService = EngineContext.Current.Resolve<IAuthenticationService>();
        }

        public HttpResponseMessage FastMembership([FromBody]UserRegister Model)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                if (Model.IsContractChecked)
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
                    processStatus.Result = "Email veya şifreyi kontrol edin";
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
        public HttpResponseMessage ForgettedPassowrd(string userEmail)
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

                //if (string.IsNullOrEmpty(passwordCode))
                //{
                //    var memberInfo = _memberService.GetMemberByMemberEmail(model.MemberEmail);
                //    if (memberInfo != null)
                //    {
                //        string code = DateTime.Now.Ticks.ToString();
                //        memberInfo.ForgetPasswodCode = code;
                //        _memberService.UpdateMember(memberInfo);
                //        memberHeader[0] = memberInfo.MemberName;
                //        memberHeader[1] = memberInfo.MemberSurname;
                //        ReCreateLinkSend(memberInfo.MemberEmail, memberHeader, false, code);

                //        processStatus.Message.Header = "Forgotten Password";
                //        processStatus.Message.Text = "İşlem başarılı";
                //        processStatus.Status = true;
                //        processStatus.Result = "Şifre yenileme işlemi başarılı şekilde tamamlanmıştır. Lütfen mail adresinize gönderilen linki kullanarak şifrenizi sıfırlayabilirsiniz.";
                //    }
                //    else
                //    {
                //        processStatus.Message.Header = "Forgotten Password";
                //        processStatus.Message.Text = "İşlem başarısız";
                //        processStatus.Status = false;
                //        processStatus.Result = "Sistemde kayıtlı email bulunamadı.";
                //    }
                //}
                //else
                //{
                //    var memberInfo = _memberService.GetMemberByForgetPasswordCode(passwordCode);
                //    if (memberInfo != null)
                //    {
                //        memberInfo.MemberPassword = model.MemberPassword;
                //        memberInfo.ForgetPasswodCode = "";
                //        _memberService.UpdateMember(memberInfo);
                //        memberHeader[0] = memberInfo.MemberName;
                //        memberHeader[1] = memberInfo.MemberSurname;
                //        ReCreateLinkSend(memberInfo.MemberEmail, memberHeader, true, "");

                //        processStatus.Message.Header = "Forgotten Password";
                //        processStatus.Message.Text = "İşlem başarılı";
                //        processStatus.Status = true;
                //        processStatus.Result = "Şifre yenileme işlemi başarılı şekilde tamamlanmıştır. Lütfen mail adresinize gönderilen linki kullanarak şifrenizi sıfırlayabilirsiniz.";
                //    }
                //    else
                //    {
                //        processStatus.Message.Header = "Forgotten Password";
                //        processStatus.Message.Text = "İşlem başarısız";
                //        processStatus.Status = false;
                //        processStatus.Result = "Şifre yenileme işlemi başarısız.";
                //    }
                //}
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
                var loginMemberEmail = Request.CheckLoginUserClaims().MemberEmail;

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
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword);
                sc.Send(mail);
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
                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp.gmail.com";
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential(mailTemp.Mail, mailTemp.MailPassword);
                    sc.Send(mail);
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
                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp.gmail.com";
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential(mailTemp.Mail, mailTemp.MailPassword);
                    sc.Send(mail);
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
                            MemberEmail = member.MemberEmail,
                            MemberNameSurname = member.MemberName + " " + member.MemberSurname
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
    }
}