using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Videos;
using MakinaTurkiye.Entities.Tables.Media;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;

using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using NeoSistem.MakinaTurkiye.Web.Models.Adverts;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Catologs;
using NeoSistem.MakinaTurkiye.Web.Models.Products;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI;

using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Services;
using MakinaTurkiye.Utilities.MailHelpers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{

    public class SessionProductPropertieModel
    {
        internal static readonly string SESSION_USERID = "MTProductPropertieModel";

        public static MTProductPropertieModel MTProductPropertieModel
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_USERID] == null)
                {
                    HttpContext.Current.Session[SESSION_USERID] = new MTProductPropertieModel();
                }
                return HttpContext.Current.Session[SESSION_USERID] as MTProductPropertieModel;
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

    public class AdvertController : BaseAccountController
    {
        private readonly IProductService _productService;
        private readonly IVideoService _videoService;
        private readonly IConstantService _constantService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IPacketService _packetService;
        private readonly ICategoryService _categoryService;
        private readonly IAddressService _addressService;
        private readonly IPictureService _pictureService;
        private readonly ICategoryPropertieService _categoryPropertieService;
        private readonly IProductCatologService _productCatologService;
        private readonly IProductCommentService _productCommentService;
        private readonly IStoreSectorService _storeSectorService;
        private readonly ICurrencyService _currencyService;
        private readonly ICertificateTypeService _certificateTypeService;
        private readonly IMemberService _memberService;



        public AdvertController(IProductService productService,
            IProductCommentService productCommentService,
            IProductCatologService productCatologService,
            ICategoryPropertieService categoryPropertieService,
            IVideoService videoService, IConstantService constantService,
            IMemberStoreService memberStoreService, IStoreService storeService,
            IPacketService packetService, ICategoryService categoryService,
            IAddressService addressService, IPictureService pictureService,
            IStoreSectorService storeSectorService, ICurrencyService currencyService, ICertificateTypeService certificateTypeService,
            IMemberService memberService)
        {
            this._productService = productService;
            this._videoService = videoService;
            this._constantService = constantService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._packetService = packetService;
            this._categoryService = categoryService;
            this._addressService = addressService;
            this._pictureService = pictureService;
            this._categoryPropertieService = categoryPropertieService;
            this._productCatologService = productCatologService;
            this._productCommentService = productCommentService;
            this._storeSectorService = storeSectorService;
            this._currencyService = currencyService;
            this._memberService = memberService;
            this._certificateTypeService = certificateTypeService;
            this._productService.CachingGetOrSetOperationEnabled = false;
            this._videoService.CachingGetOrSetOperationEnabled = false;
            this._constantService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._packetService.CachingGetOrSetOperationEnabled = false;
            this._categoryService.CachingGetOrSetOperationEnabled = false;
            this._addressService.CachingGetOrSetOperationEnabled = false;
            this._pictureService.CachingGetOrSetOperationEnabled = false;
            this._categoryPropertieService.CachingGetOrSetOperationEnabled = false;
            this._productCatologService.CachingGetOrSetOperationEnabled = false;
            this._productCommentService.CachingGetOrSetOperationEnabled = false;
            this._currencyService.CachingGetOrSetOperationEnabled = false;
            this._certificateTypeService.CachingGetOrSetOperationEnabled = false;

        }

        int TotalRecord = 0;
        byte pageDimension = 20;
        string activeClass = "active";
        string activeStyleRed = "color: Red";

        public ActionResult IndexNew(string currentPage, string categoryId)
        {
            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                int pageDimension = 20;
                int page = 1;
                if (!string.IsNullOrEmpty(currentPage))
                    page = Convert.ToInt32(currentPage);
                byte orderType = 1;
                if (!string.IsNullOrEmpty(Request.QueryString["OrderType"]))
                {
                    orderType = Convert.ToByte(Request.QueryString["OrderType"]);
                }
                int mainPartyId = AuthenticationUser.Membership.MainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
                var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();
                var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds).OrderByDescending(x => x.ProductId).ToList();
                if (!string.IsNullOrEmpty(categoryId))
                {
                    int catId = Convert.ToInt32(categoryId);
                    products = products.Where(x => x.CategoryId == catId).ToList();
                }

                if (orderType == 3)
                {
                    var products1 = products.Where(x => x.Sort != null && x.Sort != 0).OrderBy(x => x.Sort).ToList();
                    products = products1.Concat(products.Where(x => x.Sort == null || x.Sort == 0).OrderByDescending(x => x.ViewCount)).ToList();

                }
                else if (orderType == 2)
                    products = products.OrderByDescending(x => x.ViewCount).ToList();



                var model = new MTProductViewModel();
                model.OrderType = orderType;
                byte displayType = byte.Parse(Request.QueryString["DisplayType"]);
                byte productActiveType = 0;
                if (Request.QueryString["ProductActiveType"] != null)
                    productActiveType = byte.Parse(Request.QueryString["ProductActiveType"]);
                int productActive = -1;
                if (Request.QueryString["ProductActive"] != null)
                    productActive = int.Parse(Request.QueryString["ProductActive"]);

                model.PageTitle = "Tüm İlanlarım";
                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
                bool showDopingForm = store.PacketId == 29;

                model.DisplayType = displayType;
                model.ProductActiveType = productActiveType;
                model.ProductActive = productActive;
                PrepareAdvertTopViewModel(model, displayType, productActiveType, productActive, products);

                if (productActiveType != (byte)ProductActiveType.Tumu && productActive == -1 && productActiveType >= 0)
                {
                    products = products.Where(x => x.ProductActiveType == productActiveType).ToList();
                }
                else
                {
                    products = products.Where(x => x.ProductActiveType != (byte)ProductActiveType.Silindi && x.ProductActiveType != (byte)ProductActiveType.CopKutusunda).ToList();

                    if (productActive != -1)
                    {
                        bool productAc = false;
                        if (productActive == 1) productAc = true;
                        products = products.Where(x => x.ProductActive == productAc).ToList();
                    }

                }


                int takeFrom = page * pageDimension - pageDimension;
                int totalCount = products.Count;
                products = products.Skip(takeFrom).Take(pageDimension).ToList();
                SearchModel<MTProductItem> searchModel = new SearchModel<MTProductItem>();

                searchModel.Source = PrepapareProductsModel(products,  showDopingForm);
                searchModel.TotalRecord = totalCount;
                searchModel.PageDimension = pageDimension;
                searchModel.CurrentPage = page;


                model.MTProducts = searchModel;
                model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AllAd);

                return View(model);
            }
            else
            {
                return RedirectToAction("Home");
            }

        }
        public List<MTProductItem> PrepapareProductsModel(List<global::MakinaTurkiye.Entities.Tables.Catalog.Product> products, bool showDopingForm)
        {
            List<MTProductItem> productsModel = new List<MTProductItem>();
            foreach (var item in products)
            {
                string picturePath = "";
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                    picturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150);

                var model = item.ModelId.HasValue ? _categoryService.GetCategoryByCategoryId(item.ModelId.Value) : new Category();
                var serie = item.SeriesId.HasValue ? _categoryService.GetCategoryByCategoryId(item.SeriesId.Value) : new Category();
                var brand = item.BrandId.HasValue ? _categoryService.GetCategoryByCategoryId(item.BrandId.Value) : new Category();

                productsModel.Add(new MTProductItem
                {
                    BrandName = brand != null ? brand.CategoryName : "",
                    BriefDetail = item.GetBriefDetailText(),
                    CategoryName = item.Category != null ? item.Category.CategoryName : "",
                    City = (item.City != null) ? item.City.CityName : "",
                    ImagePath = picturePath,
                    Locality = (item.Locality != null) ? item.Locality.LocalityName : "",
                    ModelName = model != null ? model.CategoryName : "",
                    ModelYear = item.ModelYear.HasValue == true ? item.ModelYear.Value.ToString() : DateTime.Now.Year.ToString(),
                    ProductActive = item.ProductActive.Value,
                    ProductActiveType = item.ProductActiveType.HasValue == true ? item.ProductActiveType.Value : (byte)0,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductNo = item.ProductNo,
                    ProductPrice = item.GetFormattedPrice(),
                    ProductStatusText = item.GetProductStatuText(),
                    ProductTypeText = item.GetProductTypeText(),
                    SeriesName = serie.CategoryName != null ? serie.CategoryName : "",
                    SalesTypeText = item.GetProductSalesTypeText(),
                    ProductPriceType = item.ProductPriceType != null ? item.ProductPriceType.Value : (byte)0,
                    ViewCount = item.ViewCount.Value,
                    CurrencyId = item.CurrencyId.HasValue == true ? item.CurrencyId.Value : (byte)0,
                    CurrencyCssText = item.GetCurrencyCssName(),
                    Doping = item.Doping,
                    ShowDopingForm = showDopingForm,
                    ProductPriceWithDiscount = item.DiscountType.HasValue && item.DiscountType.Value != 0 && item.ProductPriceWithDiscount != null ? item.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : ""
                });
            }

            return productsModel;

        }

        public void PrepareAdvertTopViewModel(MTProductViewModel model, int displayType, int productActiveType, int productActive, IList<global::MakinaTurkiye.Entities.Tables.Catalog.Product> products)
        {


            MTAdvertFilterItemModel itemTumu = new MTAdvertFilterItemModel();
            itemTumu.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.Tumu + "&DisplayType=" + displayType;
            itemTumu.FilterName = "Tüm İlanlar(" + products.Count + ")";
            if (productActiveType == (byte)ProductActiveType.Tumu)
                itemTumu.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemTumu);

            MTAdvertFilterItemModel itemAktif = new MTAdvertFilterItemModel();
            itemAktif.FilterUrl = "/Account/Advert/Index?ProductActive=" + (byte)ProductActive.Aktif + "&DisplayType=" + displayType;
            itemAktif.FilterName = "Aktif İlanlar(" + products.Where(x => x.ProductActive == true).Count() + ")";
            if (productActive == (byte)ProductActive.Aktif)
            {
                itemAktif.CssClass = activeClass;

            }


            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemAktif);

            MTAdvertFilterItemModel itemPasif = new MTAdvertFilterItemModel();
            itemPasif.FilterUrl = "/Account/Advert/Index?ProductActive=" + (byte)ProductActive.Pasif + "&DisplayType=" + displayType;
            itemPasif.FilterName = "Pasif İlanlar(" + products.Where(x => x.ProductActive == false).Count() + ")";
            if (productActive == (byte)ProductActive.Pasif)
                itemPasif.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemPasif);

            MTAdvertFilterItemModel itemConfirmWaiting = new MTAdvertFilterItemModel();
            itemConfirmWaiting.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.Inceleniyor + "&DisplayType=" + displayType;
            itemConfirmWaiting.FilterName = "Onay Bekleyen İlanlar(" + products.Where(x => x.ProductActiveType == (byte)ProductActiveType.Inceleniyor).Count() + ")";
            if (productActiveType == (byte)ProductActiveType.Inceleniyor)
                if (productActive == -1)
                    itemConfirmWaiting.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemConfirmWaiting);


            MTAdvertFilterItemModel itemConfirmed = new MTAdvertFilterItemModel();
            itemConfirmed.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.Onaylandi + "&DisplayType=" + displayType;
            itemConfirmed.FilterName = "Onaylanmış İlanlar(" + products.Where(x => x.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count() + ")";
            if (productActiveType == (byte)ProductActiveType.Onaylandi)
                itemConfirmed.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemConfirmed);

            MTAdvertFilterItemModel itemNotConfirmed = new MTAdvertFilterItemModel();
            itemNotConfirmed.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.Onaylanmadi + "&DisplayType=" + displayType;
            itemNotConfirmed.FilterName = "Onaylanmamış İlanlar(" + products.Where(x => x.ProductActiveType == (byte)ProductActiveType.Onaylanmadi).Count() + ")";
            if (productActiveType == (byte)ProductActiveType.Onaylanmadi)
                itemNotConfirmed.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemNotConfirmed);

            MTAdvertFilterItemModel itemDeleted = new MTAdvertFilterItemModel();
            itemDeleted.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.Silindi + "&DisplayType=" + displayType;
            itemDeleted.FilterName = "Silinen İlanlar(" + products.Where(x => x.ProductActiveType == (byte)ProductActiveType.Silindi).Count() + ")";
            if (productActiveType == (byte)ProductActiveType.Silindi)
                itemDeleted.CssClass = activeClass;

            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemDeleted);


            MTAdvertFilterItemModel itemGarbage = new MTAdvertFilterItemModel();
            itemGarbage.FilterUrl = "/Account/Advert/Index?ProductActiveType=" + (byte)ProductActiveType.CopKutusunda + "&DisplayType=" + displayType;
            itemGarbage.FilterName = "Çöp İlanlar(" + products.Where(x => x.ProductActiveType == (byte)ProductActiveType.CopKutusunda).Count() + ")";
            if (productActiveType == (byte)ProductActiveType.CopKutusunda)
                itemGarbage.CssClass = activeClass;

            model.MTAdvertsTopViewModel.TotalProductCount = products.Count;
            model.MTAdvertsTopViewModel.MTAdvertFilterItemModel.Add(itemGarbage);


            var orderType = Request.QueryString["OrderType"];
            byte ordert = 1;

            string urlPar = "";
            if (!string.IsNullOrEmpty(orderType))
            {
                ordert = Convert.ToByte(orderType);
                urlPar = Request.Url.ToString().Replace("OrderType=" + ordert, "OrderType={0}");
            }
            else
            {
                urlPar = Request.Url + "&OrderType={0}";
            }

            MTAdvertFilterItemModel lastFilter = new MTAdvertFilterItemModel();
            lastFilter.FilterName = "Son Eklenen";
            lastFilter.FilterUrl = string.Format(urlPar, "1");
            if (ordert == 1)
                lastFilter.CssClass = "active";
            model.MTAdvertsTopViewModel.MTOrderFilter.Add(lastFilter);

            MTAdvertFilterItemModel viewInc = new MTAdvertFilterItemModel();
            viewInc.FilterName = "Görüntülenme Sayısına Göre Azalan";
            viewInc.FilterUrl = string.Format(urlPar, "2");
            if (ordert == 2)
                viewInc.CssClass = "active";
            model.MTAdvertsTopViewModel.MTOrderFilter.Add(viewInc);

            MTAdvertFilterItemModel viewDec = new MTAdvertFilterItemModel();
            viewDec.FilterName = "Sıralama Ayarlarına Göre";
            viewDec.FilterUrl = string.Format(urlPar, "3");
            if (ordert == 3)
                viewDec.CssClass = "active";
            model.MTAdvertsTopViewModel.MTOrderFilter.Add(viewDec);

            var categoryIds = products.Select(x => x.CategoryId.Value).Distinct().ToList();


            var categories = _categoryService.GetCategoriesByCategoryIds(categoryIds);
            foreach (var item in categories)
            {
                var filterUrl = Request.Url.ToString();
                if (filterUrl.IndexOf("categoryId") > 0)
                {
                    filterUrl = Regex.Replace(filterUrl, @"([?&]categoryId)=[^?&]+", "$1=" + item.CategoryId);
                }
                else
                    filterUrl += "&categoryId=" + item.CategoryId;

                model.MTAdvertsTopViewModel.MTCategoriesFilter.Add(new MTAdvertFilterItemModel
                {
                    FilterName = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName,
                    FilterUrl = filterUrl
                });
            }
        }

        [HttpGet]
        public string ProductSearchGet(string productName, string productActiveType, string productActivee)
        {

            ICollection<ProductModel> data = new ProductModel[] { };
            var dataProduct = new Data.Product();
            var currentPage = 1;
            try
            {
                if (Request.QueryString["currentPage"] != null)
                {
                    currentPage = Int32.Parse(Request.QueryString["currentPage"]);
                }
                else
                {

                    if (Request.UrlReferrer != null)
                    {

                        currentPage = Int32.Parse(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["currentPage"]);
                    }
                }
            }
            catch (Exception)
            {
                currentPage = 1;

            }
            var getProduct = new SearchModel<ProductModel>
            {
                CurrentPage = currentPage,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };

            if (productActiveType != "")
            {
                ProductActiveType advertListType = (ProductActiveType)Convert.ToByte(productActiveType);
                switch (advertListType)
                {
                    case ProductActiveType.Tumu:
                        ViewData["AdvertMenuTumu"] = activeClass;
                        break;
                    case ProductActiveType.Inceleniyor:
                        ViewData["AdvertMenuInceleniyor"] = activeClass;
                        break;
                    case ProductActiveType.Onaylanmadi:
                        ViewData["AdvertMenuOnaylanmadi"] = activeClass;
                        break;
                    case ProductActiveType.Onaylandi:
                        ViewData["AdvertMenuOnaylandi"] = activeClass;
                        break;
                    case ProductActiveType.Silindi:
                        ViewData["AdvertMenuActiveSilindi"] = activeClass;
                        break;
                    case ProductActiveType.CopKutusunda:
                        ViewData["AdvertMenuActiveCopKutusunda"] = activeClass;
                        break;
                    default:
                        break;
                }
                //data = dataProduct.GetSearchWebByProductActiveType(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, (byte)advertListType, AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();
                data = dataProduct.GetSearchWebByProductActiveTypeByProductNameNew(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, (byte)advertListType, AuthenticationUser.Membership.MainPartyId, productName).AsCollection<ProductModel>();
            }
            else if (productActivee != "")
            {
                ProductActive productActive = (ProductActive)Convert.ToByte(productActivee);
                switch (productActive)
                {
                    case ProductActive.Pasif:
                        ViewData["AdvertMenuPassive"] = activeClass;
                        break;
                    case ProductActive.Aktif:
                        ViewData["AdvertMenuActive"] = activeClass;
                        break;
                    default:
                        break;
                }
                if (productActive == (ProductActive.Aktif))
                {
                    data = dataProduct.GetSearchWebByProductActiveTypeByProductNameNew(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, 4, AuthenticationUser.Membership.MainPartyId, productName).AsCollection<ProductModel>();
                }
                else
                {
                    data = dataProduct.GetSearchWebByProductActiveTypeByProductNameNew(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, 5, AuthenticationUser.Membership.MainPartyId, productName).AsCollection<ProductModel>();
                }
            }
            getProduct.Source = data;
            getProduct.TotalRecord = TotalRecord;
            //ViewData["ProductActiveType"] = productActiveType;
            // ViewData["ProductActive"] = productActivee;
            if (getProduct == null)
                return "false";
            else
            {
                string ret = RenderViewToStringCsHtml(ControllerContext,"~/Areas/Account/Views/Advert/AdvertSearchResult.cshtml", getProduct);
                return ret;
            }
        }
        public ActionResult Edit(int id, string result)
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;

            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;
                var store = _storeService.GetStoreByMainPartyId(mainPartyId);
                if (store.ReadyForSale.HasValue && store.ReadyForSale.Value == true)
                {
                    ViewData["ProductSales"] = true;
                }
            }
            if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunEkleme))
            {
                return RedirectToRoute("AdvertNotLimitedAccess");
            }
            var product = _productService.GetProductByProductId(id);
            if (product != null)
            {
                var model = new ProductModel();
                UpdateClass(product, model);
                model.ProductLastUpdate = product.ProductLastUpdate.HasValue ? product.ProductLastUpdate.Value : DateTime.Now;
                model.CurrentListedPage = "1";
                try
                {
                    if (Request.QueryString["currentPage"] != null)
                    {
                        model.CurrentListedPage = Request.QueryString["currentPage"];
                    }
                }
                catch (Exception)
                {
                    model.CurrentListedPage = "1";
                }
                var dataPicture = new Data.Picture();
                model.ProductPictureItems = dataPicture.GetItemsByProductId(id).AsCollection<PictureModel>();
                model.VideoItems = _videoService.GetVideosByProductId(id);
                //if (model.ProductPrice != 0)
                //{
                //    //string[] ProductPrice = model.ProductPrice.ToString().Split(',');
                //    model.ProductPrice = model.ProductPrice;

                //}
                var curCountry = new Classes.Country();
                model.CountryItems = new SelectList(curCountry.GetDataTable().DefaultView, "CountryId", "CountryName", 0);
                var dataAddress = new Data.Address();
                if (product.CountryId == null)
                {
                    product.CountryId = AppSettings.Turkiye;
                }
                model.ProductPriceTypes = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProductPriceType).Where(x => x.ContstantPropertie != "241");
                if (product.ProductPriceType == 0 || product.ProductPrice == (byte)ProductPriceType.PriceDiscuss)
                    model.ProductPriceType = (byte)ProductPriceType.Price;
                else
                    model.ProductPriceType = product.ProductPriceType;

                var cityItems = dataAddress.CityGetItemByCountryId(product.CountryId.Value).AsCollection<CityModel>().ToList();
                cityItems.Insert(0, new CityModel { CityId = 0, CityName = "< Lütfen Seçiniz >" });

                model.TotalPrice = product.ProductPriceWithDiscount.HasValue ? product.ProductPriceWithDiscount.Value.ToString("0#.00") : "0";
                model.DiscountAmount = product.DiscountAmount.HasValue ? product.DiscountAmount.Value : 0;
                model.DiscountType = product.DiscountType.HasValue ? product.DiscountType.Value : (byte)0;

                List<LocalityModel> localityItems = new List<LocalityModel>() { new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
                if (product.CityId != null)
                {
                    localityItems = dataAddress.LocalityGetItemByCityId(product.CityId.Value).AsCollection<LocalityModel>().ToList();
                }

                List<Town> townItems = new List<Town>() { new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };
                if (product.TownId.HasValue)
                {
                    townItems = _addressService.GetTownsByLocalityId(product.LocalityId.Value).ToList();
                }

                model.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
                model.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");
                model.TownItems = new SelectList(townItems, "TownId", "TownName");
                if (model.SeriesId.HasValue)
                {
                    var series = _categoryService.GetCategoryByCategoryId(model.SeriesId.Value);
                    if (series != null)
                    {
                        model.SeriesName = series.CategoryName;
                    }
                }

                if (model.BrandId.HasValue)
                {
                    var brand = _categoryService.GetCategoryByCategoryId(model.BrandId.Value);
                    if (brand != null)
                    {
                        model.BrandName = brand.CategoryName;
                    }
                }


                if (model.ModelId.HasValue)
                {
                    var modelModel = _categoryService.GetCategoryByCategoryId(model.ModelId.Value);
                    if (modelModel != null)
                    {
                        model.ModelName = modelModel.CategoryName;
                    }
                }
                var category = _categoryService.GetCategoryByCategoryId(model.CategoryId);
                if (category != null)
                {
                    model.CategoryName = category.CategoryName;
                }

                model.AllowSellUrl = CheckAllowProductSellUrl();
                model.ProductSellUrl = product.ProductSellUrl;
                //model.Keywords = product.Keywords;
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AllAd);

                Session["ProductActiveType"] = product.ProductActiveType;
                Session["ProductActive"] = product.ProductActiveType;
                if (result == "success")
                {
                    ViewData["success"] = "success";
                }
                if (product.Kdv != null)
                    model.Kdv = Convert.ToBoolean(product.Kdv);
                else product.Kdv = false;
                if (product.Fob != null)
                    model.Fob = Convert.ToBoolean(product.Fob);
                else product.Fob = false;


                string[] categoryIds = new string[0];
                if (!string.IsNullOrEmpty(product.CategoryTreeName))
                {
                    categoryIds = product.CategoryTreeName.Substring(0, product.CategoryTreeName.Length - 1).Split('.');
                }

                bool anyCategoryPropertie = false;
                MTProductPropertieModel propertieModel = new MTProductPropertieModel();
                foreach (var item in categoryIds.Reverse())
                {
                    var categoryProperties = _categoryPropertieService.GetPropertieByCategoryId(Convert.ToInt32(item));
                    foreach (var categoryPropertieItem in categoryProperties)
                    {
                        var productPropertieValue = _categoryPropertieService.GetProductPropertieValuesByProductId(product.ProductId).FirstOrDefault(x => x.PropertieId == categoryPropertieItem.PropertieId && x.PropertieType == categoryPropertieItem.PropertieType);

                        var propertieItem = new MTProductPropertieItem { Value = productPropertieValue != null ? productPropertieValue.Value : "", InputName = categoryPropertieItem.PropertieId.ToString(), DisplayName = categoryPropertieItem.PropertieName, Type = categoryPropertieItem.PropertieType, PropertieId = categoryPropertieItem.PropertieId };
                        if (propertieItem.Type == (byte)PropertieType.MutipleOption)
                        {
                            var propertieAttrs = _categoryPropertieService.GetPropertiesAttrByPropertieId(categoryPropertieItem.PropertieId);


                            if (productPropertieValue == null || productPropertieValue.Value == "0")
                            {
                                propertieItem.Attrs.Add(new SelectListItem { Text = "< Seçiniz >", Value = "", Selected = true });

                            }
                            foreach (var propertieAttrItem in propertieAttrs.OrderBy(x => x.DisplayOrder).ThenBy(x => x.AttrValue))
                            {
                                var itemSelect = new SelectListItem { Text = propertieAttrItem.AttrValue, Value = propertieAttrItem.PropertieAttrId.ToString() };
                                if (productPropertieValue != null)
                                {
                                    if (int.Parse(productPropertieValue.Value) == propertieAttrItem.PropertieAttrId)
                                        itemSelect.Selected = true;

                                }

                                propertieItem.Attrs.Add(itemSelect);
                            }
                        }
                        propertieModel.MTProductProperties.Add(propertieItem);
                        anyCategoryPropertie = true;
                        model.MTProductPropertieModel = propertieModel;
                    }
                    if (anyCategoryPropertie)
                        break;

                }
                SessionProductPropertieModel.MTProductPropertieModel = propertieModel;

                var productCatologs = _productCatologService.GetProductCatologsByProductId(product.ProductId);
                foreach (var item in productCatologs)
                {
                    var filePath = FileUrlHelper.GetProductCatalogUrl(item.FileName, product.ProductId);
                    model.MTProductCatologItems.Add(new MTProductCatologItem { CatologId = item.ProductCatologId, FilePath = filePath });
                }

                model.SiparisList = _constantService.GetConstantByConstantType(ConstantTypeEnum.SiparisDurumu);
                model.TheOriginItems = _constantService.GetConstantByConstantType(ConstantTypeEnum.Mensei);

                var constants = _constantService.GetAllConstants();
                foreach (var item in constants)
                {
                    model.ConstantItems.Add(new ConstantModel
                    {
                        ConstantId = item.ConstantId,
                        ConstantName = item.ConstantName,
                        ConstantType = item.ConstantType.Value,
                        Order = item.Order.GetValueOrDefault()
                    });
                }

                var currencies = _currencyService.GetAllCurrencies();
                currencies.Insert(0, new Currency { CurrencyId = 0, CurrencyName = "< Seçiniz >" });
                model.CurrencyItems = new SelectList(currencies, "CurrencyId", "CurrencyName");

                var certificateTypes = _certificateTypeService.GetCertificateTypes();
                var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
                var certificateTypeProducts = _certificateTypeService.GetCertificateTypeProductsByProductId(id);

                foreach (var item in storeCertificates)
                {
                    var certificateType = _certificateTypeService.GetCertificateTypeProductsByProductId(product.ProductId).FirstOrDefault(x=>x.StoreCertificateId == item.StoreCertificateId);

                    if (certificateType != null)
                    {
                        var listItem = new SelectListItem
                        {
                            Text = item.CertificateName,
                            Value = certificateType.CertificateTypeId.ToString(),

                        };
                        if (certificateTypeProducts.FirstOrDefault(x => x.CertificateTypeId == certificateType.CertificateTypeId) != null)
                            listItem.Selected = true;

                        model.CertificateTypes.Add(listItem);
                    }
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ProductModel model, FormCollection coll, string ProductPrice1, string PActiveType, string productPriceType, List<HttpPostedFileBase> NewProductCatolog, string[] certificateTypes)
        {
            #region ImageUpload
            bool hasFile = false;
            bool pictureIsOK = false;
            foreach (string inputTagName in Request.Files)
            {
                if (inputTagName == "NewProductPicture" && Request.Files[inputTagName].ContentLength > 0 && pictureIsOK == false)
                {

                    List<string> thumbSizes = new List<string>();
                    thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

                    ProductImageHelpers productImageHelpers = new ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes);
                    List<PictureModel> pictureModel = productImageHelpers.SaveProductImageEdit(Request, id, model.ProductName);

                    int pictureOrder = 1;
                    foreach (PictureModel item in pictureModel)
                    {
                        var modelpicture = new Picture()
                        {
                            PictureName = "",
                            PicturePath = item.PicturePath,
                            ProductId = id,
                            PictureOrder = pictureOrder

                        };
                        _pictureService.InsertPicture(modelpicture);
                        ++pictureOrder;
                    }
                    pictureIsOK = true;

                }
                else if (inputTagName == "NewProductVideo" && Request.Files[inputTagName].ContentLength > 0)
                {
                    hasFile = true;

                    HttpPostedFileBase file = Request.Files[inputTagName];
                    VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                    DateTime timesplit;


                    if (DateTime.TryParse(vModel.Duration, out timesplit))
                    {

                    }
                    else
                    {
                        timesplit = DateTime.Now.Date;
                    }

                    var video = new Video
                    {
                        Active = true,
                        VideoPath = vModel.newFileName,
                        VideoSize = null,
                        VideoPicturePath = vModel.newFileName + ".jpg",
                        VideoTitle = model.VideoTitle,
                        VideoRecordDate = DateTime.Now,
                        SingularViewCount = 0,
                        ProductId = id,
                        VideoMinute = (byte?)timesplit.Minute,
                        VideoSecond = (byte?)timesplit.Second
                    };
                    _videoService.InsertVideo(video);
                }
            }
            #endregion

            if (hasFile)
            {
                var result = RedirectToAction("Edit", "Advert", new { id = id }).RouteValues;
                result.Add("currentPage", model.CurrentListedPage);
                return RedirectToAction("Edit", "Advert", result);
            }
            else
            {
                var cultInfo = new CultureInfo("tr-TR");
                try
                {
                    var product = _productService.GetProductByProductId(id);

                    DateTime dateProductEndDate = DateTime.Now;
                    var pPublicDateType = (ProductPublicationDateType)model.ProductPublicationDateType;
                    switch (pPublicDateType)
                    {
                        case ProductPublicationDateType.Gun:
                            dateProductEndDate = dateProductEndDate.AddDays(model.ProductPublicationDate);
                            break;
                        case ProductPublicationDateType.Ay:
                            dateProductEndDate = dateProductEndDate.AddHours(model.ProductPublicationDate);
                            break;
                        case ProductPublicationDateType.Yil:
                            dateProductEndDate = dateProductEndDate.AddYears(model.ProductPublicationDate);
                            break;
                        default:
                            break;
                    }

                    if (model.ProductPublicationDateType != 0)
                    {
                        product.ProductAdvertEndDate = dateProductEndDate;
                    }

                    if (model.CountryId > 0)
                        product.CountryId = model.CountryId;
                    else
                        product.CountryId = null;

                    if (model.CityId > 0)
                        product.CityId = model.CityId;
                    else
                        product.CityId = null;

                    if (model.LocalityId > 0)
                        product.LocalityId = model.LocalityId;
                    else
                        product.LocalityId = null;

                    if (model.TownId > 0)
                        product.TownId = model.TownId;
                    else
                        product.TownId = null;

                    if (model.CurrencyId > 0)
                        product.CurrencyId = model.CurrencyId;
                    else
                        product.CurrencyId = null;

                    //product.ProductName = model.ProductName;
                    product.ProductPriceType = Convert.ToByte(productPriceType);
                    string productPrice = ProductPrice1;
                    if (product.ProductPriceType == (byte)ProductPriceType.Price)
                    {
                        product.Fob = false;
                        product.Kdv = false;
                        product.UnitType = coll["UnitType"].ToString();

                        if (coll["pricePropertie"].ToString() == "kdvdahil")
                            product.Kdv = true;
                        else if (coll["pricePropertie"].ToString() == "fob")
                            product.Fob = true;

                        if (productPrice.Trim() != "")
                        {
                            if (productPrice.IndexOf('.') > 0 && productPrice.IndexOf(",") > 0)
                            {
                                productPrice = productPrice.Replace(".", "");
                            }
                            else
                            {
                                productPrice = productPrice.Replace(".", ",");
                            }
                        }
                        else productPrice = "0";
                        decimal price = Convert.ToDecimal(productPrice, cultInfo.NumberFormat);
                        product.ProductPrice = price;


                        product.DiscountType = Convert.ToByte(model.DiscountType);
                        if (model.DiscountType != 0)
                        {


                            if (!string.IsNullOrEmpty(coll["DiscountAmount"]))
                            {
                                product.DiscountAmount = Convert.ToDecimal(model.DiscountAmount);
                                string totalPrice = coll["TotalPrice"];
                                if (totalPrice.IndexOf('.') > 0 && totalPrice.IndexOf(",") > 0)
                                {
                                    totalPrice = totalPrice.Replace(".", "");
                                }
                                else
                                {
                                    totalPrice = totalPrice.Replace(".", ",");
                                }
                                product.ProductPriceWithDiscount = Convert.ToDecimal(totalPrice, cultInfo.NumberFormat);
                            }
                        }
                        else
                        {
                            product.DiscountAmount = 0;
                            product.ProductPriceWithDiscount = 0;
                        }
                    }
                    else if (product.ProductPriceType == (byte)ProductPriceType.PriceRange)
                    {
                        product.Fob = false;
                        product.Kdv = false;
                        if (coll["pricePropertie"].ToString() == "kdvdahil")
                            product.Kdv = true;
                        else if (coll["pricePropertie"].ToString() == "fob")
                            product.Fob = true;
                        product.Kdv = false;
                        product.Fob = false;

                        string productPriceLast = coll["ProductPriceLast"].ToString();
                        string productPriceBegin = coll["ProductPriceBegin"].ToString();
                        if (productPriceLast.Trim() != "")
                        {
                            if (productPriceLast.IndexOf('.') > 0 && productPriceLast.IndexOf(",") > 0)
                            {
                                productPriceLast = productPriceLast.Replace(".", "");
                            }
                            else
                            {
                                productPriceLast = productPriceLast.Replace(".", ",");
                            }
                        }

                        if (productPriceBegin.Trim() != "")
                        {
                            if (productPriceBegin.IndexOf('.') > 0 && productPriceLast.IndexOf(",") > 0)
                            {
                                productPriceBegin = productPriceBegin.Replace(".", "");
                            }
                            else
                            {
                                productPriceBegin = productPriceBegin.Replace(".", ",");
                            }
                        }

                        product.ProductPriceBegin = Convert.ToDecimal(productPriceBegin, cultInfo.NumberFormat);
                        product.ProductPriceLast = Convert.ToDecimal(productPriceLast, cultInfo.NumberFormat);
                        productPrice = "0";
                        product.UnitType = coll["UnitType"].ToString();

                        model.ProductPriceWithDiscount = 0;
                        model.DiscountType = 0;
                        model.DiscountAmount = 0;
                    }
                    else
                    {
                        productPrice = "0";
                        product.ProductPriceLast = 0;
                        product.ProductPriceBegin = 0;
                        product.ProductPrice = 0;
                        product.ProductPriceWithDiscount = 0;
                        product.DiscountType = 0;
                        product.DiscountAmount = 0;

                    }

                    product.ProductLastUpdate = DateTime.Now;
                    product.ProductType = model.ProductType;

                    string ProductSalesType = String.Empty;
                    if (coll["ProductSalesType"] != null)
                    {
                        string[] acProductSalesType = coll["ProductSalesType"].Split(',');
                        if (acProductSalesType != null)
                        {
                            for (int i = 0; i < acProductSalesType.Length; i++)
                            {
                                if (acProductSalesType.GetValue(i).ToString() != "false")
                                {
                                    if (string.IsNullOrEmpty(ProductSalesType))
                                        ProductSalesType = acProductSalesType.GetValue(i).ToString();
                                    else
                                        ProductSalesType = ProductSalesType + "," + acProductSalesType.GetValue(i).ToString();
                                }
                            }
                        }
                    }

                    product.ProductSalesType = ProductSalesType;
                    product.MoneyCondition = null;
                    product.ProductDescription = !string.IsNullOrEmpty(model.ProductDescription) ? model.ProductDescription.CleanProductDescriptionText() : "";
                    product.ProductStatu = model.ProductStatu;
                    product.ReadyforSale = model.ReadyforSale;
                    //product.Keywords = model.Keywords;
                    if (!string.IsNullOrWhiteSpace(model.OtherBrand))
                    {
                        product.OtherBrand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.OtherBrand.ToLower());
                    }
                    else product.OtherBrand = model.OtherBrand;
                    product.OtherModel = model.OtherModel;
                    product.ModelYear = model.ModelYear;

                    if (CheckAllowProductSellUrl())
                    {
                        product.ProductSellUrl = model.ProductSellUrl;
                    }

                    //string BriefDetail = String.Empty;
                    //if (coll["BriefDetail"] != null)
                    //{
                    //    string[] acBriefDetail = coll["BriefDetail"].Split(',');
                    //    if (acBriefDetail != null)
                    //    {
                    //        for (int i = 0; i < acBriefDetail.Length; i++)
                    //        {
                    //            if (acBriefDetail.GetValue(i).ToString() != "false")
                    //            {
                    //                if (string.IsNullOrEmpty(BriefDetail))
                    //                    BriefDetail = acBriefDetail.GetValue(i).ToString();
                    //                else
                    //                    BriefDetail = BriefDetail + "," + acBriefDetail.GetValue(i).ToString();
                    //            }
                    //        }
                    //    }
                    //}
                    product.BriefDetail = model.BriefDetail; //BriefDetail;
                    product.ProductActiveType = (byte)ProductActiveType.Inceleniyor;


                    //product.ProductName = model.ProductName;
                    string productName = LimitText(model.ProductName, 100);
                    var productNumbers = _productService.GetProductsByProductName(productName);
                    if (productNumbers.Count > 1)
                    {
                        productName = LimitText(model.ProductName, 97) + "(" + productNumbers.ToList().Count + ")";
                    }
                    product.ProductName = productName;
                    product.ProductActive = model.ProductActive;
                    product.MenseiId = model.MenseiId;
                    product.OrderStatus = model.OrderStatus;
                    product.WarrantyPeriod = model.WarrantyPeriod;
                    product.ProductLastUpdate = DateTime.Now;
                    _productService.UpdateProduct(product);

                    var propertieModel = SessionProductPropertieModel.MTProductPropertieModel;
                    var productPropertieValues = _categoryPropertieService.GetProductPropertieValuesByProductId(product.ProductId);
                    if (propertieModel != null)
                    {
                        foreach (var item in propertieModel.MTProductProperties)
                        {
                            var val = Request.Form[item.InputName];
                            if (!string.IsNullOrEmpty(val))
                            {
                                var productPropertieVal = productPropertieValues.FirstOrDefault(x => x.PropertieId == item.PropertieId);
                                if (productPropertieVal != null)
                                {
                                    productPropertieVal.Value = val;
                                    _categoryPropertieService.UpdateProductProertieValue(productPropertieVal);
                                }
                                else
                                {
                                    productPropertieVal = new ProductPropertieValue();
                                    productPropertieVal.ProductId = product.ProductId;
                                    productPropertieVal.PropertieId = item.PropertieId;
                                    productPropertieVal.PropertieType = item.Type;
                                    productPropertieVal.Value = val;
                                    _categoryPropertieService.InsertProductProertieValue(productPropertieVal);
                                }
                            }


                        }
                    }

                    if (Convert.ToByte(Session["ProductActiveType"]) == (byte)ProductActiveType.Onaylandi)
                    {

                        if (Convert.ToBoolean(Session["ProductActive"]))
                        {
                            if (model.ProductActive == false)
                            {
                                ProductCountCalc(product, false);
                            }
                        }
                        else
                        {
                            if (model.ProductActive == true)
                            {
                                ProductCountCalc(product, true);
                            }
                        }
                    }

                    int counter1 = 0;

                    foreach (var item in NewProductCatolog)
                    {
                        if (item != null)
                        {
                            string filePath = FileUploadHelper.UploadFile(item, AppSettings.ProductCatologFolder + "/" + product.ProductId, product.ProductName, counter1);
                            if (filePath != "")
                            {
                                counter1++;
                                var productCatolog = new ProductCatolog();
                                productCatolog.FileName = filePath;
                                productCatolog.ProductId = id;
                                _productCatologService.InsertProductCatolog(productCatolog);
                            }
                        }

                    }

                    if (certificateTypes != null)
                    {
                        var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
                        var certificateTypeProducts = _certificateTypeService.GetCertificateTypeProductsByProductId(id, false);
                        foreach (var item in certificateTypeProducts)
                        {
                            _certificateTypeService.DeleteCertificateTypeProduct(item);
                        }

                        for (int i = 0; i < certificateTypes.Length; i++)
                        {

                            var storeCertificate = _certificateTypeService.GetCertificateTypeProductsByCerticateTypeId(Convert.ToInt32(certificateTypes[i])).FirstOrDefault(x => x.StoreCertificateId != null);


                            var certificateTypeProduct = new CertificateTypeProduct
                            {
                                ProductId = product.ProductId,
                                CertificateTypeId = Convert.ToInt32(certificateTypes[i]),
                                StoreCertificateId = storeCertificate != null ? storeCertificate.StoreCertificateId : null
                            };
                            _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException();
                }
            }
            var resultr = RedirectToAction("Edit", "Advert", new { id = id }).RouteValues;
            resultr.Add("currentPage", model.CurrentListedPage);
            resultr.Add("result", "success");
            return RedirectToAction("Edit", "Advert", resultr);
        }

        [HttpPost]
        public JsonResult DeleteCatolog(int id)
        {
            var productCatolog = _productCatologService.GetProductCatologByProductCatologId(id);
            var filePath = FileUrlHelper.GetProductCatalogUrl(productCatolog.FileName, productCatolog.ProductId);
            FileHelpers.Delete(AppSettings.StoreCatologFolder + "/" + productCatolog.ProductId + "/" + productCatolog.FileName);
            _productCatologService.DeleteProductCatolog(productCatolog);
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public PartialViewResult EditImagesNewAjax(string ProductId, string ProductName)
        {
            int id = Convert.ToInt32(ProductId);
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;

                List<string> thumbSizes = new List<string>();
                thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));


                ProductImageHelpers productImageHelpers = new ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes);
                List<PictureModel> pictureModel = productImageHelpers.SaveProductImageEdit(Request, id, ProductName);

                int pictureOrder = 1;
                foreach (PictureModel item in pictureModel)
                {
                    var modelpicture = new Picture()
                    {
                        PictureName = "",
                        PicturePath = item.PicturePath,
                        ProductId = id,
                        PictureOrder = pictureOrder

                    };
                    _pictureService.InsertPicture(modelpicture);
                    ++pictureOrder;
                }
            }
            var dataPicture = new Data.Picture();
            var pictureModel1 = dataPicture.GetItemsByProductId(id).AsCollection<PictureModel>();

            return PartialView("/Areas/Account/Views/Advert/EditProductPicture.cshtml", pictureModel1);

        }


        public void ProductCountCalc(Product curProduct, bool add)
        {
            IList<TopCategoryResult> topCategories = new List<TopCategoryResult>();
            if (curProduct.ModelId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.ModelId.Value);

            }
            else if (curProduct.SeriesId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.SeriesId.Value);

            }
            else if (curProduct.BrandId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.BrandId.Value);

            }
            else if (curProduct.CategoryId.HasValue)
            {
                topCategories = _categoryService.GetSPTopCategories(curProduct.CategoryId.Value);

            }

            if (topCategories.Count > 0)
            {
                foreach (var item in topCategories)
                {
                    var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                    if (category != null)
                    {
                        if (add)
                        {
                            category.ProductCount = category.ProductCount + 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll + 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew + 1;
                            }
                        }
                        else
                        {
                            category.ProductCount = category.ProductCount - 1;
                            if (category.CategoryType == (byte)CategoryType.Sector)
                            {
                                if (curProduct.ProductStatu == "72")
                                    category.ProductCountAll = category.ProductCountAll - 1;
                                else if (curProduct.ProductStatu == "73")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                                else if (curProduct.ProductStatu == "201")
                                    category.ProductCountNew = category.ProductCountNew - 1;
                            }

                        }

                        _categoryService.UpdateCategory(category);
                    }
                }
            }
        }

        public static bool IsDate(Object obj)
        {
            if (obj != null)
            {
                string strDate = obj.ToString();
                try
                {
                    DateTime dt = DateTime.Parse(strDate);
                    if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                        return true;
                    return false;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        [HttpPost]
        public ActionResult AdvertPagingfor(int page, byte displayType, byte advertListType, byte ProductActiveType)
        {
            var dataProduct = new Data.Product();
            var getProduct = new SearchModel<ProductModel>
            {
                CurrentPage = page,
                PageDimension = 100,
                TotalRecord = TotalRecord
            };

            var data = dataProduct.GetSearchWebByProductActiveTypeNew(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, (byte)advertListType, AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();

            getProduct.Source = data;
            getProduct.TotalRecord = TotalRecord;

            string userControlName = string.Empty;
            var pageType = (DisplayType)displayType;
            switch (pageType)
            {
                case DisplayType.Window:
                    userControlName = "/Areas/Account/Views/Statistic/_ProductLists.cshtml";
                    break;
                case DisplayType.List:
                    userControlName = "/Areas/Account/Views/Statistic/_ProductLists.cshtml";
                    break;
                case DisplayType.Text:
                    userControlName = "/Areas/Account/Views/Statistic/_ProductLists.cshtml";
                    break;
                default:
                    break;
            }

            return View(userControlName, getProduct);
        }
        [HttpPost]
        public ActionResult AdvertPaging(int page, byte displayType, byte advertListType, byte productActiveType, int productActive, string productNo, string categoryName, string productName, string brandName, byte orderType=1)
        {
            var dataProduct = new Data.Product();
            int pageDimension = 20;
            var getProduct = new SearchModel<MTProductItem>
            {
                CurrentPage = page,
                PageDimension = pageDimension
            };
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();

            var products = _productService.GetAllProductsByMainPartyIds(mainPartyIds);
            if (orderType == 1)
            {
                products = products.OrderByDescending(x => x.ProductId).ToList();
            }
            else if (orderType == 2)
            {
                products = products.OrderByDescending(x => x.ViewCount).ToList();
            }
            else
            {

                var products1 = products.Where(x => x.Sort != null && x.Sort != 0).OrderBy(x => x.Sort).ToList();
                products = products1.Concat(products.Where(x => x.Sort == null || x.Sort == 0).OrderByDescending(x => x.ViewCount)).ToList();

            }

            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(x => x.ProductName.ToLower().Contains(productName.ToLower())).ToList();
            }
            if (productNo != "#" && !string.IsNullOrEmpty(productNo))
            {
                products = products.Where(x => x.ProductNo.Contains(productNo)).ToList();
            }
            if (!string.IsNullOrEmpty(categoryName))
            {
                products = products.Where(x => x.Category.CategoryName.ToLower().Contains(categoryName.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(brandName))
            {
                products = products.Where(x => x.Brand.CategoryName.ToLower().Contains(brandName.ToLower())).ToList();
            }
            if (productActiveType != (byte)ProductActiveType.Tumu && productActive == -1)
            {
                products = products.Where(x => x.ProductActiveType == productActiveType).ToList();
            }
            else
            {
                if (productActive != -1)
                {
                    bool productAc = false;
                    if (productActive == 1) productAc = true;
                    products = products.Where(x => x.ProductActive == productAc).ToList();
                }

            }

            int takeFrom = page * pageDimension - pageDimension;
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            bool showDopingForm = store.PacketId == 29;
            List<MTProductItem> productModels = new List<MTProductItem>();
            foreach (var item in products.Skip(takeFrom).Take(pageDimension))
            {
                string picturePath = "";
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                if (picture != null)
                    picturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150);

                productModels.Add(new MTProductItem
                {
                    BrandName = item.Brand != null ? item.Brand.CategoryName : "",
                    BriefDetail = item.GetBriefDetailText(),
                    CategoryName = item.Category != null ? item.Category.CategoryName : "",
                    City = item.City.CityName,
                    ImagePath = picturePath,
                    Locality = item.Locality.LocalityName,
                    ModelName = item.Model != null ? item.Model.CategoryName : "",
                    ModelYear = item.ModelYear.HasValue == true ? item.ModelYear.Value.ToString() : "0",
                    ProductActive = item.ProductActive.Value,
                    ProductActiveType = item.ProductActiveType.Value,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductNo = item.ProductNo,
                    ProductPrice = item.GetFormattedPrice(),
                    ProductStatusText = item.GetProductStatuText(),
                    ProductTypeText = item.GetProductTypeText(),
                    SalesTypeText = item.GetProductSalesTypeText(),
                    ProductPriceType = item.ProductPriceType != null ? item.ProductPriceType.Value : (byte)0,
                    ViewCount = item.ViewCount.Value,
                    CurrencyId = item.CurrencyId.HasValue == true ? item.CurrencyId.Value : (byte)0,
                    CurrencyCssText = item.GetCurrencyCssName(),
                    Doping = item.Doping,
                    ShowDopingForm = showDopingForm
                });
            }

            getProduct.Source = productModels;
            getProduct.TotalRecord = products.Count;
            getProduct.CurrentPage = page;
            string userControlName = string.Empty;
            var pageType = (DisplayType)displayType;
            switch (pageType)
            {
                case DisplayType.Window:
                    userControlName = "/Areas/Account/Views/Advert/AdvertWindow.cshtml";
                    break;
                case DisplayType.List:
                    userControlName = "/Areas/Account/Views/Advert/_AdvertListWindow.cshtml";
                    break;
                case DisplayType.Text:
                    userControlName = "/Areas/Account/Views/Advert/AdvertText.cshtml";
                    break;
                case DisplayType.Table:
                    userControlName = "/Areas/Account/Views/Advert/_AdvertListTable.cshtml";
                    break;
                default:
                    break;
            }

            return View(userControlName, getProduct);

        }

        public ActionResult Advert()
        {
            if (AuthenticationUser.Membership.MemberType != (byte)MemberType.Enterprise) return RedirectToRoute("AdvertNotMemberTypeAccess");

            PictureList = null;
            VideoList = null;

            int productcount = 0;
            int maxproductcount = 0;
            var model = new AdvertViewModel();
            byte isitstore = (byte)AuthenticationUser.Membership.MemberType;
            var storemember = new global::MakinaTurkiye.Entities.Tables.Members.MemberStore();
            var store = new global::MakinaTurkiye.Entities.Tables.Stores.Store();

            if (isitstore == 20)
            {
                storemember = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);
                store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(storemember.StoreMainPartyId));
                if (store.PacketId == 12)
                {
                    store.PacketId = 20;
                    _storeService.UpdateStore(store);
                }
                if (store.ProductCount == null)
                {

                    var packetFeature = _packetService.GetPacketFeatureByPacketIdAndPacketFeatureTypeId(store.PacketId, 3);

                    if (packetFeature.FeatureContent != null)
                    {
                        maxproductcount = 9999;
                    }
                    else if (packetFeature.FeatureActive != null)
                    {
                        if (packetFeature.FeatureActive == false)
                        {
                            maxproductcount = 0;
                        }
                        else if (packetFeature.FeatureActive == true)
                        {
                            //her üründe deneme ürünü eklemek için tıklayınız diyecek.
                            //if (Session["t"] == null)
                            //{
                            //    Session["t"] = 1;

                            //    return RedirectToRoute("AdvertNotLimitedAccess");
                            //}
                            //maxproductcount = 3;
                            //Session["t"] = null;

                            return Redirect("~/UyelikSatis/adim1?sayfa=ilanekle");
                        }
                    }
                    else
                    {
                        maxproductcount = packetFeature.FeatureProcessCount.Value;
                    }
                }
                else
                {
                    maxproductcount = store.ProductCount.Value;
                }
                var products = _productService.GetProductsByMainPartyId(Convert.ToInt32(AuthenticationUser.Membership.MainPartyId));
                productcount = products.Count() - products.Where(c => c.ProductActive == false).Count() - products.Where(c => c.ProductActiveType == (byte)ProductActiveType.Silindi).Count();
                if (maxproductcount <= productcount)
                {
                    return RedirectToRoute("AdvertNotLimitedAccess");
                }
            }

            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
            {
                return RedirectToRoute("AdvertNotLimitedAccess");
            }
            else if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunEkleme))
            {
                return RedirectToRoute("AdvertNotLimitedAccess");
            }

            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            var sectorCategories = _storeSectorService.GetStoreSectorsByMainPartyId(store.MainPartyId);

            var privateSectorCategories = new List<SelectListItem>();
            if (sectorCategories.Count > 0)
            {
                foreach (var item in sectorCategories)
                {
                    var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);

                    privateSectorCategories.Add(new SelectListItem
                    {
                        Text = category.CategoryName,
                        Value = item.CategoryId.ToString()
                    });
                }
                privateSectorCategories.Add(new SelectListItem { Value = "-1", Text = "Tüm Sektörleri Gör" });
            }

            model.PrivateSectorCategories = privateSectorCategories.OrderBy(x => x.Text).ToList();
            return View(model);
        }

        public List<PictureModel> PictureList
        {
            get
            {
                if (Session["PictureItems"] == null)
                {
                    Session["PictureItems"] = new List<PictureModel>();
                }
                return Session["PictureItems"] as List<PictureModel>;
            }
            set { Session["PictureItems"] = value; }
        }

        public List<VideoModel> VideoList
        {
            get
            {
                if (Session["VideoItems"] == null)
                {
                    Session["VideoItems"] = new List<VideoModel>();
                }
                return Session["VideoItems"] as List<VideoModel>;
            }
            set { Session["VideoItems"] = value; }
        }

        [HttpDelete]
        public ActionResult DeletePicture(int index, bool horizontal)
        {
            var item = PictureList[index];

            FileHelpers.Delete(AppSettings.ProductImageFolder + item.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb120x120Folder + item.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb240x160Folder + item.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb55x40Folder + item.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb75x75Folder + item.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb800x600Folder + item.PicturePath);

            PictureList.RemoveAt(index);
            if (horizontal)
            {
                return View("/Areas/Account/Views/Advert/PictureListHor.cshtml", PictureList);
            }
            return View("/Areas/Account/Views/Advert/PictureList.cshtml", PictureList);
        }
        /*
        [HttpDelete]
        public ActionResult DeleteImage(int index, int productID, string picturePath, bool horizontal)
        {
            PictureModel picture = PictureList[index];

            if (picture.PicturePath == picturePath && picture.ProductId == productID)
            {
                List<string> thumbSizes = new List<string>();
                thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

                foreach (string thumb in thumbSizes)
                {
                    string imageExtension = picture.PicturePath.Substring(picture.PicturePath.LastIndexOf("."), picture.PicturePath.Length - picture.PicturePath.LastIndexOf("."));
                    string imageName = picture.PicturePath.Remove(0, picture.PicturePath.Length - imageExtension.Length);

                    //<add key="ProductThumbSizes" value="900x675;400x300;100x75;100x*"/>
                }
            }

            return null;
        }
        */

        [HttpPost]
        public ActionResult mainImage(int productID, string picturePath)
        {
            List<PictureModel> pictures = Session["PictureItems"] as List<PictureModel>;
            List<PictureModel> newpictures = new List<PictureModel>();

            foreach (var row in pictures)
            {
                if (row.PicturePath == picturePath)
                {
                    newpictures.Add(row);

                    foreach (var ro in pictures)
                    {
                        if (ro.PicturePath != picturePath)
                        {
                            newpictures.Add(ro);
                        }
                    }
                }
            }

            Session["PictureItems"] = newpictures;

            return View("/Areas/Account/Views/Advert/PictureList.cshtml", PictureList);
        }

        [HttpPost]
        public ActionResult mainImageEdit(string idArray, int productID, string picturePath)
        {

            var dataPicture = new Data.Picture();
            var pictureModel = dataPicture.GetItemsByProductId(productID).AsCollection<PictureModel>();
            List<PictureModel> newpictures = new List<PictureModel>();

            foreach (var row in pictureModel)
            {
                if (row.PicturePath == picturePath)
                {
                    newpictures.Add(row);

                    foreach (var ro in pictureModel)
                    {
                        if (ro.PicturePath != picturePath)
                        {
                            newpictures.Add(ro);
                        }
                    }
                }
            }

            int count = 1;
            foreach (var v in newpictures)
            {
                var picture = _pictureService.GetPictureByPictureId(v.PictureId);
                picture.PictureOrder = count;
                _pictureService.UpdatePicture(picture);
                ++count;
            }
            //product search  tablosunu bu ürün için güncellemen gerekli

            _productService.CheckSPProductSearch(productID);

            return View("/Areas/Account/Views/Advert/EditProductPicture.cshtml", newpictures);
        }

        [HttpPost]
        public ActionResult DeleteImage(int index, int productID, string picturePath, bool horizontal)
        {
            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));
            List<PictureModel> pictures = Session["PictureItems"] as List<PictureModel>;
            PictureModel picture = null;

            foreach (var row in pictures)
            {
                if (row.PicturePath == picturePath)
                {
                    picture = row;
                }
            }

            pictures.Remove(picture);

            FileHelpers.Delete(AppSettings.ProductImageFolder + picture.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb120x120Folder + picture.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb240x160Folder + picture.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb55x40Folder + picture.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb75x75Folder + picture.PicturePath);
            FileHelpers.Delete(AppSettings.ProductImageThumb800x600Folder + picture.PicturePath);


            Session["PictureItems"] = pictures;

            if (horizontal)
            {
                return View("/Areas/Account/Views/Advert/PictureListHor.cshtml", PictureList);
            }

            return View("/Areas/Account/Views/Advert/PictureList.cshtml", PictureList);
        }

        [HttpGet]
        public ActionResult DeletePictureEditPage(string ProductId, string PictureId, string PictureName)
        {
            var curPicture = new Classes.Picture();


            curPicture.Delete(PictureId);

            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));
            FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + PictureName);
            foreach (string thumb in thumbSizes)
            {

                string imagetype = PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf("."));//örnek .jpeg
                string imagename = PictureName.Remove(PictureName.Length - PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf(".")).Length);

                FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + "thumbs/" + imagename + "-" + thumb + imagetype);
            }

            var dataPicture = new Data.Picture();
            var pictureModel = dataPicture.GetItemsByProductId(Convert.ToInt32(ProductId)).AsCollection<PictureModel>();


            return View("/Areas/Account/Views/Advert/EditProductPicture.cshtml", pictureModel);
        }


        [HttpPost]
        public ActionResult DeletePictureFromList(int ProductId, int PictureId, string PictureName)
        {


            var curPicture = new Classes.Picture();
            curPicture.Delete(PictureId);


            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Replace("*", "").Split(';'));
            FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + PictureName);
            foreach (string thumb in thumbSizes)
            {

                string imagetype = PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf("."));//örnek .jpeg
                string imagename = PictureName.Remove(PictureName.Length - PictureName.Substring(PictureName.LastIndexOf("."), PictureName.Length - PictureName.LastIndexOf(".")).Length);

                FileHelpers.Delete(AppSettings.ProductImageFolder + ProductId + "/" + "thumbs/" + imagename + "-" + thumb + imagetype);
            }

            var dataPicture = new Data.Picture();
            var pictureModel = dataPicture.GetItemsByProductId(ProductId).AsCollection<PictureModel>();


            return View("/Areas/Account/Views/Advert/PictureListNew.cshtml", pictureModel);
        }

        [HttpPost]
        public ActionResult DeleteVideoEdit(int ProductId, int VideoId)
        {
            var video = _videoService.GetVideoByVideoId(VideoId);

            FileHelpers.Delete(AppSettings.VideoFolder + video.VideoPath + ".flv");
            FileHelpers.Delete(AppSettings.VideoThumbnailFolder + video.VideoPicturePath);

            _videoService.DeleteVideo(video);

            var product = _productService.GetProductByProductId(ProductId);
            if (product != null)
            {
                var videos = _videoService.GetVideosByProductId(ProductId);

                if (videos.Count > 0)
                {
                    if (!product.HasVideo)
                    {
                        product.HasVideo = true;
                        _productService.UpdateProduct(product);
                    }
                }
                else
                {
                    if (product.HasVideo)
                    {
                        product.HasVideo = false;
                        _productService.UpdateProduct(product);
                    }
                }
            }

            var videoModel = _videoService.GetVideosByProductId(ProductId);
            return View("/Areas/Account/Views/Advert/EditProductVideo.cshtml", videoModel);
        }

        [HttpDelete]
        public ActionResult DeleteVideo(int index)
        {
            var item = VideoList[index];
            FileHelpers.Delete(AppSettings.VideoThumbnailFolder + item.VideoPicturePath);
            FileHelpers.Delete(AppSettings.VideoFolder + item.VideoPath);
            VideoList.RemoveAt(index);
            return View("/Areas/Account/Views/Advert/VideoList.cshtml", VideoList);
        }

        [HttpPost]
        public ActionResult Advert(AdvertViewModel model, int SelectedCategory)
        {
            model.CategorizationSessionModel.SectorId = model.DropDownSector;
            model.CategorizationSessionModel.ProductGroupId = model.DropDownProductGroup;
            model.CategorizationSessionModel.CategoryId = SelectedCategory;

            //return RedirectToAction("Picture", "Advert");
            //return RedirectToAction("Category", "Advert");
            return RedirectToAction("Brand", "Advert");
        }

        public ActionResult Picture()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model = new AdvertViewModel();

            model.PictureList = PictureList;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }

        [HttpPost]
        public JsonResult UpdatePictureOrder(string idArray)
        {
            int count = 1;
            string[] arr = idArray.Split(',');
            foreach (var id in arr)
            {

                var picture = _pictureService.GetPictureByPictureId(int.Parse(id));
                if (picture != null)
                {
                    picture.PictureOrder = count;
                    _pictureService.UpdatePicture(picture);
                    ++count;
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult Picture(AdvertViewModel model)
        {
            Product product = (Product)Session["CurProduct"];


            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));

            ProductImageHelpers productImageHelpers = new ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes);
            List<PictureModel> pictureModel = productImageHelpers.SaveProductImage(Request, product.ProductId, product.ProductName);
            this.PictureList.AddRange(pictureModel);

            #region Old Code
            //for (int i = 0; i < request.files.count; i++)
            //{
            //    httppostedfilebase file = request.files[i];

            //    if (file.contentlength > 0)
            //    {

            //        hasfile = true;
            //        var thumns = new dictionary<string, string>();

            //        thumns.add(appsettings.productımagethumb120x120folder, "120x120");
            //        thumns.add(appsettings.productımagethumb240x160folder, "240x160");
            //        thumns.add(appsettings.productımagethumb55x40folder, "55x40");
            //        thumns.add(appsettings.productımagethumb75x75folder, "75x75");
            //        thumns.add(appsettings.productımagethumb800x600folder, "800x600");

            //        string filename = filehelpers.ımageresize(appsettings.productımagefolder, file, thumns);
            //        var picturemodel = new picturemodel { picturepath = filename };

            //        picturelist.add(picturemodel);
            //    }
            //}
            #endregion

            if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunResimSayisi, PictureList.Count + 1))
            {
                PictureList.Clear();
                return RedirectToRoute("AdvertNotLimitedAccess");
            }

            if (pictureModel.Count > 0)
            {
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
                model.PictureList = PictureList;
                return View(model);
            }

            return RedirectToAction("Video", "Advert");
        }


        public ActionResult ProductCatolog()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model = new AdvertViewModel();

            //model.PictureList = PictureList;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductCatolog(AdvertViewModel model)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                Product product = (Product)Session["CurProduct"];
                var fileSavePath = AppSettings.ProductCatologFolder;
                var filePath = FileUploadHelper.UploadFile(Request.Files[i], fileSavePath + "/" + product.ProductId, product.ProductName, i);
                if (filePath != "")
                {
                    var productCatolog = new global::MakinaTurkiye.Entities.Tables.Catalog.ProductCatolog();
                    productCatolog.FileName = filePath;
                    productCatolog.ProductId = product.ProductId;
                    _productCatologService.InsertProductCatolog(productCatolog);
                }
            }
            return RedirectToAction("AdvertEnd", "Advert");
        }
        [HttpPost]
        public ActionResult SavePicture(AdvertViewModel model, HttpRequestBase[] files)
        {
            Product product = (Product)Session["CurProduct"];



            List<string> thumbSizes = new List<string>();
            thumbSizes.AddRange(AppSettings.ProductThumbSizes.Split(';'));


            for (int i = 0; i < files.Length; i++)
            {
                ProductImageHelpers productImageHelpers = new ProductImageHelpers(AppSettings.ProductImageFolder, thumbSizes);
                List<PictureModel> pictureModel = productImageHelpers.SaveProductImage(files[i], product.ProductId, product.ProductName);
                this.PictureList.AddRange(pictureModel);


                if (!PacketAuthenticationModel.IsAccess(PacketPage.UrunResimSayisi, PictureList.Count + 1))
                {
                    PictureList.Clear();
                    return RedirectToRoute("AdvertNotLimitedAccess");
                }

                if (pictureModel.Count > 0)
                {
                    model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
                    model.PictureList = PictureList;
                    return View(model);
                }
            }
            return RedirectToAction("Video", "Advert");
        }

        public ActionResult Category()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model = new AdvertViewModel();

            model.CategoryList = _categoryService.GetCategoriesByCategoryParentId(model.CategorizationSessionModel.ProductGroupId);

            ViewData["SectorId"] = model.CategorizationSessionModel.ProductGroupId;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }

        [HttpPost]
        public ActionResult TreeCategoryList(int productGroupId)
        {

            AdvertViewModel model = new AdvertViewModel();
            var categoryList = _categoryService.GetCategoriesByCategoryParentId(productGroupId);

            return PartialView("CategoryParent", categoryList);
        }

        [HttpPost]
        public ActionResult CategoryBind(int id)
        {
            var items = _categoryService.GetCategoriesLessThanCategoryType(CategoryTypeEnum.Brand, true, id);

            if (items == null || items.Count() == 0)
            {
                string hidden = "<input type=\"hidden\" name=\"SelectedCategory\" value=\"" + id.ToString() + "\" />";
                return Content(hidden + "<div class=\"form-group\"><div class=\"col-sm-offset-5 col-sm-9 btn-group\"><br><p>Kategori Seçimi Başarıyla Tamamlanmıştır.</p><a onclick=\"window.location.href='/Account/Advert/Advert'\" class=\"btn btn-default\"> Geri </a><button type=\"submit\" class=\"btn btn-primary\"> Devam </button></div></div><span class=\"clearfix\"></span>");
            }

            return View("CategoryParent", items);
        }

        [HttpPost]
        public ActionResult Category(AdvertViewModel model, int SelectedCategory)
        {
            model.CategorizationSessionModel.CategoryId = SelectedCategory;
            return RedirectToAction("Brand", "Advert");
        }

        public ActionResult Brand()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model1 = new AdvertViewModel();
            var model = new MTBrandViewModel();

            //model1.CategoryList = entities.Categories.Where(c => c.CategoryId == model1.CategorizationSessionModel.CategoryId).ToList();
            model1.PictureList = PictureList;

            var brandItems = _categoryService.GetCategoriesByCategoryParentIdWithCategoryType(model1.CategorizationSessionModel.CategoryId, CategoryTypeEnum.Brand);

            foreach (var brandItem in brandItems)
            {
                model.BrandItems.Add(new MTCategoryModel { CategoryId = brandItem.CategoryId, CategoryName = brandItem.CategoryName });
            }

            model.BrandItems.Add(new MTCategoryModel { CategoryId = 0, CategoryName = "Diğer" });
            model.CategoryIdSession = model1.CategorizationSessionModel.CategoryId;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }

        [HttpPost]
        public ActionResult Brand(AdvertViewModel model)
        {
            model.CategorizationSessionModel.BrandId = model.DropDownBrand;
            model.CategorizationSessionModel.SeriesId = model.DropDownSeries;
            model.CategorizationSessionModel.ModelId = model.DropDownModel;

            List<int> treeList;
            List<string> treeListForCategoryName;

            var AdvertVModel = new AdvertViewModel();
            if (model.DropDownModel.HasValue && model.DropDownModel > 0)
            {
                var treeItems = _categoryService.GetSPTopCategories(model.DropDownModel.Value);

                var ids = (from c in treeItems select c.CategoryId);
                var categoryNames = (from c in treeItems where c.CategoryContentTitle != null select c.CategoryContentTitle);

                treeList = ids.ToList();
                treeListForCategoryName = categoryNames.ToList();
            }
            else if (model.DropDownSeries.HasValue && model.DropDownSeries > 0)
            {
                var treeItems = _categoryService.GetSPTopCategories(model.DropDownSeries.Value);
                var ids = (from c in treeItems select c.CategoryId);
                var categoryNames = (from c in treeItems where c.CategoryContentTitle != null select c.CategoryContentTitle);

                treeList = ids.ToList();
                treeListForCategoryName = categoryNames.ToList();
            }
            else if (model.DropDownBrand.HasValue && model.DropDownBrand > 0)
            {
                var treeItems = _categoryService.GetSPTopCategories(model.DropDownBrand.Value);
                var ids = (from c in treeItems select c.CategoryId);
                var categoryNames = (from c in treeItems where c.CategoryContentTitle != null select c.CategoryContentTitle);

                treeList = ids.ToList();
                treeListForCategoryName = categoryNames.ToList();
            }
            else if (model.DropDownProductGroup > 0)
            {
                var treeItems = _categoryService.GetSPTopCategories(model.DropDownProductGroup);
                var ids = (from c in treeItems select c.CategoryId);
                var categoryNames = (from c in treeItems where c.CategoryContentTitle != null select c.CategoryContentTitle);

                treeList = ids.ToList();
                treeListForCategoryName = categoryNames.ToList();
            }
            else
            {
                var treeItems = _categoryService.GetSPTopCategories(AdvertVModel.CategorizationSessionModel.CategoryId);
                var ids = (from c in treeItems select c.CategoryId);
                var categoryNames = (from c in treeItems where c.CategoryContentTitle != null select c.CategoryContentTitle);

                treeList = ids.ToList();
                treeListForCategoryName = categoryNames.ToList();
            }

            var treList = new List<string>();
            for (int i = treeListForCategoryName.Count - 1; i >= 0; i--)
            {
                treList.Add(treeListForCategoryName[i].ToString());
            }

            Session["TreeListForCategoryName"] = treList;

            string treeName = String.Empty;
            for (int i = 0; i < treeList.Count; i++)
            {
                if (i == 0)
                {
                    treeName += treeList[i].ToString() + ".";
                }
                else
                    treeName += treeList[i].ToString() + ".";
            }

            if (!string.IsNullOrEmpty(model.OtherBrand) && model.OtherBrand.Trim() == "")
                model.OtherBrand = null;
            if (!string.IsNullOrEmpty(model.OtherModel) && model.OtherModel.Trim() == "") model.OtherModel = null;


            model.CategorizationSessionModel.OtherBrand = model.OtherBrand;
            model.CategorizationSessionModel.OtherModel = model.OtherModel;

            var categoryParent = _categoryService.GetCategoryByCategoryId(model.CategorizationSessionModel.CategoryId);
            if (!string.IsNullOrWhiteSpace(model.CategorizationSessionModel.OtherBrand))
            {
                treeListForCategoryName.Add(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.OtherBrand.ToLower()));


                model.CategorizationSessionModel.CategoryPath = String.Join(" - ", treeListForCategoryName.ToList());

                var category = new global::MakinaTurkiye.Entities.Tables.Catalog.Category
                {
                    Active = true,
                    RecordCreatorId = 99,
                    RecordDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    LastUpdaterId = 99,
                    CategoryType = (byte)CategoryType.Brand,
                    CategoryOrder = 0,
                    CategoryName = model.CategorizationSessionModel.OtherBrand,
                    CategoryParentId = model.CategorizationSessionModel.CategoryId,
                    MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                    CategoryContentTitle = model.CategorizationSessionModel.OtherBrand,
                    CategoryPath = model.CategorizationSessionModel.CategoryPath,

                };

                _categoryService.InsertCategory(category);

                treeName = treeName + category.CategoryId + ".";
                category.CategoryPathUrl = UrlBuilder.GetCategoryUrl(model.CategorizationSessionModel.CategoryId, !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName, category.CategoryId, category.CategoryName);
                _categoryService.UpdateCategory(category);

                model.DropDownBrand = category.CategoryId;
                model.CategorizationSessionModel.BrandId = category.CategoryId;
                model.CategorizationSessionModel.OtherBrand = "";
            }
            model.CategorizationSessionModel.CategoryTreeName = treeName;
            return RedirectToAction("ProductInfo", "Advert");
        }

        public ActionResult Video()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model = new AdvertViewModel();
            model.PictureList = PictureList;
            model.VideoItems = VideoList;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }

        [HttpPost]
        public ActionResult Video(FormCollection coll, string VideoTitle)
        {
            //if (VideoList.Count == 5)
            //{
            //  return RedirectToAction("ProductInfo", "Advert");
            //}
            HttpPostedFileBase file = Request.Files[0];


            if (file.ContentLength > 0)
            {

                VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                DateTime timesplit;


                if (DateTime.TryParse(vModel.Duration, out timesplit))
                {

                }
                else
                {
                    timesplit = DateTime.Now.Date;
                }

                var videoModel = new VideoModel
                {
                    Active = true,
                    VideoPath = vModel.newFileName,
                    VideoSize = null,
                    VideoPicturePath = vModel.newFileName + ".jpg",
                    VideoTitle = VideoTitle,
                    VideoRecordDate = DateTime.Now,
                    VideoMinute = (byte?)timesplit.Minute,
                    VideoSecond = (byte?)timesplit.Second
                };

                VideoList.Add(videoModel);
            }

            if (!PacketAuthenticationModel.IsAccess(PacketPage.VideoEkleme, VideoList.Count + 1))
            {
                VideoList.Clear();
                return RedirectToRoute("AdvertNotLimitedAccess");
            }
            if (file.ContentLength > 0)
            {
                var model = new AdvertViewModel();
                model.PictureList = PictureList;
                model.VideoItems = VideoList;
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
                model.PictureList = PictureList;
                return View(model);
            }


            return RedirectToAction("ProductCatolog", "Advert");
        }

        public ActionResult ProductVideos(int id)
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;

            var videos = _videoService.GetVideosByProductId(id);
            MTProductUpdateVideoModel model = new MTProductUpdateVideoModel();
            var product = _productService.GetProductByProductId(id);
            model.ProductName = product.ProductName;
            model.ProductId = id;
            foreach (var item in videos)
            {

                model.Videos.Add(new VideoModel
                {
                    Active = item.Active.HasValue ? item.Active.Value : false,
                    VideoPath = item.VideoPath,
                    ProductId = item.ProductId.HasValue ? item.ProductId.Value : 0,
                    VideoSize = null,
                    VideoPicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                    VideoId = item.VideoId,
                    VideoTitle = item.VideoTitle,
                    ProductName = product.ProductName,
                    VideoRecordDate = DateTime.Now,
                    VideoMinute = item.VideoMinute,
                    VideoSecond = item.VideoSecond,
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, product.ProductName),



                });
            }

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductVideos(MTProductUpdateVideoModel model)
        {
            if (!PacketAuthenticationModel.IsAccess(PacketPage.VideoEkleme, VideoList.Count + 1))
            {
                VideoList.Clear();
                return RedirectToRoute("AdvertNotLimitedAccess");
            }
            else
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];


                    if (file.ContentLength > 0)
                    {

                        VideoModelHelper vModel = FileHelpers.fffmpegVideoConvert(file, AppSettings.TempFolder, AppSettings.VideoThumbnailFolder, AppSettings.NewVideosFolder, AppSettings.ffmpegFolder, 490, 328);
                        DateTime timesplit;
                        if (DateTime.TryParse(vModel.Duration, out timesplit))
                        {

                        }
                        else
                        {
                            timesplit = DateTime.Now.Date;
                        }
                        var curVideo = new Video
                        {
                            Active = true,
                            ProductId = model.ProductId,
                            VideoPath = vModel.newFileName,
                            VideoPicturePath = vModel.newFileName + ".jpg",
                            VideoTitle = model.Title,
                            VideoRecordDate = DateTime.Now,
                            VideoMinute = (byte?)timesplit.Minute,
                            VideoSecond = (byte?)timesplit.Second,
                            SingularViewCount = 0,


                        };

                        _videoService.InsertVideo(curVideo);
                        TempData["SuccessMessage"] = "Video başarıyla eklenmiştir";
                    }
                }
                catch (Exception)
                {

                    TempData["ErrorMessage"] = "Bir hata oluştu lütfen tekrar deneyiniz";
                }

            }


            return RedirectToAction("ProductVideos", new { id = model.ProductId });

        }
        [HttpPost]
        public JsonResult DeleteProductVideo(int videoId)
        {
            var video = _videoService.GetVideoByVideoId(videoId);

            FileHelpers.Delete(AppSettings.VideoThumbnailFolder + video.VideoPicturePath);
            FileHelpers.Delete(AppSettings.VideoFolder + video.VideoPath);
            _videoService.DeleteVideo(video);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //category tree güncelleme
        //public ActionResult CategoryUpdate()
        //{
        //    string treeName = String.Empty;
        //    var categorytreeupdate = entities.Products.ToList();
        //    var dataCategory = new Data.Category();
        //    List<int> treeList;
        //    foreach (var item in categorytreeupdate)
        //    {
        //        if (item.ModelId != null)
        //        {
        //            var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.ModelId.Value).AsCollection<CategoryModel>();
        //            var ids = (from c in treeItems select c.CategoryId);
        //            var categoryNames = (from c in treeItems select c.CategoryName);

        //            treeList = ids.ToList();
        //        }
        //        else if (item.BrandId != null)
        //        {
        //            var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.BrandId.Value).AsCollection<CategoryModel>();
        //            var ids = (from c in treeItems select c.CategoryId);
        //            var categoryNames = (from c in treeItems select c.CategoryName);

        //            treeList = ids.ToList();

        //        }
        //        else if (item.SeriesId != null)
        //        {
        //            var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.SeriesId.Value).AsCollection<CategoryModel>();
        //            var ids = (from c in treeItems select c.CategoryId);
        //            var categoryNames = (from c in treeItems select c.CategoryName);

        //            treeList = ids.ToList();
        //        }
        //        else
        //        {
        //            var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.CategoryId.Value).AsCollection<CategoryModel>();
        //            var ids = (from c in treeItems select c.CategoryId);
        //            var categoryNames = (from c in treeItems select c.CategoryName);

        //            treeList = ids.ToList();

        //        }

        //        treeName = String.Empty;
        //        for (int i = 0; i < treeList.Count; i++)
        //        {
        //            treeName += treeList[i].ToString() + ".";
        //        }
        //        item.CategoryTreeName = treeName;



        //    }
        //    entities.SaveChanges();

        //    return View();
        //}


        public ActionResult Model()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var model = new AdvertViewModel();
            var dataCategory = new Data.Category();

            model.CategoryBrandItems.Add(new CategoryModel { CategoryId = 0, CategoryName = "Diğer" });

            model.PictureList = PictureList;

            return View(model);
        }

        [HttpPost]
        public ActionResult Series(int value)
        {
            var dataCategory = new Data.Category();
            var items =
              dataCategory.GetCategoryByCategoryParent(value, (byte)CategoryType.Series).AsCollection<CategoryModel>().ToList();
            if (items.Count <= 0)
            {
                return Content("NotSerie");
            }
            items.Add(new CategoryModel { CategoryId = 0, CategoryName = "Diğer" });
            return View(items);
        }

        [HttpPost]
        public ActionResult Model(int value)
        {
            var dataCategory = new Data.Category();
            var items =
              dataCategory.GetCategoryByCategoryParent(value, (byte)CategoryType.Model).AsCollection<CategoryModel>().ToList();

            items.Add(new CategoryModel { CategoryId = 0, CategoryName = "Diğer" });
            return View(items);
        }

        [ValidateInput(false)]
        public ActionResult ProductInfo()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            ViewData["ProductSales"] = false;

            int mainPartyId = AuthenticationUser.Membership.MainPartyId;



            if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
            {
                mainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId).StoreMainPartyId.Value;

                if (_storeService.GetStoreByMainPartyId(mainPartyId).ReadyForSale == true)
                {
                    ViewData["ProductSales"] = true;
                }
            }

            var memberAddress = _addressService.GetFisrtAddressByMainPartyId(mainPartyId);
            //var memberAddress = entities.Addresses.FirstOrDefault(c => c.MainPartyId == mainPartyId);
            if (memberAddress == null)
            {
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);

                if (memberStore != null)
                {
                    memberAddress = _addressService.GetFisrtAddressByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));

                }
            }

            var dataAddress = new Data.Address();
            var productModel = new ProductModel();

            var productPriceTypes = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProductPriceType).Where(x => x.ContstantPropertie != "241");

            productModel.ProductPriceTypes = productPriceTypes;

            var curCountry = new Classes.Country();
            //productModel.CountryItems = new SelectList(curCountry.GetDataTable().DefaultView, "CountryId", "CountryName", 0);
            productModel.CountryItems = new SelectList(_addressService.GetAllCountries(), "CountryId", "CountryName", 0);



            if (memberAddress != null)
            {
                memberAddress.CountryId = AppSettings.Turkiye;
                //var cityItems = dataAddress.CityGetItemByCountryId(memberAddress.CountryId.Value).AsCollection<CityModel>().ToList();
                var cityItems = _addressService.GetCitiesByCountryId(memberAddress.CountryId.Value);
                var cityItemChoose = new global::MakinaTurkiye.Entities.Tables.Common.City { CityId = 0, CityName = "< Lütfen Seçiniz >" };
                cityItems.Insert(0, cityItemChoose);

                //List<LocalityModel> localityItems = new List<LocalityModel>() { new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
                IList<global::MakinaTurkiye.Entities.Tables.Common.Locality> localityItems = new List<global::MakinaTurkiye.Entities.Tables.Common.Locality> { new global::MakinaTurkiye.Entities.Tables.Common.Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } };
                if (memberAddress.CityId != null)
                {
                    productModel.CityId = memberAddress.CityId.Value;
                    //localityItems = dataAddress.LocalityGetItemByCityId(memberAddress.CityId.Value).AsCollection<LocalityModel>().ToList();
                    localityItems = _addressService.GetLocalitiesByCityId(Convert.ToInt32(memberAddress.CityId));
                }

                IList<global::MakinaTurkiye.Entities.Tables.Common.District> districtItems = new List<global::MakinaTurkiye.Entities.Tables.Common.District>() { new global::MakinaTurkiye.Entities.Tables.Common.District { DistrictId = 0, DistrictName = "< Lütfen Seçiniz >" } };

                IList<global::MakinaTurkiye.Entities.Tables.Common.Town> townItems = new List<global::MakinaTurkiye.Entities.Tables.Common.Town>() { new global::MakinaTurkiye.Entities.Tables.Common.Town { TownId = 0, TownName = "< Lütfen Seçiniz >" } };
                if (memberAddress.LocalityId != null)
                {
                    productModel.LocalityId = memberAddress.LocalityId.Value;
                }

                if (memberAddress.TownId.HasValue)
                {
                    productModel.TownId = memberAddress.TownId.Value;
                    //townItems = entities.Towns.Where(c => c.LocalityId == memberAddress.LocalityId).ToList();
                    townItems = _addressService.GetTownsByLocalityId(Convert.ToInt32(memberAddress.LocalityId));
                }

                productModel.TownItems = new SelectList(townItems, "TownId", "TownName");
                productModel.CityItems = new SelectList(cityItems, "CityId", "CityName", 0);
                productModel.LocalityItems = new SelectList(localityItems, "LocalityId", "LocalityName");

                productModel.CountryId = memberAddress.CountryId.Value;

            }
            else
            {
                productModel.CityItems = new SelectList(new List<global::MakinaTurkiye.Entities.Tables.Common.City>() { new global::MakinaTurkiye.Entities.Tables.Common.City { CityId = 0, CityName = "< Lütfen Seçiniz>" } }, "CityId", "CityName", 0);
                productModel.LocalityItems = new SelectList(new List<global::MakinaTurkiye.Entities.Tables.Common.Locality>() { new global::MakinaTurkiye.Entities.Tables.Common.Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "LocalityName");
                productModel.TownItems = new SelectList(new List<global::MakinaTurkiye.Entities.Tables.Common.Town>() { new global::MakinaTurkiye.Entities.Tables.Common.Town { CityId = 0, TownName = "< Şehir >" } }, "TownId", "TownName");
                productModel.CountryId = AppSettings.Turkiye;
            }

            productModel.DropDownModelDate = 0;
            productModel.PictureList = PictureList;
            productModel.CurrencyId = 1;
            productModel.AllowSellUrl = CheckAllowProductSellUrl();

            productModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            var AdvertVModel = new AdvertViewModel();
            var curCategory = new Classes.Category();
            curCategory.LoadEntity(AdvertVModel.CategorizationSessionModel.CategoryId);

            string[] categoryIds = AdvertVModel.CategorizationSessionModel.CategoryTreeName.Substring(0, AdvertVModel.CategorizationSessionModel.CategoryTreeName.Length - 1).Split('.');

            bool anyCategoryPropertie = false;
            MTProductPropertieModel propertieModel = new MTProductPropertieModel();
            foreach (var item in categoryIds.Reverse())
            {
                var categoryProperties = _categoryPropertieService.GetPropertieByCategoryId(Convert.ToInt32(item));
                foreach (var categoryPropertieItem in categoryProperties)
                {
                    var propertieItem = new MTProductPropertieItem { InputName = categoryPropertieItem.PropertieId.ToString(), DisplayName = categoryPropertieItem.PropertieName, Type = categoryPropertieItem.PropertieType, PropertieId = categoryPropertieItem.PropertieId };
                    if (propertieItem.Type == (byte)PropertieType.MutipleOption)
                    {
                        var propertieAttrs = _categoryPropertieService.GetPropertiesAttrByPropertieId(categoryPropertieItem.PropertieId);
                        if (propertieAttrs.Count > 0)
                            propertieItem.Attrs.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
                        foreach (var propertieAttrItem in propertieAttrs.OrderBy(x => x.DisplayOrder).ThenBy(x => x.AttrValue))
                        {
                            propertieItem.Attrs.Add(new SelectListItem { Text = propertieAttrItem.AttrValue, Value = propertieAttrItem.PropertieAttrId.ToString() });
                        }
                    }
                    propertieModel.MTProductProperties.Add(propertieItem);
                    anyCategoryPropertie = true;
                    productModel.MTProductPropertieModel = propertieModel;
                }
                if (anyCategoryPropertie)
                    break;

            }

            productModel.SiparisList = _constantService.GetConstantByConstantType(ConstantTypeEnum.SiparisDurumu);
            productModel.TheOriginItems = _constantService.GetConstantByConstantType(ConstantTypeEnum.Mensei);

            var constants = _constantService.GetAllConstants();
            foreach (var item in constants)
            {
                productModel.ConstantItems.Add(new ConstantModel
                {
                    ConstantId = item.ConstantId,
                    ConstantName = item.ConstantName,
                    ConstantType = item.ConstantType.Value,
                    Order = item.Order.GetValueOrDefault()
                });
            }

            var currencies = _currencyService.GetAllCurrencies();
            currencies.Insert(0, new Currency { CurrencyId = 0, CurrencyName = "< Seçiniz >" });
            productModel.CurrencyItems = new SelectList(currencies, "CurrencyId", "CurrencyName");

            var certificateTypes = _certificateTypeService.GetCertificateTypes();
            var storeCertificates = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
            foreach (var item in storeCertificates)
            {
                var certificateType = _certificateTypeService.GetCertificateTypeProductsByProductId(productModel.ProductId).FirstOrDefault(x=>x.StoreCertificateId==item.StoreCertificateId);
                if (certificateType != null)
                {
                    productModel.CertificateTypes.Add(new SelectListItem
                    {
                        Text = item.CertificateName,
                        Value = certificateType.CertificateTypeId.ToString(),
                        Selected = true
                    });
                }
            }
            SessionProductPropertieModel.MTProductPropertieModel = propertieModel;
            return View(productModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ProductInfo(ProductModel modelProduct, FormCollection coll, string ProductPrice1, string productPriceType, string ProductPriceBegin, string ProductPriceLast, string[] certificateTypes)
        {
            DateTime dateProductEndDate = DateTime.Now;
            var pPublicDateType = (ProductPublicationDateType)modelProduct.ProductPublicationDateType;
            switch (pPublicDateType)
            {
                case ProductPublicationDateType.Gun:
                    dateProductEndDate = dateProductEndDate.AddDays(modelProduct.ProductPublicationDate);
                    break;
                case ProductPublicationDateType.Ay:
                    dateProductEndDate = dateProductEndDate.AddHours(modelProduct.ProductPublicationDate);
                    break;
                case ProductPublicationDateType.Yil:
                    dateProductEndDate = dateProductEndDate.AddYears(modelProduct.ProductPublicationDate);
                    break;
                default:
                    break;
            }

            var AdvertVModel = new AdvertViewModel();
            var curCategory = new Classes.Category();
            curCategory.LoadEntity(AdvertVModel.CategorizationSessionModel.CategoryId);

            string productName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LimitText(StringHelpers.ProductNameRegulator(modelProduct.ProductName).Trim(), 100).ToLower());

            var productNumbers = _productService.GetProductsByProductName(productName);
            if (productNumbers.Count > 0)
            {
                productName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LimitText(StringHelpers.ProductNameRegulator(modelProduct.ProductName.Trim()), 97).ToLower() + "(" + productNumbers.ToList().Count + ")");
            }
            var curProduct = new Product
            {
                CategoryId = AdvertVModel.CategorizationSessionModel.CategoryId,
                MainPartyId = AuthenticationUser.Membership.MainPartyId,
                ProductNo = String.Empty,
                CurrencyId = modelProduct.CurrencyId,
                ProductName = productName,
                ProductType = null,
                CategoryTreeName = AdvertVModel.CategorizationSessionModel.CategoryTreeName,
                ProductDescription = !string.IsNullOrEmpty(modelProduct.ProductDescription) ? modelProduct.ProductDescription.CleanProductDescriptionText() : "",
                ProductAdvertBeginDate = DateTime.Now,
                ProductActiveType = (byte)ProductActiveType.Inceleniyor,
                ProductActive = true,
                //OtherBrand = AdvertVModel.CategorizationSessionModel.OtherBrand,
                OtherModel = AdvertVModel.CategorizationSessionModel.OtherModel,
                ModelYear = modelProduct.DropDownModelDate,
                ProductGroupId = AdvertVModel.CategorizationSessionModel.ProductGroupId,
                ProductAdvertEndDate = dateProductEndDate,
                ViewCount = 0,
                SingularViewCount = 0,
                LastViewDate = null,
                ProductRecordDate = DateTime.Now,
                ProductLastUpdate = DateTime.Now,
                ReadyforSale = modelProduct.ReadyforSale,
                MenseiId = modelProduct.MenseiId,
                OrderStatus = modelProduct.OrderStatus,
                WarrantyPeriod = modelProduct.WarrantyPeriod,
                UnitType = coll["UnitType"].ToString(),
                ProductPriceType = Convert.ToByte(productPriceType),
                ProductSellUrl = modelProduct.ProductSellUrl,
                Keywords = PrepareKeyword(productName)

            };
            string productPrice = ProductPrice1;
            System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("tr-TR");
            if (productPriceType == Convert.ToString((byte)ProductPriceType.Price))
            {
                curProduct.Kdv = false;
                curProduct.Fob = false;
                if (coll["pricePropertie"].ToString() == "kdvdahil")
                    curProduct.Kdv = true;
                else if (coll["pricePropertie"].ToString() == "fob")
                    curProduct.Fob = true;

                if (productPrice.Trim() != "")
                {

                    if (productPrice.IndexOf('.') > 0)
                    {
                        productPrice = productPrice.Replace(".", "");
                    }
                }
                else productPrice = "0";


                curProduct.DiscountType = Convert.ToByte(modelProduct.DiscountType);
                if (modelProduct.DiscountType != 0)
                {

                    if (!string.IsNullOrEmpty(coll["DiscountAmount"]))
                    {
                        curProduct.DiscountAmount = Convert.ToDecimal(modelProduct.DiscountAmount);
                        string totalPrice = coll["TotalPrice"];
                        if (totalPrice.IndexOf('.') > 0 && totalPrice.IndexOf(",") > 0)
                        {
                            totalPrice = totalPrice.Replace(".", "");
                        }
                        else
                        {
                            totalPrice = totalPrice.Replace(".", ",");
                        }
                        curProduct.ProductPriceWithDiscount = Convert.ToDecimal(totalPrice, culInfo.NumberFormat);
                    }

                }
                curProduct.ProductPrice = Convert.ToDecimal(productPrice, culInfo.NumberFormat);
            }
            else if (productPriceType == Convert.ToString((byte)ProductPriceType.PriceRange))//product price
            {
                curProduct.Kdv = false;
                curProduct.Fob = false;
                if (coll["pricePropertie"].ToString() == "kdvdahil")
                    curProduct.Kdv = true;
                else if (coll["pricePropertie"].ToString() == "fob")
                    curProduct.Fob = true;


                string productPriceBegin = ProductPriceBegin.ToString();
                if (productPriceBegin.Trim() != "")
                {
                    if (productPriceBegin.IndexOf('.') > 0)
                    {
                        productPriceBegin = productPriceBegin.Replace(".", "");
                    }
                }
                string productPriceLast = ProductPriceLast.ToString();

                if (productPriceLast.Trim() != "")
                {
                    if (productPriceLast.IndexOf('.') > 0)
                    {
                        productPriceLast = productPriceLast.Replace(".", "");
                    }
                }
                curProduct.ProductPriceBegin = Convert.ToDecimal(productPriceBegin, culInfo);
                curProduct.ProductPriceLast = Convert.ToDecimal(productPriceLast, culInfo);
            }
            else
            {
                curProduct.ProductPriceBegin = 0;
                curProduct.ProductPriceLast = 0;
                curProduct.ProductPrice = 0;

            }

            if (!string.IsNullOrWhiteSpace(AdvertVModel.CategorizationSessionModel.OtherBrand))
            {
                curProduct.OtherBrand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(AdvertVModel.CategorizationSessionModel.OtherBrand.ToLower());
            }
            else curProduct.OtherBrand = AdvertVModel.CategorizationSessionModel.OtherBrand;
            curProduct.ProductType = modelProduct.ProductType;
            curProduct.ProductStatu = modelProduct.ProductStatu;

            string ProductSalesType = String.Empty;
            if (coll["ProductSalesType"] != null)
            {
                string[] acProductSalesType = coll["ProductSalesType"].Split(',');
                if (acProductSalesType != null)
                {
                    for (int i = 0; i < acProductSalesType.Length; i++)
                    {
                        if (acProductSalesType.GetValue(i).ToString() != "false")
                        {
                            if (string.IsNullOrEmpty(ProductSalesType))
                                ProductSalesType = acProductSalesType.GetValue(i).ToString();
                            else
                                ProductSalesType = ProductSalesType + "," + acProductSalesType.GetValue(i).ToString();
                        }
                    }
                }
            }
            curProduct.ProductSalesType = ProductSalesType;

            string BriefDetail = String.Empty;
            if (coll["ProductBriefDetail"] != null)
            {
                //string[] acBriefDetail = coll["ProductBriefDetail"].Split(',');
                //if (acBriefDetail != null)
                //{
                //    for (int i = 0; i < acBriefDetail.Length; i++)
                //    {
                //        if (acBriefDetail.GetValue(i).ToString() != "false")
                //        {
                //            if (string.IsNullOrEmpty(BriefDetail))
                //                BriefDetail = acBriefDetail.GetValue(i).ToString();
                //            else
                //                BriefDetail = BriefDetail + "," + acBriefDetail.GetValue(i).ToString();
                //        }
                //    }
                //}
                BriefDetail = coll["ProductBriefDetail"].ToString();
            }
            curProduct.BriefDetail = BriefDetail;


            if (modelProduct.CountryId > 0)
                curProduct.CountryId = modelProduct.CountryId;
            else
                curProduct.CountryId = null;

            if (modelProduct.CityId > 0)
                curProduct.CityId = modelProduct.CityId;
            else
                curProduct.CityId = null;

            if (modelProduct.LocalityId > 0)
                curProduct.LocalityId = modelProduct.LocalityId;
            else
                curProduct.LocalityId = null;

            if (modelProduct.TownId > 0)
                curProduct.TownId = modelProduct.TownId;
            else
                curProduct.TownId = null;

            curProduct.DistrictId = null;

            if (modelProduct.CurrencyId > 0)
                curProduct.CurrencyId = modelProduct.CurrencyId;
            else
                curProduct.CurrencyId = null;

            if (AdvertVModel.CategorizationSessionModel.ModelId != 0)
            {
                curProduct.ModelId = AdvertVModel.CategorizationSessionModel.ModelId;
            }
            if (AdvertVModel.CategorizationSessionModel.BrandId != 0)
            {
                curProduct.BrandId = AdvertVModel.CategorizationSessionModel.BrandId;
            }
            if (AdvertVModel.CategorizationSessionModel.SeriesId != 0)
            {
                curProduct.SeriesId = AdvertVModel.CategorizationSessionModel.SeriesId;
            }

            _productService.InsertProduct(curProduct);

            int producId = curProduct.ProductId;

            string productNo = "#";
            for (int i = 0; i < 8 - producId.ToString().Length; i++)
            {
                productNo = productNo + "0";
            }
            productNo = productNo + producId;


            curProduct.ProductNo = productNo;
            _productService.UpdateProduct(curProduct);

            curProduct.ProductId = producId;
            curProduct.ProductNo = productNo;
            Session["CurProduct"] = curProduct;

            var propertieModel = SessionProductPropertieModel.MTProductPropertieModel;
            if (propertieModel != null)
            {
                foreach (var item in propertieModel.MTProductProperties)
                {
                    var val = Request.Form[item.InputName];
                    if (!string.IsNullOrEmpty(val))
                    {
                        var productPropertie = new global::MakinaTurkiye.Entities.Tables.Catalog.ProductPropertieValue();
                        productPropertie.ProductId = curProduct.ProductId;
                        productPropertie.PropertieId = item.PropertieId;
                        productPropertie.PropertieType = item.Type;
                        productPropertie.Value = val;
                        _categoryPropertieService.InsertProductProertieValue(productPropertie);
                    }
                }

            }
            if (certificateTypes != null)
            {
                for (int i = 0; i < certificateTypes.Length; i++)
                {
                    var certificateTypeProduct = new CertificateTypeProduct
                    {
                        CertificateTypeId = Convert.ToInt32(certificateTypes[i]),
                        ProductId = curProduct.ProductId,

                    };
                    _certificateTypeService.InsertCertificateTypeProduct(certificateTypeProduct);
                }
            }
            return RedirectToAction("Picture", "Advert");
        }
        public string PrepareKeyword(string productName)
        {
            string keyword = "";
            if (productName.Contains("Makinası"))
            {
                keyword = productName.Replace("Makinası","Makinesi").ToLower();
            }
            else if(productName.Contains("Makinesi"))
            {
                keyword = productName.Replace("Makinesi", "Makinası").ToLower();
            }
            else if (productName.Contains("Makineleri"))
            {
                keyword = productName.Replace("Makineleri", "Makinaları").ToLower();
            }
            else if (productName.Contains("Makinaları"))
            {
                keyword = productName.Replace("Makinaları", "Makineleri").ToLower();
            }

            return keyword;
        }

        public bool CheckAllowProductSellUrl()
        {
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            return store.IsAllowProductSellUrl.HasValue ? store.IsAllowProductSellUrl.Value : false;

        }
        public ActionResult AdvertEnd()
        {
            ViewData["AdvertMenuActiveAdd"] = activeStyleRed;
            var curProduct = (Product)Session["CurProduct"];
            var productModel = new ProductModel();
            UpdateClass(curProduct, productModel);
            decimal fiyat = curProduct.ProductPrice.Value;
            byte currid = (byte)curProduct.CurrencyId;

            var cu = _currencyService.GetCurrencyByCurrencyId(currid);

            string curr = cu.CurrencyName.ToString();
            string tutar = fiyat.ToString("0.00");
            if (tutar == "0,00")
            {
                ViewData["dov"] = "Belirtilmedi";
            }
            else
            {
                ViewData["dov"] = tutar + " " + curr;
            }
            if (VideoList.Any())
            {
                //productModel.HasVideo = true;
            }
            productModel.PictureList = PictureList;
            productModel.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);

            return View(productModel);
        }

        [HttpPost]
        public ActionResult AdverEnd(FormCollection coll)
        {
            var curProduct = (Product)Session["CurProduct"];

            #region Picture & Video Save
            var curPicture = new Classes.Picture();
            int counter = 1;
            foreach (PictureModel item in PictureList)
            {
                var picture = new global::MakinaTurkiye.Entities.Tables.Media.Picture();

                picture.PictureName = "";
                picture.PicturePath = item.PicturePath;
                picture.ProductId = curProduct.ProductId;
                picture.PictureOrder = counter;
                _pictureService.InsertPicture(picture);
                counter++;
            }
            int videoCounter = 0;
            foreach (VideoModel itemVideo in VideoList)
            {
                videoCounter++;
                var curVideo = new Video
                {
                    Active = itemVideo.Active,
                    ProductId = curProduct.ProductId,
                    VideoPath = itemVideo.VideoPath,
                    VideoPicturePath = itemVideo.VideoPicturePath,
                    VideoRecordDate = itemVideo.VideoRecordDate,
                    VideoSize = itemVideo.VideoSize,
                    VideoTitle = curProduct.ProductName,
                    SingularViewCount = 0,
                    VideoMinute = itemVideo.VideoMinute,
                    VideoSecond = itemVideo.VideoSecond
                };
                if (VideoList.Count > 1)
                {
                    curVideo.VideoTitle = itemVideo.VideoTitle + " (" + videoCounter + ")";
                }

                _videoService.InsertVideo(curVideo);
            }

            #endregion

            var product = _productService.GetProductByProductId(curProduct.ProductId);
            if (product != null)
            {
                if (videoCounter > 0)
                {
                    if (!product.HasVideo)
                    {
                        product.HasVideo = true;
                        _productService.UpdateProduct(product);
                    }

                }
                else
                {
                    if (product.HasVideo)
                    {
                        product.HasVideo = false;
                        _productService.UpdateProduct(product);
                    }

                }
            }
            return RedirectToAction("AdvertSuccessfull", "Advert");
        }

        public ActionResult AdvertSuccessfull()
        {
            AdvertSuccessfullModel model = new AdvertSuccessfullModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);

            return View(model);
        }

        public string LimitText(string text, short limit)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > limit)
                {
                    return text.Substring(0, limit);
                }
                return text;
            }
            return "";
        }

        [HttpPost]
        public JsonResult Cities(int id)
        {
            var dataAddress = new Data.Address();
            var items = dataAddress.CityGetItemByCountryId(id).AsCollection<CityModel>().ToList();
            var cityItems = new SelectList(items, "CityId", "CityName");

            return Json(cityItems, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Localities(int id)
        {
            var dataAddress = new Data.Address();
            var items = dataAddress.LocalityGetItemByCityId(id).AsCollection<LocalityModel>().ToList();
            items.Insert(0, new LocalityModel { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
            var localityItems = new SelectList(items, "LocalityId", "LocalityName");

            return Json(localityItems);
        }

        [HttpPost]
        public JsonResult Towns(int id)
        {
            var townItems = _addressService.GetTownsByLocalityId(id);
            townItems.Insert(0, new Town { TownId = 0, TownName = "< Lütfen Seçiniz >" });

            return Json(new SelectList(townItems, "TownId", "TownName"));
        }

        public bool IsNumeric(string text)
        {
            return Regex.IsMatch(text.ToString(), "^\\d+$");
        }


        [HttpPost]
        public JsonResult CategoryProductGroup(int id)
        {
            IList<Category> Catitems = _categoryService.GetCategoriesByCategoryParentIdWithCategoryType(id, CategoryTypeEnum.ProductGroup);
            var category = new SelectList(Catitems, "CategoryId", "CategoryName");
            return Json(category);
        }

        [HttpGet]
        public PartialViewResult _ProductInfoDoping(int productId)
        {
            MTProductDopingModel model = new MTProductDopingModel();
            var product = _productService.GetProductByProductId(productId);
            model.ProductName = product.ProductName;
            var packets = _packetService.GetAllPacket().Where(x => x.IsDopingPacket == true).OrderBy(x => x.DopingPacketDay);
            foreach (var item in packets)
            {
                model.Packets.Add(new SelectListItem
                {
                    Text = item.PacketName.Split(' ')[0],
                    Value = item.PacketId.ToString()

                });
            }

            model.CategoryName = product.Category.CategoryContentTitle;
            model.ProductId = productId;
            model.ProductDopingPricePerMonth = packets.FirstOrDefault().PacketPrice;
            return PartialView("_ProductInfoDoping", model);
        }
        [HttpGet]
        public string GetDopingPrice(int PacketId)
        {
            var packet = _packetService.GetPacketByPacketId(PacketId);
            //float perDay = (float)packet;
            //float newPrice = perDay * DopingDay;
            return packet.PacketPrice.ToString("C2");
        }
        [HttpPost]
        public string SellProductDoping(int productId, int dopingDay, int packetId)
        {

            //string Url=
            //return View();
            return "";
        }
        [HttpPost]
        public JsonResult ProductActiveUpdate(int ProductId, bool Active)
        {
            var product = _productService.GetProductByProductId(ProductId);
            if (Active)
            {
                product.ProductActiveType = (byte)ProductActiveType.Inceleniyor;

            }
            if (product.ProductActive == true && Active == false)
            {
                if (product.ProductActive == true)
                {
                    ProductCountCalc(product, false);
                }
            }
            product.ProductActive = Active;
            product.ProductLastUpdate = DateTime.Now;
            _productService.UpdateProduct(product);

            return Json(true);
        }

        [HttpPost]
        public JsonResult ProductUndoDelete(int ProductId, bool Active)
        {
            var product = _productService.GetProductByProductId(ProductId);
            if (Active)
            {
                product.ProductActiveType = (byte)ProductActiveType.Onaylandi;

            }
            if (product.ProductActive == false && product.ProductActiveType == (byte)ProductActiveType.Onaylandi)
            {
                ProductCountCalc(product, true);
            }
            product.ProductActive = Active;
            product.ProductLastUpdate = DateTime.Now;
            _productService.UpdateProduct(product);

            return Json(true);
        }

        [HttpPost]
        public JsonResult ProductToReceyleBin(int ProductId)
        {
            var product = _productService.GetProductByProductId(ProductId);
            product.ProductActiveType = (byte)ProductActiveType.CopKutusunda;
            product.ProductActive = false;
            if (product.ProductActive == false && product.ProductActiveType == (byte)ProductActiveType.CopKutusunda)
            {
                ProductCountCalc(product, false);
            }
            product.ProductLastUpdate = DateTime.Now;
            _productService.UpdateProduct(product);

            return Json(true);
        }
        [HttpPost]
        public JsonResult ProductDelete(int ProductId)
        {

            //ilan silindiği zaman kullanıcıya mail gidecek.
            var product = _productService.GetProductByProductId(ProductId);
            if (product.ProductActiveType != (byte)ProductActiveType.Silindi)
            {
                ProductCountCalc(product, false);
            }
            product.ProductActiveType = (byte)ProductActiveType.Silindi;
            product.ProductLastUpdate = DateTime.Now;
            _productService.UpdateProduct(product);

            return Json(true);

        }
        public ActionResult NotLimitedAccess()
        {
            var model = new AdvertViewModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);
            return View(model);
        }

        public ActionResult NotMemberTypeAccess()
        {
            var model = new AdvertViewModel();
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.AddAd);


            return View(model);
        }


        [HttpPost]
        public JsonResult ProductSortUpdate(int ProductId, int SortNumber)
        {
            var product = _productService.GetProductByProductId(ProductId);
            if (SortNumber > 0)
            {
                product.Sort = SortNumber;
            }
            else
            {
                product.Sort = 0;
            }
            product.ProductLastUpdate = DateTime.Now;
            _productService.UpdateProduct(product);

            //var products = entities.Products.Where(x => x.Sort == null);
            //foreach (var item in products)
            //{
            //    item.Sort = 0;
            //}
            //entities.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult ProductSortGet(int ProductId)
        {
            var product = _productService.GetProductByProductId(ProductId);
            return Json(product.Sort.ToString());
        }



        [HttpPost]
        public ActionResult ProductSearchGet(int ProductId)
        {
            var product = _productService.GetProductByProductId(ProductId);
            return Json(product.ProductPrice.Value.ToString("N2"));
        }
        [HttpPost]
        public ActionResult PriceUpdateAdvertList(int id, string price)
        {
            var product = _productService.GetProductByProductId(id);
            try
            {
                product.ProductPrice = Convert.ToDecimal(price);
                product.ProductLastUpdate = DateTime.Now;
                _productService.UpdateProduct(product);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult AdvertSearch(string name)
        {
            var storeforProduct = _productService.GetProductsByMainPartyId(AuthenticationUser.Membership.MainPartyId);
            var productNames = from p in storeforProduct where p.ProductName.ToLower().Contains(name.Trim().ToLower()) select (p.ProductName);
            return Json(productNames.ToList().Take(5), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult CheckProductName(string productname)
        {
            var anyProduct = _productService.GetProductsByProductName(productname);
            if (anyProduct.Count > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet); ;

        }


        public static string RenderViewToStringCsHtml(ControllerContext context, string viewPath, object model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view, context.Controller.ViewData, context.Controller.TempData, sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }


        public static string RenderPartialToString(string controlName, object viewData)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext() };

            viewPage.ViewData = new ViewDataDictionary(viewData);
            viewPage.Controls.Add(viewPage.LoadControl(controlName));

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                    viewPage.RenderControl(tw);
                }
            }

            return sb.ToString();
        }


        public ActionResult Comments()
        {

            var model = new MTCommentsViewModel();
            int page = 1;
            int pageDimension = 20;
            var productComments = _productCommentService.GetProductCommentsForStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var commentsList = new List<MTProductCommentStoreItem>();
            SearchModel<MTProductCommentStoreItem> productCommentsFilter = new SearchModel<MTProductCommentStoreItem>();
            productCommentsFilter.TotalRecord = productComments.Count;
            productCommentsFilter.PageDimension = pageDimension;
            productCommentsFilter.CurrentPage = page;
            productComments = productComments.OrderByDescending(x => x.ProductCommentId).Skip(page * pageDimension - pageDimension).Take(pageDimension).ToList();
            foreach (var item in productComments)
            {
                var member = _memberService.GetMemberByMainPartyId(item.MemberMainPartyId);
                item.Product = _productService.GetProductByProductId(item.ProductId);

                commentsList.Add(new MTProductCommentStoreItem
                {
                    CommentText = item.CommentText,
                    MemberNameSurname = member.MemberName + " " + member.MemberSurname,
                    Rate = item.Rate.Value,
                    RecordDate = item.RecordDate,
                    ProductCommentId = item.ProductCommentId,
                    ProductName = item.Product.ProductName,
                    ProductNo = item.Product.ProductNo,
                    Status = item.Status,
                    Reported = item.Reported,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.Product.ProductName)
                });
                if (item.Viewed == false)
                {
                    item.Viewed = true;
                    _productCommentService.UpdateProductComment(item);
                }
            }

            productCommentsFilter.Source = commentsList;
            model.ProductCommentStoreItems = productCommentsFilter;
            model.LeftMenu = model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAds, (byte)LeftMenuConstants.MyAd.Comments);

            return View(model);
        }
        [HttpPost]
        public PartialViewResult commentpaging(int page)
        {
            int pageDimension = 20;
            var productComments = _productCommentService.GetProductCommentsForStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            var commentsList = new List<MTProductCommentStoreItem>();

            SearchModel<MTProductCommentStoreItem> productCommentsFilter = new SearchModel<MTProductCommentStoreItem>();
            productCommentsFilter.TotalRecord = productComments.Count;
            productCommentsFilter.PageDimension = pageDimension;
            productCommentsFilter.CurrentPage = page;
            productComments = productComments.OrderByDescending(x => x.ProductCommentId).Skip(page * pageDimension - pageDimension).Take(pageDimension).ToList();
            foreach (var item in productComments)
            {
                item.Member = _memberService.GetMemberByMainPartyId(item.MemberMainPartyId);
                item.Product = _productService.GetProductByProductId(item.ProductId);
                commentsList.Add(new MTProductCommentStoreItem
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

            productCommentsFilter.Source = commentsList;
            return PartialView("_CommentList", productCommentsFilter);
        }

        [HttpPost]
        public ActionResult ReportProductComment(int productCommentId)
        {
            var productComment = _productCommentService.GetProductCommentByProductCommentId(productCommentId);

            productComment.Reported = true;

            _productCommentService.UpdateProductComment(productComment);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProductsUpdateDate()
        {
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            _productService.CachingGetOrSetOperationEnabled = false;
            var products = _productService.GetProductsByMainPartyId(memberMainPartyId,true);

            foreach (var item in products)
            {
                item.ProductLastUpdate = DateTime.Now;
                _productService.UpdateProduct(item);

            }
            TempData["Message"] = "Ürünler Güncellenmiştir";
            return Redirect("/Account/Advert/Index?ProductActive=1&DisplayType=2");
        }

    }


}