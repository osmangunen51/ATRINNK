using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.Mvc;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Home;
using NeoSistem.MakinaTurkiye.Web.Models.Products;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    [AllowSameSite]
    public class CatologAjaxController : Controller
    {
        IProductComplainService _productComplainService;
        IMemberService _memberService;
        IProductService _productService;
        IPhoneService _phoneService;
        IMessagesMTService _messagesMTService;
        IFavoriteStoreService _favoriteStoreService;
        IFavoriteProductService _favoriteProductService;
        IStoreService _storeService;
        IWhatsappLogService _whatsappLogService;
        IProductCommentService _productCommentService;
        IProductStatisticService _productStatisticService;
        IAddressService _addressService;
        IMemberStoreService _memberStoreService;
        ICacheManager _cacheManager;
        ICategoryService _categoryService;



        public CatologAjaxController(IProductComplainService productComplainService, IMemberService memberService,
            IProductService productService, IPhoneService phoneService, IMessagesMTService messagesMTService,
            IFavoriteStoreService favoriteStoreService,
            IFavoriteProductService favoriteProductService,
            IWhatsappLogService whatsappLogService, IProductCommentService productCommentService,
            IProductStatisticService productStatisticService,
            IAddressService addressService,
            IMemberStoreService memberStoreService, IStoreService storeService,
            ICacheManager cacheManager, ICategoryService categoryService)
        {
            _productComplainService = productComplainService;
            _memberService = memberService;
            _productService = productService;
            _phoneService = phoneService;
            _messagesMTService = messagesMTService;
            _favoriteStoreService = favoriteStoreService;
            _favoriteProductService = favoriteProductService;
            _whatsappLogService = whatsappLogService;
            _productCommentService = productCommentService;
            _productStatisticService = productStatisticService;
            _addressService = addressService;
            _memberStoreService = memberStoreService;
            _storeService = storeService;
            _cacheManager = cacheManager;
            _categoryService = categoryService;
        }
        // GET: CatologAjax
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ProductComplainAdd(MTProductComplainModel model, string[] complainTypeItems)
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
                if (captchaResponse.ErrorCodes.Count <= 0) return Json(true, JsonRequestBehavior.AllowGet);

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        return Json(false, JsonRequestBehavior.AllowGet);

                    case ("invalid-input-secret"):
                        return Json(false, JsonRequestBehavior.AllowGet);

                    case ("missing-input-response"):
                        return Json(false, JsonRequestBehavior.AllowGet);

                    case ("invalid-input-response"):
                        return Json(false, JsonRequestBehavior.AllowGet);

                    default:
                        return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var member = new Member();
                    int mainPartyId = 0;
                    //üye ekleme
                    if (_memberService.GetMemberByMemberEmail(model.UserEmail) == null)
                    {

                        var mainParty = new MainParty
                        {
                            Active = false,
                            MainPartyType = (byte)MainPartyType.Firm,
                            MainPartyRecordDate = DateTime.Now,
                            MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserName.ToLower() + " " + model.UserSurname.ToLower())
                        };

                        _memberService.InsertMainParty(mainParty);
                        mainPartyId = mainParty.MainPartyId;

                        Member member1 = new Member();
                        member1.MainPartyId = mainParty.MainPartyId;
                        member1.MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserName.ToLower());
                        member1.MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.UserSurname.ToLower());
                        member1.MemberEmail = model.UserEmail;
                        member1.MemberType = (byte)MemberType.FastIndividual;
                        member1.FastMemberShipType = (byte)FastMembershipType.ProductComplain;

                        _memberService.InsertMember(member);

                        if (model.UserPhone != "")
                        {
                            var phone = new Phone();
                            if (model.UserPhone.Substring(0, 1) == "0")
                            {
                                model.UserPhone = model.UserPhone.Substring(1, model.UserPhone.Length);
                            }

                            phone.PhoneCulture = "+90";
                            phone.MainPartyId = mainParty.MainPartyId;
                            phone.PhoneAreaCode = model.UserPhone.Substring(0, 3);
                            phone.PhoneNumber = model.UserPhone.Substring(3, 7);
                            phone.PhoneType = (byte)PhoneType.Gsm;
                            phone.GsmType = 0;

                            //_memberService.AddPhoneForMember(phone);
                            _phoneService.InsertPhone(phone);
                        }

                        var memberLast = _memberService.GetMemberByMainPartyId(mainParty.MainPartyId);
                        string memberNo = "##";
                        for (int i = 0; i < 7 - memberLast.MainPartyId.ToString().Length; i++)
                        {
                            memberNo = memberNo + "0";
                        }
                        memberNo = memberNo + memberLast.MainPartyId;
                        memberLast.MemberNo = memberNo;
                        _memberService.UpdateMember(memberLast);

                    }
                    else
                    {
                        member = _memberService.GetMemberByMemberEmail(model.UserEmail);
                        mainPartyId = member.MainPartyId;

                    }

                    var productComplain = new ProductComplain();
                    if (AuthenticationUser.Membership.MainPartyId > 0)
                    {
                        productComplain.UserName = AuthenticationUser.Membership.MemberName;
                        productComplain.UserSurname = AuthenticationUser.Membership.MemberSurname;
                        productComplain.MemberMainPartyId = mainPartyId;
                        productComplain.UserEmail = AuthenticationUser.Membership.MemberEmail;
                        productComplain.IsMember = true;
                    }
                    else
                    {
                        productComplain.UserSurname = model.UserSurname;
                        productComplain.UserEmail = model.UserEmail;
                        productComplain.UserName = model.UserName;
                        productComplain.MemberMainPartyId = mainPartyId;
                        productComplain.IsMember = false;
                    }

                    productComplain.UserComment = model.UserComment;
                    productComplain.ProductId = model.ProductId;
                    productComplain.UserPhone = model.UserPhone;
                    productComplain.CreatedDate = DateTime.Now;
                    foreach (var item in complainTypeItems)
                    {
                        if (!item.Contains("false"))
                        {
                            int prmID = Convert.ToInt32(item);
                            ProductComplainDetail detail = new ProductComplainDetail();
                            detail.ProductComplainId = productComplain.ProductComplainId;
                            detail.ProductComplainTypeId = prmID;
                            productComplain.ProductComplainDetails.Add(detail);
                        }
                    }
                    _productComplainService.InsertProductComplain(productComplain);

                    #region bilgimakina

                    string complainNames = string.Empty;
                    var insertedProductComplain = _productComplainService.GetProductComplainByProductComplainId(productComplain.ProductComplainId);
                    foreach (var item in insertedProductComplain.ProductComplainDetails)
                    {
                        complainNames += string.Format("{0} ,", item.ProductComplainType.Name);
                    }
                    complainNames = complainNames.Substring(0, complainNames.Length - 2);


                    MailMessage mailb = new MailMessage();

                    MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                    mailb.From = new MailAddress(mailTmpInf.Mail, "Bilgi Makina");
                    mailb.To.Add("bilgi@makinaturkiye.com");
                    mailb.Subject = "Ürün Şikayeti " + model.ProductName;
                    string bilgimakinaicin = "<table><tr><td>Kullanıcı Adı</td><td>" + model.UserName + " " + model.UserSurname + "</td></tr><tr><td>Email</td><td>" + model.UserEmail + "</td></tr><tr><td>Şikatet Tipi</td><td>" + complainNames + "</td></tr></table>";
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
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

        }

        [HttpGet]
        public JsonResult CheckEmail(string email, string productNumber, string memberNumber)
        {
            var password = false;
            var anyUser = _memberService.GetMemberByMemberEmail(email);
            if (anyUser != null)
            {
                if (anyUser.MemberPassword != null) password = true;
                return Json(new { FastMembershipType1 = anyUser.FastMemberShipType, Password = password }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(anyUser, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddFavoriteStoreProduct(int id)
        {
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                var curFavoriteStore = new FavoriteStore
                {
                    MemberMainPartyId = AuthenticationUser.Membership.MainPartyId,
                    StoreMainPartyId = id
                };
                _favoriteStoreService.InsertFavoriteStore(curFavoriteStore);

                return Redirect(Request.UrlReferrer.ToString());

            }

            var party = _memberService.GetMainPartyByMainPartyId(id);
            var redirectUrl = string.Format("/{0}/{1}/{2}/{3}", Core.Web.Helpers.Helpers.ToUrl("Sirket"),
                                    party.MainPartyId, Core.Web.Helpers.Helpers.ToUrl(party.MainPartyFullName),
                                    Core.Web.Helpers.Helpers.ToUrl("SirketProfili"));

            TempData["RedirectUrl"] = redirectUrl;
            TempData["MessageError"] = party.MainPartyFullName + " firmasını favori satıcılıarınıza eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.";
            return RedirectToAction("logon", "Membership");
        }

        [HttpGet]
        public JsonResult AddFavoriteProduct(int ProductId)
        {
            if (AuthenticationUser.Membership != null)
            {
                if (AuthenticationUser.Membership.MainPartyId > 0)
                {

                    int mainPartyId = AuthenticationUser.Membership.MainPartyId;
                    var favoriteProduct = _favoriteProductService.GetFavoriteProductByMainPartyIdWithProductId(mainPartyId, ProductId);
                    if (favoriteProduct == null)
                    {
                        var curFavoriteProduct = new FavoriteProduct
                        {
                            MainPartyId = AuthenticationUser.Membership.MainPartyId,
                            ProductId = ProductId,
                            CreatedDate = DateTime.Now
                        };
                        _favoriteProductService.InsertFavoriteProduct(curFavoriteProduct);

                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var productItem = _productService.GetProductByProductId(ProductId);
                    string productUrl = UrlBuilder.GetProductUrl(productItem.ProductId, productItem.ProductName);

                    TempData["RedirectUrl"] = productUrl;
                    TempData["MessageError"] = "Ürünü favorilere eklemek için üye olunuz veya giriş yapınız";
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }


            var productItem1 = _productService.GetProductByProductId(ProductId);
            string productUrl1 = UrlBuilder.GetProductUrl(productItem1.ProductId, productItem1.ProductName);

            TempData["RedirectUrl"] = productUrl1;
            TempData["MessageError"] = "Görüntülemiş olduğunuz ürünü favori ürünlerinize eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.";
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public ActionResult RemoveFavoriteProduct(int ProductId)
        {
            var result = false;
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                var favoriteProduct = _favoriteProductService.GetFavoriteProductByMainPartyIdWithProductId(AuthenticationUser.Membership.MainPartyId, ProductId);
                if (favoriteProduct != null)
                {
                    _favoriteProductService.DeleteFavoriteProduct(favoriteProduct);
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]

        public ActionResult RemoveFavoriteStoreProduct(int id)
        {
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                var favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(AuthenticationUser.Membership.MainPartyId, id);
                if (favoriteStore != null)
                {
                    _favoriteStoreService.DeleteFavoriteStore(favoriteStore);
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {

                TempData["MessageError"] = "firmayı favori satıcılıarınızdan silmek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.";
                return RedirectToAction("logon", "Membership");
            }

        }


        [HttpGet]
        public ActionResult IsAuthenticate(string redirect)
        {
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                return Json(AuthenticationUser.Membership.MainPartyId > 0, JsonRequestBehavior.AllowGet);
            }
            TempData["RedirectUrl"] = redirect;
            TempData["MessageError"] = "Mesaj gönderebilmek için giriş yapmanız gerekmektedir.";
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AddWhatsappLog(int storeId)
        {
            var store = _storeService.GetStoreByMainPartyId(storeId);
            var now = DateTime.Now.Date;

            var whatsapLog = _whatsappLogService.GetWhatsappLogByMainPartyIdAndRecordDate(storeId, DateTime.Now.Date);
            if (whatsapLog != null)
            {
                whatsapLog.ClickCount += 1;
                _whatsappLogService.UpdateWhatsappLog(whatsapLog);
            }
            else
            {
                whatsapLog = new WhatsappLog();
                whatsapLog.RecordDate = DateTime.Now.Date;
                whatsapLog.ClickCount = 1;
                whatsapLog.MainPartyId = storeId;
                _whatsappLogService.InsertWhatsappLog(whatsapLog);
            }


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AddProductComment(string CommentText, byte Rate, string ProductId)
        {
            if (AuthenticationUser.CurrentUser.Membership.MainPartyId != 0)
            {
                var productComment = new ProductComment();
                productComment.CommentText = CommentText;
                productComment.Rate = Rate;
                productComment.RecordDate = DateTime.Now;
                productComment.MemberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                productComment.ProductId = Convert.ToInt32(ProductId);
                productComment.Status = false;
                productComment.Viewed = false;
                productComment.Reported = false;
                _productCommentService.InsertProductComment(productComment);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["MessageError"] = "Lütfen yorum yapmak için giriş yapınız üyeliğiniz yoksa ücretsiz üye olabilirsiniz.";
                return Json(false, JsonRequestBehavior.AllowGet);

            }

        }
        [HttpGet]
        public JsonResult ProductStatisticCreate(int productId)
        {
            string ipAdress = Request.UserHostAddress;
            //string ipAdress = "85.99.183.57";
            string country = "";
            string city = "";

            DateTime dateNow = DateTime.Now;
            DateTime recordDate = DateTime.Now;

            var locationHelper = new LocationHelper(ipAdress);


            if (SessionStatisticIds.StatisticIds.Any(x => x.Key == productId))
            {
                var dic = SessionStatisticIds.StatisticIds.FirstOrDefault(c => c.Key == productId);

                if (!string.IsNullOrEmpty(dic.Value))
                {
                    int statisticId = Convert.ToInt32(dic.Value);
                    var productStatistic = _productStatisticService.GetProductStatisticByStatisticId(statisticId);
                    if (!string.IsNullOrEmpty(productStatistic.UserCity))
                    {

                        if (productStatistic.RecordDate.Minute <= (DateTime.Now.Minute - 1))
                        {
                            productStatistic.ViewCount += 1;
                            _productStatisticService.UpdateProductStatistic(productStatistic);
                        }
                    }
                    else
                    {
                        try
                        {
                            var JSONObj = locationHelper.GetLocationFromIp();
                            city = JSONObj["regionName"].ToString();
                            country = JSONObj["country"].ToString();
                            productStatistic.UserCity = city;
                            productStatistic.UserCountry = country;
                            _productStatisticService.UpdateProductStatistic(productStatistic);
                        }
                        catch (Exception ex)
                        {

                        }

                    }


                }
            }

            return Json(new { added = true }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public PartialViewResult ProductCommentPagination(int productId, int page)
        {
            SearchModel<MTProductCommentItem> searchModel = new SearchModel<MTProductCommentItem>();
            var source = new List<MTProductCommentItem>();
            var product = _productService.GetProductByProductId(productId);
            int pageSize = 2;
            int currentPage = page;
            int pageDimension = 10;
            var productComments = product.ProductComments.Where(x => x.Status == true).OrderBy(x => x.ProductId).ToList();
            searchModel.CurrentPage = currentPage;
            searchModel.PageDimension = pageDimension;
            searchModel.TotalRecord = productComments.Count;

            foreach (var item in productComments.Skip(page * pageSize - pageSize).Take(pageDimension))
            {
                var adress = _addressService.GetFisrtAddressByMainPartyId(item.MemberMainPartyId);
                string location = "";
                if (adress == null)
                {
                    var memberStoreM = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MemberMainPartyId);
                    if (memberStoreM != null)
                    {
                        adress = _addressService.GetFisrtAddressByMainPartyId(memberStoreM.StoreMainPartyId.Value);

                    }

                }

                if (adress != null)
                    location = adress.City.CityName.ToUpper() + ", " + adress.Locality.LocalityName;
                source.Add(new MTProductCommentItem
                {
                    CommentText = item.CommentText,
                    Rate = item.Rate.Value,
                    MemberNameSurname = item.Member.MemberName + " " + item.Member.MemberSurname,
                    MemberProfilPhotoString = item.Member.MemberName.Substring(0, 1) + item.Member.MemberSurname.Substring(0, 1),
                    ProductCommentId = item.ProductId,
                    RecordDate = item.RecordDate.ToString("dd MMMM, yyyy", CultureInfo.CurrentCulture),
                    Location = location
                });
            }
            searchModel.Source = source;
            return PartialView("_ProductCommentList", searchModel);
        }


        [HttpGet]
        public PartialViewResult GetHomeSector()
        {
            string key = string.Format("makinaturkiye.home-sector-test");
            var model = _cacheManager.Get(key, () =>
            {
                var categoryService = EngineContext.Current.Resolve<ICategoryService>();
                var sectorCategories = categoryService.GetMainCategories().Where(x => x.HomeImagePath != null);
                List<MTHomeSectorItem> source = new List<MTHomeSectorItem>();
                foreach (var item in sectorCategories)
                {

                    source.Add(new MTHomeSectorItem
                    {
                        CategoryContentTitle = item.CategoryContentTitle,
                        CategoryName = item.CategoryName,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, item.CategoryContentTitle, null, string.Empty),
                        ImagePath = ImageHelper.GetHomeSectorImagePath(item.HomeImagePath)
                    });
                }
                return source;
            });

            return PartialView("_HomeSector", model);
        }

        [HttpGet]
        public JsonResult GetStoreProductComment()
        {
            if (AuthenticationUser.CurrentUser.Membership != null && AuthenticationUser.CurrentUser.Membership.MainPartyId != 0)
            {
                var mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                IProductCommentService _productCommentService = EngineContext.Current.Resolve<IProductCommentService>();
                var productComments = _productCommentService.GetProductCommentsForStoreByMemberMainPartyId(mainPartyId).Where(x => x.Viewed == false).ToList();

                return Json(productComments.Count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public PartialViewResult GetProductShowCase()
        {
            IProductService _productService = EngineContext.Current.Resolve<IProductService>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            IPictureService _pictureService = EngineContext.Current.Resolve<IPictureService>();

            var products = _productService.GetProductsByShowCase();
            List<MTHomeAdModel> source = new List<MTHomeAdModel>();
            foreach (var item in products)
            {
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                {
                    source.Add(new MTHomeAdModel
                    {
                        ProductName = item.ProductName,
                        TruncatedProductName = StringHelper.Truncate(item.ProductName, 55),
                        ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                        PicturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150)
                    });
                }

            }
            return PartialView("_HomeShowCaseProducts", source);
        }

        [HttpPost]
        public JsonResult SearchStoreCategory(string categoryName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName) && categoryName.Length >= 3)
            {
                IList<Category> categoryItems = null;
                List<SelectListItem> categoryList = new List<SelectListItem>();
                categoryItems = _categoryService.GetSPCategoryGetCategoryByCategoryName(categoryName);

                foreach (var item in categoryItems)
                {
                    string title = "";
                    if (!string.IsNullOrEmpty(item.StorePageTitle))
                        title = item.StorePageTitle;
                    else
                    {
                        title = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                    }
                    categoryList.Add(new SelectListItem
                    {
                        Text = title,
                        Value = item.CategoryId.ToString()
                    });
                }
                return Json(categoryList, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
    }
}