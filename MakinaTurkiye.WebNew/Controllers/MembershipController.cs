using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Authentication;
using MakinaTurkiye.Services.Bulletins;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.Mvc;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Bulletins;
using NeoSistem.MakinaTurkiye.Web.Models.MemberShip;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{

    public class SessionMembershipViewModel
    {


        internal static readonly string SESSION_USERID = "MembershipViewModel";

        public static MembershipViewModel MembershipViewModel
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USERID] == null)
                {
                    HttpContext.Current.Session[SESSION_USERID] = new MembershipViewModel();
                }
                return HttpContext.Current.Session[SESSION_USERID] as MembershipViewModel;
            }
            set { HttpContext.Current.Session[SESSION_USERID] = value; }
        }

        public static bool HasSession()
        {
            if (HttpContext.Current.Session[SESSION_USERID] != null)
                return true;
            return false;
        }

        public static void Flush()
        {
            HttpContext.Current.Session[SESSION_USERID] = null;
        }
    }

    public class LinkHelper
    {

        TripleDESCryptoServiceProvider cryptDES3 = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider cryptMD5Hash = new MD5CryptoServiceProvider();
        string key = "info@makinaturkiye.com";
        public string Encrypt(string text)
        {
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateEncryptor();
            byte[] buff = ASCIIEncoding.ASCII.GetBytes(text);
            string Encrypt = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
            Encrypt = Encrypt.Replace("+", "!");
            return Encrypt;
        }

        public string Decypt(string text)
        {
            text = text.Replace("!", "+");
            byte[] buf = new byte[text.Length];
            cryptDES3.Key = cryptMD5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            cryptDES3.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = cryptDES3.CreateDecryptor();
            buf = Convert.FromBase64String(text);
            string Decrypt = ASCIIEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buf, 0, buf.Length));
            return Decrypt;
        }

    }
    public enum RememberPasswordProcessType
    {
        ShowSendMailForm = 0,
        PostMail = 1,
        PostPassword = 2,
        ShowReCreatePasswordForm = 3,
        ShowSuccesSendMail = 4,
        FailReCreatePassword = 5,
        ShowSuccesChangePassword = 6

    }

    //[System.Runtime.InteropServices.GuidAttribute("408DC755-E9DF-4C5F-85C2-3B18EA9B92F4")]
    [AllowAnonymous]
    [Compress]
    public class MembershipController : BaseController
    {
        #region Fields

        private readonly IMemberService _memberService;
        private readonly ICompanyDemandMembershipService _companyDemandMembershipService;
        private readonly ILoginLogService _loginLogService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IBulletinService _bulettinService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IMessageService _messageService;
        private readonly IAddressService _addressService;
        private readonly IPhoneService _phoneService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly IUserLogService _userLogService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly IConstantService _constantService;
        private readonly IPacketService _packetService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IStoreActivityCategoryService _storeActivityCategoryService;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Ctor

        public MembershipController(IMemberService memberService,
            ICompanyDemandMembershipService companyDemandMembershipService,
            ILoginLogService loginLogService,
            IMemberStoreService memberStoreService,
            IBulletinService bulettinService,
            ICategoryService categoryService,
            IStoreService storeService,
            IProductService productService,
            IMessageService messageService,
            IAddressService addressService,
            IPhoneService phoneService,
            IMessagesMTService messagesMTService,
            IUserLogService userLogService,
            IActivityTypeService activityTypeService,
            IConstantService constantService,
            IPacketService packetService,
            IStoreActivityTypeService storeActivityTypeService,
            IStoreActivityCategoryService storeActivityCategoryService,
            IAuthenticationService authenticationService)
        {
            this._loginLogService = loginLogService;
            this._memberService = memberService;
            this._companyDemandMembershipService = companyDemandMembershipService;
            this._memberStoreService = memberStoreService;
            this._bulettinService = bulettinService;
            this._categoryService = categoryService;
            this._storeService = storeService;
            this._productService = productService;
            this._messageService = messageService;
            this._addressService = addressService;
            this._phoneService = phoneService;
            this._messagesMTService = messagesMTService;
            this._userLogService = userLogService;
            this._activityTypeService = activityTypeService;
            this._constantService = constantService;
            this._packetService = packetService;
            this._storeActivityTypeService = storeActivityTypeService;
            this._storeActivityCategoryService = storeActivityCategoryService;
            this._authenticationService = authenticationService;

            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            //SeoPageType = (byte)PageType.General;
            return View();
        }

        [HttpGet]
        public ActionResult ForgettedPassowrd(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                ViewData["ProcessType"] = RememberPasswordProcessType.ShowSendMailForm;
            }
            else
            {
                ViewData["ProcessType"] = RememberPasswordProcessType.ShowReCreatePasswordForm;
                ViewData["forgetPasswordCode"] = userId;
            }
            return View("ForgetPassword");
        }

        [HttpPost]
        public ActionResult ForgettedPassowrd(MembershipViewModel model, string passwordCode)
        {
            string[] memberHeader = new string[2];

            if (string.IsNullOrEmpty(passwordCode))
            {

                var memberInfo = _memberService.GetMemberByMemberEmail(model.MembershipModel.MemberEmail);
                if (memberInfo != null)
                {
                    string code = DateTime.Now.Ticks.ToString();
                    memberInfo.ForgetPasswodCode = code;
                    _memberService.UpdateMember(memberInfo);
                    memberHeader[0] = memberInfo.MemberName;
                    memberHeader[1] = memberInfo.MemberSurname;
                    ReCreateLinkSend(memberInfo.MemberEmail, memberHeader, false, code);
                    ViewData["ProcessType"] = RememberPasswordProcessType.ShowSuccesSendMail;
                }
                else
                    ViewData["ProcessType"] = RememberPasswordProcessType.ShowSendMailForm;
            }
            else
            {
                var memberInfo = _memberService.GetMemberByForgetPasswordCode(passwordCode);
                if (memberInfo != null)
                {
                    memberInfo.MemberPassword = model.MembershipModel.MemberPassword;
                    memberInfo.ForgetPasswodCode = "";
                    _memberService.UpdateMember(memberInfo);
                    memberHeader[0] = memberInfo.MemberName;
                    memberHeader[1] = memberInfo.MemberSurname;
                    ReCreateLinkSend(memberInfo.MemberEmail, memberHeader, true, "");
                    ViewData["ProcessType"] = RememberPasswordProcessType.ShowSuccesChangePassword;
                }
                else
                    ViewData["ProcessType"] = RememberPasswordProcessType.FailReCreatePassword;
            }
            return View("ForgetPassword");
        }
        //Declare the below

        public JsonResult CheckEmailForNewMember(string email)
        {
            var member = _memberService.GetMemberByMemberEmail(email);

            var anyUser = member != null;
            return Json(anyUser, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public string CheckMail(string email)
        {
            var itemMember = _memberService.GetMemberByMemberEmail(email);
            var itemStore = _storeService.GetStoreByStoreEmail(email);
            if (itemMember != null || itemStore != null)
            {
                return "false";
            }
            if (email == AuthenticationUser.Membership.MemberEmail)
            {
                return "true";
            }

            return "true";
        }

        [HttpGet]
        public JsonResult CheckMemberEmail(string memberEmail)
        {
            var member = _memberService.GetMemberByMemberEmail(memberEmail);
            if (member != null)
                return Json(new { valid = false }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { valid = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckMailforMessage(FormCollection coll)
        {
            if (coll.Count > 0)
            {
                string email = coll[0].ToString();

                var itemMember = _memberService.GetMemberByMemberEmail(email);
                var itemStore = _storeService.GetStoreByStoreEmail(email);

                if (email == AuthenticationUser.Membership.MemberEmail)
                {
                    ViewData["inuser"] = 1;
                    return Json(true);
                }

                if (itemMember != null || itemStore != null)
                {
                    ViewData["inuser"] = 0;
                    return Json("&nbsp;&nbsp;&nbsp;<span style=\"color:Red;font-size:11px;\">E-Posta adresi kullanılıyor.</span>");
                }
                return Json(true);
            }
            return Json(true);
        }

        public ActionResult MembershipProductMessage()
        {
            ViewData["MembershipType"] = (byte)MemberType.Individual;
            //SeoPageType = (byte)PageType.General;
            SessionMembershipViewModel.Flush();
            var model = new MembershipViewModel();
            model.MembershipModel.MemberEmail = Session["emailadresi"].ToString();
            Session["emailforadress"] = Session["emailadresi"].ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult MembershipProductMessage(MembershipViewModel model)
        {
            //model.MembershipModel.MemberEmailAgain = model.MembershipModel.MemberEmail;

            #region bireyselkayit

            byte MembershipType = 10;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel = model.MembershipModel;
            if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
            {
                DateTime birthDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                model.MembershipModel.BirthDate = birthDate;
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.BirthDate = birthDate;
            }

            if (ModelState.IsValid)
            {
                string activCode = Guid.NewGuid().ToString("N").ToUpper();

                if (MembershipType == (byte)MemberType.FastIndividual)
                {


                    var mainParty = new MainParty
                    {
                        Active = false,
                        MainPartyType = (byte)MainPartyType.FastMember,
                        MainPartyRecordDate = DateTime.Now,
                        MainPartyFullName = model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname
                    };
                    _memberService.InsertMainParty(mainParty);

                    var member = new Member
                    {
                        MainPartyId = mainParty.MainPartyId,
                        Gender = model.MembershipModel.Gender,
                        MemberEmail = model.MembershipModel.MemberEmail,
                        MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower()),
                        MemberPassword = model.MembershipModel.MemberPassword,
                        MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberSurname.ToLower()),
                        MemberType = (byte)MemberType.FastIndividual,
                        Active = false,
                        MemberTitleType = model.MembershipModel.MemberTitleType,
                        ActivationCode = activCode,
                        BirthDate = model.MembershipModel.BirthDate,
                    };

                    string memberNo = "##";
                    for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                    {
                        memberNo = memberNo + "0";
                    }
                    memberNo = memberNo + mainParty.MainPartyId;
                    member.MemberNo = memberNo;

                    _memberService.InsertMember(member);

                    SessionMembershipViewModel.Flush();
                    ActivationCodeSend(model.MembershipModel.MemberEmail, activCode, model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname);

                    return View("MembershipApproval");
                }
                else if (MembershipType == (byte)MemberType.Individual)
                {
                    SaveIndividualMembershipProduct(model);
                    return View("MembershipApproval");
                }
                else
                {
                    return RedirectToAction("InstitutionalStep1", "Membership");
                }
            }
            return View(new MembershipViewModel());
            #endregion

        }

        public ActionResult Membershipformessages()
        {
            MemberType type = (MemberType)Convert.ToByte(this.RouteData.Values["MemberType"]);
            ViewData["MembershipType"] = (byte)MemberType.FastIndividual;
            //SeoPageType = (byte)PageType.General;
            SessionMembershipViewModel.Flush();
            var model = new MembershipViewModel();
            model.MembershipModel.MemberEmail = Session["emailadresi"].ToString();
            Session["emailforadress"] = Session["emailadresi"].ToString();
            //model.MembershipModel.MemberEmailAgain = Session["emailadresi"].ToString();
            return View(model);
        }

        [HttpPost]
        public ActionResult Membershipformessages(MembershipViewModel model)
        {
            Session["emailadresi"] = null;
            //model.MembershipModel.MemberEmailAgain = model.MembershipModel.MemberEmail.ToString();
            byte MembershipType = (byte)MemberType.FastIndividual;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel = model.MembershipModel;
            if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
            {
                DateTime birthDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                model.MembershipModel.BirthDate = birthDate;
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.BirthDate = birthDate;
            }
            if (ModelState.IsValid)
            {
                string activCode = Guid.NewGuid().ToString("N").ToUpper();

                if (MembershipType == (byte)MemberType.FastIndividual)
                {
                    var mainParty = new MainParty
                    {
                        Active = false,
                        MainPartyType = (byte)MainPartyType.FastMember,
                        MainPartyRecordDate = DateTime.Now,
                        MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower() + " " + model.MembershipModel.MemberSurname.ToLower())
                    };
                    _memberService.InsertMainParty(mainParty);

                    var member = new Member
                    {
                        MainPartyId = mainParty.MainPartyId,
                        Gender = model.MembershipModel.Gender,
                        MemberEmail = model.MembershipModel.MemberEmail,
                        MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower()),
                        MemberPassword = model.MembershipModel.MemberPassword,
                        MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberSurname.ToLower()),
                        MemberType = (byte)MemberType.FastIndividual,
                        Active = false,
                        MemberTitleType = model.MembershipModel.MemberTitleType,
                        ActivationCode = activCode,
                        BirthDate = model.MembershipModel.BirthDate,
                    };

                    string memberNo = "##";
                    for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                    {
                        memberNo = memberNo + "0";
                    }
                    memberNo = memberNo + mainParty.MainPartyId;
                    member.MemberNo = memberNo;

                    _memberService.InsertMember(member);


                    //mesaj kayıt
                    //string mesaj = "";
                    //if (Session["mesaggesicerik"] != null)
                    //{
                    //    mesaj = Session["mesaggesicerik"].ToString();
                    //}
                    //var curMessage = new MessageEntry
                    //{
                    //    MainPartyId = mainParty.MainPartyId,
                    //    FromToMainPartyId = 51582,
                    //    ForWhoMainPartyId = mainParty.MainPartyId,
                    //    MessageSubject = model.MembershipModel.MemberEmail,
                    //    MessageContent = mesaj,
                    //    MessageRead = true,
                    //    MessageDate = DateTime.Now,
                    //    ProductId = null,
                    //    Active = true
                    //};
                    //entities.MessageEntries.AddObject(curMessage);
                    //entities.SaveChanges();
                    //mesaj kayıt bilgisi.



                    SessionMembershipViewModel.Flush();
                    ActivationCodeSend(model.MembershipModel.MemberEmail, activCode, model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname);

                    Session["sendingsucciid"] = 2;
                    return RedirectToAction("messageView", "Home");
                }
                else if (MembershipType == (byte)MemberType.Individual)
                {
                    SaveIndividualMembership(model);
                    return RedirectToAction("MembershipApproval", new { email = model.MembershipModel.MemberEmail });
                }
                else
                {
                    return RedirectToAction("InstitutionalStep1", "Membership");
                }
            }

            Session["sendingsucciid"] = 3;
            return RedirectToAction("messageView", "Home");
        }

        public ActionResult FastMembership(string gelenSayfa)
        {
            MTMembershipFormModel model = new MTMembershipFormModel();
            return View(model);
        }


        public void GoogleLogin(string email, string name, string gender, string lastname, string location)
        {

        }


        [HttpPost]
        public ActionResult SocialMembership(MembershipModel model, byte MembershipType, string profileId, byte? fastmembershipType)
        {
            var LogOn = false;
            if (model.Year > 0 && model.Month > 0 && model.Day > 0)
            {
                DateTime birthDate = new DateTime(model.Year, model.Month, model.Day);
                model.BirthDate = birthDate;
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.BirthDate = birthDate;
            }
            if (!string.IsNullOrEmpty(model.MemberEmail))
            {
                var anyMember = _memberService.GetMemberByMemberEmail(model.MemberEmail);
                if (anyMember == null)
                {
                    if (MembershipType == (byte)MemberType.FastIndividual)
                    {
                        var mainParty = new MainParty
                        {
                            Active = false,
                            MainPartyType = (byte)MainPartyType.FastMember,
                            MainPartyRecordDate = DateTime.Now,
                            MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberName.ToLower() + " " + model.MemberSurname.ToLower())
                        };
                        _memberService.InsertMainParty(mainParty);

                        var member = new Member
                        {
                            MainPartyId = mainParty.MainPartyId,
                            Gender = model.Gender,
                            MemberEmail = model.MemberEmail,
                            MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberName.ToLower()),
                            MemberPassword = model.MemberPassword,
                            MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberSurname.ToLower()),
                            MemberType = (byte)MemberType.FastIndividual,
                            Active = true,
                            MemberTitleType = model.MemberTitleType,
                            ActivationCode = profileId,
                            FastMemberShipType = fastmembershipType.HasValue == true ? fastmembershipType.Value : (byte)FastMembershipType.Facebook,
                            BirthDate = model.BirthDate
                        };

                        string memberNo = "##";
                        for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                        {
                            memberNo = memberNo + "0";
                        }
                        memberNo = memberNo + mainParty.MainPartyId;
                        member.MemberNo = memberNo;

                        _memberService.InsertMember(member);


                        _authenticationService.SignIn(member, true);

                        ////SessionMembershipViewModel.Flush();

                        ////AuthenticationUser.Membership = mainParty.Member;
                        //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");



                        //var ctx = Request.GetOwinContext();
                        //var authManager = ctx.Authentication;
                        //authManager.SignIn(identity);
                        ////HttpCookie MainPartyId = new HttpCookie("mtCookie");
                        ////Response.Cookies["MainPartyId"].Value = mainParty.MainPartyId.ToString();

                        ////Response.Cookies["MainPartyId"].Expires = DateTime.Now.AddDays(1);
                        ////Response.Cookies.Add(MainPartyId);

                        LogOn = true;
                    }
                    else
                    {
                        return RedirectToAction("InstitutionalStep1", "Membership");
                    }
                }
                else
                {
                    if (anyMember.ActivationCode == profileId)
                    {
                        //  _authenticationService.SignIn(anyMember, true);
                        ////AuthenticationUser.Membership = memberUser;
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, anyMember.MainPartyId.ToString()) }, "LoginCookie");

                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;
                        authManager.SignIn(identity);
                    }
                    LogOn = true;
                }
            }
            return Json(LogOn);
        }

        public ActionResult CompanyMemberShipDemand()
        {
            return View();
        }






        [HttpPost]
        public JsonResult CompanyMemberShipDemand(CompanyDemandMembership model)
        {
            if (ModelState.IsValid)
            {
                var newDemand = new CompanyDemandMembership();
                newDemand.CompanyName = model.CompanyName;
                newDemand.Email = model.Email;
                newDemand.NameSurname = model.NameSurname;
                newDemand.Phone = model.Phone;
                newDemand.Statement = model.Statement;
                newDemand.WebUrl = model.WebUrl;
                newDemand.DemandDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                newDemand.Status = 0;
                newDemand.isDemandForPacket = false;
                _companyDemandMembershipService.AddCompanyDemandMembership(newDemand);
                //#region receiveWithMail
                //try
                //{
                //    MailMessage mail = new MailMessage();
                //    mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye");
                //    mail.To.Add("makinaturkiye@makinaturkiye.com");
                //    mail.Subject = model.NameSurname + " adlı kişi Firma aranma talebinde bulunuyor";
                //    mail.Body = model.CompanyName + " isimli firmaya sahip" + model.NameSurname + " adlı kişi aranmak istiyor.<br>Telefon Numarası: " + model.Phone + " <br>Email adresi:" + model.Email + "<br>Açıklama:" + model.Statement;
                //    mail.IsBodyHtml = true;
                //    mail.Priority = MailPriority.Normal;
                //    SmtpClient sc = new SmtpClient();
                //    sc.Port = 587;
                //    sc.Host = "smtp.gmail.com";
                //    sc.EnableSsl = true;
                //    sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777");
                //    sc.Send(mail);
                //}
                //catch (Exception)
                //{


                //}
                //#endregion
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Support(string sNameSurname, string sEmail, string sPhoneNumber, string sDescription)
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LemzhUUAAAAAMT5ZapBu4L5Au3QsrsfPIIUyLE6";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (captchaResponse.Success != "true")
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return Json(true);
                string opr = "";

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        opr = "ErrorCaptcha";
                        break;
                    case ("invalid-input-secret"):
                        opr = "ErrorCaptcha";
                        break;

                    case ("missing-input-response"):
                        opr = "ErrorCaptcha";
                        break;
                    case ("invalid-input-response"):
                        opr = "ErrorCaptcha";
                        break;

                    default:
                        opr = "ErrorCaptcha";
                        break;
                }
                return Json(new { opr = opr }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Destek Makina Türkiye");
                mail.To.Add("destek@makinaturkiye.com");
                mail.Subject = sEmail + " Mail Adresli Destek Talebi";
                string templatet = "<table><tr><td>Adı Soyadı</td><td>" + sNameSurname + "</td></tr><tr><td>Email</td><td>" + sEmail + "</td></tr><tr><td>Telefon No:</td><td>" + sPhoneNumber + "</td></tr><tr><td>Açıklama</td><td>" + sDescription + "</td></tr></table>";
                mail.Body = templatet;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
                return Json(true);
            }



        }

        [HttpPost]
        public ActionResult FastMembership(MTMembershipFormModel model)
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LemzhUUAAAAAMT5ZapBu4L5Au3QsrsfPIIUyLE6";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (captchaResponse.ErrorCodes != null)
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        model.ErrorMessage = "Güvenlik Kodunu Geçiniz";
                        break;
                    case ("invalid-input-secret"):
                        model.ErrorMessage = "Güvenlik Kodu Onaylanmadı.";
                        break;

                    case ("missing-input-response"):
                        model.ErrorMessage = "Lütfen Güvenlik Anahtarını Gerçekleştiriniz.";
                        break;
                    case ("invalid-input-response"):
                        model.ErrorMessage = "Güvenlik Kodu Onaylanmadı.";
                        break;

                    default:
                        model.ErrorMessage = "Bir hata oluştu.Lütfen Tekrar Deneyiniz";
                        break;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    string activCode = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 6);
                    var anyUser = _memberService.GetMemberByMemberEmail(model.MemberEmail);
                    if (anyUser == null)
                    {
                        InsertMember(model);
                        return View("MembershipApproval");
                    }
                    else
                    {
                        ViewData["EmailMessage"] = "Belirtmiş olduğunuz e-posta adresi kullanılmaktadır. ";
                    }
                }
            }
            return View(model);
        }

        public string CreateActiveCode()
        {
            string kod = Guid.NewGuid().ToString();
            string sonKod = "";
            foreach (char k in kod)
            {
                if (char.IsNumber(k)) sonKod = sonKod + k;

            }

            sonKod = sonKod.Substring(0, 6);

            var member = _memberService.GetMemberByActivationCode(sonKod);
            if (member != null)
                sonKod = CreateActiveCode();
            return sonKod;
        }

        public void SaveIndividualMembershipProduct(MembershipViewModel model)
        {
            string activCode = Guid.NewGuid().ToString("N").ToUpper();

            var mainParty = new MainParty
            {
                Active = false,
                MainPartyType = (byte)MainPartyType.Firm,
                MainPartyRecordDate = DateTime.Now,
                MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower() + " " + model.MembershipModel.MemberSurname.ToLower())
            };
            _memberService.InsertMainParty(mainParty);

            var member = new Member
            {
                MainPartyId = mainParty.MainPartyId,
                Gender = model.MembershipModel.Gender,
                MemberEmail = model.MembershipModel.MemberEmail,
                MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower()),
                MemberPassword = model.MembershipModel.MemberPassword,
                MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberSurname.ToLower()),
                MemberType = (byte)MemberType.Individual,
                Active = false,
                MemberTitleType = model.MembershipModel.MemberTitleType,
                FastMemberShipType = (byte)FastMembershipType.Normal,
                ActivationCode = activCode,
            };

            if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
            {
                var bithDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                member.BirthDate = bithDate;
            }
            else
                model.MembershipModel.BirthDate = null;

            string memberNo = "##";
            for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
            {
                memberNo = memberNo + "0";
            }
            memberNo = memberNo + mainParty.MainPartyId;
            member.MemberNo = memberNo;

            _memberService.InsertMember(member);

            string productNo = "#" + Session["urunno"];
            string memberNom = "##" + Session["uyeno"];


            var product = _productService.GetProductByProductNo(productNo);
            var memberN = _memberService.GetMemberByMemberNo(memberNom);

            //var productId = entities.Products.SingleOrDefault(c => c.ProductNo == productNo).ProductId;
            //var mainPartyId = entities.Members.SingleOrDefault(c => c.MemberNo == memberNom).MainPartyId;

            //var kullaniciemail = entities.Members.Where(c => c.MainPartyId == mainPartyId).SingleOrDefault();
            //string mailadresifirma = kullaniciemail.MemberEmail.ToString();
            //string productName = product.ProductName.ToString();
            //var productno = entities.Products.Where(c => c.ProductId == productId).SingleOrDefault().ProductNo;

            //var categoryModelName = _categoryService.GetCategoryByCategoryId(product.ModelId.Value).CategoryName;
            //var categoryBrandName = _categoryService.GetCategoryByCategoryId(product.BrandId.Value);

            string productnosub = product.ProductName + " " + product.Brand.CategoryName + " " + product.Model.CategoryName + " İlan no:" + product.ProductNo;

            //satıcıya burdan mail gidecek.
            //var groupname = entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName;
            //var categoryname = entities.Categories.Where(c => c.CategoryId == product.CategoryId).SingleOrDefault().CategoryName;
            string productUrl = AppSettings.SiteUrlWithoutLastSlash + UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);

            var message = new Message
            {
                Active = false,
                MessageContent = Session["mesaggesicerik"].ToString(),
                MessageSubject = productnosub,
                MessageDate = DateTime.Now,
                MessageRead = false,
                ProductId = product.ProductId,
            };
            _messageService.InsertMessage(message);

            int messageId = message.MessageId;

            var messageMainParty = new MessageMainParty
            {
                MainPartyId = member.MainPartyId,
                MessageId = messageId,
                InOutMainPartyId = memberN.MainPartyId,
                MessageType = (byte)MessageType.Outbox,
            };
            _messageService.InsertMessageMainParty(messageMainParty);

            var curMessageMainParty = new MessageMainParty
            {
                InOutMainPartyId = member.MainPartyId,
                MessageId = messageId,
                MainPartyId = memberN.MainPartyId,
                MessageType = (byte)MessageType.Inbox,
            };
            _messageService.InsertMessageMainParty(curMessageMainParty);

            var address = new Address
            {
                MainPartyId = mainParty.MainPartyId,
                Avenue = model.MembershipModel.Avenue,
                Street = model.MembershipModel.Street,
                DoorNo = model.MembershipModel.DoorNo,
                ApartmentNo = model.MembershipModel.ApartmentNo,
                AddressDefault = true,
            };


            if (model.MembershipModel.CountryId > 0 && model.MembershipModel.CountryId != 246)
            {
                address.Avenue = model.MembershipModel.AvenueOtherCountries;
            }

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
                    MainPartyId = mainParty.MainPartyId,
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
                    MainPartyId = mainParty.MainPartyId,
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
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode,
                    PhoneCulture = model.MembershipModel.InstitutionalGSMCulture,
                    PhoneNumber = model.MembershipModel.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.MembershipModel.GsmType
                };
                _phoneService.InsertPhone(phoneGsm);


            }

            if (model.MembershipModel.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalFaxNumber))
            {
                var phoneFax = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.MembershipModel.InstitutionalFaxAreaCode,
                    PhoneCulture = model.MembershipModel.InstitutionalFaxCulture,
                    PhoneNumber = model.MembershipModel.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                    GsmType = null
                };
                _phoneService.InsertPhone(phoneFax);
            }

            #region messageissendbilgilendirme
            try
            {
                MailMessage mail = new MailMessage();
                var mailTemp = _messagesMTService.GetMessagesMTByMessageMTName("mesajınızvar");
                mail.From = new MailAddress(mailTemp.Mail, mailTemp.MailSendFromName);
                mail.To.Add(memberN.MemberEmail);
                mail.Subject = productnosub;
                string templatet = mailTemp.MessagesMTPropertie;
                templatet = templatet.Replace("#kullaniciadi", member.MemberName + " " + member.MemberSurname).Replace("#urunadi", product.ProductName).Replace("#email#", memberN.MemberEmail).Replace("#link", productUrl).Replace("#ilanno", product.ProductNo);
                mail.Body = templatet;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
            }
            catch (Exception ex)
            {

                UserLog lg = new UserLog
                {
                    LogName = member.MemberEmail + " mail adresili üyeye mesajınız var maili gönderilemedi",
                    LogDescription = ex.ToString(),
                    LogShortDescription = ex.Message,
                    CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                    LogType = (byte)LogType.Messaging,
                    LogStatus = 0
                };
                _userLogService.InsertUserLog(lg);
            }

            #endregion


            SessionMembershipViewModel.Flush();
            ActivationCodeSend(model.MembershipModel.MemberEmail, activCode, model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname);

        }

        //bireysel
        public void SaveIndividualMembership(MembershipViewModel model)
        {
            string activCode = Guid.NewGuid().ToString("N").ToUpper();

            var mainParty = new MainParty
            {
                Active = false,
                MainPartyType = (byte)MainPartyType.Firm,
                MainPartyRecordDate = DateTime.Now,
                MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower() + " " + model.MembershipModel.MemberSurname.ToLower())
            };
            _memberService.InsertMainParty(mainParty);

            var member = new Member
            {
                MainPartyId = mainParty.MainPartyId,
                Gender = model.MembershipModel.Gender,
                MemberEmail = model.MembershipModel.MemberEmail,
                MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower()),
                MemberPassword = model.MembershipModel.MemberPassword,
                MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberSurname.ToLower()),
                MemberType = (byte)MemberType.Individual,
                Active = false,
                MemberTitleType = model.MembershipModel.MemberTitleType,
                ActivationCode = activCode,
            };

            if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
            {
                var bithDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                member.BirthDate = bithDate;
            }
            else
                model.MembershipModel.BirthDate = null;

            string memberNo = "##";
            for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
            {
                memberNo = memberNo + "0";
            }
            memberNo = memberNo + mainParty.MainPartyId;
            member.MemberNo = memberNo;

            _memberService.InsertMember(member);

            var address = new Address
            {
                MainPartyId = mainParty.MainPartyId,
                Avenue = model.MembershipModel.Avenue,
                Street = model.MembershipModel.Street,
                DoorNo = model.MembershipModel.DoorNo,
                ApartmentNo = model.MembershipModel.ApartmentNo,
                AddressDefault = true,
            };
            if (model.MembershipModel.CountryId > 0 && model.MembershipModel.CountryId != 246)
            {
                address.Avenue = model.MembershipModel.AvenueOtherCountries;
            }

            if (model.MembershipModel.CountryId > 1)
                address.CountryId = model.MembershipModel.CountryId;
            else
                address.CountryId = null;

            //if (model.MembershipModel.AddressTypeId > 0)
            //    address.AddressTypeId = model.MembershipModel.AddressTypeId;
            //else
            //    address.AddressTypeId = null;
            address.AddressTypeId = 15;

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
                    MainPartyId = mainParty.MainPartyId,
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
                    MainPartyId = mainParty.MainPartyId,
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
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.MembershipModel.InstitutionalGSMAreaCode,
                    PhoneCulture = model.MembershipModel.InstitutionalGSMCulture,
                    PhoneNumber = model.MembershipModel.InstitutionalGSMNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.MembershipModel.GsmType
                };
                _phoneService.InsertPhone(phoneGsm);


            }
            if (model.MembershipModel.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.MembershipModel.InstitutionalFaxNumber))
            {
                var phoneFax = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.MembershipModel.InstitutionalFaxAreaCode,
                    PhoneCulture = model.MembershipModel.InstitutionalFaxCulture,
                    PhoneNumber = model.MembershipModel.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                    GsmType = null
                };
                _phoneService.InsertPhone(phoneFax);
            }

            SessionMembershipViewModel.Flush();
            ActivationCodeSend(model.MembershipModel.MemberEmail, activCode, model.MembershipModel.MemberName + " " + model.MembershipModel.MemberSurname);
        }

        public ActionResult InstitutionalStep1()
        {
            //SeoPageType = (byte)PageType.General;
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/"));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/HizliUyelik/"));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/KurumsalUyelik/Adim-1/"));
            this.LoadNavigation(alMenu);
            var model = SessionMembershipViewModel.MembershipViewModel;
            return View(model);
        }

        [HttpPost]
        public ActionResult InstitutionalStep1(FormCollection coll, bool InsertButton)
        {
            if (string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberName))
            {
                Session["TimeOut"] = true;
                return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
            }
            if (InsertButton)
            {
                if (!string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo))
                {
                    FileHelpers.Delete(AppSettings.StoreLogoFolder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    FileHelpers.Delete(AppSettings.StoreLogoThumb100x100Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    FileHelpers.Delete(AppSettings.StoreLogoThumb300x200Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    //FileHelpers.Delete(AppSettings.StoreLogoThumb170x90Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    //FileHelpers.Delete(AppSettings.StoreLogoThumb200x100Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    //FileHelpers.Delete(AppSettings.StoreLogoThumb55x40Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                    //FileHelpers.Delete(AppSettings.StoreLogoThumb75x75Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                }
                string fileName = String.Empty;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        //var thumns = new Dictionary<string, string>();
                        //thumns.Add(AppSettings.StoreLogoThumb110x110Folder, "110x110");
                        //thumns.Add(AppSettings.StoreLogoThumb150x90Folder, "150x90");
                        //thumns.Add(AppSettings.StoreLogoThumb170x90Folder, "170x90");
                        //thumns.Add(AppSettings.StoreLogoThumb200x100Folder, "200x100");
                        //thumns.Add(AppSettings.StoreLogoThumb55x40Folder, "55x40");
                        //thumns.Add(AppSettings.StoreLogoThumb75x75Folder, "75x75");
                        //fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);
                        var thumns = new Dictionary<string, string>();
                        thumns.Add(AppSettings.StoreLogoThumb100x100Folder, "100x100");
                        thumns.Add(AppSettings.StoreLogoThumb300x200Folder, "300x200");
                        fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);
                    }
                }
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo = fileName;
                //SeoPageType = (byte)PageType.General;
                var model = SessionMembershipViewModel.MembershipViewModel;
                return View(model);
            }
            else
            {
                string fileName = String.Empty;

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo))
                        {
                            FileHelpers.Delete(AppSettings.StoreLogoFolder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                        }

                        //fileName = FileHelpers.ImageThumbnail(AppSettings.StoreLogoFolder, file, 100, FileHelpers.ThumbnailType.Width);

                        var thumns = new Dictionary<string, string>();
                        thumns.Add(AppSettings.StoreLogoThumb100x100Folder, "100x100");
                        thumns.Add(AppSettings.StoreLogoThumb300x200Folder, "300x200");
                        fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);

                        SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo = fileName;
                    }
                }
            }
            return RedirectToAction("KurumsalUyelik/Adim-2", "Uyelik");
        }

        public ActionResult InstitutionalStep2()
        {
            //SeoPageType = (byte)PageType.General;
            var model = SessionMembershipViewModel.MembershipViewModel;
            return View(model);
        }

        [HttpPost]
        public ActionResult InstitutionalStep2(MembershipViewModel model, byte? GsmType)
        {
            if (string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberName))
            {
                Session["TimeOut"] = true;
                return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
            }
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.AddressTypeId = model.MembershipModel.AddressTypeId;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.CountryId = model.MembershipModel.CountryId;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.CityId = model.MembershipModel.CityId;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.LocalityId = model.MembershipModel.LocalityId;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.TownId = model.MembershipModel.TownId;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.Avenue = model.MembershipModel.Avenue;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.Street = model.MembershipModel.Street;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.ApartmentNo = model.MembershipModel.ApartmentNo;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.DoorNo = model.MembershipModel.DoorNo;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneNumber = model.MembershipModel.InstitutionalPhoneNumber;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneCulture = model.MembershipModel.InstitutionalPhoneCulture;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneAreaCode = model.MembershipModel.InstitutionalPhoneAreaCode;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneNumber2 = model.MembershipModel.InstitutionalPhoneNumber2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneCulture2 = model.MembershipModel.InstitutionalPhoneCulture2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalPhoneAreaCode2 = model.MembershipModel.InstitutionalPhoneAreaCode2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMNumber = model.MembershipModel.InstitutionalGSMNumber;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMCulture = model.MembershipModel.InstitutionalGSMCulture;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMAreaCode = model.MembershipModel.InstitutionalGSMAreaCode;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMNumber2 = model.MembershipModel.InstitutionalGSMNumber2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMCulture2 = model.MembershipModel.InstitutionalGSMCulture2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalGSMAreaCode2 = model.MembershipModel.InstitutionalGSMAreaCode2;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.GsmType = GsmType != null ? GsmType.Value : (byte)0;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalFaxAreaCode = model.MembershipModel.InstitutionalFaxAreaCode;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalFaxCulture = model.MembershipModel.InstitutionalFaxCulture;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.InstitutionalFaxNumber = model.MembershipModel.InstitutionalFaxNumber;
            return RedirectToAction("KurumsalUyelik/Adim-3", "Uyelik");
        }

        public ActionResult InstitutionalStep3()
        {
            //SeoPageType = (byte)PageType.General;

            SessionMembershipViewModel.MembershipViewModel.ActivityItems = _activityTypeService.GetAllActivityTypes();

            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/HizliUyelik/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/KurumsalUyelik/Adim-2/", Navigation.TargetType._self));
            this.LoadNavigation(alMenu);

            var model = SessionMembershipViewModel.MembershipViewModel;
            if (string.IsNullOrWhiteSpace(model.MembershipModel.StoreWeb))
            {
                model.MembershipModel.StoreWeb = "http://";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult InstitutionalStep3(MembershipViewModel model)
        {
            if (string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberName))
            {
                Session["TimeOut"] = true;
                return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
            }
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreName = model.MembershipModel.StoreName;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreWeb = model.MembershipModel.StoreWeb;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.ActivityName = model.MembershipModel.ActivityName;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital = model.MembershipModel.StoreCapital;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEstablishmentDate = model.MembershipModel.StoreEstablishmentDate;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount = model.MembershipModel.StoreEmployeesCount;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement = model.MembershipModel.StoreEndorsement;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreAbout = model.MembershipModel.StoreAbout;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentEmail = model.MembershipModel.PurchasingDepartmentEmail;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentName = model.MembershipModel.PurchasingDepartmentName;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreType = model.MembershipModel.StoreType;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.ReceiveEmail = false;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxNumber = model.MembershipModel.TaxNumber;
            SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxOffice = model.MembershipModel.TaxOffice;


            return RedirectToAction("KurumsalUyelik/Adim-5", "Uyelik");
        }

        //public ActionResult GetCategories(int categoryID, bool isActive)
        //{
        //    GetCategoriesModel getCategoriesModel = new GetCategoriesModel();
        //    getCategoriesModel.ParentCategoryID = categoryID;
        //    getCategoriesModel.ProductGroupList = entities.Categories.Where(c => c.CategoryParentId == categoryID && c.CategoryType == (byte)CategoryType.ProductGroup && c.ProductCount.HasValue).OrderBy(c => c.CategoryOrder).ThenBy(e => e.CategoryName);
        //    getCategoriesModel.MemberRelatedCategory = entities.RelMainPartyCategories.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId);
        //    getCategoriesModel.IsActive = isActive;
        //    return PartialView(getCategoriesModel);
        //}

        public ActionResult InstitutionalStep4()
        {
            return RedirectToAction("KurumsalUyelik/Adim-3", "Uyelik");

            //var entities = new MakinaTurkiyeEntities();
            //SeoPageType = (byte)PageType.General;
            //IList<Navigation> alMenu = new List<Navigation>();
            //alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            //alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/HizliUyelik/", Navigation.TargetType._self));
            //alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/KurumsalUyelik/Adim-3/", Navigation.TargetType._self));
            //this.LoadNavigation(alMenu);
            //var model = SessionMembershipViewModel.MembershipViewModel;
            //model.CategoryItems = entities.Categories.Where(c => c.CategoryParentId == null && c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryName);
            //return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult InstitutionalStep4(MembershipViewModel model, string[] StoreActivityCategory)
        {
            if (string.IsNullOrWhiteSpace(SessionMembershipViewModel.MembershipViewModel.MembershipModel.MemberName))
            {
                Session["TimeOut"] = true;
                return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
            }

            SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreActivityCategory = StoreActivityCategory;

            return RedirectToAction("KurumsalUyelik/Adim-5", "Uyelik");
        }

        public ActionResult InstitutionalStep5()
        {
            //SeoPageType = (byte)PageType.General;
            IList<Constant> dataConstant = _constantService.GetAllConstants();
            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement > 0)
            {
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsementName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement).ConstantName;
            }

            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital > 0)
            {
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapitalName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital).ConstantName;
            }

            if (SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount > 0)
            {
                SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCountName = dataConstant.FirstOrDefault(c => c.ConstantId == SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount).ConstantName;
            }
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/HizliUyelik/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Ücretsiz Üyelik", "/Uyelik/KurumsalUyelik/Adim-7/", Navigation.TargetType._self));
            this.LoadNavigation(alMenu);

            var model = SessionMembershipViewModel.MembershipViewModel;
            return View(model);
        }

        [HttpPost]
        public ActionResult InstitutionalStep5(FormCollection coll)
        {
            try
            {
                var model = SessionMembershipViewModel.MembershipViewModel;
                if (string.IsNullOrWhiteSpace(model.MembershipModel.MemberName))
                {
                    Session["TimeOut"] = true;
                    return RedirectToAction("HizliUyelik/UyelikTipi-20", "Uyelik");
                }
                string activCode = Guid.NewGuid().ToString("N").ToUpper();


                var memberMainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)MainPartyType.Member,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MembershipModel.MemberName.ToLower() + " " + model.MembershipModel.MemberSurname.ToLower())
                };
                _memberService.InsertMainParty(memberMainParty);

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
                if (model.MembershipModel.Year > 0 && model.MembershipModel.Month > 0 && model.MembershipModel.Day > 0)
                {
                    var bithDate = new DateTime(model.MembershipModel.Year, model.MembershipModel.Month, model.MembershipModel.Day);
                    member.BirthDate = bithDate;
                }
                else
                {
                    member.BirthDate = null;
                }

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
                model = null;
                SessionMembershipViewModel.Flush();
            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ex);
                Response.Write(ex.Message);
            }
            return RedirectToRoute("MembershipApprovalRoute");
        }

        public ActionResult ActivationCode()
        {
            return View();
        }

        public JsonResult Cities(int? id)
        {
            IList<City> cityItems = _addressService.GetCitiesByCountryId(id.Value);
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });
            return Json(new SelectList(cityItems, "CityId", "CityName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Localities(int? id)
        {
            IList<Locality> localityItems = _addressService.GetLocalitiesByCityId(id.Value);

            localityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(localityItems, "LocalityId", "LocalityName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult District(int? id)
        {
            IList<District> districtItems = _addressService.GetDistrictsByLocalityId(id.Value);

            districtItems.Insert(0, new District { DistrictId = 0, DistrictName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(districtItems, "DistrictId", "DistrictName"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Towns(int? id)
        {
            IList<Town> townItems = _addressService.GetTownsByLocalityId(id.Value);

            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(townItems, "TownId", "TownName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AreaCode(string CityId)
        {
            int id = int.Parse(CityId);

            City city = _addressService.GetCityByCityId(id);

            if (city != null)
            {
                return Content(city.AreaCode);
            }
            else
                return Content("");
        }

        public ActionResult CultureCode(string CountryId)
        {
            int id = int.Parse(CountryId);
            Country country = _addressService.GetCountryByCountryId(id);

            if (country != null)
            {
                return Content(country.CultureCode);
            }
            else
                return Content("");
        }

        public ActionResult RememberPassword()
        {
            //SeoPageType = (byte)PageType.General;
            return View();
        }

        [HttpPost]
        public JsonResult CheckEmailForgetPassword(string email)
        {
            var anyMember = _memberService.GetMemberByMemberEmail(email);
            if (anyMember != null)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RememberPassword(string Email)
        {
            var result = true;
            var member = _memberService.GetMemberByMemberEmail(Email);

            if (member == null)
            {
                result = false;
            }

            if (result)
            {


                var value = Guid.NewGuid().ToString("N");
                member.MemberPassword = value.Substring(0, 8);
                _memberService.UpdateMember(member);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye");
                mail.To.Add(Email);
                mail.Subject = "Şifre Hatırlatma";
                string template = "Şifreniz: " + member.MemberPassword + " ";
                mail.Body = template;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
            }
            return Json(result);
        }

        public ActionResult Activation(string uyelikSifre)
        {
            if (uyelikSifre == null)
            {
                uyelikSifre = "";
            }
            RelCategoryModel model = new RelCategoryModel();

            if (uyelikSifre == "0")
                ViewData["uyelikSifre"] = 0;

            string actCode = Request.Path.Replace("/Uyelik/Aktivasyon", "");
            actCode = actCode.Replace("/", "");


            model.ActivationCode = actCode;
            if (actCode != "")
            {
                var member = _memberService.GetMemberByActivationCode(actCode);
                if (member != null)
                {
                    member.Active = true;
                    _memberService.UpdateMember(member);

                    #region kullanıcı

                    MailMessage mailh = new MailMessage();
                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("Hizliuyelik");
                    mailh.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName);
                    mailh.To.Add(member.MemberEmail);
                    mailh.Subject = mailTemplate.MessagesMTTitle;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 4).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string template = mailTemplate.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#kullanicieposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                    mailh.Body = template;
                    mailh.IsBodyHtml = true;
                    mailh.Priority = MailPriority.Normal;
                    this.SendMail(mailh);

                    #endregion

                    //_authenticationService.SignIn(member, true);

                    //EnterpriseFormsAuthentication.CreateFormsAuthenticationTicket(member.MemberEmail, member.MemberType.Value.ToString("G"));
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");


                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    //if (Session["ProductDetailModel"] != null)
                    //{
                    //    var model = Session["ProductDetailModel"] as ProductDetailViewModel;
                    //    return RedirectToAction("ProductSales", "Product", model);
                    //}

                    return Redirect("/Account/Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        public ActionResult CreatePassword(string id)
        {

            ViewData["activationCode"] = id;

            var member = _memberService.GetMemberByActivationCode(id);
            //  _authenticationService.SignIn(member, true);

            //EnterpriseFormsAuthentication.CreateFormsAuthenticationTicket(member.MemberEmail, member.MemberType.Value.ToString("G"));
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(identity);

            return View();
        }

        [HttpPost]
        public ActionResult CreatePassword(string password, string passwordAgain, string ActivationCode)
        {
            var member = _memberService.GetMemberByActivationCode(ActivationCode);
            if (member != null)
            {
                if (passwordAgain == password)
                {
                    member.MemberPassword = password;
                    member.Active = true;

                    _memberService.UpdateMember(member);

                    //hızlı üyelikte kullanıcı mainpartyid eger mesaj kutusunda mevcutsa o mesajı gönder.
                    //var usermessagehave = entities1.MessageEntries.Where(c => c.MainPartyId == member.MainPartyId).SingleOrDefault();
                    //hızlı üyelik esnasında kullanıcıya ve bilgi@makinaturkiye.com'a mail gidecek.
                    //Mailinizi gönderiyoruz.

                    var log = new UserLog
                    {
                        LogName = "ŞOM.O",
                        LogShortDescription = "Şifre oluşturdu",
                        LogDescription = member.MemberNo.ToString(),
                        LogType = 1
                    };
                    _userLogService.InsertUserLog(log);

                    #region kullanıcı


                    MailMessage mailh = new MailMessage();

                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("Hizliuyelik");

                    mailh.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName);
                    mailh.To.Add(member.MemberEmail);
                    mailh.Subject = mailTemplate.MessagesMTTitle;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 4).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string template = mailTemplate.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#kullanicieposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                    mailh.Body = template;
                    mailh.IsBodyHtml = true;
                    mailh.Priority = MailPriority.Normal;
                    this.SendMail(mailh);

                    #endregion

                    #region bilgimakina

                    MailMessage mailb = new MailMessage();

                    MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Hızlı Üyelik " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                    bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Hızlı Üyelik");
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    this.SendMail(mailb);
                    #endregion

                    #region bilgimakina

                    MailMessage mailb1 = new MailMessage();

                    MessagesMT mailTempInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb1.From = new MailAddress(mailTempInf.Mail, mailTempInf.MailSendFromName);
                    mailb1.To.Add("bilgi@makinaturkiye.com");
                    mailb1.Subject = "Bireysel Üyelik " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin1 = mailTempInf.MessagesMTPropertie;
                    bilgimakinaicin1 = bilgimakinaicin1.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Bireysel Üyelik");
                    mailb1.Body = bilgimakinaicin1;
                    mailb1.IsBodyHtml = true;
                    mailb1.Priority = MailPriority.Normal;
                    this.SendMail(mailb1);

                    #endregion
                }

                //_authenticationService.SignIn(member, true);

                //EnterpriseFormsAuthentication.CreateFormsAuthenticationTicket(member.MemberEmail, member.MemberType.Value.ToString("G"));
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");



                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                if (Session["ProductDetailModel"] != null)
                {
                    var model = Session["ProductDetailModel"] as ProductDetailViewModel;
                    return RedirectToAction("ProductSales", "Product", model);
                }
                return Redirect("/Account/MemberType/Individual");
            }
            return RedirectToAction("sifreolustur", "MemberShip");
        }

        [HttpPost]
        public ActionResult Activation(string ActivationCode, string[] StoreActivityCategory, bool ReceiveEmail, string password, string passwordAgain)
        {
            ActivationCode = ActivationCode.ToUpper();
            var member = _memberService.GetMemberByActivationCode(ActivationCode);
            if (member != null)
            {
                //if (StoreActivityCategory != null)
                //{
                //    for (int i = 0; i < StoreActivityCategory.Length; i++)
                //    {
                //        if (StoreActivityCategory.GetValue(i).ToString() != "false")
                //        {
                //            var relMainPartyCategory = new RelMainPartyCategory
                //            {
                //                MainPartyId = member.MainPartyId,
                //                CategoryId = StoreActivityCategory.GetValue(i).ToInt32()
                //            };
                //            entities.RelMainPartyCategories.AddObject(relMainPartyCategory);
                //            entities.SaveChanges();
                //        }
                //    }
                //}

                member.Active = true;
                member.ReceiveEmail = ReceiveEmail;
                _memberService.UpdateMember(member);

                if (member.MemberType == (byte)MemberType.Enterprise)
                {
                    #region kullanici
                    MailMessage mail = new MailMessage();

                    MessagesMT mailTemp = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");

                    mail.From = new MailAddress(mailTemp.Mail, mailTemp.MailSendFromName);
                    mail.To.Add(member.MemberEmail);
                    mail.Subject = mailTemp.MessagesMTTitle;
                    string template = mailTemp.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                    mail.Body = template;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    this.SendMail(mail);

                    #endregion

                    #region bilgimakina

                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    MailMessage mailf = new MailMessage();
                    mailf.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                    mailf.To.Add("bilgi@makinaturkiye.com");                                                              //Mailin kime gideceğini belirtiyoruz
                    mailf.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailTemplate.MessagesMTPropertie;

                    bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Firma Üyeliği");
                    mailf.Body = bilgimakinaicin;                                                            //Mailin içeriği
                    mailf.IsBodyHtml = true;
                    mailf.Priority = MailPriority.Normal;
                    this.SendMail(mailf);
                    #endregion
                }
                else if (member.MemberType == (byte)MemberType.FastIndividual)
                {
                    //hızlı üyelikte kullanıcı mainpartyid eger mesaj kutusunda mevcutsa o mesajı gönder.
                    //var usermessagehave = entities.MessageEntries.Where(c => c.MainPartyId == member.MainPartyId).SingleOrDefault();
                    //if (usermessagehave != null)
                    //{

                    //    string memberNamee = member.MemberName + " " + member.MemberSurname;
                    //    MailMessage mail = new MailMessage();
                    //    mail.From = new MailAddress("makinaturkiye@makinaturkiye.com", "Makina Turkiye");
                    //    mail.To.Add("bilgi@makinaturkiye.com");
                    //    mail.Subject = "Bir Soru Sor :" + memberNamee + " " + member.MemberEmail;
                    //    string templatee = usermessagehave.MessageContent.ToString();
                    //    mail.Body = templatee;
                    //    mail.IsBodyHtml = true;
                    //    mail.Priority = MailPriority.Normal;
                    //    SmtpClient sc = new SmtpClient();
                    //    sc.Port = 587;
                    //    sc.Host = "smtp.gmail.com";
                    //    sc.EnableSsl = true;
                    //    sc.Credentials = new NetworkCredential("makinaturkiye@makinaturkiye.com", "haciosman7777");
                    //    sc.Send(mail);


                    //}

                    //hızlı üyelik esnasında kullanıcıya ve bilgi@makinaturkiye.com'a mail gidecek.
                    //Mailinizi gönderiyoruz.

                    #region kullanıcı



                    MailMessage mailh = new MailMessage();

                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("Hizliuyelik");

                    mailh.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName);
                    mailh.To.Add(member.MemberEmail);
                    mailh.Subject = mailTemplate.MessagesMTTitle;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 4).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string template = mailTemplate.MessagesMTPropertie;
                    template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#kullanicieposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                    mailh.Body = template;
                    mailh.IsBodyHtml = true;
                    mailh.Priority = MailPriority.Normal;
                    this.SendMail(mailh);
                    #endregion
                    #region bilgimakina
                    MailMessage mailb = new MailMessage();

                    MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                    mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Hızlı Üyelik " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                    bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Hızlı Üyelik");
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    this.SendMail(mailb);
                    #endregion
                }
                else if (member.MemberType == (byte)MemberType.Individual)
                {
                    MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("bireyseluyelik");

                    try
                    {
                        #region kullanici


                        MailMessage maill = new MailMessage();
                        maill.From = new MailAddress(mailTemplate.Mail, mailTemplate.MailSendFromName);
                        maill.To.Add(member.MemberEmail);
                        maill.Subject = mailTemplate.MessagesMTTitle;
                        //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 3).SingleOrDefault();
                        //templatet = messagesmttemplate.MessagesMTPropertie;
                        string template = mailTemplate.MessagesMTPropertie;
                        template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#kullanicieposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword);
                        maill.Body = template;
                        maill.IsBodyHtml = true;
                        maill.Priority = MailPriority.Normal;
                        this.SendMail(maill);
                        #endregion
                    }
                    catch (Exception ex)
                    {

                        UserLog lg = new UserLog
                        {
                            LogName = member.MemberEmail + " mail adresili üyeye aktivasyon maili gönderilemedi",
                            LogDescription = ex.ToString(),
                            LogShortDescription = ex.Message,
                            CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                            LogType = (byte)LogType.Messaging,
                            LogStatus = 0
                        };
                        _userLogService.InsertUserLog(lg);
                    }
                    #region bilgimakina

                    MailMessage mailb = new MailMessage();

                    MessagesMT mailTempInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTempInf.Mail, mailTempInf.MailSendFromName);
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Bireysel Üyelik " + member.MemberName + " " + member.MemberSurname;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailTempInf.MessagesMTPropertie;
                    bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Bireysel Üyelik");
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    this.SendMail(mailb);
                    #endregion
                }

                // _authenticationService.SignIn(member, true);

                //EnterpriseFormsAuthentication.CreateFormsAuthenticationTicket(member.MemberEmail, member.MemberType.Value.ToString("G"));
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");



                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                if (Session["ProductDetailModel"] != null)
                {
                    var model = Session["ProductDetailModel"] as ProductDetailViewModel;
                    return RedirectToAction("ProductSales", "Product", model);
                }

                return RedirectToAction("Index", "Account");
            }

            return RedirectToAction("Activation", "Membership");
        }

        public ActionResult MembershipError()
        {
            //SeoPageType = (byte)PageType.General;

            return View();
        }

        public ActionResult ZipCode(int TownId)
        {
            string zipCode = "";
            Town town = _addressService.GetTownByTownId(TownId);
            if (town != null)
            {
                var district = _addressService.GetDistrictByDistrictId(town.DistrictId.Value);
                if (district != null)
                {
                    zipCode = district.ZipCode;
                }
            }
            return Json(zipCode, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MembershipApproval()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MembershipApproval(string[] MemberRelatedSectorItems, string memberEmail)
        {
            //if (Session["MembershipMainPartyId"] != null)
            //{
            //  int MainPartyId = Session["MembershipMainPartyId"].ToInt32();

            //  using (var trans = new TransactionUI())
            //  {
            //    if (MemberRelatedSectorItems != null)
            //    {
            //      for (int i = 0; i < MemberRelatedSectorItems.Length; i++)
            //      {
            //        if (MemberRelatedSectorItems.GetValue(i).ToString() != "false")
            //        {
            //          var curRelMainPartyCategory = new Classes.RelMainPartyCategory
            //          {
            //            MainPartyId = MainPartyId,
            //            CategoryId = MemberRelatedSectorItems.GetValue(i).ToInt32()
            //          };
            //          curRelMainPartyCategory.Save(trans);
            //        }
            //      }
            //    }
            //  }
            //}

            if (memberEmail != null)
            {
                var member = _memberService.GetMemberByMemberEmail(memberEmail);
                if (member != null) member.Active = true;
            }

            return Redirect("/Account/Home");
        }

        public ActionResult SignUp()
        {
            Response.RedirectPermanent(Url.RouteUrl(new
            {
                controller = "Membership",
                action = "LoginError"
            }));

            return null;

            //if (this.RouteData.Values["MemberType"] != null)
            //{
            //    MemberType type = (MemberType)this.RouteData.Values["MemberType"].ToByte();
            //    switch (type)
            //    {
            //        case MemberType.FastIndividual:
            //            ViewData["MembershipType"] = (byte)MemberType.FastIndividual;
            //            break;
            //        case MemberType.Individual:
            //            ViewData["MembershipType"] = (byte)MemberType.Individual;
            //            break;
            //        case MemberType.Enterprise:
            //            ViewData["MembershipType"] = (byte)MemberType.Enterprise;
            //            break;
            //        default:
            //            ViewData["MembershipType"] = 0;
            //            break;
            //    }
            //}
            //else
            //{
            //    ViewData["MembershipType"] = (byte)MemberType.FastIndividual;
            //}

            //SeoPageType = (byte)PageType.General;
            //SessionMembershipViewModel.Flush();

            //IList<Navigation> alMenu = new List<Navigation>();
            //alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            //alMenu.Add(new Navigation("Ücretsiz Hızlı Üyelik", "/Uyelik/HizliUyelik/", Navigation.TargetType._self));
            //this.LoadNavigation(alMenu);

            //var model = new MembershipViewModel();
            //return View(model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="Email"></param>"makinaturkiye@makinaturkiye.com", "haciosman666"
        /// <param name="userName"></param>
        /// <param name="recerateLink"></param>
        /// <param name="isInfo">isInfo=>infoMail:newLinkMail</param>
        public void ReCreateLinkSend(string Email, string[] userNameSurname, bool isInfo, string code)
        {

            try
            {


                string actLink = AppSettings.SiteUrl + "Uyelik/SifremiUnuttum/" + code;
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

        public void ActivationCodeSend(string Email, string activationCode, string nameSurname)
        {

            try
            {

                MailMessage mail = new MailMessage();
                MessagesMT mailMessage = _messagesMTService.GetMessagesMTByMessageMTName("Aktivasyon");

                mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName);
                mail.To.Add(Email);
                mail.Subject = mailMessage.MessagesMTTitle;
                string actLink = AppSettings.SiteUrl + "Uyelik/Aktivasyon/" + activationCode;
                string template = mailMessage.MessagesMTPropertie;
                template = template.Replace("#OnayKodu#", activationCode).Replace("#OnayLink#", actLink).Replace("#uyeadisoyadi#", nameSurname);
                mail.Body = template;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                this.SendMail(mail);
            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ex);
            }

        }

        //[DecryptSolution(Constants.SecretKey)]
        //public ActionResult CaptchaImage(string solution)
        //{
        //    return new CaptchaImageResult(solution);
        //}

        //[HttpGet]
        //public string EncryptedCaptchaSolution()
        //{
        //    var textGenerator = new XCaptcha.RandomTextGenerator();
        //    return textGenerator.CreateRandomUrlEncodedEncrypedText(Constants.SecretKey, 4); ;
        //}

        //public ActionResult LogOn()
        //{
        //    if (Request.Cookies["MainPartyId"] != null)
        //    {
        //        int mainPartyId = Convert.ToInt32(Request.Cookies["MainPartyId"].Value);
        //        var entities = new MakinaTurkiyeEntities();
        //        var member = entities.Members.SingleOrDefault(c => c.MainPartyId == mainPartyId);
        //        if (member != null && member.Active.Value)
        //        {
        //            AuthenticationUser.Membership = member;

        //            if (SessionProductSalesModel.ProductSalesViewModel.ProductDetailInfo != null)
        //            {
        //                if (AuthenticationUser.Membership.MemberType == 5)
        //                {
        //                    return View("/Areas/Account/Views/MemberType/Individual");
        //                }
        //                else
        //                {
        //                    return RedirectToAction("ProductSales", "Product");
        //                }
        //            }

        //            return Redirect(AppSettings.SiteUrl);
        //        }
        //        else if (member != null && member.Active.Value == false)
        //        {
        //            TempData["kullaniciaktifdegil"] = member.MainPartyId;
        //            return RedirectToAction("HataliGiris", "Uyelik");
        //        }
        //        else
        //        {
        //            return RedirectToAction("HataliGiris", "Uyelik");
        //        }
        //    }
        //    return View();
        //}
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return "/Account/Home";
            }
            return returnUrl;
        }
        public ActionResult LogOn(string returnUrl)
        {
            int memberMainPartyId = AuthenticationUser.Membership.MainPartyId;
            if (memberMainPartyId != 0)
            {
                return Redirect("/Account/Home");
            }
            MTLoginViewModel model = new MTLoginViewModel();
            model.LoginTabType = (byte)LoginTabType.Login;
            LoginModel loginModel = new LoginModel();
            loginModel.ReturnUrl = returnUrl;

            if (Request.Cookies["MainPartyId"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Cookies["MainPartyId"].Value))
                {
                    int mainPartyId = Convert.ToInt32(Request.Cookies["MainPartyId"].Value);

                    var member = _memberService.GetMemberByMainPartyId(mainPartyId);
                    if (member != null)
                    {
                        ViewData["mail"] = member.MemberEmail;
                        ViewData["password"] = member.MemberPassword;
                        loginModel.Remember = true;
                    }
                }
            }
            else if (Request.Cookies["mtMail"] != null)
            {
                //
                ViewData["mail"] = Request.Cookies["mtMail"].Values.GetValues("mtMail").FirstOrDefault();
            }
            //SeoPageType = (byte)PageType.Uyelik;
            model.LoginModel = loginModel;

            return View(model);

        }

        [HttpPost]
        public JsonResult LogOn(LoginModel model)
        {

            ResponseModel<LoginModel> responseModel = new ResponseModel<LoginModel>();
            responseModel.Result = model;
            var member = _memberService.GetMemberByMemberEmail(model.Email);
            if (member != null)
            {
                if (member.MemberPassword == model.Password)
                {
                    var sendErrorMessage = _messageService.GetSendMessageErrorsBySenderId(member.MainPartyId);
                    if (member.FastMemberShipType == (byte)FastMembershipType.Phone && sendErrorMessage == null)
                    {
                        member.MemberPassword = null;
                        member.Active = false;
                    }
                }

            }

            if (member != null && member.Active.Value)
            {
                _authenticationService.SignIn(member, model.Remember);
                if (member.MemberPassword == model.Password)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");
                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);
                    var user = this.User;
                    var redirect = GetRedirectUrl(model.ReturnUrl);
                    // AuthenticationUser.Membership = member;

                    if (member.MemberType == (byte)MemberType.Enterprise)
                    {
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                        LoginLog loginLog = new LoginLog();
                        loginLog.StoreMainPartyId = Convert.ToInt32(memberStore.StoreMainPartyId);
                        loginLog.LoginDate = DateTime.Now;
                        loginLog.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                        _loginLogService.InsertLoginLog(loginLog);

                    }



                    string linkRef = string.Empty;

                    responseModel.IsSuccess = true;
                    responseModel.Message = "Giriş işlemi başarılı";


                    if (TempData["RedirectUrl"] != null)
                    {

                        responseModel.Result.ReturnUrl = TempData["RedirectUrl"].ToString();
                    }
                    else
                    {
                        responseModel.Result.ReturnUrl = redirect;
                    }

                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Email adresi veya parolanız yanlıştır.";
                }
            }
            else if (member != null && member.Active.Value == false)
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Kullanıcı Aktif Değil";
                //TempData["kullaniciaktifdegil"] = member.MainPartyId;
                //return RedirectToAction("HataliGiris", "Uyelik");
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Email adresi veya parolanız yanlıştır.";

            }
            return Json(responseModel, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LogonAuto(string validateId, string returnUrl, string OrderId)
        {

            LinkHelper linkHelper = new LinkHelper();
            int userId = int.Parse(linkHelper.Decypt(validateId));
            var member = _memberService.GetMemberByMainPartyId(userId);

            if (member != null && member.Active.Value)
            {
                //  _authenticationService.SignIn(member, true);

                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");


                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                if (member.MemberType == (byte)MemberType.Enterprise && returnUrl != "/account/advert/advert")
                {
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                    LoginLog loginLog = new LoginLog();
                    loginLog.StoreMainPartyId = Convert.ToInt32(memberStore.StoreMainPartyId);
                    loginLog.LoginDate = DateTime.Now;
                    loginLog.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                    _loginLogService.InsertLoginLog(loginLog);

                }

                HttpCookie MainPartyId = new HttpCookie("mtCookie");
                Response.Cookies["MainPartyId"].Value = member.MainPartyId.ToString();
                Response.Cookies["MainPartyId"].Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(MainPartyId);
                string linkRef = string.Empty;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    if (!string.IsNullOrEmpty(OrderId))
                        returnUrl = returnUrl + $"&OrderId={OrderId}";
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/account/advert/notlimitedaccess");
                }

            }
            else
            {
                return RedirectToAction("HataliGiris", "Uyelik");
            }
        }
        [HttpPost]
        public ActionResult RegisterOnLogin(MTMembershipFormModel model)
        {
            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair
            const string secret = "6LemzhUUAAAAAMT5ZapBu4L5Au3QsrsfPIIUyLE6";

            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (captchaResponse.ErrorCodes != null)
            {
                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        model.ErrorMessage = "Güvenlik Kodunu Geçiniz";
                        break;
                    case ("invalid-input-secret"):
                        model.ErrorMessage = "Güvenlik Kodu Onaylanmadı.";
                        break;

                    case ("missing-input-response"):
                        model.ErrorMessage = "Lütfen Güvenlik Anahtarını Gerçekleştiriniz.";
                        break;
                    case ("invalid-input-response"):
                        model.ErrorMessage = "Güvenlik Kodu Onaylanmadı.";
                        break;

                    default:
                        model.ErrorMessage = "Bir hata oluştu.Lütfen Tekrar Deneyiniz";
                        break;
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    InsertMember(model);
                    return View("MembershipApproval");
                }
            }
            //model.companyMembershipDemand = new CompanyDemandMembership();
            MTLoginViewModel viewModel = new MTLoginViewModel();
            viewModel.MembershipViewModel = model;
            viewModel.LoginTabType = (byte)LoginTabType.Register;
            return View("LogOn", viewModel);
        }
        private void InsertMember(MTMembershipFormModel model)
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
            else
            {
                model.ErrorMessage = "Belirtmiş olduğunuz e-posta adresi kullanılmaktadır.";
            }


        }

        public ActionResult Logout()
        {
            //AuthenticationUser.Flush();
            Session.Abandon();
            Session.Clear();
            //if (Request.Cookies["mtCookie"] != null)
            //{
            //    HttpCookie aCookie;
            //    string cookieName;
            //    cookieName = Request.Cookies["mtCookie"].Name;
            //    aCookie = new HttpCookie(cookieName);
            //    aCookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(aCookie);
            //}

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("LoginCookie");

            _authenticationService.SignOut();

            return RedirectToAction("index", "home");
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginError()
        {
            return View();
        }

        public JsonResult Activationsend(int id)
        {
            var ourmember = _memberService.GetMemberByMainPartyId(id);
            if (ourmember.ActivationCode != null)
            {
                ActivationCodeSend(ourmember.MemberEmail, ourmember.ActivationCode, ourmember.MemberName + " " + ourmember.MemberSurname);
            }
            return Json(true);
        }

        public ActionResult BulletinRegister(string email)
        {

            BulletinMemberCreateModel model = new BulletinMemberCreateModel();
            var sectorCategories = _categoryService.GetMainCategories();
            foreach (var item in sectorCategories)
            {
                model.SectorCategories.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
            }
            model.SectorCategories.Add(new SelectListItem { Text = "Hepsi", Value = "1" });
            if (!string.IsNullOrEmpty(email))
                model.Email = email;
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {

                model.MemberName = AuthenticationUser.Membership.MemberName;
                model.MemberSurname = AuthenticationUser.Membership.MemberSurname;
                model.Email = AuthenticationUser.Membership.MemberEmail;
                model.HaveMemberShip = true;
            }
            return View(model);
        }
        [HttpPost]

        public ActionResult BulletinRegister(BulletinMemberCreateModel model, string[] SectorCategories)
        {
            if (string.IsNullOrEmpty(model.MemberName) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.MemberSurname) || SectorCategories == null)
            {
                if (string.IsNullOrEmpty(model.MemberName))
                    ModelState.AddModelError("MemberName", "İsim Boş Geçilemez");
                if (string.IsNullOrEmpty(model.Email)) ModelState.AddModelError("Email", "Email Adresi Boş Geçilemez");
                if (string.IsNullOrEmpty(model.MemberSurname)) ModelState.AddModelError("MemberSurname", "Soyisim Boş Geçilemez");
                if (SectorCategories == null) ModelState.AddModelError("SectorCategories", "En Az Bir Tanesi Seçilmesi Zorunludur");

            }
            else
            {
                var anyBullettin = _bulettinService.GetBulletinMembers().FirstOrDefault(x => x.MemberEmail == model.Email);
                if (anyBullettin == null)
                {
                    var bulletinMember = new global::MakinaTurkiye.Entities.Tables.Bullettins.BulletinMember();
                    bulletinMember.MemberEmail = model.Email;
                    bulletinMember.MemberName = model.MemberName;
                    bulletinMember.MemberSurname = model.MemberSurname;
                    bulletinMember.RecordDate = DateTime.Now;
                    _bulettinService.InsertBulletinMember(bulletinMember);
                    if (SectorCategories.Contains("1"))
                    {
                        var sectors = _categoryService.GetMainCategories();
                        foreach (var sectoreItem in sectors)
                        {
                            var bulletinCategory1 = new global::MakinaTurkiye.Entities.Tables.Bullettins.BulletinMemberCategory();
                            bulletinCategory1.CategoryId = Convert.ToInt32(sectoreItem.CategoryId);
                            bulletinCategory1.BulletinMemberId = bulletinMember.BulletinMemberId;
                            _bulettinService.InsertBulletinMemberCategory(bulletinCategory1);

                        }
                    }
                    else
                    {
                        foreach (var item in SectorCategories)
                        {

                            var bulletinCategory = new global::MakinaTurkiye.Entities.Tables.Bullettins.BulletinMemberCategory();
                            bulletinCategory.CategoryId = Convert.ToInt32(item);
                            bulletinCategory.BulletinMemberId = bulletinMember.BulletinMemberId;
                            _bulettinService.InsertBulletinMemberCategory(bulletinCategory);

                        }
                    }

                    TempData["Success"] = "true";
                    return RedirectToAction("BulletinRegister");
                }
                else
                {
                    ViewBag.ErrorMessage = "Bu mail adresiyle bülten üyeliği kaydınız bulunmaktadır.";
                }
            }




            var sectorCategories = _categoryService.GetMainCategories();
            foreach (var item in sectorCategories)
            {
                model.SectorCategories.Add(new SelectListItem { Text = item.CategoryName, Value = item.CategoryId.ToString() });
            }
            model.SectorCategories.Add(new SelectListItem { Text = "Hepsi", Value = "1" });
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {


                model.HaveMemberShip = true;
            }
            return View(model);
        }

        public ActionResult UpdateRelCategories()
        {
            var model = new RelCategoryModel();

            model.SectorItems = _categoryService.GetCategoriesByCategoryType(CategoryTypeEnum.Sector);

            return View(model);
        }

        #endregion

    }
}