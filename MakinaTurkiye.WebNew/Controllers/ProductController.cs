using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.VideoHelpers;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.MailHelpers;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Utilities.Mvc;
using MakinaTurkiye.Services.Settings;

using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Products;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MakinaTurkiye.Services.Authentication;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{

    [AllowAnonymous]
    [Compress]
    public class ProductController : BaseController
    {

        #region Fields
        //private static ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly IFavoriteProductService _favoriteProductService;
        private readonly ICategoryService _categoryService;
        private readonly IMemberService _memberService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IAddressService _addressService;
        private readonly IVideoService _videoService;
        private readonly IFavoriteStoreService _favoriteStoreService;
        private readonly IPhoneService _phoneService;
        private readonly IProductComplainService _productComplainService;
        private readonly IConstantService _constantService;
        private readonly IMessageService _messageService;
        private readonly IWhatsappLogService _whatsappLogService;
        private readonly ICategoryPropertieService _categoryPropertieService;
        private readonly IStoreStatisticService _storeStatisticService;
        private readonly IProductStatisticService _productStatisticService;
        private readonly IMobileMessageService _mobileMessageService;
        private readonly IProductCatologService _productCatologService;
        private readonly IProductCommentService _productCommentService;
        private readonly IMemberSettingService _memberSettingService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly IDeletedProductRedirectService _deletedProductRedirectSerice;
        private readonly IAuthenticationService _authenticationService;
        private readonly ICertificateTypeService _cerfificateService;
  

        #endregion

        #region Ctor

        public ProductController(IProductService productService, IStoreService storeService,
            IFavoriteProductService favoriteProductService, ICategoryService categoryService,
            IMemberService memberService, IPictureService pictureService, IMemberStoreService memberStoreService,
            IAddressService addressService, IVideoService videoService, IFavoriteStoreService favoriteStoreService,
            ICategoryPropertieService categoryPropertieService, IStoreStatisticService storeStatisticService,
            IProductStatisticService productStatisticService,
            IPhoneService phoneService, IProductComplainService productComplainService,
            IConstantService constantService,
            IMessageService messageService,
            IWhatsappLogService whatsappLogService,
            IProductCommentService productCommentService,
            IMobileMessageService mobileMessageService,
            IProductCatologService productCatologService,
            IMemberSettingService memberSettingService,
            IMessagesMTService messagesMTService,
            IDeletedProductRedirectService deletedProductRedirectSerice,
            IAuthenticationService authenticationService,
            ICertificateTypeService certificateTypeService)
        {
            _productService = productService;
            _storeService = storeService;
            _favoriteProductService = favoriteProductService;
            _categoryService = categoryService;
            _memberService = memberService;
            _pictureService = pictureService;
            _memberStoreService = memberStoreService;
            _addressService = addressService;
            _videoService = videoService;
            _favoriteStoreService = favoriteStoreService;
            _phoneService = phoneService;
            _productComplainService = productComplainService;
            _constantService = constantService;
            _messageService = messageService;
            _whatsappLogService = whatsappLogService;
            _categoryPropertieService = categoryPropertieService;
            _storeStatisticService = storeStatisticService;
            _productStatisticService = productStatisticService;
            _mobileMessageService = mobileMessageService;
            _productCatologService = productCatologService;
            _productCommentService = productCommentService;
            _memberSettingService = memberSettingService;
            _messagesMTService = messagesMTService;
            _deletedProductRedirectSerice = deletedProductRedirectSerice;
            _authenticationService = authenticationService;
            _cerfificateService = certificateTypeService;
        }

        #endregion

        #region Utilities 

        private string GetNavigation(int productId, string productName, int modelId, int seriesId, int brandId, int categoryId, out string unifiedCategories, MTProductDetailViewModel model)
        {
            int entityId = 0;
            if (modelId > 0)
            {
                entityId = modelId;
            }
            else if (seriesId > 0)
            {
                entityId = seriesId;
            }
            else if (brandId > 0)
            {
                entityId = brandId;
            }
            else if (categoryId > 0)
            {
                entityId = categoryId;
            }

            var topCategories = _categoryService.GetSPTopCategories(entityId);
            unifiedCategories = topCategories.GetUnifiedCategories();

            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            foreach (var item in topCategories)
            {
                var categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                if (item.CategoryParentId == null)
                {
                    alMenu.Add(new Navigation(item.CategoryName, "/" + UrlBuilder.ToUrl(categoryNameUrl) + "-c-" + item.CategoryId.ToString(), Navigation.TargetType._self));
                }
                else if (item.CategoryType == (byte)CategoryType.ProductGroup)
                {

                    alMenu.Add(new Navigation(item.CategoryName, "/" + UrlBuilder.ToUrl(categoryNameUrl) + "-c-" + item.CategoryId.ToString(), Navigation.TargetType._self));
                }
                else if (item.CategoryType == (byte)CategoryType.Brand)
                {
                    var category = topCategories.Where(c => c.CategoryId == item.CategoryParentId).FirstOrDefault();

                    categoryNameUrl = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                    string categoryName = UrlBuilder.ToUrl(categoryNameUrl);
                    alMenu.Add(new Navigation(item.CategoryName, "/" + UrlBuilder.ToUrl(item.CategoryName) + "-" + categoryName + "-c-" + category.CategoryId + "-" + item.CategoryId.ToString(), Navigation.TargetType._self));
                }
                else if (item.CategoryType == (byte)CategoryType.Model)
                {
                    var topCatForModel = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(item.CategoryParentId));
                    var brandName = UrlBuilder.ToUrl(topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand).CategoryName);

                    if (topCategories.LastOrDefault(c => c.CategoryType == 1) != null)
                    {
                        var cat = topCategories.LastOrDefault(c => c.CategoryType == 1);
                        categoryNameUrl = !string.IsNullOrEmpty(cat.CategoryContentTitle) ? cat.CategoryContentTitle : cat.CategoryName;
                        var catName = UrlBuilder.ToUrl(categoryNameUrl);
                        alMenu.Add(new Navigation(item.CategoryName, "/" + UrlBuilder.ToUrl(item.CategoryName) + "-" + brandName + "-" + catName + "-m-" + item.CategoryId.ToString() + "-" + topCatForModel.CategoryId, Navigation.TargetType._self));
                    }

                }
                else if (item.CategoryType == (byte)CategoryType.Series)
                {
                    var brandName = UrlBuilder.ToUrl(topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand).CategoryName);
                    var cat = topCategories.LastOrDefault(c => c.CategoryType == 1);
                    if (cat != null)
                    {

                        categoryNameUrl = !string.IsNullOrEmpty(cat.CategoryContentTitle) ? cat.CategoryContentTitle : cat.CategoryName;
                        var catName = UrlBuilder.ToUrl(categoryNameUrl);
                        alMenu.Add(new Navigation(categoryNameUrl, "/" + UrlBuilder.ToUrl(categoryNameUrl) + "-" + brandName + "-" + catName + "-s-" + item.CategoryId.ToString(), Navigation.TargetType._self));
                    }
                }
                else
                {
                    alMenu.Add(new Navigation(item.CategoryName, "/" + UrlBuilder.ToUrl(categoryNameUrl) + "-c-" + item.CategoryId.ToString(), Navigation.TargetType._self));
                }
            }
            alMenu.Add(new Navigation(productName, Request.FilePath, Navigation.TargetType._self));
            model.MtJsonLdModel.Navigations = alMenu;
            StringBuilder navigation = new StringBuilder();
            navigation.AppendLine("<ol class='breadcrumb breadcrumb-mt'>");
            for (int i = 0; i < alMenu.Count; i++)
            {
                Navigation nn = (Navigation)alMenu[i];

                if (String.IsNullOrEmpty(nn.Url) || i == alMenu.Count - 1)
                {
                    navigation.AppendLine("<li class='active'>" + nn.Title + "</li>");
                }
                else
                {
                    navigation.AppendLine(" <li><a target='" + nn.Target + "' href='" + AppSettings.SiteUrl.Substring(0, AppSettings.SiteUrl.Length - 1) + nn.Url + "'>" + nn.Title + "</a></li>");
                }
            }

            navigation.AppendLine("</ol>");
            return navigation.ToString();
        }

        private void ActivationCodeSend(string Email, string activationCode, string nameSurname)
        {
            try
            {

                var settings = ConfigurationManager.AppSettings;
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
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential(mailMessage.Mail, mailMessage.MailPassword);
                sc.Send(mail);


            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ex);
            }

        }

        private void PrepareProductContactModel(MTProductDetailViewModel productModel, Product product, Store store)
        {
            ProductContactModel model = new ProductContactModel();
            model.ProductId = product.ProductId;
            model.ProductName = product.ProductName;
            model.ProductNo = product.ProductNo;
            model.ProductPictureUrl = "";
            var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
            if (picture != null)
                model.ProductPictureUrl = ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px400x300);
            model.MemberMainPartyId = Convert.ToInt32(product.MainPartyId);

            model.ProductUrl = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
            //store 

            var whatsappMessageTypes = _mobileMessageService.GetMobileMessagesByMessageType(MobileMessageTypeEnum.Whatsapp);
            string wpMessagecontent = "Merhaba #urunlinki# ilanınız için makinaturkiye.com üzerinden ulaşıyorum";
            model.WhatsappMessageItem = new MTWhatsappMessageItem { MessageContent = wpMessagecontent.Replace("#urunlinki#", model.ProductUrl), MessageName = "", MessageId = 1 };


            if (store != null)
            {
                model.StoreModel.StoreAllProductUrl = UrlBuilder.GetProductUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName);
                model.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                model.StoreModel.StoreLogoPath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 300);
                model.StoreModel.StoreName = store.StoreName;
                model.StoreModel.MainPartyId = store.MainPartyId;
                model.StoreModel.TruncateStoreName = StringHelper.Truncate(store.StoreName, 200);
                model.StoreModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);

                var phones = _phoneService.GetPhonesByMainPartyId(store.MainPartyId);
                foreach (var item in phones)
                {
                    string name = GetEnumValue<PhoneType>((int)item.PhoneType).ToString();
                    var memberSetting = _memberSettingService.GetMemberSettingsBySettingNameWithStoreMainPartyId(name, store.MainPartyId).FirstOrDefault();
                    bool showPhone = true;
                    if (memberSetting != null)
                    {
                        var now = DateTime.Now;
                        var times = memberSetting.FirstValue.Split('-');
                        string[] startTime = times[0].Split(':');
                        string[] endTime = times[1].Split(':');

                        int startH = Convert.ToInt32(startTime[0]);
                        int startM = Convert.ToInt32(startTime[1]);
                        int endH = Convert.ToInt32(endTime[0]);
                        int endM = Convert.ToInt32(endTime[1]);
                        bool startTimeCheck = now.Hour >= startH && now.Minute >= startM;
                        bool endTimeCheck = ((now.Hour < endH) || (now.Hour == endH && now.Minute < endM));
                        if (!string.IsNullOrEmpty(memberSetting.SecondValue))
                        {
                            string[] weekend = memberSetting.SecondValue.Split('-');
                            int dayofWeek = (int)DateTime.Now.DayOfWeek;
                            if (dayofWeek == 6 && weekend[0] == "0")
                            {
                                showPhone = false;
                            }
                            if ((int)now.DayOfWeek == 7 && weekend[1] == "0")
                            {
                                showPhone = false;
                            }
                        }

                        showPhone = (startTimeCheck && endTimeCheck);

                    }

                    model.StoreModel.PhoneModels.Add(new MTStorePhoneModel
                    {
                        PhoneAreaCode = item.PhoneAreaCode,
                        PhoneCulture = item.PhoneCulture,
                        PhoneNumber = item.PhoneNumber,
                        PhoneType = (PhoneType)item.PhoneType.Value,
                        ShowPhone = showPhone
                    });

                }
            }



            //member
            var member = _memberService.GetMemberByMainPartyId(product.MainPartyId.Value);
            if (member != null)
            {
                model.StoreModel.MemberName = member.MemberName;
                model.StoreModel.MemberSurname = member.MemberSurname;
                model.StoreModel.MemberNo = member.MemberNo;
                model.MemberEmail = member.MemberEmail;
            }

            productModel.ProductContanctModel = model;
        }

        private void PreviousAndNextProductFind(MTProductDetailViewModel model, Product product)
        {
            var products = _productService.GetProductsByCategoryId(product.CategoryId.Value);

            List<int> index = products.Select(x => x.ProductId).ToList();
            int indexOf = index.IndexOf(product.ProductId);
            if (indexOf > 0)
            {
                var preProduct = products.ElementAt(indexOf - 1);
                if (preProduct != null)
                {
                    model.ProductPageHeaderModel.preProductUrl = UrlBuilder.GetProductUrl(preProduct.ProductId, preProduct.ProductName);
                }
            }
            if (products.Count > (indexOf + 1))
            {
                var nextProduct = products.ElementAt(indexOf + 1);
                if (nextProduct != null)
                {
                    model.ProductPageHeaderModel.nextProductUrl = UrlBuilder.GetProductUrl(nextProduct.ProductId, nextProduct.ProductName);
                }
            }

        }

        private void PrepareProductComplain(MTProductDetailViewModel model, byte IsMember)
        {
            MTProductComplainModel productComplainModel = new MTProductComplainModel();
            var complaintTypes = _productComplainService.GetAllProductComplainType();
            foreach (var compType in complaintTypes)
            {
                var item = new MTProductComplainTypeItemModel
                {
                    ItemName = compType.Name,
                    ItemId = compType.ProductComplainTypeId,

                };
                productComplainModel.ComplainTypeItemModels.Add(item);
            }
            productComplainModel.ProductName = model.ProductDetailModel.ProductName;
            productComplainModel.ProductCityName = model.ProductDetailModel.Location;
            model.ProductComplainModel = productComplainModel;
            model.ProductComplainModel.ProductId = model.ProductDetailModel.ProductId;
            model.ProductComplainModel.IsMember = IsMember;
        }

        private void PrepareJsonLd(MTProductDetailViewModel model)
        {
            try
            {
                //var imageList = model.ProductPictureModels.Select(pictureModel => new ImageObject
                //{
                //    ContentUrl = string.IsNullOrEmpty(pictureModel.LargePath) ? null : new Uri(pictureModel.LargePath),
                //    Caption = pictureModel.ProductName
                //}).FirstOrDefault();
                //var webPageElement = new WebPageElement();
                //var productElement = new Schema.NET.Product();
                //if (model.ProductDetailModel.PriceDec > 0 && model.ProductDetailModel.ProductPriceType == (byte)ProductPriceType.Price)
                //{
                //    webPageElement.Offers = new Offer
                //    {
                //        Availability = ItemAvailability.InStock,
                //        Name = model.ProductDetailModel.ProductName.CheckNullString(),
                //        Description = model.ProductDetailModel.DeliveryStatus.CheckNullString(),
                //        Url = Request.Url,
                //        ItemOffered = new Schema.NET.Product
                //        {
                //            Category = model.ProductDetailModel.CategoryName.CheckNullString(),
                //            Brand = new Organization
                //            {
                //                Name = model.ProductStoreModel.StoreName.CheckNullString()
                //            },
                //            Name = model.ProductDetailModel.ProductName,
                //            Image = imageList,
                //            Offers = new Offer
                //            {
                //                Availability = ItemAvailability.InStock,
                //                Price = model.ProductDetailModel.PriceDec,
                //                PriceCurrency = model.ProductDetailModel.PriceDec.HasValue ? model.ProductDetailModel.ProductCurrencySymbol : "",
                //                AreaServed = "TR",
                //                ItemCondition = OfferItemCondition.NewCondition,
                //                Name = model.ProductDetailModel.ProductName,

                //            }
                //var webPage = new WebPage();


                //}
                //var webPage = new Schema.NET.WebPage();
                //if (webPageElement.Offers.Any())
                //{
                //    webPage = new Schema.NET.WebPage
                //    {
                //        Name = model.ProductDetailModel.ProductName.CheckNullString(),
                //        InLanguage = "tr-TR",
                //        Url = Request.Url,
                //        Description = model.ProductTabModel.ProductSeoDescription.CheckNullString(),
                //        Breadcrumb = JsonLdHelper.SetBreadcrumbList(model.MtJsonLdModel.Navigations),
                //        MainEntity = webPageElement
                //    };
                //}
                //else
                //{

                //}

                var webPage = new Schema.NET.WebPage
                {
                    Name = model.ProductDetailModel.ProductName.CheckNullString(),
                    InLanguage = "tr-TR",
                    Url = Request.Url,
                    Description = model.ProductTabModel.ProductSeoDescription.CheckNullString(),
                    Breadcrumb = JsonLdHelper.SetBreadcrumbList(model.MtJsonLdModel.Navigations)
                };
                model.MtJsonLdModel.JsonLdString = webPage.ToString();
            }
            catch (Exception e)
            {
                model.MtJsonLdModel.JsonLdString = string.Empty;
            }
        }

        private void PrepareSimilarProducts(MTProductDetailViewModel model)
        {
            var similarProducts = _productService.GetProductsByCategoryIdAndNonProductId(model.ProductDetailModel.CategoryId, model.ProductDetailModel.ProductId);
            var category = _categoryService.GetCategoryByCategoryId(model.ProductDetailModel.CategoryId);
            string categoryNameUrl = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
            int similarProductIndex = 1;
            model.SimilarProductModel.AllSimilarProductUrl = UrlBuilder.GetCategoryUrl(model.ProductDetailModel.CategoryId, categoryNameUrl, null, null);

            foreach (var item in similarProducts)
            {

                string smallPicturePath = string.Empty;
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                {
                    smallPicturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.x160x120);
                }
                var memberStore1 = _memberStoreService.GetMemberStoreByMemberMainPartyId(Convert.ToInt32(item.MainPartyId));
                if (memberStore1 == null) continue;
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore1.StoreMainPartyId));
                if (store == null) continue;
                var storeName = store.StoreName;
                MTSimilarProductItemModel similarProduct = new MTSimilarProductItemModel
                {
                    ProductId = item.ProductId,
                    BrandName = item.Brand != null ? item.Brand.CategoryName : string.Empty,
                    ModelName = item.Model != null ? item.Model.CategoryName : string.Empty,
                    ProductName = item.ProductName,
                    Index = similarProductIndex,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SmallPictureName = StringHelper.Truncate(item.ProductName, 80),
                    SmallPicturePah = smallPicturePath,
                    CurrencyCss = item.GetCurrencyCssName(),
                    Price = item.GetFormattedPrice(),
                    ProductContactUrl = UrlBuilder.GetProductContactUrl(item.ProductId, storeName),
                    ViewCount = item.ViewCount

                };

                model.SimilarProductModel.ProductItemModels.Add(similarProduct);
                similarProductIndex++;
            }


        }

        #endregion

        [Compress]
        public ActionResult DetailClear(int? productId, string productGroupName, string firstCategoryName, string productName, string pageType)
        {
            if (!productId.HasValue)
            {
                return RedirectToAction("HataSayfasi", "Common");
            }

            var product = _productService.GetProductByProductId(productId.Value);
            var memberStore = new MemberStore();


            if (product == null)
            {
                var deletedProduct = _deletedProductRedirectSerice.GetDeletedProductRedirectByProductId(productId.Value);
                if (deletedProduct != null)
                {
                    var category = _categoryService.GetCategoryByCategoryId(deletedProduct.CategoryId);
                    if (category != null)
                    {
                        var categoryUrlName = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                        var categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
                        return RedirectPermanent(categoryUrl);
                    }
                }
                string urlredirect = AppSettings.SiteAllCategoryUrl;
                return RedirectPermanent(urlredirect);
            }
            else
            {
                memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
                if (memberStore == null)
                {
                    string urlredirect = AppSettings.SiteAllCategoryUrl;
                    return RedirectPermanent(urlredirect);
                }
                if (product.ProductActiveType == (byte)ProductActiveType.CopKutusuYeni || product.ProductActiveType == (byte)ProductActiveType.CopKutusunda)
                {
                    var category = _categoryService.GetCategoryByCategoryId(product.CategoryId.Value);
                    if (category != null)
                    {
                        string categoryName = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                        string categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryName, null, "");
                        return RedirectPermanent(categoryUrl);
                    }
                }
            }


            var url = Request.Url.AbsolutePath;

#if !DEBUG
             url =   Request.Url.AbsoluteUri;
#endif
            var link = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);

#if !DEBUG
            link =link+Request.Url.Query;
#endif


            #region productstatisticmong

            //string ipAdress= Request.UserHostAddress;


            #endregion


            if (url != link)
                return RedirectPermanent(link);

            //SeoPageType = (byte)PageType.Product;

            ViewBag.Canonical = url;

            var model = new MTProductDetailViewModel();


            if (AuthenticationUser.CurrentUser.Membership != null && (product.MainPartyId == AuthenticationUser.CurrentUser.Membership.MainPartyId && product.ProductActiveType == (byte)ProductActiveType.Inceleniyor) || pageType == "firstadd")
            {
                model.OnlyStoreSee = true;

            }
            else model.OnlyStoreSee = false;

            string unifiedCategories = string.Empty;
            model.ProductDetailModel.ProductName = product.ProductName;
            model.ProductDetailModel.ProductId = product.ProductId;
            model.ProductDetailModel.CategoryId = product.CategoryId.Value;
            model.ProductDetailModel.ProductPriceWithDiscount = product.DiscountType.HasValue && product.DiscountType.Value != 0 ? product.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : "";

            model.ProductDetailModel.ProductActive = product.GetActiveStatus();

            #region productcookie
            if (Request.Cookies["ProductVisited"] != null)
            {

                HttpCookie cookie = Request.Cookies["ProductVisited"];

                string data = cookie.Value;

                int lastIndex = data.LastIndexOf(',');
                string newData = "";
                if (lastIndex > 0)
                {
                    newData = data.Substring(lastIndex + 1, data.Length - (lastIndex + 1));
                }

                if (newData != product.ProductId.ToString())
                {
                    if (lastIndex > 24)
                    {
                        data = data.Substring(lastIndex + 1, data.Length - (lastIndex + 1));
                    }
                    data += "," + product.ProductId;
                    //cookie.Value = data;
                    cookie.Value = data;
#if !DEBUG
      cookie.Domain = ".makinaturkiye.com";
#endif
                    cookie.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(cookie);
                }

            }
            else
            {
                List<MTProductItemRecomandation> list = new List<MTProductItemRecomandation>();
                string data = product.ProductId.ToString();


                HttpCookie cookieVisitor = new HttpCookie("ProductVisited", data);
#if !DEBUG
  cookieVisitor.Domain = ".makinaturkiye.com";
#endif

                cookieVisitor.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Add(cookieVisitor);
            }

            #endregion

            #region header

            var member = _memberService.GetMemberByMainPartyId(product.MainPartyId.Value);
            model.ProductPageHeaderModel.ProductId = product.ProductId;
            model.ProductPageHeaderModel.ProductName = product.ProductName;
            model.ProductPageHeaderModel.MainPartyId = product.MainPartyId.Value;
            model.ProductPageHeaderModel.MemberEmail = member.MemberEmail;
            model.ProductPageHeaderModel.IsActive = product.GetActiveStatus();
            PreviousAndNextProductFind(model, product);
            if (AuthenticationUser.Membership != null)
            {

                //favorite  product
                var favoriteProduct = _favoriteProductService.GetFavoriteProductByMainPartyIdWithProductId(AuthenticationUser.Membership.MainPartyId, product.ProductId);
                model.ProductPageHeaderModel.IsFavoriteProduct = favoriteProduct != null ? true : false;
            }
            unifiedCategories = string.Empty;
            model.ProductPageHeaderModel.Navigation = GetNavigation(product.ProductId, product.ProductName,
                                                                   product.ModelId.HasValue ? product.ModelId.Value : 0,
                                                                   product.SeriesId.HasValue ? product.SeriesId.Value : 0,
                                                                   product.BrandId.HasValue ? product.BrandId.Value : 0,
                                                                   product.CategoryId.HasValue ? product.CategoryId.Value : 0, out unifiedCategories, model);

            #endregion
            #region product


            model.ProductDetailModel.ProductNo = product.ProductNo;
            model.ProductDetailModel.MenseiName = product.GetMenseiText();
            model.ProductDetailModel.DeliveryStatus = product.GetOrderStatusText();
            model.ProductDetailModel.ProductType = product.GetProductTypeText();
            model.ProductDetailModel.ProductStatus = product.GetProductStatuText();
            model.ProductDetailModel.ProductStatuConstantId = Convert.ToInt32(product.ProductStatu);
            model.ProductDetailModel.ProductViewCount = Convert.ToInt64(product.ViewCount);

            if (product.Kdv != null)
                model.ProductDetailModel.Kdv = Convert.ToBoolean(product.Kdv);
            if (product.Fob != null)
                model.ProductDetailModel.Fob = Convert.ToBoolean(product.Fob);

            short.TryParse(product.UnitType, out short unitType);

            var unitTypeConst = _constantService.GetConstantByConstantId(unitType);
            if (unitTypeConst != null)
                model.ProductDetailModel.UnitTypeText = unitTypeConst.ConstantName;
            model.ProductDetailModel.IsActive = product.GetActiveStatus();
            model.ProductDetailModel.BriefDetail = product.GetBriefDetailText();
            model.ProductDetailModel.SalesType = product.GetProductSalesTypeText();
            if (product.ModelYear.HasValue && product.ModelYear.Value > 0)
            {
                model.ProductDetailModel.ModelYear = product.ModelYear.Value.ToString();
            }
            model.ProductDetailModel.WarriantyPerriod = product.WarrantyPeriod;
            if (!string.IsNullOrEmpty(model.ProductDetailModel.BriefDetail))
            {
                model.ProductDetailModel.BriefDetail = string.Format("{0} Yıl {1}", model.ProductDetailModel.WarriantyPerriod, model.ProductDetailModel.BriefDetail);
            }

            model.ProductDetailModel.AdvertBeginDate = product.GetActiveBeginDate();
            model.ProductDetailModel.AdvertEndDate = product.GetActiveEndDate();
            model.ProductDetailModel.Price = product.GetFormattedPriceWithCurrency();
            model.ProductDetailModel.PriceDec = product.ProductPrice.HasValue == true ? product.ProductPrice.Value : 0;
            model.ProductDetailModel.ProductPriceType = product.ProductPriceType;
            model.ProductDetailModel.PriceWithoutCurrency = product.GetFormattedPrice();
            model.ProductDetailModel.Currency = product.GetCurrency();
            model.ProductDetailModel.ProductCurrencySymbol = product.GetCurrencyCssName();

            model.ProductDetailModel.ButtonDeliveryStatusText = string.Format("{0} 'de Teslim", model.ProductDetailModel.DeliveryStatus);
            string categoryNameUrl = !string.IsNullOrEmpty(product.Category.CategoryContentTitle) ? product.Category.CategoryContentTitle : product.Category.CategoryName;



            if (product.Brand != null)
            {
                model.ProductDetailModel.BrandName = product.Brand.CategoryName;

                model.ProductDetailModel.BrandUrl = UrlBuilder.GetCategoryUrl(product.CategoryId.Value, categoryNameUrl, product.Brand.CategoryId, product.Brand.CategoryName);
            }

            model.ProductDetailModel.CategoryName = product.Category.CategoryName;
            model.ProductDetailModel.CategoryUrl = UrlBuilder.GetCategoryUrl(product.CategoryId.Value, categoryNameUrl, null, null);
            if (product.Model != null)
            {
                model.ProductDetailModel.ModelName = product.Model.CategoryName;
                model.ProductDetailModel.ModelUrl = UrlBuilder.GetModelUrl(product.ModelId.Value, product.Model.CategoryName, product.Brand.CategoryName, categoryNameUrl, product.Category.CategoryId);
            }
            //}
            #endregion


            #region store

            memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
            if (memberStore != null)
            {
                if (AuthenticationUser.Membership != null)
                {
                    //favorite  store
                    var favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(AuthenticationUser.Membership.MainPartyId, memberStore.StoreMainPartyId.Value);
                    model.ProductStoreModel.IsFavoriteStore = favoriteStore != null ? true : false;
                }
                model.ProductStoreModel.MainPartyId = memberStore.StoreMainPartyId.Value;
                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                if (store != null)
                {
                    model.ProductStoreModel.StoreAllProductUrl = UrlBuilder.GetProductUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    model.ProductStoreModel.StoreLogoPath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 100);
                    model.ProductStoreModel.StoreName = store.StoreName;
                    model.ProductStoreModel.TruncateStoreName = StringHelper.Truncate(store.StoreName, 200);
                    model.ProductStoreModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    model.ProductStoreModel.StoreShortName = store.StoreShortName;
                    model.ProductStoreModel.ProductCount = _productService.GetProductsByMainPartyId(Convert.ToInt32(memberStore.MemberMainPartyId)).Count;

                    model.ProductDetailModel.StoreName = store.StoreShortName;
                    model.StoreOtherProductModel.AllStoreOtherProductUrl = UrlBuilder.GetProductUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    model.ProductDetailModel.IsAllowProductSellUrl = store.IsAllowProductSellUrl.HasValue ? store.IsAllowProductSellUrl.Value : false;
                    model.ProductDetailModel.ProductSellUrl = product.ProductSellUrl;
                }

                PrepareProductContactModel(model, product, store);
            }
            if (member != null)
            {
                model.ProductStoreModel.MemberName = member.MemberName;
                model.ProductStoreModel.MemberSurname = member.MemberSurname;
                model.ProductStoreModel.MemberNo = member.MemberNo;
            }
            model.ProductStoreModel.ProductName = product.ProductName;
            model.ProductStoreModel.ProductNo = product.ProductNo;

            #endregion

            #region tab

            //map, view count, description
            model.ProductTabModel.ProductDescription = product.ProductDescription;
            model.ProductTabModel.ProductName = product.ProductName;
            model.ProductTabModel.ProductViewCount = product.ViewCount.Value;

            //if (!string.IsNullOrEmpty(product.Keywords))
            //{
            //    string searchUrl = AppSettings.SiteUrlWithoutLastSlash + "/kelime-arama?SearchText={0}";
            //    if (product.Keywords.IndexOf(",") > 0)
            //    {
            //        var keywords = product.Keywords.Split(',');

            //        foreach (var item in keywords)
            //        {
            //            model.ProductTabModel.ProductKeywords.Add(new MTProductKeywordItem
            //            {
            //                Keyword = item,
            //                Url = string.Format(searchUrl, item)
            //            });
            //        }
            //    }
            //    else
            //    {
            //        model.ProductTabModel.ProductKeywords.Add(new MTProductKeywordItem
            //        {
            //            Keyword = product.Keywords,
            //            Url = string.Format(searchUrl, product.Keywords)
            //        });
            //    }
            //}

            if (memberStore != null)
            {
                var address = _addressService.GetFisrtAddressByMainPartyId(memberStore.StoreMainPartyId.Value);
                if (address != null)
                {
                    model.ProductTabModel.MapCode = address.GetFullAddress();

                    //satici bilgileri
                    model.ProductStoreModel.CityName = address.City != null ? address.City.CityName : string.Empty;
                    model.ProductStoreModel.CountryName = address.Country != null ? address.Country.CountryName : string.Empty;
                    model.ProductStoreModel.LocalityName = address.Locality != null ? address.Locality.LocalityName : string.Empty;
                    model.ProductDetailModel.ProductContactUrl = UrlBuilder.GetProductContactUrl(model.ProductDetailModel.ProductId, model.ProductStoreModel.StoreName);
                    //ürün detay location bilgisi
                    model.ProductDetailModel.Location = product.GetLocation();
                    if (string.IsNullOrEmpty(model.ProductDetailModel.Location))
                    {
                        model.ProductDetailModel.Location = address.GetLocation();
                    }
                }
                else
                {
                    model.ProductTabModel.MapCode = product.GetFullAddress();
                }
            }
            else
            {
                model.ProductTabModel.MapCode = product.GetFullAddress();
            }
            if (!string.IsNullOrEmpty(model.ProductTabModel.MapCode))
            {
                model.ProductTabModel.MapCode = string.Format("https://maps.google.com/maps?q={0}", model.ProductTabModel.MapCode);
            }

            #endregion
            PrepareSimilarProducts(model);

            #region saticimesaj
            if (product.GetActiveStatus())
            {
                //satici mesaj
                model.ProductStoreMessageModel.ProductName = product.ProductName;
            }
            #endregion

            #region video

            if (product.GetActiveStatus())
            {
                //video
                var videos = _videoService.GetVideosByProductId(product.ProductId);
                List<MTProductVideoModel> videoModels = new List<MTProductVideoModel>();
                foreach (var item in videos)
                {
                    videoModels.Add(new MTProductVideoModel
                    {
                        VideoId = item.VideoId,
                        VideoTitle = item.Product.ProductName,
                        VideoPicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                        VideoPath = VideoHelper.GetVideoPath(item.VideoPath)
                    });
                }

                if (videoModels.Count > 0)
                {
                    int videoCount = videoModels.Count;
                    const int pageSize = 6;
                    int totalPageCount = 0;
                    int remainder = videoCount % pageSize;
                    if (remainder > 0)
                    {
                        totalPageCount = (videoCount / pageSize) + 1;
                    }
                    else
                    {
                        totalPageCount = (videoCount / pageSize);
                    }

                    var videoForPages = new Dictionary<int, List<MTProductVideoModel>>();
                    for (int i = 0; i < totalPageCount; i++)
                    {
                        var videoForPage = videoModels.Skip(i * pageSize).Take(pageSize).ToList();
                        videoForPages.Add(i, videoForPage);
                    }
                    model.ProductTabModel.VideoModels = videoForPages;
                }
            }
            #endregion

            #region sikayet

            //if (product.GetActiveStatus())
            //{
            //ProductComplain
            byte IsMember = 0;
            if (AuthenticationUser.Membership != null) { if (AuthenticationUser.Membership.MainPartyId > 0) IsMember = 1; }
            PrepareProductComplain(model, IsMember);
            //}
            #endregion

            //satıcının diğer ilanları
            var otherProducts = _productService.GetProductsByMainPartIdAndNonProductId(product.MainPartyId.Value, product.ProductId);
            int storeOtherProductIndex = 1;
            foreach (var item in otherProducts)
            {
                string smallPicturePath = string.Empty;
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                {
                    smallPicturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px100x75);
                }
                MTStoreOtherProductItemModel otherProduct = new MTStoreOtherProductItemModel
                {
                    BrandName = item.Brand != null ? item.Brand.CategoryName : string.Empty,
                    ModelName = item.Model != null ? item.Model.CategoryName : string.Empty,
                    ProductName = item.ProductName,
                    Index = storeOtherProductIndex,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SmallPictureName = StringHelper.Truncate(item.ProductName, 80),
                    SmallPicturePah = smallPicturePath,
                    CurrencyCss = item.GetCurrencyCssName(),
                    Price = item.GetFormattedPrice()
                };

                model.StoreOtherProductModel.ProductItemModels.Add(otherProduct);
                storeOtherProductIndex++;
            }


            #region producttecnicalinfo

            var productTechnicalInfos = _categoryPropertieService.GetProductPropertieValuesResultByProductId(product.ProductId);
            foreach (var item in productTechnicalInfos)
            {
                string value = item.PropertieValue;
                if (item.Type == (byte)PropertieType.MutipleOption)
                {
                    var productAttrValue = _categoryPropertieService.GetPropertieAttrByPropertieAttrId(int.Parse(item.PropertieValue));
                    if (productAttrValue != null)
                        value = productAttrValue.AttrValue;
                    else
                        value = "";
                }
                if (!string.IsNullOrEmpty(value))
                    model.ProductTabModel.MTProductTechnicalInfoItems.Add(new MTProductTechnicalInfoItem { DisplayName = item.PropertieName, Value = value });

            }

            #endregion

            #region productcatolog
            //products catolog
            var catologs = _productCatologService.GetProductCatologsByProductId(product.ProductId);
            foreach (var item in catologs)
            {
                var filePath = FileUrlHelper.GetProductCatalogUrl(item.FileName, product.ProductId);
                model.ProductTabModel.MTProductCatologs.Add(new MTProductCatologItem { CatologId = item.ProductCatologId, FilePath = filePath });

            }

            #endregion

            #region productcomment

            var productComments = _productCommentService.GetProductCommentsByProductId(product.ProductId);

            int pageDimension = 10;
            int currentPageComment = 1;
            model.ProductTabModel.MTProductComment.TotalProductComment = productComments.Count;
            var productCommentItems = new List<MTProductCommentItem>();
            foreach (var item in productComments.Skip(currentPageComment * pageDimension - pageDimension).Take(pageDimension))
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
                {
                    if (adress.City != null)
                    {
                        location = adress.City.CityName.ToUpper() + ", " + adress.Locality.LocalityName;
                    }

                }
                var memberComment = _memberService.GetMemberByMainPartyId(item.MemberMainPartyId);
                if (memberComment != null)
                {
                    productCommentItems.Add(new MTProductCommentItem
                    {
                        CommentText = item.CommentText,
                        Rate = item.Rate.Value,
                        MemberNameSurname = memberComment.MemberName + " " + memberComment.MemberSurname,
                        MemberProfilPhotoString = item.Member.MemberName.Substring(0, 1) + item.Member.MemberSurname.Substring(0, 1),
                        ProductCommentId = item.ProductId,
                        RecordDate = item.RecordDate.ToString("dd MMMM, yyyy", CultureInfo.CurrentCulture),
                        Location = location
                    });

                }


            }
            SearchModel<MTProductCommentItem> productCommentItemsModel = new SearchModel<MTProductCommentItem>();
            productCommentItemsModel.CurrentPage = currentPageComment;
            productCommentItemsModel.Source = productCommentItems;
            productCommentItemsModel.TotalRecord = productComments.Count;
            productCommentItemsModel.PageDimension = pageDimension;
            model.ProductTabModel.MTProductComment.MTProductCommentItems = productCommentItemsModel;

            #endregion

            #region picture
            //picture
            var pictures = _pictureService.GetPicturesByProductId(product.ProductId);
            foreach (var item in pictures)
            {
                model.ProductPictureModels.Add(new MTProductPictureModel
                {
                    ProductName = product.ProductName,
                    PictureName = item.PictureName,
                    LargePath = ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.px500x375),
                    SmallPath = ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.px100x75),
                    MegaPicturePath = ImageHelper.GetProductImagePath(item.ProductId.Value, item.PicturePath, ProductImageSize.pxx980)
                });
            }

            #endregion

            #region certificates
            var productCertificates = _cerfificateService.GetCertificateTypeProductsByProductId(product.ProductId);
            if (productCertificates.Count > 0)
            {
                var certificateNames = _cerfificateService.GetCertificatesByIds(productCertificates.Select(x => x.CertificateTypeId).ToList()).Select(x => x.Name);
                model.ProductDetailModel.Certificates = string.Join(",", certificateNames);
                foreach (var productCertificate in productCertificates)
                {
                    if (productCertificate.StoreCertificateId.HasValue)
                    {
                        var ceritifcate = _cerfificateService.GetCertificateTypeByCertificateTypeId(productCertificate.CertificateTypeId);
                        var picturesCertificate = _pictureService.GetPictureByStoreCertificateId(productCertificate.StoreCertificateId.Value);
                        foreach (var item in picturesCertificate)
                        {
                            model.ProductTabModel.Certificates.Add(ceritifcate.Name, AppSettings.StoreCertificateImageFolder + item.PicturePath.Replace("_certificate", "-500x800"));
                        }
                    }

                }
            }
            #endregion
            //ürün görünün güncelleme

            try
            {
                if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == productId && c.Value == SingularViewCountType.Product))
                {
                    var productStatistic = new ProductStatistic();
                    if (product.MainPartyId.HasValue)
                        productStatistic.MemberMainPartyId = product.MainPartyId.Value;

                    string ipAdress = Request.UserHostAddress;
                    DateTime dateNow = DateTime.Now;
                    DateTime recordDate = DateTime.Now;
                    productStatistic.RecordDate = dateNow;
                    productStatistic.IpAdress = ipAdress;
                    productStatistic.ProductId = model.ProductDetailModel.ProductId;
                    productStatistic.SingularViewCount = 1;
                    productStatistic.Hour = Convert.ToByte(DateTime.Now.Hour);
                    productStatistic.ViewCount = 1;
                    _productStatisticService.InsertProductStatistic(productStatistic);

                    SessionStatisticIds.StatisticIds.Add(productId.Value, productStatistic.Id.ToString());

                    _productService.CachingGetOrSetOperationEnabled = false;
                    var updatedProduct = _productService.GetProductByProductId(product.ProductId);

                    updatedProduct.SingularViewCount += 1;
                    _productService.UpdateProduct(updatedProduct);

                    SessionSingularViewCountType.SingularViewCountTypes.Add(productId.Value, SingularViewCountType.Product);
                }
            }
            finally
            {

            }
            try
            {
                _productService.CachingGetOrSetOperationEnabled = false;
                var updatedProduct = _productService.GetProductByProductId(product.ProductId);

                updatedProduct.ViewCount += 1;
                _productService.UpdateProduct(updatedProduct);
            }
            finally
            {

            }


            //seo
            //if (!string.IsNullOrWhiteSpace(model.ProductStoreModel.StoreName))
            //{
            //    CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, model.ProductStoreModel.StoreName);
            //}
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductName, model.ProductDetailModel.ProductName);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.Category, model.ProductDetailModel.CategoryName);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.Brand, model.ProductDetailModel.BrandName);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.Model, model.ProductDetailModel.ModelName);
            //if (!string.IsNullOrEmpty(model.ProductDetailModel.ModelYear))
            //{
            //    CreateSeoParameter(SeoModel.SeoProductParemeters.ModelYear, model.ProductDetailModel.ModelYear);
            //}

            //CreateSeoParameter(SeoModel.SeoProductParemeters.TopCategory, unifiedCategories);

            //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductType, model.ProductDetailModel.ProductType);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductStatu, model.ProductDetailModel.ProductStatus);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductSalesType, model.ProductDetailModel.SalesType);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.BriefDetail, model.ProductDetailModel.BriefDetail);
            //CreateSeoParameter(SeoModel.SeoProductParemeters.Price, model.ProductDetailModel.PriceWithoutCurrency);

            //if (product.City != null && !string.IsNullOrEmpty(product.City.CityName))
            //{
            //    CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, product.City.CityName);
            //}
            //else if (string.IsNullOrEmpty(model.ProductStoreModel.CityName))
            //{
            //    CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, model.ProductStoreModel.CityName);
            //}

            //if (product.Locality != null && !string.IsNullOrEmpty(product.Locality.LocalityName))
            //{
            //    CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, product.Locality.LocalityName);
            //}
            //else if (string.IsNullOrEmpty(model.ProductStoreModel.LocalityName))
            //{

            //    CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, model.ProductStoreModel.LocalityName);

            //}


            if (product.GetActiveStatus())
            {
                //var seo = SeoModel.GeneralforAll(Convert.ToByte(ViewData["SEOPAGETYPE"]), Convert.ToByte(ViewData["SeoPageSpecial"]));
                //model.ProductTabModel.ProductSeoDescription = seo.Description;
            }
            if (product.ProductActive.HasValue && product.ProductActive == true)
            {
                PrepareJsonLd(model);
            }

            return View(viewName: "DetailClear", model: model);
        }

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
        public JsonResult fastSignup(string email, string password)
        {
            var member = _memberService.GetMemberWithLogin(email, password);
            if (member != null)
            {
                _authenticationService.SignIn(member, true);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(member, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult FastMember(MembershipModel model, string PhoneNumber, int? productId, string memberEmail1, string content, string subject)
        {

            var member = new Member();
            var PhoneGsmResult = new Phone();

            string activCode = Guid.NewGuid().ToString("N").ToUpper();


            string activationCodePhone = "";

            var anyUser = _memberService.GetMemberByMemberEmail(model.MemberEmail);
            if (anyUser != null)
            {
                return Json(new
                {
                    ActivationCode = "",
                    MainPartyId = "Exist",
                    MemberName = "",
                    MemberEmail = ""
                }, JsonRequestBehavior.AllowGet);
            }

            var mainParty = new MainParty
            {
                Active = false,
                MainPartyType = (byte)MainPartyType.Firm,
                MainPartyRecordDate = DateTime.Now,
                MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberName.ToLower() + " " + model.MemberSurname.ToLower())
            };
            _memberService.InsertMainParty(mainParty);

            member = new Member
            {
                MainPartyId = mainParty.MainPartyId,
                Gender = model.Gender,
                MemberEmail = model.MemberEmail,
                MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberName.ToLower()),
                MemberPassword = model.MemberPassword,
                MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.MemberSurname.ToLower()),
                MemberType = (byte)MemberType.FastIndividual,
                Active = false,
                MemberTitleType = model.MemberTitleType,
                ActivationCode = activCode,
                FastMemberShipType = (byte)FastMembershipType.Phone
            };

            if (model.Year > 0 && model.Month > 0 && model.Day > 0)
            {
                var bithDate = new DateTime(model.Year, model.Month, model.Day);
                member.BirthDate = bithDate;
            }
            else
                model.BirthDate = null;

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
                Avenue = model.Avenue,
                Street = model.Street,
                DoorNo = model.DoorNo,
                ApartmentNo = model.ApartmentNo,
                AddressDefault = true,
            };


            if (model.CountryId > 0 && model.CountryId != 246)
            {
                address.Avenue = model.AvenueOtherCountries;
            }

            if (model.CountryId > 0)
                address.CountryId = model.CountryId;
            else
                address.CountryId = null;

            if (model.AddressTypeId > 0)
                address.AddressTypeId = model.AddressTypeId;
            else
                address.AddressTypeId = null;

            if (model.CityId > 0)
                address.CityId = model.CityId;
            else
                address.CityId = null;

            if (model.LocalityId > 0)
                address.LocalityId = model.LocalityId;
            else
                address.LocalityId = null;

            if (model.TownId > 0)
                address.TownId = model.TownId;
            else
                address.TownId = null;


            _addressService.InsertAdress(address);

            if (model.InstitutionalPhoneNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber))
            {
                var phone1 = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode,
                    PhoneCulture = model.InstitutionalPhoneCulture,
                    PhoneNumber = model.InstitutionalPhoneNumber,
                    PhoneType = (byte)PhoneType.Phone,
                    GsmType = null
                };
                _phoneService.InsertPhone(phone1);
            }

            if (model.InstitutionalPhoneNumber2 != null && !string.IsNullOrWhiteSpace(model.InstitutionalPhoneNumber2))
            {
                var phone2 = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.InstitutionalPhoneAreaCode2,
                    PhoneCulture = model.InstitutionalPhoneCulture2,
                    PhoneNumber = model.InstitutionalPhoneNumber2,
                    PhoneType = (byte)PhoneType.Phone,
                    GsmType = null
                };
                _phoneService.InsertPhone(phone2);
            }

            if (PhoneNumber != "")
            {
                PhoneNumber = PhoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                var phoneGsmAreaCode = PhoneNumber.Substring(0, 3);
                var phoneNumber = PhoneNumber.Substring(3);

                var phoneGsm = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = phoneGsmAreaCode,
                    PhoneCulture = "+90",
                    PhoneNumber = phoneNumber,
                    PhoneType = (byte)PhoneType.Gsm,
                    GsmType = model.GsmType
                };

                SmsHelper sms = new SmsHelper();
                activationCodePhone = sms.CreateActiveCode();
                MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");
                string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname).Replace("#aktivasyonkodu#", activationCodePhone);
                sms.SendPhoneMessage("+90" + PhoneNumber, messageMobile);

                PhoneGsmResult = phoneGsm;
                phoneGsm.ActivationCode = activationCodePhone;

                _phoneService.InsertPhone(phoneGsm);

            }

            if (model.InstitutionalFaxNumber != null && !string.IsNullOrWhiteSpace(model.InstitutionalFaxNumber))
            {
                var phoneFax = new Phone
                {
                    MainPartyId = mainParty.MainPartyId,
                    PhoneAreaCode = model.InstitutionalFaxAreaCode,
                    PhoneCulture = model.InstitutionalFaxCulture,
                    PhoneNumber = model.InstitutionalFaxNumber,
                    PhoneType = (byte)PhoneType.Fax,
                    GsmType = null
                };
                _phoneService.InsertPhone(phoneFax);
            }


            var receiverMainPartyId = _memberService.GetMemberByMemberEmail(memberEmail1).MainPartyId;

            SendMessageError sendMessage = new SendMessageError();
            sendMessage.MessageContent = content;
            sendMessage.MessageSubject = subject;
            sendMessage.ProductID = productId.Value;
            sendMessage.SenderID = Convert.ToInt32(member.MainPartyId);
            sendMessage.ReceiverID = receiverMainPartyId;
            sendMessage.ErrorDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            _messageService.InsertSendMessageError(sendMessage);

            SessionMembershipViewModel.Flush();



            return Json(new
            {
                ActivationCode = member.ActivationCode,
                ActivationCodePhone = activationCodePhone,
                MainPartyId = member.MainPartyId,
                MemberName = member.MemberName,
                MemberEmail = member.MemberEmail,
                PhoneNumber = PhoneGsmResult.PhoneCulture + " " + PhoneGsmResult.PhoneAreaCode + " " + PhoneGsmResult.PhoneNumber,
                MessageId = sendMessage.MessageID
            }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public JsonResult SendMessageErrorAgain(string MessageId, string PhoneNumber)
        {
            SendMessageError messageError = new SendMessageError();
            int messageId = Convert.ToInt32(MessageId);
            messageError = _messageService.GetSendMessageErrorByMessageId(messageId);
            var member = _memberService.GetMemberByMainPartyId(messageError.SenderID);
            if (messageError != null)
            {


                if (PhoneNumber != "")
                {
                    PhoneNumber = PhoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                    var phoneGsmAreaCode = PhoneNumber.Substring(0, 3);
                    var phoneNumber = PhoneNumber.Substring(3);

                    var phoneGsm = _phoneService.GetPhonesByMainPartyIdByPhoneType(messageError.SenderID, PhoneTypeEnum.Gsm);

                    SmsHelper sms = new SmsHelper();
                    string activationCodePhone = sms.CreateActiveCode();


                    MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("telefononayi");

                    string messageMobile = messageTmp.MessageContent.Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname).Replace("#aktivasyonkodu#", activationCodePhone);
                    sms.SendPhoneMessage("+90" + PhoneNumber, messageMobile);
                    var phoneGsmAreaCode1 = PhoneNumber.Substring(0, 3);
                    var phoneNumber1 = PhoneNumber.Substring(3);
                    phoneGsm.PhoneAreaCode = phoneGsmAreaCode1;
                    phoneGsm.PhoneNumber = phoneNumber1;
                    phoneGsm.ActivationCode = activationCodePhone;
                    _phoneService.UpdatePhone(phoneGsm);
                    return Json(new
                    {
                        MainPartyId = member.MainPartyId,
                        phoneNumber = PhoneNumber,
                        ActivationCode = activationCodePhone
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }

        }

        [HttpPost]
        public string ProductMessageSender(string content, string subject, int? productId, string error, string phoneCode, int senderMainPartyId, string memberEmail, string MailActivationValue)
        {

            int receverMainPartyId = _memberService.GetMemberByMemberEmail(memberEmail).MainPartyId;

            if (MailActivationValue == "0" || MailActivationValue == "")
            {
                if (error == "0")
                {
                    var phone = _phoneService.GetPhonesByMainPartyId(senderMainPartyId).FirstOrDefault();
                    var member = _memberService.GetMemberByMainPartyId(senderMainPartyId);
                    string message = member.MemberNo + " " + phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber + " GK:" + phoneCode + " ÜK:" + phone.ActivationCode; ;
                    //log.Info(message);
                    return "hatali";
                }
                else
                {

                    var phoneItem = _phoneService.GetPhonesByMainPartyId(senderMainPartyId).FirstOrDefault();
                    phoneItem.active = 1;
                    _phoneService.UpdatePhone(phoneItem);

                    string messageContent = "";
                    int messageId = 0;

                    var curMessage = new Message
                    {
                        Active = true,
                        MessageContent = content,
                        MessageDate = DateTime.Now,
                        MessageRead = false,
                        MessageSubject = subject,
                        ProductId = productId.Value
                    };
                    messageContent = curMessage.MessageContent;
                    //burda boş gelirse kontrolnü yap.
                    _messageService.InsertMessage(curMessage);


                    var user = _memberService.GetMemberByMainPartyId(senderMainPartyId);
                    user.Active = true;
                    _memberService.UpdateMember(user);

                    messageId = curMessage.MessageId;
                    var messageMainParty = new MessageMainParty
                    {
                        MainPartyId = senderMainPartyId,
                        MessageId = messageId,
                        InOutMainPartyId = receverMainPartyId,
                        MessageType = (byte)MessageType.Outbox,
                    };
                    _messageService.InsertMessageMainParty(messageMainParty);

                    var curMessageMainParty = new MessageMainParty
                    {
                        InOutMainPartyId = senderMainPartyId,
                        MessageId = messageId,
                        MainPartyId = receverMainPartyId,
                        MessageType = (byte)MessageType.Inbox,
                    };
                    _messageService.InsertMessageMainParty(curMessageMainParty);


                    var sendMessageErrors = _messageService.GetAllSendMessageErrors(senderMainPartyId, subject, receverMainPartyId);
                    if (sendMessageErrors.Any())
                    {
                        var sendMessageError = sendMessageErrors.FirstOrDefault();
                        _messageService.DeleteSendMessageError(sendMessageError);

                    }

                    var product = _productService.GetProductByProductId(productId.Value);

                    var kullaniciemail = _memberService.GetMemberByMainPartyId(receverMainPartyId);
                    string mailadresifirma = kullaniciemail.MemberEmail.ToString();
                    string productName = product.ProductName.ToString();
                    var productno = product.ProductNo;

                    string categoryModelName = "";
                    string brandName = "";

                    var categoryModel = product.Model;
                    if (categoryModel != null)
                        categoryModelName = categoryModel.CategoryName;

                    var categorBrand = product.Brand;
                    if (categorBrand != null)
                        brandName = categorBrand.CategoryName;

                    string productnosub = productName + " " + brandName + " " + categoryModelName + " İlan no:" + productno;
                    string productUrl = NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.ProductUrl(product.ProductId, productName);
                    LinkHelper linkHelper = new LinkHelper();
                    string encValue = linkHelper.Encrypt(receverMainPartyId.ToString());
                    string messageLink = "/Account/Message/Detail/" + messageId + "?RedirectMessageType=0";
                    string loginauto = AppSettings.SiteUrlWithoutLastSlash + "/MemberShip/LogonAuto?validateId=" + encValue + "&returnUrl=" + messageLink;
                    try
                    {
                        MessagesMT mailTemplate = _messagesMTService.GetMessagesMTByMessageMTName("mesajınızvar");

                        string templatet = mailTemplate.MessagesMTPropertie;
                        templatet = templatet.Replace("#kullaniciadi", kullaniciemail.MemberName + " " + kullaniciemail.MemberSurname).Replace("#urunadi", productName).Replace("#email#", mailadresifirma).Replace("#link", productUrl).Replace("#ilanno", productno).Replace("#producturl#", productUrl).Replace("#messagecontent#", messageContent).Replace("#loginautolink#", loginauto);

                        MailHelper mailHelper = new MailHelper(productnosub, templatet, mailTemplate.Mail, mailadresifirma, mailTemplate.MailPassword, mailTemplate.MailSendFromName);
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(receverMainPartyId);
                        if (memberStore != null)
                        {
                            var memberMainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Where(x => x.MemberMainPartyId != receverMainPartyId).Select(x => x.MemberMainPartyId).ToList();
                            var members = _memberService.GetMembersByMainPartyIds(memberMainPartyIds).Select(x => x.MemberEmail).ToList();
                            members.ForEach(x => mailHelper.ToMails.Add(x));
                        }
                        mailHelper.Send();


                    }
                    catch (Exception ex)
                    {
                        //log.Error("Mesajınız var maili " + mailadresifirma + " mail adresine gönderilemedi. " + ex.Message);
                    }

                    var member = _memberService.GetMemberByMainPartyId(senderMainPartyId);

                    _authenticationService.SignIn(member, true);

                    ////AuthenticationUser.Membership = member;
                    //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");
                    //var ctx = Request.GetOwinContext();
                    //var authManager = ctx.Authentication;
                    //authManager.SignIn(identity);
                    //HttpCookie MainPartyId = new HttpCookie("mtCookie");
                    //Response.Cookies["MainPartyId"].Value = member.MainPartyId.ToString();
                    //if (Request.Cookies["MainPartyId"] != null)
                    //{
                    //    Response.Cookies["MainPartyId"].Expires = DateTime.Now.AddDays(1);
                    //    Response.Cookies.Add(MainPartyId);
                    //}

                    return "basarili";
                }
            }
            else
            {
                var member = _memberService.GetMemberByMainPartyId(senderMainPartyId);
                ActivationCodeSend(member.MemberEmail, member.ActivationCode, member.MemberName + " " + member.MemberSurname);
                return "MailActivation";
            }
        }


        [HttpPost]
        public JsonResult fastSignupPhone1(string activationCode)
        {
            var phone = _phoneService.GetPhoneByActivationCode(activationCode);
            if (phone != null)
            {
                if (phone.ActivationCode == activationCode)
                {
                    var member = _memberService.GetMemberByMainPartyId(phone.MainPartyId.Value);
                    _authenticationService.SignIn(member, true);
                    //var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");
                    //var ctx = Request.GetOwinContext();
                    //var authManager = ctx.Authentication;
                    //authManager.SignIn(identity);
                    phone.active = 1;

                    _phoneService.UpdatePhone(phone);

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult fastSignupPhone(string Email)
        {

            var member = _memberService.GetMemberByMemberEmail(Email);
            if (member != null)
            {
                SmsHelper sms = new SmsHelper();
                string ActivationCode = sms.CreateActiveCode();

                var phone = _phoneService.GetPhonesByMainPartyId(member.MainPartyId).FirstOrDefault();

                phone.ActivationCode = ActivationCode;
                _phoneService.UpdatePhone(phone);

                MobileMessage messageTmp = _mobileMessageService.GetMobileMessageByMessageName("tekkullanimliksifre");

                string message = messageTmp.MessageContent.Replace("#aktivasyonkodu#", ActivationCode).Replace("#isimsoyisim#", member.MemberName + " " + member.MemberSurname);
                sms.SendPhoneMessage(phone.PhoneCulture + phone.PhoneAreaCode + phone.PhoneNumber, message);
                return Json(new
                {
                    result = true,
                    ActivationCode1 = ActivationCode
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Detail(ProductDetailViewModel model)
        {
            return View(model);
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

        [HttpPost]
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

        [HttpPost]
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
            return Json(result);
        }

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
        public ActionResult ProductContact(int productId)
        {//AdilD
            var model = new ProductContactModel();

            //product 
            var product = _productService.GetProductByProductId(productId);
            model.ProductId = product.ProductId;
            model.ProductName = product.ProductName;
            model.ProductNo = product.ProductNo;
            model.ProductPictureUrl = "";
            var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
            if (picture != null)
                model.ProductPictureUrl = ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px400x300);
            model.MemberMainPartyId = Convert.ToInt32(product.MainPartyId);

            model.ProductUrl = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
            //store 
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
            if (memberStore != null)
            {
                model.StoreModel.MainPartyId = memberStore.StoreMainPartyId.Value;
                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                if (store != null)
                {
                    model.StoreModel.StoreAllProductUrl = UrlBuilder.GetProductUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    model.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    model.StoreModel.StoreLogoPath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 300);
                    model.StoreModel.StoreName = store.StoreName;
                    model.StoreModel.TruncateStoreName = StringHelper.Truncate(store.StoreName, 200);
                    model.StoreModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                }
                var phones = _phoneService.GetPhonesByMainPartyId(memberStore.StoreMainPartyId.Value);
                foreach (var item in phones)
                {
                    model.StoreModel.PhoneModels.Add(new MTStorePhoneModel
                    {
                        PhoneAreaCode = item.PhoneAreaCode,
                        PhoneCulture = item.PhoneCulture,
                        PhoneNumber = item.PhoneNumber,
                        PhoneType = (PhoneType)item.PhoneType.Value
                    });
                }
            }
            //member
            var member = _memberService.GetMemberByMainPartyId(product.MainPartyId.Value);
            if (member != null)
            {
                model.StoreModel.MemberName = member.MemberName;
                model.StoreModel.MemberSurname = member.MemberSurname;
                model.StoreModel.MemberNo = member.MemberNo;
                model.MemberEmail = member.MemberEmail;
            }
            return View(model);
        }

        [HttpPost]
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

        public ActionResult WrongUrlRedirect(string productId, string storeName)
        {
            return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + "/Product/ProductContact?productId=" + productId + "&storeName=" + storeName);
        }

        public ActionResult WrongUrlHelp(string categoryId)
        {
            return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + Request.Url.AbsolutePath);

        }

        [HttpPost]
        public PartialViewResult GetSimilarProducts(int categoryId, int productId)
        {

            var similarProducts = _productService.GetProductsByCategoryIdAndNonProductId(categoryId, productId);
            var category = _categoryService.GetCategoryByCategoryId(categoryId);
            string categoryNameUrl = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
            int similarProductIndex = 1;
            MTSimilarProductModel model = new MTSimilarProductModel();
            model.AllSimilarProductUrl = UrlBuilder.GetCategoryUrl(categoryId, categoryNameUrl, null, null);

            foreach (var item in similarProducts)
            {

                string smallPicturePath = string.Empty;
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                {
                    smallPicturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.x160x120);
                }
                var memberStore1 = _memberStoreService.GetMemberStoreByMemberMainPartyId(Convert.ToInt32(item.MainPartyId));
                if (memberStore1 == null) continue;
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore1.StoreMainPartyId));
                if (store == null) continue;
                var storeName = store.StoreName;
                MTSimilarProductItemModel similarProduct = new MTSimilarProductItemModel
                {
                    ProductId = item.ProductId,
                    BrandName = item.Brand != null ? item.Brand.CategoryName : string.Empty,
                    ModelName = item.Model != null ? item.Model.CategoryName : string.Empty,
                    ProductName = item.ProductName,
                    Index = similarProductIndex,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SmallPictureName = StringHelper.Truncate(item.ProductName, 80),
                    SmallPicturePah = smallPicturePath,
                    CurrencyCss = item.GetCurrencyCssName(),
                    Price = item.GetFormattedPrice(),
                    ProductContactUrl = UrlBuilder.GetProductContactUrl(item.ProductId, storeName),
                    ViewCount = item.ViewCount

                };

                model.ProductItemModels.Add(similarProduct);
                similarProductIndex++;
            }
            return PartialView("_SimilarProductNew", model);
        }

        [HttpPost]
        public JsonResult AddProductComment(string CommentText, byte Rate, string ProductId)
        {
            if (AuthenticationUser.Membership.MainPartyId != 0)
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

        [HttpPost]
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

            return Json(true, JsonRequestBehavior.AllowGet);

        }


    }
}
