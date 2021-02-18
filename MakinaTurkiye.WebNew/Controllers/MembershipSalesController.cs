using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;

using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Checkouts;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Payments;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Packets;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Logs;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Checkouts;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

using static NeoSistem.MakinaTurkiye.Web.Models.EnumModel;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using MakinaTurkiye.Utilities.MailHelpers;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{

    public class SessionPacketModel
    {
        //private static ILog log = log4net.LogManager.GetLogger(typeof(HomeController));
        internal static readonly string SESSION_USERID = "PacketModel";


        public static PacketModel PacketModel
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USERID] == null)
                {
                    HttpContext.Current.Session[SESSION_USERID] = new PacketModel();
                }
                return HttpContext.Current.Session[SESSION_USERID] as PacketModel;
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
    public class SessionPayWithCreditCardModel
    {
        internal static readonly string SESSION_USERID = "SessionPayWithCreditCard";

        public static MTPayWithCreditCardModel MTPayWithCreditCardModel
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USERID] == null)
                {
                    HttpContext.Current.Session[SESSION_USERID] = new MTPayWithCreditCardModel();
                }
                return HttpContext.Current.Session[SESSION_USERID] as MTPayWithCreditCardModel;
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

    public class MembershipSalesController : Controller
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly IConstantService _constantService;
        private readonly ICompanyDemandMembershipService _companyDemandMembership;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IPhoneService _phoneService;
        private readonly IAddressService _addressService;
        private readonly IProductService _productService;
        private readonly IMemberService _memberService;
        private readonly IBankAccountService _bankAccountService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMessagesMTService _messageMTService;
        private readonly IVirtualPosService _virtualPosService;
        private readonly ICreditCardLogService _creditCardLogService;

        //private static ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        #endregion

        #region Ctor

        public MembershipSalesController(IOrderService orderService,
            IProductService productService, IStoreService storeService,
            IPacketService packetService, IConstantService constantService,
            ICompanyDemandMembershipService companyDemandMembership,
            IMemberStoreService memberStoreService,
            IPhoneService phoneService,
            IMemberService memberService,
            IAddressService addressService,
            IMemberService _memberService,
            IBankAccountService bankAccountService,
            ICreditCardService creditCardService,
            IMessagesMTService messageMTService,
            IVirtualPosService virtualPosService, ICreditCardLogService creditCardLogService)
        {
            this._orderService = orderService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._constantService = constantService;
            this._companyDemandMembership = companyDemandMembership;
            this._memberStoreService = memberStoreService;
            this._phoneService = phoneService;
            this._addressService = addressService;
            this._productService = productService;
            this._memberService = memberService;
            this._bankAccountService = bankAccountService;
            this._creditCardService = creditCardService;
            this._messageMTService = messageMTService;
            this._virtualPosService = virtualPosService;
            this._creditCardLogService = creditCardLogService;
        }

        #endregion

        #region Methods

        public ActionResult StepFailure()
        {
            return View();
        }

        public ActionResult OneStep(string sayfa)
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);

            SessionPacketModel.PacketModel.MainPartyId = memberStore.StoreMainPartyId.Value;
            SessionPacketModel.PacketModel.OrderNo = "S" + SessionPacketModel.PacketModel.MainPartyId;
            SessionPacketModel.PacketModel.OrderCode = Guid.NewGuid().ToString("N").Substring(0, 5);

            SessionPacketModel.PacketModel.StoreName = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId).StoreName;

            var packetViewModel = new PacketViewModel();
            packetViewModel.LastPageAdvertAdd = (sayfa == "ilanekle") ? true : false;

            var orderStore = _orderService.GetOrdersByMainPartyId(memberStore.StoreMainPartyId.Value);

            if (orderStore.Count > 0)
                packetViewModel.AnyOrder = true;
            else
                packetViewModel.AnyOrder = false;

            var showPacketFeatureTypeItems = new List<int>();

            showPacketFeatureTypeItems.Add(3);
            showPacketFeatureTypeItems.Add(4);
            showPacketFeatureTypeItems.Add(5);
            showPacketFeatureTypeItems.Add(6);
            showPacketFeatureTypeItems.Add(7);

            packetViewModel.PacketFeatureItems = _packetService.GetAllPacketFeatures();

            packetViewModel.PacketFeatureTypeItems = _packetService.GetAllPacketFeatureTypes().ToList();

            packetViewModel.PacketItems = _packetService.GetPacketIsOnsetFalseByDiscountType(false).Where(x => x.DopingPacketDay.HasValue == false).ToList();
            var constant = _constantService.GetConstantByConstantType(ConstantTypeEnum.PacketSalesFooter).FirstOrDefault();
            if (constant != null)
                packetViewModel.BottomDescription = constant.ContstantPropertie;

            return View(packetViewModel);
        }

        public ActionResult DiscountPackets()
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var anyOrder = _orderService.GetOrdersByMainPartyId(memberStore.StoreMainPartyId.Value);
            if (anyOrder.Count > 0)
            {
                return Redirect("~/UyelikSatis/adim1?sayfa=ilanekle");
            }
            SessionPacketModel.PacketModel.MainPartyId = memberStore.StoreMainPartyId.Value;
            SessionPacketModel.PacketModel.OrderNo = "S" + SessionPacketModel.PacketModel.MainPartyId;
            SessionPacketModel.PacketModel.OrderCode = Guid.NewGuid().ToString("N").Substring(0, 5);

            SessionPacketModel.PacketModel.StoreName = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId)).StoreName;

            var packetViewModel = new PacketViewModel();
            var showPacketFeatureTypeItems = new List<int>();
            showPacketFeatureTypeItems.Add(3);
            showPacketFeatureTypeItems.Add(4);
            showPacketFeatureTypeItems.Add(5);
            showPacketFeatureTypeItems.Add(6);
            showPacketFeatureTypeItems.Add(7);

            packetViewModel.PacketFeatureItems = _packetService.GetAllPacketFeatures();
            packetViewModel.PacketFeatureTypeItems = _packetService.GetAllPacketFeatureTypes().Where(p => showPacketFeatureTypeItems.Contains(p.PacketFeatureTypeId)).ToList();

            packetViewModel.PacketItems = _packetService.GetPacketIsOnsetFalseByDiscountType(true);
            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.DiscountPacketDescription);
            if (constants.Count > 0)
            {
                packetViewModel.Description = constants.First().ContstantPropertie;
            }
            else
            {
                packetViewModel.Description = string.Empty;
            }
            return View(packetViewModel);

        }

        [HttpPost]
        public ActionResult OneStep(int PacketId)
        {
            SessionPacketModel.PacketModel.PacketId = PacketId;
            return RedirectToAction("ThreeStep", "MembershipSales");
        }

        public ActionResult Trialpackage()
        {
            TempData["haberles"] = 1;
            return Redirect("/Account/Advert/Advert");
        }

        [HttpGet]
        public ActionResult ThreeStepPre(int id)
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);

            SessionPacketModel.PacketModel.MainPartyId = memberStore.StoreMainPartyId.Value;
            SessionPacketModel.PacketModel.OrderNo = "S" + SessionPacketModel.PacketModel.MainPartyId;
            SessionPacketModel.PacketModel.OrderCode = Guid.NewGuid().ToString("N").Substring(0, 5);

            SessionPacketModel.PacketModel.StoreName = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value).StoreName;
            SessionPacketModel.PacketModel.PacketId = id;
            return RedirectToAction("ThreeStep", "MembershipSales");
        }



        public ActionResult ThreeStep()
        {
            if (SessionPacketModel.PacketModel.PacketId > 0)
            {
                var packet = _packetService.GetPacketByPacketId(SessionPacketModel.PacketModel.PacketId);
                SessionPacketModel.PacketModel.OrderPrice = packet.PacketPrice;

                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

                SessionPacketModel.PacketModel.AccountList = _bankAccountService.GetAllAccounts();

                var adress = _addressService.GetFisrtAddressByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
                SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address = adress.GetAddressEdit();
                SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber = store.TaxNumber;
                SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice = store.TaxOffice;
                SessionPacketModel.PacketModel.TaxAndAddressViewModel.StoreName = store.StoreName;
                SessionPacketModel.PacketModel.PacketDay = packet.PacketDay;
                SessionPacketModel.PacketModel.PacketName = packet.PacketName;

                SessionPacketModel.PacketModel.CreditCardItems = _creditCardService.GetAllCreditCards();

                var creditCard = _creditCardService.GetAllCreditCards().FirstOrDefault();
                SessionPacketModel.PacketModel.CreditCardInstallmentItems = _creditCardService.GetCreditCardInstallmentsByCreditCardId(creditCard.CreditCardId);

                return View(SessionPacketModel.PacketModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ThreeStep(byte? AccountId, byte? OrderType, short? CreditCardInstallmentId, byte? CreditCardId, string TaxOffice, string TaxNumber, string Address)
        {
            SessionPacketModel.PacketModel.OrderType = OrderType.Value;
            SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber = TaxNumber;
            SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice = TaxOffice;
            SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address = Address;


            var store = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
            if (store != null)
            {
                store.TaxNumber = TaxNumber;
                store.TaxOffice = TaxOffice;
                _storeService.UpdateStore(store);
            }

            //var store = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
            //store.TaxNumber = TaxNumber;
            //store.TaxOffice = TaxOffice;
            //_storeService.UpdateStore(store);

            if (OrderType.Value == (byte)Ordertype.KrediKarti)
            {
                SessionPacketModel.PacketModel.CreditCardId = CreditCardId.Value;
                SessionPacketModel.PacketModel.CreditCardInstallmentId = CreditCardInstallmentId.Value;
                TempData["ordertype"] = "2";
                //if (CreditCardId.Value == 13)
                //{
                //    return RedirectToAction("PosnetOdeme");
                //}
            }
            else
            {
                SessionPacketModel.PacketModel.AccountId = AccountId.Value;
                TempData["ordertype"] = "1";
            }
            //UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            //return Redirect(u.Action("FourStep", "MembershipSales", null, "https"));
            return RedirectToAction("FourStep");
        }

        [RequireHttps]
        public ActionResult PosnetOdeme()
        {
            ViewData["odemesonucu"] = 0;
            TempData["hatacode"] = "Lütfen kart bilgilerinizi eksiksiz olarak doldurunuz.";
            if (SessionPacketModel.PacketModel.PacketId > 0)
            {
                return View(SessionPacketModel.PacketModel);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult PosnetOdeme(FormCollection[] fColl)
        {
            string siparisno = Request.Form.Get("SiparisNo");
            string odemetamamlandı = Request.Form.Get("SonucKodu");
            ViewData["odemesonucu"] = odemetamamlandı;
            string hatakodu = Request.Form.Get("HataID");
            string kod = Request.Form.Get("HataKodu");
            string tutar = Request.Form.Get("Tutar");
            TempData["hatacode"] = Request.Form.Get("HataKodu");
            //return View(SessionPacketModel.PacketModel);
            if (Convert.ToInt32(odemetamamlandı) == 1)
            {

                var order = new Order
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                    Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                    PacketId = SessionPacketModel.PacketModel.PacketId,
                    OrderCode = SessionPacketModel.PacketModel.OrderCode,
                    OrderNo = SessionPacketModel.PacketModel.OrderNo,
                    TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                    TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = SessionPacketModel.PacketModel.OrderType,
                    RecordDate = DateTime.Now
                };

                if (SessionPacketModel.PacketModel.OrderType == (byte)Ordertype.KrediKarti)
                {
                    if (SessionPacketModel.PacketModel.CreditCardInstallmentId > 0)
                    {
                        decimal price = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                        order.OrderPrice = (price + (price * SessionPacketModel.PacketModel.GetCreditCardInstallment.CreditCardValue / 100));
                        order.CreditCardInstallmentId = SessionPacketModel.PacketModel.CreditCardInstallmentId;
                    }
                    else
                    {
                        order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                    }
                }
                else
                {
                    order.AccountId = SessionPacketModel.PacketModel.AccountId;

                    order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                }

                _orderService.InsertOrder(order);

                TempData["hatacode"] = "ödeme başarıyla tamamlandı";
            }
            //try
            //{
            //  CreditCardLog ccl = new CreditCardLog();
            //  ccl.MainPartyID = SessionPacketModel.PacketModel.MainPartyId;
            //  if (taksit == "00")
            //    ccl.Ordertype = "Tek Çekim";
            //  else
            //    ccl.Ordertype = "Taksitli";

            //  if (value == "00")
            //    ccl.status = "Başarılı";
            //  else
            //    ccl.status = "Başarısız";
            //  var cc = (from c in entities.CreditCards
            //            where c.CreditCardId == SessionPacketModel.PacketModel.CreditCardId
            //            select c.CreditCardName).SingleOrDefault();
            //  ccl.PosName = cc;
            //  string kartno = kkno;

            //  ccl.Date = DateTime.Now;
            //  ccl.IP = Request.UserHostAddress.ToString();
            //  ccl.Code = value;
            //  ccl.Detail = TempData["text"].ToString();
            //  entities.CreditCardLogs.Add(ccl);
            //  entities.SaveChanges();
            //}
            //catch { }
            return View(SessionPacketModel.PacketModel);
        }

#if !DEBUG
            [RequireHttps]
#endif
        public ActionResult FourStep(string messagge, string orderId)
        {


            if (!string.IsNullOrEmpty(orderId))
            {
                var order = _orderService.GetOrderByOrderId(Convert.ToInt32(orderId));
                if (order != null)
                {
                    SessionPacketModel.PacketModel.PacketId = order.PacketId;
                    SessionPacketModel.PacketModel.CreditCardId = 8;
                    SessionPacketModel.PacketModel.MainPartyId = order.MainPartyId;
                    SessionPacketModel.PacketModel.OrderNo = "S" + SessionPacketModel.PacketModel.MainPartyId;
                    SessionPacketModel.PacketModel.OrderCode = Guid.NewGuid().ToString("N").Substring(0, 5);

                    SessionPacketModel.PacketModel.StoreName = _storeService.GetStoreByMainPartyId(order.MainPartyId).StoreName;


                }

                SessionPacketModel.PacketModel.OrderId = Convert.ToInt32(orderId);

                SessionPacketModel.PacketModel.CreditCardInstallmentItems = _creditCardService.GetCreditCardInstallmentsByCreditCardId(8);


            }
            if (SessionPacketModel.PacketModel.PacketId > 0)
            {
                if (TempData["ordertype"] != null)
                {
                    if (TempData["ordertype"] == "1")
                    {

                        try
                        {
                            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                            var packetStore = _packetService.GetPacketByPacketId(store.PacketId);
                            var recordDate = DateTime.Now;
                            if (packetStore.PacketPrice > 0 && store.StorePacketEndDate > DateTime.Now)
                            {
                                recordDate = store.StorePacketEndDate.Value;
                            }
                            var settings = ConfigurationManager.AppSettings;
                            #region
                            //*****************************************

                            var order = new Order
                            {
                                MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                                Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                                PacketId = SessionPacketModel.PacketModel.PacketId,
                                OrderCode = SessionPacketModel.PacketModel.OrderCode,
                                OrderNo = SessionPacketModel.PacketModel.OrderNo,
                                TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                                TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                                PacketStatu = (byte)PacketStatu.Inceleniyor,
                                OrderType = SessionPacketModel.PacketModel.OrderType,
                                RecordDate = DateTime.Now,
                                PacketStartDate = recordDate,
                                PacketDay = SessionPacketModel.PacketModel.PacketDay,

                            };
                            if (store.AuthorizedId != null)
                            {
                                order.AuthorizedId = store.AuthorizedId.Value;
                            }
                            order.AccountId = SessionPacketModel.PacketModel.AccountId;
                            //order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice,18);
                            order.OrderPrice = SessionPacketModel.PacketModel.OrderPrice;

                            _orderService.InsertOrder(order);
                            //*****************************************

                            #endregion

                            var mailAdress = _memberService.GetMemberByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId).MemberEmail;
                            var mailsend = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
                            string Email = mailAdress;




                            var packet = _packetService.GetPacketByPacketId(Convert.ToInt32(order.PacketId));

                            var mailMessage = _messageMTService.GetMessagesMTByMessageMTName("havalbildirimmail");
                            var bankAccount = _bankAccountService.GetBankAccountByBankAccountId(order.AccountId.Value);

                            MailMessage mail = new MailMessage();
                            mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                            mail.To.Add(Email);                                                              //Mailin kime gideceğini belirtiyoruz
                            mail.Subject = mailMessage.MessagesMTTitle;                                              //Mail konusu

                            string template = mailMessage.MessagesMTPropertie;
                            template = template.Replace("#firmadi#", mailsend.StoreName).Replace("#kullaniciadi#", mailsend.StoreName).Replace("#pakettipi#", packet.PacketName).Replace("#tutar#", order.OrderPrice.ToString("N") + " TL").Replace("#bankahesapbilgileri#", bankAccount.BankName + "-" + bankAccount.AccountNo);
                            mail.Body = template;                                                            //Mailin içeriği
                            mail.IsBodyHtml = true;
                            mail.Priority = MailPriority.Normal;
                            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                            sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                            sc.Send(mail);

                            //

                            #region bilgimakina

                            MailMessage mailb = new MailMessage();
                            var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                            mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                            mailb.To.Add("bilgi@makinaturkiye.com");
                            mailb.Subject = "Paket Alımı " + mailsend.StoreName;
                            //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                            //templatet = messagesmttemplate.MessagesMTPropertie;
                            string bilgimakinaicin = mailsend.StoreName + " isimli firma " + packet.PacketName + " paketini havale ile satın alma işlemini gerçekleştirmiştir.";
                            mailb.Body = bilgimakinaicin;
                            mailb.IsBodyHtml = true;
                            mailb.Priority = MailPriority.Normal;
                            SmtpClient scr1 = new SmtpClient();
                            scr1.Port = 587;
                            scr1.Host = "smtp.gmail.com";
                            scr1.EnableSsl = true;
                            scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                            scr1.Send(mailb);

                            #endregion


                        }
                        catch (Exception ex)
                        {
                            //ExceptionHandler.HandleException(ex);
                            TempData["Mail"] = ex.InnerException;
                        }
                        return RedirectToAction("FinishSales");
                    }
                }
                if (messagge == "failure")
                {
                    ViewData["messageError"] = TempData["errorPosMessage"];
                }
                var member = _memberService.GetMemberByMainPartyId(Convert.ToInt32(AuthenticationUser.CurrentUser.Membership.MainPartyId));

                var memberStoreN = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
                var phones = _phoneService.GetPhonesByMainPartyId(memberStoreN.StoreMainPartyId.Value);
                if (phones.Count == 0)
                {


                    var phoneGsm = phones.Where(x => x.PhoneType == 3).FirstOrDefault();
                    if (phoneGsm != null)
                    {
                        SessionPacketModel.PacketModel.Gsm = phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber;
                    }
                }
                else
                {

                    /*Eski Hali*/

                    //var phoneGsm = phones.FirstOrDefault(x => x.PhoneType == 3);
                    //SessionPacketModel.PacketModel.Gsm = phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber;

                    /**/


                    /*Osman : Bu kısımda neden whatsapp numrasına bakılıyor baklıyorsa bile bu kısımda mantık hatası var.*/
                    var phoneGsm = phones.Where(x => x.PhoneType == 3).FirstOrDefault();
                    if (phoneGsm != null)
                    {
                        SessionPacketModel.PacketModel.Gsm = phoneGsm.PhoneCulture + phoneGsm.PhoneAreaCode + phoneGsm.PhoneNumber;
                    }

                }

                return View("FourStepNew", SessionPacketModel.PacketModel);
            }

            return View("FourStepNew", SessionPacketModel.PacketModel);
            //return RedirectToAction("Index", "Home");
        }


        string kkno;
        string ay;
        string yil;
        string tutar;
        string cv2;
        string khip;

#if !DEBUG
                    [RequireHttps]
#endif
        [HttpPost]
        public ActionResult FourStep(FormCollection[] fColl)
        {
            #region
            string hav = TempData["ordertype"].ToString();
            if (hav == "1")
            {

                try
                {
                    var settings = ConfigurationManager.AppSettings;

                    var mailsend = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
                    string Email = mailsend.StoreEMail;



                    MailMessage mail = new MailMessage();

                    var mtMessage = _messageMTService.GetMessagesMTByMessageMTName("havalbildirimmail");

                    mail.From = new MailAddress(mtMessage.Mail, mtMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                    mail.To.Add(Email);                                                              //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = mtMessage.MessagesMTTitle;                                              //Mail konusu
                    string body = mtMessage.MailContent;
                    body = body.Replace("#kullanciadi#", AuthenticationUser.CurrentUser.Membership.MemberName).Replace("#pakettipi#", SessionPacketModel.PacketModel.PacketName).Replace("#tutar#", SessionPacketModel.PacketModel.OrderPrice.ToString("N"));
                    mail.Body = body;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mtMessage.Mail, mtMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                }
                catch (Exception ex)
                {
                    //ExceptionHandler.HandleException(ex);
                    TempData["Mail"] = ex.InnerException;
                }
                #endregion

                #region
                //*****************************************
                var order = new Order
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                    Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                    PacketId = SessionPacketModel.PacketModel.PacketId,
                    OrderCode = SessionPacketModel.PacketModel.OrderCode,
                    OrderNo = SessionPacketModel.PacketModel.OrderNo,
                    TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                    TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = SessionPacketModel.PacketModel.OrderType,
                    OrderPacketType = (byte)OrderPacketType.Normal,
                    RecordDate = DateTime.Now,
                    PriceCheck = false
                };


                order.AccountId = SessionPacketModel.PacketModel.AccountId;
                //order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice,18);
                order.OrderPrice = SessionPacketModel.PacketModel.OrderPrice;
                _orderService.InsertOrder(order);
                //*****************************************
                #endregion
                return RedirectToAction("FinishSales");
            }
            else if (SessionPacketModel.PacketModel.CreditCardId == 12)
            {
                #region order
                //Order
                var order = new Order
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                    Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                    PacketId = SessionPacketModel.PacketModel.PacketId,
                    OrderCode = SessionPacketModel.PacketModel.OrderCode,
                    OrderNo = SessionPacketModel.PacketModel.OrderNo,
                    TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                    TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = SessionPacketModel.PacketModel.OrderType,
                    OrderPacketType = (byte)OrderPacketType.Normal,
                    RecordDate = DateTime.Now
                };

                if (SessionPacketModel.PacketModel.OrderType == (byte)Ordertype.KrediKarti)
                {
                    if (SessionPacketModel.PacketModel.CreditCardInstallmentId > 0)
                    {
                        decimal price = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                        order.OrderPrice = (price + (price * SessionPacketModel.PacketModel.GetCreditCardInstallment.CreditCardValue / 100));
                        order.CreditCardInstallmentId = SessionPacketModel.PacketModel.CreditCardInstallmentId;
                    }
                    else
                    {
                        order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                    }
                }
                else
                {
                    order.AccountId = SessionPacketModel.PacketModel.AccountId;
                    order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                }
                #endregion


                kkno = Request.Form.Get("pan");

                cv2 = Request.Form.Get("cv2");
                yil = Request.Form.Get("Ecom_Payment_Card_ExpDate_Year");
                ay = Request.Form.Get("Ecom_Payment_Card_ExpDate_Month");

                //------------------------------------------------------------

                //kredi kartı çekim
                byte[] b = new byte[1500];

                //GET mesaji oldugu icin bosluklar + ile doldurulmustur. provizyonMesaji vpos724v3.doc dokumaninda belirtilen formata uygun
                //olarak olusturulmali ve donen mesaj ayni dokumana uygun olarak parcalanmalidir.

                //string provizyonMesaji="UYGUN FORMATTA OTORIZASYON MESAJI";

                //string provizyonMesaji = "kullanici=0001&sifre=00000000&islem=PRO&uyeno=000000000&posno=00000000&kkno=4938410000000000&gectar=0000&cvc=000&tutar=000000000100&provno=000000&taksits=00&islemyeri=I&uyeref=200501011234567890&vbref=6527BB1815F9AB1DE864A488E5198663002D0000&khip=195.0.0.24&xcip=ABABABABAB";


                b.Initialize();
                try
                {

                    string taksit = SessionPacketModel.PacketModel.CreditCardInstallmentId > 0 ? SessionPacketModel.PacketModel.GetCreditCardInstallment.CreditCardCount.ToString() : "00";
                    if (int.Parse(taksit) < 10 && taksit != "00")
                    {
                        taksit = "0" + taksit;
                    }
                    string s = Math.Truncate(order.OrderPrice * 100).ToString();

                    string str = new string('0', 12 - (s.Length));
                    tutar = str + s;
                    khip = Request.ServerVariables["REMOTE_ADDR"];

                    var pos = _virtualPosService.GetVirtualPosByVirtualPosId(6);
                    //var kart = from c in fColl
                    //           select c.AllKeys;
                    //Request.Form.Get("amount");
                    StringBuilder sParameters = new StringBuilder();
                    sParameters.Append("?kullanici=" + pos.VirtualPosApiUserName);
                    sParameters.Append("&sifre=" + pos.VirtualPosApiPass);
                    sParameters.Append("&islem=" + "PRO");
                    sParameters.Append("&uyeno=" + pos.VirtualPosClientId);
                    sParameters.Append("&posno=" + "vp000295");
                    sParameters.Append("&kkno=" + kkno);
                    sParameters.Append("&gectar=" + yil + ay);
                    sParameters.Append("&cvc=" + cv2);
                    sParameters.Append("&tutar=" + tutar);
                    sParameters.Append("&provno=" + "000000");
                    sParameters.Append("&taksits=" + taksit);
                    sParameters.Append("&islemyeri=" + "I");
                    sParameters.Append("&uyeref=" + "0");
                    sParameters.Append("&vbref=" + "0");
                    sParameters.Append("&khip=" + "213.128.75.66");
                    sParameters.Append("&xcip=" + pos.VirtualPosStoreKey);

                    //Console.WriteLine("Provizyon Mesaji:" + sParameters.ToString());
                    b = Encoding.ASCII.GetBytes(sParameters.ToString());

                    WebRequest h1 = (WebRequest)HttpWebRequest.Create("https://subesiz.vakifbank.com.tr/vpos724v3/" + sParameters.ToString());
                    h1.Method = "GET";

                    Stream dataStream;
                    WebResponse response = h1.GetResponse();
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    //char[] ary=responseFromServer.ToCharArray();
                    // string sonuc=responseFromServer.Substring(0,2);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(responseFromServer);
                    XmlNode xnode = xmlDocument.DocumentElement.SelectSingleNode("/Cevap/Msg").FirstChild;

                    string value = xnode.InnerText;
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                    #region hata kodları
                    switch (value)
                    {
                        case "00":
                            TempData["text"] = "Ödemeniz işleminiz başarıyla gerçekleşmiştir.Paketiniz en kısa sürede aktif hale gelecektir.";


                            #region
                            try
                            {
                                var settings = ConfigurationManager.AppSettings;

                                var mailsend = _storeService.GetStoreByMainPartyId(SessionPacketModel.PacketModel.MainPartyId);
                                string Email = mailsend.StoreEMail;

                                var packet = _packetService.GetPacketByPacketId(Convert.ToInt32(order.PacketId));

                                var mailMessage = _messageMTService.GetMessagesMTByMessageMTName("kredikartıodememail");

                                var bankAccount = _bankAccountService.GetBankAccountByBankAccountId(order.AccountId.Value);


                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                                mail.To.Add(Email);
                                //Mailin kime gideceğini belirtiyoruz
                                //fiyat tl cinsinden

                                mail.Subject = mailMessage.MessagesMTTitle;                                              //Mail konusu

                                string template = mailMessage.MessagesMTPropertie;
                                template = template.Replace("#uyeadisoyadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname);
                                mail.Body = template;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                                sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                                sc.Send(mail);

                                #region packetconfirm

                                //hesap numnarası fiyatı
                                var packet1 = _packetService.GetPacketByPacketId(order.PacketId);
                                var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                                #region emailicin
                                settings = ConfigurationManager.AppSettings;
                                mail = new MailMessage();
                                var mailT = _messageMTService.GetMessagesMTByMessageMTName("goldenpro");
                                mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                                mail.To.Add(store.StoreEMail);                                                              //Mailin kime gideceğini belirtiyoruz
                                mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                                template = mailT.MessagesMTPropertie;
                                string dateimiz = DateTime.Now.AddDays(packet1.PacketDay).ToString();
                                template = template.Replace("#uyeliktipi#", packet1.PacketName).Replace("#uyelikbaslangıctarihi#", DateTime.Now.ToShortDateString()).Replace("#uyelikbitistarihi#", dateimiz).Replace("#kullaniciadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname).Replace("#pakettipi#", packet1.PacketName);
                                mail.Body = template;                                                            //Mailin içeriği
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.Normal;
                                sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                                sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                                sc.Send(mail);
                                #endregion
                                store.PacketId = packet.PacketId;
                                store.StorePacketBeginDate = DateTime.Now;

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
                                var orderLastList = _orderService.GetOrdersByMainPartyId(store.MainPartyId);
                                foreach (var item in orderLastList.ToList())
                                {
                                    item.OrderPacketEndDate = store.StorePacketEndDate;
                                    _orderService.UpdateOrder(item);
                                }

                                store.StorePacketEndDate = DateTime.Now.AddDays(packet1.PacketDay);
                                _storeService.UpdateStore(store);

                                order.PacketStatu = (byte)PacketStatu.Onaylandi;
                                order.OrderPacketEndDate = DateTime.Now.AddDays(packet1.PacketDay);

                                #endregion

                                #region bilgimakina
                                MailMessage mailb = new MailMessage();

                                var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");


                                mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                                mailb.To.Add("bilgi@makinaturkiye.com");
                                mailb.Subject = "Paket Alımı " + mailsend.StoreName;
                                //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                                //templatet = messagesmttemplate.MessagesMTPropertie;
                                string bilgimakinaicin = mailsend.StoreName + " isimli firma " + packet.PacketName + " paketini kredi kartı ile satın alma işlemini gerçekleştirmiştir.";
                                mailb.Body = bilgimakinaicin;
                                mailb.IsBodyHtml = true;
                                mailb.Priority = MailPriority.Normal;
                                SmtpClient scr1 = new SmtpClient();
                                scr1.Port = 587;
                                scr1.Host = "smtp.gmail.com";
                                scr1.EnableSsl = true;
                                scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                                scr1.Send(mailb);
                                #endregion


                            }
                            catch (Exception ex)
                            {
                                //ExceptionHandler.HandleException(ex);
                                TempData["Mail"] = ex.InnerException;
                            }
                            #endregion

                            _orderService.InsertOrder(order);
                            break;
                        case "02":
                            TempData["text"] = "Kredi Kartınızdan kaynaklanan bir problem oluştu.Lütfen bankanızla irtibata geçiniz.";
                            break;
                        case "69":
                            TempData["text"] = "Kredi Kartıbilgilerinizi eksik veya yanlış girdiniz lütfen tekrar deneyin.";
                            break;
                        case "67":
                            TempData["text"] = "Kredi Kartıbilgilerinizi eksik veya yanlış girdiniz lütfen tekrar deneyin.";
                            break;
                        case "64":
                            TempData["text"] = "İşlem tipi taksite uygun değil.Lütfen uygun kartla deneyiniz.";
                            break;
                        case "40":
                            TempData["text"] = "İadesi denenen işlemin orijinali yok.";
                            break;
                        case "42":
                            TempData["text"] = "Günlük iade limiti aşıldı.";
                            break;
                        case "68":
                            TempData["text"] = "Hatalı İşlem Tipi";
                            break;

                        case "66":
                            TempData["text"] = "Numeric deger hatası.Rakam girmeniz gereken bir alana başka bir karakter girdiniz";
                            break;
                        case "65":
                            TempData["text"] = "Hatalı tutar.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "63":
                            TempData["text"] = "Hatalı bir karakter girdiniz.Lütfen kontrol edip tekrar deneyin.";
                            break;
                        case "62":
                            TempData["text"] = "Yetkisiz veya tanımsız kullanıcı hatası.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "61":
                            TempData["text"] = "Hatalı Tarih.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "60":
                            TempData["text"] = "Hareket Bulunamadi.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "59":
                            TempData["text"] = "Gunsonu yapilacak hareket yok/GS Yapilmis";
                            break;
                        case "90":
                            TempData["text"] = "Kayıt bulunamadı.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "91":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(Begin Transaction Error)";
                            break;
                        case "92":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(Insert Update Error)";
                            break;
                        case "96":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(DLL registration error)";
                            break;
                        case "97":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(IP Hatası)";
                            break;
                        case "98":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(H. Iletisim hatası )";
                            break;
                        case "99":
                            TempData["text"] = "Bankadan kaynaklanan bir sorun oluştu.Lütfen daha sonra tekrar deneyiniz.(DB Baglantı hatası)";
                            break;
                        case "F2":
                            TempData["text"] = "Sanal POS inaktif durumda.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "G0":
                            TempData["text"] = "Yurt Dışı kart işlem izniniz yok.Lütfen 02124736060`ı arayınız";
                            break;
                        case "G5":
                            TempData["text"] = "Terminal izni yok.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "70":
                            TempData["text"] = "XCIP hatalı.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "71":
                            TempData["text"] = "Üye İşyeri blokeli ya da tanımsız.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "72":
                            TempData["text"] = "Tanımsız POS.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "73":
                            TempData["text"] = "POS table update hatası.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "76":
                            TempData["text"] = "Sanal POS Taksite kapalı.Lütfen tek çekim olarak alışverişinizi tamamlayınız.";
                            break;
                        case "75":
                            TempData["text"] = "Illegal State hatası.Lütfen site yöneticisiyle irtibata geçiniz.";
                            break;
                        case "74":
                            TempData["text"] = "Hatalı taksit sayısı.Lütfen tekrar deneyin.";
                            break;
                        case "80":
                            TempData["text"] = "CAVV bilgisi hatalı.Lütfen tekrar deneyin.";
                            break;
                        case "81":
                            TempData["text"] = "Eksik güvenlik Bilgisi.Lütfen tekrar deneyin.";
                            break;
                        case "Eski kayıt.":
                            TempData["text"] = "Eski kayıt.Lütfen tekrar deneyin.";
                            break;


                        default:
                            TempData["text"] = "Ödemeniz sırasında bir hata oluştu.Lütfen daha sonra tekrar deneyiniz.Hata kodu : " + value;
                            break;
                    }
                    #endregion
                    #region log
                    try
                    {
                        var ccl = new CreditCardLog();
                        ccl.MainPartyId = SessionPacketModel.PacketModel.MainPartyId;
                        if (taksit == "00")
                            ccl.OrderType = "Tek Çekim";
                        else
                            ccl.OrderType = "Taksitli";

                        if (value == "00")
                            ccl.Status = "Başarılı";
                        else
                            ccl.Status = "Başarısız";

                        var cc = _creditCardService.GetCreditCardByCreditCardId(SessionPacketModel.PacketModel.CreditCardId);
                        ccl.PosName = cc.CreditCardName;
                        string kartno = kkno;

                        ccl.CreatedDate = DateTime.Now;
                        ccl.IPAddress = Request.UserHostAddress.ToString();
                        ccl.Code = value;
                        ccl.Detail = TempData["text"].ToString();

                        _creditCardLogService.InsertCreditCardLog(ccl);
                    }
                    catch (Exception ex)
                    {
                        //ExceptionHandler.HandleException(ex);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    //ExceptionHandler.HandleException(ex);
                    TempData["text"] = ex.Message;
                }

                //------------------------------------------------------------
                return RedirectToAction("sonuc");
            }
            #region
            else
            {
                var order = new Order
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                    Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                    PacketId = SessionPacketModel.PacketModel.PacketId,
                    OrderCode = SessionPacketModel.PacketModel.OrderCode,
                    OrderNo = SessionPacketModel.PacketModel.OrderNo,
                    TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                    TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = SessionPacketModel.PacketModel.OrderType,
                    RecordDate = DateTime.Now
                };

                if (SessionPacketModel.PacketModel.OrderType == (byte)Ordertype.KrediKarti)
                {
                    if (SessionPacketModel.PacketModel.CreditCardInstallmentId > 0)
                    {
                        decimal price = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                        order.OrderPrice = (price + (price * SessionPacketModel.PacketModel.GetCreditCardInstallment.CreditCardValue / 100));
                        order.CreditCardInstallmentId = SessionPacketModel.PacketModel.CreditCardInstallmentId;
                    }
                    else
                    {
                        order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                    }
                }
                else
                {
                    order.AccountId = SessionPacketModel.PacketModel.AccountId;
                    order.OrderPrice = SessionPacketModel.PacketModel.MaturityCalculation(SessionPacketModel.PacketModel.OrderPrice, 18);
                }

                _orderService.InsertOrder(order);

                return RedirectToAction("FinishSales");
            }
            #endregion

        }

#if !DEBUG
                    [RequireHttps]
#endif
        [HttpPost]
        public ActionResult FourStepNew(string pan, string Ecom_Payment_Card_ExpDate_Month, string Ecom_Payment_Card_ExpDate_Year, string cv2, string cardType, string kartisim, string taksit, string tutar, string gsm, string OrderId)
        {

            MembershipIyzicoModel model = new MembershipIyzicoModel();
            tutar = tutar.Replace(',', '.');
            var order = new Order();
            if (OrderId != "0")
            {
                order = _orderService.GetOrderByOrderId(Convert.ToInt32(OrderId));
            }
            else
            {
                order = new Order
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId,
                    Address = SessionPacketModel.PacketModel.TaxAndAddressViewModel.Address,
                    PacketId = SessionPacketModel.PacketModel.PacketId,
                    OrderCode = SessionPacketModel.PacketModel.OrderCode,
                    OrderNo = SessionPacketModel.PacketModel.OrderNo,
                    TaxNo = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxNumber,
                    TaxOffice = SessionPacketModel.PacketModel.TaxAndAddressViewModel.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = SessionPacketModel.PacketModel.OrderType,
                    RecordDate = DateTime.Now,
                    OrderPacketType = (byte)OrderPacketType.Normal,
                    OrderPrice = SessionPacketModel.PacketModel.OrderPrice,
                    PriceCheck = false,
                    PacketDay = SessionPacketModel.PacketModel.PacketDay
                };
                if (SessionPacketModel.PacketModel.CreditCardInstallmentId != 0)
                    order.CreditCardInstallmentId = SessionPacketModel.PacketModel.CreditCardInstallmentId;
                _orderService.InsertOrder(order);
            }
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(member.MainPartyId);
            var address = new global::MakinaTurkiye.Entities.Tables.Common.Address();
            address = _addressService.GetFisrtAddressByMainPartyId(member.MainPartyId);

            if (address == null)
                address = _addressService.GetFisrtAddressByMainPartyId(memberStore.StoreMainPartyId.Value);
            var packet = _packetService.GetPacketByPacketId(order.PacketId);

            var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(memberStore.StoreMainPartyId.Value, PhoneTypeEnum.Gsm);

            var iyzicoPayment = new IyzicoPayment(order, member, address, packet, tutar, pan, kartisim, cv2,
                Ecom_Payment_Card_ExpDate_Month, Ecom_Payment_Card_ExpDate_Year, null, "/membershipsales/resultpay", phone, taksit);
            var result = iyzicoPayment.CreatePaymentRequest();

            //------------------------------------------------------------
            if (result.HtmlContent != null)
            {
                model.HtmlContent = result.HtmlContent;
                return View("Secure", model);
            }
            else
            {
                TempData["errorPosMessage"] = result.ErrorMessage;
                var ccl = new CreditCardLog
                {
                    MainPartyId = SessionPacketModel.PacketModel.MainPartyId
                };

                if (taksit == "00" | taksit == "0" | taksit == "")
                    ccl.OrderType = "Tek Çekim";
                else
                    ccl.OrderType = "Taksitli";

                if (result.Status == "success")
                    ccl.Status = "Başarılı";
                else
                    ccl.Status = "Başarısız";

                var cc = _creditCardService.GetCreditCardByCreditCardId(SessionPacketModel.PacketModel.CreditCardId);
                ccl.PosName = cc.CreditCardName;


                ccl.CreatedDate = DateTime.Now;
                ccl.IPAddress = Request.UserHostAddress.ToString();
                ccl.Code = result.ErrorCode;
                ccl.Detail = result.ErrorMessage;

                _creditCardLogService.InsertCreditCardLog(ccl);

                return RedirectToAction("FourStep", "membershipsales", new { messagge = "failure", order.OrderId });
            }



        }

        [HttpPost]
        [AllowAnonymous]
#if !DEBUG
        [RequireHttps]
#endif
        public ActionResult ResultPay(FormCollection frm)
        {
            Options options = new Options();
            options.ApiKey = AppSettings.IyzicoApiKey;
            options.SecretKey = AppSettings.IyzicoSecureKey;
            options.BaseUrl = AppSettings.IyzicoApiUrl;
            string paymentId = Request.Form.Get("paymentId");
            string status = Request.Form.Get("status");
            string conversationData = Request.Form.Get("conversationData");
            string conversationId = Request.Form.Get("conversationId");
            string mdStatus = Request.Form.Get("mdStatus");
            CreateThreedsPaymentRequest request = new CreateThreedsPaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = conversationId.ToString();
            request.PaymentId = paymentId;
            if (!string.IsNullOrEmpty(conversationData))
                request.ConversationData = conversationData;


            ThreedsPayment threedsPayment = ThreedsPayment.Create(request, options);
            var status1 = threedsPayment.Status;
            var order = _orderService.GetOrderByOrderId(Convert.ToInt32(conversationId));

            #region mtlog
            var ccl = new CreditCardLog();
            ccl.MainPartyId = order.MainPartyId;

            if (threedsPayment.Installment.ToString() == "1" | threedsPayment.Installment.ToString() == "0" | threedsPayment.Installment.ToString() == "")
                ccl.OrderType = "Tek Çekim";
            else
            {
                ccl.OrderType = "Taksitli";
                order.OrderType = 5; // Kredi Kartı Taksitli Fiyat
            }

            if (threedsPayment.Status == "success")
                ccl.Status = "Başarılı";
            else
                ccl.Status = "Başarısız";

            var cc = _creditCardService.GetCreditCardByCreditCardId(8);
            ccl.PosName = cc.CreditCardName;


            ccl.CreatedDate = DateTime.Now;
            ccl.IPAddress = Request.UserHostAddress.ToString();
            if (threedsPayment.ErrorCode == null)
                ccl.Code = "0";
            else ccl.Code = threedsPayment.ErrorCode;
            ccl.Detail = threedsPayment.ErrorMessage;

            _creditCardLogService.InsertCreditCardLog(ccl);

            #endregion

            if (status1 == "success")
            {
                #region payment
                var paymentM = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
                paymentM.OrderId = order.OrderId;
                paymentM.PaidAmount = Convert.ToDecimal(threedsPayment.PaidPrice);
                paymentM.PaymentType = order.OrderType;
                paymentM.RecordDate = DateTime.Now;
                paymentM.RestAmount = (order.OrderPrice - Math.Round(Convert.ToDecimal(threedsPayment.PaidPrice.Replace(".", ",")), 2));
                _orderService.InsertPayment(paymentM);
                #endregion

                var settings = ConfigurationManager.AppSettings;
                var mailsend = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                string Email = mailsend.StoreEMail;
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                var mailMessage = _messageMTService.GetMessagesMTByMessageMTName("kredikartıodememail");


                string template = mailMessage.MessagesMTPropertie;
                template = template.Replace("#uyeadisoyadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname);
                MailHelper mailHelper = new MailHelper(mailMessage.MessagesMTTitle, template, mailMessage.Mail, Email, mailMessage.MailPassword, mailMessage.MailSendFromName);
                mailHelper.Send();

                #region goldpacketmail
                settings = ConfigurationManager.AppSettings;
                var mailT = _messageMTService.GetMessagesMTByMessageMTName("goldenpro");
                template = mailT.MessagesMTPropertie;
                string dateimiz = DateTime.Now.AddDays(packet.PacketDay).ToString();
                template = template.Replace("#uyeliktipi#", packet.PacketName).Replace("#uyelikbaslangıctarihi#", DateTime.Now.ToShortDateString()).Replace("#uyelikbitistarihi#", dateimiz).Replace("#kullaniciadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname).Replace("#pakettipi#", packet.PacketName);
                var mailAnother = new MailHelper(mailT.MessagesMTTitle, template, mailT.Mail, Email, mailT.MailPassword, mailT.MailSendFromName);
                mailAnother.Send();
                #endregion

                #region packetconfirm
                var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                store.PacketId = packet.PacketId;
                store.StorePacketBeginDate = DateTime.Now;
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
                var orderLastList = _orderService.GetOrdersByMainPartyId(store.MainPartyId);
                if (!order.ProductId.HasValue)
                {
                    foreach (var item in orderLastList.ToList())
                    {
                        item.OrderPacketEndDate = store.StorePacketEndDate;
                        _orderService.UpdateOrder(item);
                    }

                    store.StorePacketEndDate = DateTime.Now.AddDays(order.PacketDay.Value);
                    _storeService.UpdateStore(store);

                }

                order.PacketStatu = (byte)PacketStatu.Onaylandi;
                order.OrderPacketEndDate = DateTime.Now.AddDays(order.PacketDay.Value);
                order.PriceCheck = true;
                _orderService.UpdateOrder(order);

                #endregion

                #region bilgimakina

                MailMessage mailb = new MailMessage();
                var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                mailb.To.Add("bilgi@makinaturkiye.com");
                mailb.Subject = "Paket Alımı " + mailsend.StoreName;

                string bilgimakinaicin = mailsend.StoreName + " isimli firma " + packet.PacketName + " paketini kredi kartı ile satın alma işlemini gerçekleştirmiştir.";
                mailb.Body = bilgimakinaicin;
                mailb.IsBodyHtml = true;
                mailb.Priority = MailPriority.Normal;
                SmtpClient scr1 = new SmtpClient();
                scr1.Port = 587;
                scr1.Host = "smtp.gmail.com";
                scr1.EnableSsl = true;
                scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                scr1.Send(mailb);

                #endregion

                ViewData["text"] = "TEBRİKLER!<br> Ödeme işleminiz tamamlandı.Paketiniz yükseltilmiştir.<br>Makinaturkiye.com'un sağlamış olduğu avantajlardan yararlanabilirsiniz.";
                return View("PosComplete");
            }
            else
            {
                TempData["errorPosMessage"] = threedsPayment.ErrorMessage;
                return RedirectToAction("FourStep", "membershipsales", new { messagge = "failure", orderId = order.OrderId });
                //  return View();
            }
        }

        public ActionResult sonuc(string sonuc)
        {

            ViewData["mesaj"] = TempData["text"];
            ViewData["mail"] = TempData["Mail"];
            return View();
        }

        public ActionResult FinishSales()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddInfoForDemand(string productNumber)
        {
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var companyDemandMemberShip = new CompanyDemandMembership();
            var storeMainParty = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var store = _storeService.GetStoreByMainPartyId(storeMainParty.StoreMainPartyId.Value);
            companyDemandMemberShip.CompanyName = store.StoreName;
            var phone = _phoneService.GetPhonesByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId).FirstOrDefault();
            if (phone != null)
            {
                companyDemandMemberShip.Phone = phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber;
            }
            companyDemandMemberShip.NameSurname = AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname;
            companyDemandMemberShip.DemandDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            companyDemandMemberShip.Statement = AuthenticationUser.CurrentUser.Membership.MemberNo + " İndirimli paket için aranma talebi ürün Sayısı:" + productNumber;
            companyDemandMemberShip.Email = AuthenticationUser.CurrentUser.Membership.MemberEmail;
            companyDemandMemberShip.Status = 0;
            companyDemandMemberShip.isDemandForPacket = true;
            _companyDemandMembership.AddCompanyDemandMembership(companyDemandMemberShip);

            var mailMessage = _messageMTService.GetMessagesMTByMessageMTName("paketdestekmaili");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(AuthenticationUser.CurrentUser.Membership.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = mailMessage.MessagesMTTitle;                                              //Mail konusu

            string template = mailMessage.MessagesMTPropertie;
            template = template.Replace("#adisoyadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname.ToUpper());
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
            sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
            sc.Send(mail);

            #region bilgimakina

            MailMessage mailb = new MailMessage();
            var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

            mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
            mailb.To.Add("bilgi@makinaturkiye.com");
            mailb.Subject = "Paketler için bilgi alma talebi " + AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname;
            //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
            //templatet = messagesmttemplate.MessagesMTPropertie;
            string bilgimakinaicin = AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname + " isimli üye üyelik paketleri için aranma talebinde bulundu.Telefon Numarası:" + phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber;

            mailb.Body = bilgimakinaicin;
            mailb.IsBodyHtml = true;
            mailb.Priority = MailPriority.Normal;
            SmtpClient scr1 = new SmtpClient();
            scr1.Port = 587;
            scr1.Host = "smtp.gmail.com";
            scr1.EnableSsl = true;
            scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
            scr1.Send(mailb);

            #endregion

            return Json(true);

        }
        [HttpPost]
        public JsonResult GetInstallmentPrice(int installment, int packetId, int orderId)
        {

            var packet = _packetService.GetPacketByPacketId(packetId);
            decimal paid = 0;
            decimal packetPrice = packet.PacketPrice;
            if (orderId != 0)
            {
                var order = _orderService.GetOrderByOrderId(orderId);
                var payments = _orderService.GetPaymentsByOrderId(orderId);

                if (payments != null)
                    paid = payments.Select(x => x.PaidAmount).Sum();
                if (paid < 0)
                    paid = 0;

                packetPrice = order.OrderPrice;
            }

            var creditCard = _creditCardService.GetAllCreditCards().FirstOrDefault();
            var crediCartInstellment = _creditCardService.GetCreditCardInstallmentsByCreditCardId(creditCard.CreditCardId).Where(ci => ci.CreditCardCount == installment).FirstOrDefault();

            packetPrice = packetPrice - paid;

            var amount = (crediCartInstellment.CreditCardCount * ((packetPrice + (packetPrice * crediCartInstellment.CreditCardValue / 100)) / Convert.ToDecimal(crediCartInstellment.CreditCardCount))).ToString("N2").Replace(".", "").Replace(",", ".");
            //taksit = crediCartInstellment.CreditCardCount.ToString();
            var tutar = amount.Replace(".", ",");
            var vadeFarki = (packetPrice * (crediCartInstellment.CreditCardValue) / 100).ToString("C2");

            var taksitSol = crediCartInstellment.CreditCardCount;
            var taksitSag = ((packetPrice + (packetPrice * crediCartInstellment.CreditCardValue / 100)) / crediCartInstellment.CreditCardCount).ToString("C2");
            string taksit = taksitSol + "X" + taksitSag;

            return Json(new { Tutar = tutar, Amount = amount, VadeFarki = vadeFarki, Taksit = taksit }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PayWithCreditCard(string priceAmount, string ProductId, string PacketId, string OrderId)
        {
            MTPayWithCreditCardModel model = new MTPayWithCreditCardModel();

            var packetModel = new PacketModel();
            int pID = Convert.ToInt32(PacketId);
            var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var order = new Order();

            if (string.IsNullOrEmpty(OrderId) && memberStore == null)
            {
                #region mtlog
                var ccl = new CreditCardLog();
                ccl.MainPartyId = memberMainPartyId;
                ccl.Detail = "Paywith creditcard memberstore null geldi ";
                var cc = _creditCardService.GetCreditCardByCreditCardId(8);
                ccl.PosName = cc.CreditCardName;


                ccl.CreatedDate = DateTime.Now;
                ccl.IPAddress = Request.UserHostAddress.ToString();

                _creditCardLogService.InsertCreditCardLog(ccl);

                #endregion

                if (TempData["OrderId"] != null)
                {
                    order = _orderService.GetOrderByOrderId(Convert.ToInt32(TempData["OrderId"]));
                    memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(order.MainPartyId);

                }
            }
            if (memberStore != null)
            {
                order = _orderService.GetOrdersByMainPartyId(memberStore.StoreMainPartyId.Value).LastOrDefault();

            }
            if (!string.IsNullOrEmpty(OrderId))
            {
                int orderId = Convert.ToInt32(OrderId);
                order = _orderService.GetOrderByOrderId(orderId);

            }
            if (!string.IsNullOrEmpty(ProductId))
            {
                if (order.ProductId != Convert.ToInt32(ProductId))
                    order = null;
            }
            if (order != null && !order.ProductId.HasValue)
            {
                var orderPriceCheck = Convert.ToBoolean(order.PriceCheck);
                var payments = _orderService.GetPaymentsByOrderId(order.OrderId);
                decimal paid = 0;
                if (payments != null)
                    paid = payments.Select(x => x.PaidAmount).Sum();
                if (paid < 0)
                    paid = 0;

                if (!orderPriceCheck)
                {

                    packetModel.OrderCode = order.OrderCode;
                    packetModel.OrderId = order.OrderId;
                    packetModel.OrderNo = order.OrderNo;
                    packetModel.OrderPrice = order.OrderPrice;
                    packetModel.PacketId = order.PacketId;
                    var packet = _packetService.GetPacketByPacketId(order.PacketId);
                    packetModel.PacketName = packet.PacketName;
                    packetModel.CreditCardInstallmentItems = _creditCardService.GetCreditCardInstallmentsByCreditCardId(8);
                    if (!string.IsNullOrEmpty(priceAmount))
                        packetModel.PayPriceAmount = Convert.ToDecimal(priceAmount.Replace(".", ","));
                    else
                        if (paid != 0)
                        packetModel.PayPriceAmount = order.OrderPrice - paid;
                    else
                        packetModel.PayPriceAmount = 0;


                    model.ProductId = 0;
                    model.IsDoping = false;
                }

            }
            else
            {
                var packet = _packetService.GetPacketByPacketId(Convert.ToInt32(PacketId));
                int day = 0;

                if (packet.DopingPacketDay.HasValue)
                {
                    day = Convert.ToInt32(packet.DopingPacketDay.Value);
                }
                else
                {
                    //log.Error("Ürün doping için doping gün sayısı bulunamadı. " + packet.PacketName);
                    throw new ArgumentNullException("packetDay");
                }
                model.DopingDay = day;
                packetModel.OrderCode = "";
                packetModel.OrderNo = "";
                packetModel.OrderPrice = packet.PacketPrice;
                packetModel.PacketId = packet.PacketId;
                packetModel.PacketName = packet.PacketName;
                packetModel.CreditCardInstallmentItems = _creditCardService.GetCreditCardInstallmentsByCreditCardId(8);
                packetModel.PayPriceAmount = 0;

                int productId = Convert.ToInt32(ProductId);
                var product = _productService.GetProductByProductId(productId);
                var productName = product.ProductName;
                model.ProductId = productId;
                model.IsDoping = true;
                model.ProductName = productName;
                if (order != null)
                {
                    packetModel.OrderId = order.OrderId;
                    packetModel.OrderNo = order.OrderNo;
                    packetModel.OrderCode = order.OrderCode;
                }
            }
            model.PacketModel = packetModel;
            return View(model);


        }
        public ActionResult BeforePayCreditCard()
        {
            var storeMainPartyId = Convert.ToInt32(_memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId).StoreMainPartyId);
            var order = _orderService.GetOrdersByMainPartyId(storeMainPartyId).LastOrDefault();
            var payment = _orderService.GetPaymentsByOrderId(order.OrderId).LastOrDefault();
            if (payment != null)
            {
                order.OrderPrice = payment.RestAmount;
            }

            return View(order);
        }
        [HttpPost]
        public ActionResult PayWithCreditCard(string pan, string OrderId, string Ecom_Payment_Card_ExpDate_Month, string orderId, string Ecom_Payment_Card_ExpDate_Year, string cv2, string cardType, string kartisim, string taksit, string tutar, string gsm, string IsDoping, string ProductId, string PacketId, string DopingDay)
        {
            MembershipIyzicoModel model = new MembershipIyzicoModel();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var member = _memberService.GetMemberByMainPartyId(mainPartyId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            var adressNew = _addressService.GetFisrtAddressByMainPartyId(store.MainPartyId);
            if (adressNew == null)
                adressNew = _addressService.GetFisrtAddressByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            tutar = tutar.Replace(',', '.');

            var order = new Order();
            if (OrderId != "0")
                order = _orderService.GetOrderByOrderId(Convert.ToInt32(OrderId));
            var packet = new Packet();
            if (order.PacketId != 0)
                packet = _packetService.GetPacketByPacketId(order.PacketId);
            else
                packet = _packetService.GetPacketByPacketId(Convert.ToInt32(PacketId));

            if (ProductId != "0" && orderId == "0") // insert product order
            {
                order = new Order
                {
                    MainPartyId = memberStore.StoreMainPartyId.Value,
                    Address = adressNew.GetFullAddress(),
                    PacketId = Convert.ToInt32(PacketId),
                    OrderCode = "",
                    OrderNo = "DP" + store.MainPartyId.ToString().PadLeft(6, '0'),
                    TaxNo = store.TaxNumber,
                    TaxOffice = store.TaxOffice,
                    PacketStatu = (byte)PacketStatu.Inceleniyor,
                    OrderType = (byte)Ordertype.KrediKarti,
                    RecordDate = DateTime.Now,
                    PacketStartDate = DateTime.Now,
                    OrderPacketType = (byte)OrderPacketType.Doping,
                    PacketDay = packet.DopingPacketDay,
                    OrderPrice = packet.PacketPrice,
                    PriceCheck = false,
                    IsRenewPacket = false,
                    ProductId = Convert.ToInt32(ProductId)

                };
                _orderService.InsertOrder(order);
            }
            var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(memberStore.StoreMainPartyId.Value, PhoneTypeEnum.Gsm);
            IyzicoPayment iyzicoPayment = new IyzicoPayment(order, member, adressNew, packet, tutar, pan, kartisim, cv2, Ecom_Payment_Card_ExpDate_Month,
                    Ecom_Payment_Card_ExpDate_Year, packet.DopingPacketDay, "/membershipsales/resultpayForCreditCard", phone, taksit);

            var paymentResult = iyzicoPayment.CreatePaymentRequest();

            //var cclRequest = new CreditCardLog();
            //cclRequest.MainPartyId = store.MainPartyId;

            //if (taksit == "00" | taksit == "0" | taksit == "")
            //    cclRequest.OrderType = "Tek Çekim";
            //else
            //    cclRequest.OrderType = "Taksitli";
            //if (paymentResult.Status == "success")
            //    cclRequest.Status = "Başarılı";
            //else
            //    cclRequest.Status = "Başarısız";
            //cclRequest.CreatedDate = DateTime.Now;
            //cclRequest.IPAddress = Request.UserHostAddress.ToString();
            //cclRequest.Code = paymentResult.ErrorCode;
            //cclRequest.Detail = Newtonsoft.Json.JsonConvert.SerializeObject(paymentResult,Newtonsoft.Json.Formatting.None);
            //_creditCardLogService.InsertCreditCardLog(cclRequest);

            if (paymentResult.HtmlContent != null)
            {
                model.HtmlContent = paymentResult.HtmlContent;
                return View("Secure", model);
            }
            else
            {
                TempData["errorPosMessage"] = paymentResult.ErrorMessage;

                var ccl = new CreditCardLog();
                ccl.MainPartyId = store.MainPartyId;

                if (taksit == "00" | taksit == "0" | taksit == "")
                    ccl.OrderType = "Tek Çekim";
                else
                    ccl.OrderType = "Taksitli";
                if (paymentResult.Status == "success")
                    ccl.Status = "Başarılı";
                else
                    ccl.Status = "Başarısız";
                var cc = _creditCardService.GetCreditCardByCreditCardId(8);
                ccl.PosName = cc.CreditCardName;
                ccl.CreatedDate = DateTime.Now;
                ccl.IPAddress = Request.UserHostAddress.ToString();
                ccl.Code = paymentResult.ErrorCode;
                ccl.Detail = paymentResult.ErrorMessage;
                _creditCardLogService.InsertCreditCardLog(ccl);
            }
            if (ProductId == "0")
            {

                if (decimal.Parse(tutar) != order.OrderPrice)
                {
                    return RedirectToAction("PayWithCreditCard", "membershipsales", new { priceAmount = tutar, OrderId = order.OrderId });

                }
            }
            else
            {
                return RedirectToAction("PayWithCreditCard", "membershipsales", new { PacketId = PacketId, DopingDay = DopingDay, OrderId = order.OrderId, ProductId = ProductId });
            }
            return RedirectToAction("PayWithCreditCard", "membershipsales", new { OrderId = order.OrderId });
        }
#if !DEBUG
        [RequireHttps]
#endif
        public ActionResult resultpayForCreditCard()
        {
            Options options = new Options();
            options.ApiKey = AppSettings.IyzicoApiKey;
            options.SecretKey = AppSettings.IyzicoSecureKey;
            options.BaseUrl = AppSettings.IyzicoApiUrl;
            string paymentId = Request.Form.Get("paymentId");
            string status = Request.Form.Get("status");
            string conversationData = Request.Form.Get("conversationData");
            string conversationId = Request.Form.Get("conversationId");
            string mdStatus = Request.Form.Get("mdStatus");
            CreateThreedsPaymentRequest request = new CreateThreedsPaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = conversationId.ToString();
            if (!string.IsNullOrEmpty(paymentId))
                request.PaymentId = paymentId;
            if (!string.IsNullOrEmpty(conversationData))
                request.ConversationData = conversationData;
            ThreedsPayment threedsPayment = ThreedsPayment.Create(request, options);
            string status1 = "error";
            decimal paidPrice = 0;
            var order = new Order();
            int orderId = Convert.ToInt32(request.ConversationId);
            order = _orderService.GetOrderByOrderId(orderId);
            if (threedsPayment != null)
            {
                paidPrice = order.OrderPrice;

                status1 = threedsPayment.Status;
                if (threedsPayment.Price != null)
                {
                    paidPrice = Convert.ToDecimal(threedsPayment.PaidPrice, CultureInfo.InvariantCulture);
                }
            }
            #region mtlog
            var ccl = new CreditCardLog();
            ccl.MainPartyId = order.MainPartyId;

            if (threedsPayment.Installment.ToString() == "1" | threedsPayment.Installment.ToString() == "0" | threedsPayment.Installment.ToString() == "")
                ccl.OrderType = "Tek Çekim";
            else
                ccl.OrderType = "Taksitli";

            if (threedsPayment.Status == "success")
                ccl.Status = "Başarılı";
            else
                ccl.Status = "Başarısız";

            var cc = _creditCardService.GetCreditCardByCreditCardId(8);
            ccl.PosName = cc.CreditCardName;


            ccl.CreatedDate = DateTime.Now;
            ccl.IPAddress = Request.UserHostAddress.ToString();
            if (threedsPayment.ErrorCode == null)
                ccl.Code = "0";
            else ccl.Code = threedsPayment.ErrorCode;
            ccl.Detail = threedsPayment.ErrorMessage;
            _creditCardLogService.InsertCreditCardLog(ccl);

            #endregion

            if (status1 == "success")
            {

                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(order.MainPartyId);
                order.IyzicoPaymentId = paymentId;
                var payments = _orderService.GetPaymentsByOrderId(order.OrderId);
                decimal totalPaidAmount = 0;
                if (payments.Count > 0)
                {
                    totalPaidAmount = payments.Select(x => x.PaidAmount).Sum();
                }
                order.OrderType = (byte)Ordertype.KrediKarti;
                _orderService.UpdateOrder(order);
                var paymentM = new global::MakinaTurkiye.Entities.Tables.Checkouts.Payment();
                paymentM.OrderId = order.OrderId;
                paymentM.PaidAmount = paidPrice;
                paymentM.PaymentType = (byte)Ordertype.KrediKarti;
                paymentM.RecordDate = DateTime.Now;
                decimal restAmount = order.OrderPrice - (totalPaidAmount + paidPrice);
                paymentM.RestAmount = restAmount;
                _orderService.InsertPayment(paymentM);

                if (paymentM.RestAmount == 0)
                    order.PriceCheck = true;
                else
                    order.PriceCheck = false;

                _orderService.UpdateOrder(order);
            }

            if (status1 == "success" && !order.ProductId.HasValue)
            {
                var mailsend = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                var settings = ConfigurationManager.AppSettings;
                string Email = mailsend.StoreEMail;
                #region kredikartıodememail

                var mailMessage = _messageMTService.GetMessagesMTByMessageMTName("kredikartıodememail");

                //var account = entities.Accounts.FirstOrDefault(x => x.AccountId == order.AccountId);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(mailMessage.Mail, mailMessage.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                mail.To.Add(Email);
                //Mailin kime gideceğini belirtiyoruz
                //fiyat tl cinsinden

                mail.Subject = mailMessage.MessagesMTTitle;                                              //Mail konusu

                string template = mailMessage.MessagesMTPropertie;
                template = template.Replace("#uyeadisoyadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname).Replace("#paidprice#", paidPrice.ToString("C2"));
                mail.Body = template;                                                            //Mailin içeriği
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                sc.Send(mail);
                #endregion

                if (order.PriceCheck == true)
                {
                    #region packetconfirm
                    //hesap numnarası fiyatı

                    var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);

                    #region emailicin
                    settings = ConfigurationManager.AppSettings;
                    mail = new MailMessage();
                    var mailT = _messageMTService.GetMessagesMTByMessageMTName("goldenpro");
                    mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                    mail.To.Add(store.StoreEMail);                                                              //Mailin kime gideceğini belirtiyoruz
                    mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                    template = mailT.MessagesMTPropertie;
                    var packetStartDate = DateTime.Now;
                    if (order.PacketStartDate.HasValue)
                        packetStartDate = order.PacketStartDate.Value;
                    string dateimiz = packetStartDate.AddDays(packet.PacketDay).ToString();
                    template = template.Replace("#uyeliktipi#", packet.PacketName).Replace("#uyelikbaslangıctarihi#", packetStartDate.ToShortDateString()).Replace("#uyelikbitistarihi#", dateimiz).Replace("#kullaniciadi#", AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname).Replace("#pakettipi#", packet.PacketName);
                    mail.Body = template;                                                            //Mailin içeriği
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;
                    sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                    sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                    sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                    sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                    sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                    sc.Send(mail);
                    #endregion
                    store.PacketId = packet.PacketId;
                    var recordDate = DateTime.Now;
                    if (packet.PacketPrice > 0 && store.StorePacketEndDate > DateTime.Now)
                    {
                        recordDate = store.StorePacketEndDate.Value;
                    }
                    store.StorePacketBeginDate = recordDate;
                    _storeService.UpdateStore(store);


                    #endregion
                }
                try
                {
                    #region bilgimakina

                    MailMessage mailb = new MailMessage();

                    var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                    mailb.To.Add("makinaturkiye@makinaturkiye.com");
                    mailb.Subject = "Kredi Kartı Ödeme " + mailsend.StoreName;
                    //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                    //templatet = messagesmttemplate.MessagesMTPropertie;
                    string bilgimakinaicin = mailsend.StoreName + " isimli firma " + packet.PacketName + " paketini kredi kartı ile ödeme alma işlemini gerçekleştirmiştir.";
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    SmtpClient scr1 = new SmtpClient();
                    scr1.Port = 587;
                    scr1.Host = "smtp.gmail.com";
                    scr1.EnableSsl = true;
                    scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                    scr1.Send(mailb);

                    #endregion
                }
                catch (Exception ex)
                {
                    //log.Error(" kredi kartı ödeme ödeme bilgimakina maili hatası: " + ex.Message);
                }
                if (paidPrice == order.OrderPrice)
                {
                    ViewData["text"] = "TEBRİKLER!<br> Ödeme işleminiz tamamlandı.Paketiniz yükseltilmiştir.<br>Makinaturkiye.com'un sağlamış olduğu avantajlardan yararlanabilirsiniz.";

                }
                else
                {
                    ViewData["text"] = "TEBRİKLER! Ödeme işleminiz tamamlanmıştır.<br><p>Ödenen Miktar:<b>" + paidPrice.ToString("C2") + "</b></p>";

                }
                return View("PosComplete");
            }
            else if (status1 == "success" && order.ProductId.HasValue)
            {

                var store = _storeService.GetStoreByMainPartyId(order.MainPartyId);
                int OrderNo = 10001;

                var orderDopings = _orderService.GetAllOrders().Where(x => x.OrderPacketType == (byte)OrderPacketType.Doping);
                OrderNo = orderDopings.Count() + OrderNo;
                var packetStore = _packetService.GetPacketByPacketId(store.PacketId);

                if (store.AuthorizedId.HasValue)
                    order.AuthorizedId = store.AuthorizedId.Value;

                order.PacketStatu = (byte)PacketStatu.Onaylandi;
                _orderService.UpdateOrder(order);
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                DateTime dateBegin = DateTime.Now.Date;
                DateTime dateEnd = DateTime.Now.Date.AddDays(packet.DopingPacketDay.Value);
                var product = _productService.GetProductByProductId(order.ProductId.Value);
                product.Doping = true;
                product.ProductDopingBeginDate = dateBegin;
                product.ProductDopingEndDate = dateEnd;
                product.ProductRateWithDoping += 1000;
                _productService.UpdateProduct(product);
                SendMailForProductDoping(product);
                #region bilgimakina
                try
                {
                    MailMessage mailb = new MailMessage();

                    var mailTmpInf = _messageMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                    mailb.To.Add("makinaturkiye@makinaturkiye.com");
                    mailb.Subject = "Kredi Kartı Ödeme " + store.StoreName;

                    string bilgimakinaicin = store.StoreName + " isimli firma " + product.ProductName + " ürünü için " + packet.DopingPacketDay + " gün doping satın alıp kredi kartı ile ödeme yapma işlemini gerçekleştirmiştir.";
                    mailb.Body = bilgimakinaicin;
                    mailb.IsBodyHtml = true;
                    mailb.Priority = MailPriority.Normal;
                    SmtpClient scr1 = new SmtpClient();
                    scr1.Port = 587;
                    scr1.Host = "smtp.gmail.com";
                    scr1.EnableSsl = true;
                    scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                    scr1.Send(mailb);

                    #endregion
                }
                catch (Exception ex)
                {
                    //log.Error(" kredi kartı ödeme ödeme bilgimakina maili hatası: " + ex.Message);
                }
                ViewData["text"] = "TEBRİKLER!<br> Ödeme işleminiz tamamlandı." + product.Category.CategoryContentTitle + " kategorisinde bulunan " + product.ProductName + " isimli ürününüze " + SessionPayWithCreditCardModel.MTPayWithCreditCardModel.DopingDay + " gün süreli doping uygulanmıştır.";
                return View("PosComplete");
            }

            TempData["errorPosMessage"] = threedsPayment.ErrorMessage;
            TempData["OrderId"] = order.OrderId;
            if (threedsPayment.ErrorMessage == "paymentId gönderilmesi zorunludur")
                TempData["errorPosMessage"] = "Bir hata oluştu lütfen bankanız ile iletişime geçiniz.";
            if (!order.ProductId.HasValue) // packet order
            {
                if (paidPrice == order.OrderPrice) // pay money which we determined
                    return RedirectToAction("PayWithCreditCard", "membershipsales");
                else // pay all money screen
                    return RedirectToAction("PayWithCreditCard", "membershipsales", new { priceAmount = Convert.ToInt32(paidPrice) });
            }
            else // product doping order
            {
                var packet = _packetService.GetPacketByPacketId(order.PacketId);
                return RedirectToAction("PayWithCreditCard", "membershipsales", new { ProductId = order.ProductId, PacketId = order.PacketId, DopingDay = packet.DopingPacketDay, OrderId = order.OrderId });
            }
        }
        public void SendMailForProductDoping(Product curProduct)
        {


            string productUrl = UrlBuilder.GetProductUrl(curProduct.ProductId, curProduct.ProductName);
            string dopingBeginDate = curProduct.ProductAdvertBeginDate.Value.ToString("dd.MM.yyyy");
            string endDate = curProduct.ProductDopingEndDate.Value.ToString("dd.MM.yyyy");
            string dopingDates = string.Format("{0}-{1}", dopingBeginDate, endDate);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(curProduct.MainPartyId.Value);
            var member = _memberService.GetMemberByMainPartyId(curProduct.MainPartyId.Value);
            string memberemail = member.MemberEmail;
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            string storename = store.StoreName;
            var settings = ConfigurationManager.AppSettings;
            MailMessage mail = new MailMessage();
            var mailT = _messageMTService.GetMessagesMTByMessageMTName("dopingverildi");
            mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
            mail.To.Add(memberemail);                                                              //Mailin kime gideceğini belirtiyoruz
            mail.Subject = mailT.MessagesMTTitle;                                            //Mail konusu
            string template = mailT.MessagesMTPropertie;
            template = template.Replace("#firmaadi#", storename).Replace("#urunlink#", productUrl).Replace("#tarih#", dopingDates).Replace("#urunadi#", curProduct.ProductName).Replace("#ilanno#", curProduct.ProductNo);
            mail.Body = template;                                                            //Mailin içeriği
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
            sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
            sc.Send(mail);
        }
        #endregion
    }

}