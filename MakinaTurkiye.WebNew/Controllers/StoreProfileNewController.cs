using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Videos;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Settings;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.Mvc;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models.Products;
using NeoSistem.MakinaTurkiye.Web.Models.StoreNews;
using NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles;
using NeoSistem.MakinaTurkiye.Web.Models.Videos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class StoreProfileNewController : BaseController
    {
        #region Constants

        private const string CATEGORY_ID_QUERY_STRING_KEY = "CategoryId";

        #endregion Constants

        #region Fields

        private readonly IStoreInfoNumberShowService _storeNumberShowService;
        private readonly IStoreService _storeService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IVideoService _videoService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreDealerService _storeDealerService;
        private readonly IStoreBrandService _storeBrandService;
        private readonly IFavoriteStoreService _favoriteStoreService;
        private readonly IConstantService _constantService;
        private readonly IMemberService _memberService;
        private readonly IPhoneService _phoneService;
        private readonly IAddressService _addressService;
        private readonly IStoreStatisticService _storeStatisticService;
        private readonly IStoreCatologFileService _storeCatologFileService;
        private readonly IStoreNewService _storeNewService;
        private readonly IMemberSettingService _memberSettingService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IDealarBrandService _dealarBrandService;

        #endregion Fields

        #region Ctor

        public StoreProfileNewController(IStoreInfoNumberShowService storeNumberShowService,
            IStoreNewService storeNewService, IStoreCatologFileService storeCatologFileService,
            IStoreStatisticService storeStatisticService, IStoreService storeService, IPictureService pictureService,
            IProductService productService, IMemberStoreService memberStoreService, IVideoService videoService,
            ICategoryService categoryService, IStoreDealerService storeDealerService, IStoreBrandService storeBrandService,
            IFavoriteStoreService favoriteStoreService, IConstantService constantService, IMemberService memberService,
            IPhoneService phoneService,
            IAddressService addressService,
            IMemberSettingService memberSettingService,
            IStoreActivityTypeService storeActivityTypeService, IDealarBrandService dealarBrandService)
        {
            _storeNumberShowService = storeNumberShowService;
            _storeService = storeService;
            _pictureService = pictureService;
            _productService = productService;
            _memberStoreService = memberStoreService;
            _videoService = videoService;
            _categoryService = categoryService;
            _storeDealerService = storeDealerService;
            _storeBrandService = storeBrandService;
            _constantService = constantService;
            _favoriteStoreService = favoriteStoreService;
            _memberService = memberService;
            _phoneService = phoneService;
            _addressService = addressService;
            _storeStatisticService = storeStatisticService;
            _storeNewService = storeNewService;
            _storeCatologFileService = storeCatologFileService;
            _memberSettingService = memberSettingService;
            _storeActivityTypeService = storeActivityTypeService;
            _dealarBrandService = dealarBrandService;
        }

        #endregion Ctor

        #region Utilities

        //public void CreateSeoForStore(Store store)
        //{
        //    if (!string.IsNullOrEmpty(store.StoreShortName))
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, store.StoreShortName);
        //    else
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, store.StoreName);

        //    CreateSeoParameter(SeoModel.SeoProductParemeters.FirmSeoTitle, store.SeoTitle);
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.FirmSeoDescription, store.SeoDescription);
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.FirmSeoKeyword, store.SeoKeyword);
        //}

        public void PrepareFilterModel(MTStoreProfileProductsModel model)
        {
            CustomFilterModel filterAll = new CustomFilterModel();
            filterAll.FilterName = "Tümü";
            filterAll.FilterId = 1;
            filterAll.FilterUrl = Request.Url.AbsolutePath + "?SearchType=tumu";
            var productAllCount = model.MTProductsProductListModel.MTProductsPageProductLists.TotalRecord;
            filterAll.ProductCount = productAllCount;
            if (GetSearchTypeQueryString() == "tumu" || string.IsNullOrEmpty(GetSearchTypeQueryString()))
            {
                filterAll.Selected = true;
            }
            model.CustomFilterModels.Add(filterAll);

            CustomFilterModel filterNew = new CustomFilterModel();
            filterNew.FilterId = 2;
            filterNew.FilterName = "Sıfır";
            filterNew.FilterUrl = Request.Url.AbsolutePath + "?SearchType=sifir";
            int totalRecordNew = 0;

            _productService.GetSPProductsCountByStoreMainPartyIdAndSearchType(out totalRecordNew, model.MTProductsProductListModel.StoreMainPartyId, 72, model.CategoryId);
            if (GetSearchTypeQueryString() == "sifir")
            {
                filterNew.Selected = true;
            }
            filterNew.ProductCount = totalRecordNew;
            model.CustomFilterModels.Add(filterNew);

            CustomFilterModel filterOld = new CustomFilterModel();
            filterOld.FilterId = 3;
            filterOld.FilterName = "İkinci El";
            filterOld.FilterUrl = Request.Url.AbsolutePath + "?SearchType=ikinciel";
            int totalRecordOld = 0;

            _productService.GetSPProductsCountByStoreMainPartyIdAndSearchType(out totalRecordOld, model.MTProductsProductListModel.StoreMainPartyId, 73, model.CategoryId);
            filterOld.ProductCount = totalRecordOld;
            if (GetSearchTypeQueryString() == "ikinciel")
            {
                filterOld.Selected = true;
            }
            model.CustomFilterModels.Add(filterOld);

            CustomFilterModel filterService = new CustomFilterModel();
            filterService.FilterId = 4;
            filterService.FilterName = "Hizmetler";
            int productCountService = 0;

            _productService.GetSPProductsCountByStoreMainPartyIdAndSearchType(out productCountService, model.MTProductsProductListModel.StoreMainPartyId, 201, model.CategoryId);
            filterService.ProductCount = productCountService;
            filterService.FilterUrl = Request.Url.AbsolutePath + "?SearchType=hizmet";
            if (GetSearchTypeQueryString() == "hizmet")
            {
                filterService.Selected = true;
            }
            model.CustomFilterModels.Add(filterService);
        }

        private void PrepareCertificates(MTCompanyProfileModel model, int mainPartyId)
        {
            var certificates = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);

            foreach (var item in certificates)
            {
                var pictures = _pictureService.GetPictureByStoreCertificateId(item.StoreCertificateId);
                int counter = 1;
                foreach (var picture in pictures)
                {
                    string certificateName = item.CertificateName;
                    if (pictures.Count > 1)
                        certificateName = certificateName + " - " + counter;
                    model.Certificates.Add(new MTCertificateItem
                    {
                        ImagePath = AppSettings.StoreCertificateImageFolder + picture.PicturePath.Replace("_certificate", "-500x800"),
                        Name = certificateName,
                        ImageFullPath = AppSettings.StoreCertificateImageFolder + picture.PicturePath
                    });
                }
            }
        }

        public void PrepareVideoStoreCategoryModel(IList<VideoCategoryResult> videoCategories, MTStoreProfileVideoModel model)
        {
            int categoryId = GetCategoryIdQueryString();
            string searchText = GetSearchTextQueryString();

            if (categoryId > 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(categoryId);
                if (topCategories.Count > 0)
                {
                    var lastCategory = topCategories.LastOrDefault();
                    model.VideoCategoryModel.SelectedCategoryId = categoryId;
                    model.VideoCategoryModel.SelectedCategoryName = lastCategory.CategoryName;
                }
                foreach (var item in topCategories.Where(x => x.CategoryType != (byte)CategoryType.Sector))
                {
                    string categoryUrl = QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), CATEGORY_ID_QUERY_STRING_KEY + "=" + item.CategoryId, null);
                    var categoryItemModel = new MTVideoCategoryItemModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = item.CategoryParentId,
                        CategoryType = item.CategoryType,
                        CategoryUrl = categoryUrl
                    };
                    model.VideoCategoryModel.VideoTopCategoryItemModels.Add(categoryItemModel);
                }
            }

            foreach (var item in videoCategories)
            {
                string categoryUrl = QueryStringBuilder.ModifyQueryString(this.Request.Url.ToString(), CATEGORY_ID_QUERY_STRING_KEY + "=" + item.CategoryId, null);

                var categoryItemModel = new MTVideoCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryUrl = categoryUrl
                };
                model.VideoCategoryModel.VideoCategoryItemModels.Add(categoryItemModel);
            }
        }

        public void PrepareVideosModel(IList<VideoResult> videos, MTStoreProfileVideoModel storeProfileModel)
        {
            foreach (var item in videos)
            {
                var videoModel = new MTVideoModel
                {
                    PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    ShortDescription = StringHelper.Truncate(string.Format("{0}-{1}", item.BrandName, item.ModelName), 25),
                    SingularViewCount = item.SingularViewCount,
                    StoreName = item.StoreName,
                    StoreUrl = UrlBuilder.GetStoreProfileUrl(item.StoreId, item.StoreName, item.StoreUrlName),
                    //TruncateProductName = item.ProductName.Length > 35 ? StringHelper.Truncate(item.ProductName, 35) + "..." : item.ProductName,
                    TruncateProductName = (item.ProductName.Length > 65) ? StringHelper.Truncate(item.ProductName, 65) + "..." : item.ProductName,
                    TruncateStoreName = item.StoreName.Length > 35 ? StringHelper.Truncate(item.StoreName, 35) + "..." : item.StoreName,
                    VideoMinute = item.Minute.HasValue ? item.Minute.Value : (byte)1,
                    VideoSecond = item.Second.HasValue ? item.Second.Value : (byte)1,
                    VideoRecordDate = string.Format("{0:dd/MM/yyyy}", item.VideoRecordDate),
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                };
                storeProfileModel.VideosPage.Add(videoModel);
            }
        }

        public void PrepareCompanyProfileStoreMemberAndMap(MTCompanyProfileModel model, Store store, int memberMainPartyId)
        {
            var member = _memberService.GetMemberByMainPartyId(memberMainPartyId);
            MTStoreMemberAndMapModel modelSub = new MTStoreMemberAndMapModel();
            modelSub.AuthorizedNameSurname = member.MemberName + " " + member.MemberSurname;
            var phones = _phoneService.GetPhonesByMainPartyId(memberMainPartyId);
            modelSub.Phones = phones;
            modelSub.StoreWebUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
            model.MTStoreMemberAndMapModel = modelSub;
            modelSub.StoreName = store.StoreName;
            Address address = new Address();
            address = _addressService.GetFisrtAddressByMainPartyId(store.MainPartyId);
            modelSub.StoreAddress = address;

            modelSub.AddressMap = address.GetAddressEdit();
            if (!string.IsNullOrEmpty(store.StoreWeb))
            {
                if (store.StoreWeb == "http://")
                {
                    modelSub.StoreWebUrl = AppSettings.SiteUrlWithoutLastSlash + UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                }
                else if (store.StoreWeb.Contains("http"))
                {
                    modelSub.StoreWebUrl = store.StoreWeb;
                }
                else
                {
                    if (store.StoreWeb.Contains("makinaturkiye"))
                    {
                        modelSub.StoreWebUrl = "https://" + store.StoreWeb;
                    }
                    else
                    {
                        modelSub.StoreWebUrl = "http://" + store.StoreWeb;
                    }
                }
            }
            model.MTStoreMemberAndMapModel = modelSub;
        }

        public void PrepareCompanyProfileImageAndVideos(MTCompanyProfileModel model, Store store, int memberMainPartyId)
        {
            var pictures = _pictureService.GetPictureByMainPartyId(Convert.ToInt32(store.MainPartyId));
            foreach (var item in pictures)
            {
                model.MTStoreImageAndVideosModel.StoreImages.Add(AppSettings.StoreImageFolder + item.PicturePath);
            }

            var videos = _videoService.GetSPVideoByMainPartyIdAndCategoryId(memberMainPartyId, 0).OrderByDescending(x => x.SingularViewCount);
            int totalCount = videos.ToList().Count;
            float pageSize = 6;
            double totalPage = Math.Ceiling((float)totalCount / pageSize);
            int currentPage = 0;

            for (int i = 0; i < (int)totalPage; i++)
            {
                MTCompanyProfileVideosModel videosModel = new MTCompanyProfileVideosModel();
                var videos1 = videos.Skip(currentPage).Take((int)pageSize);
                foreach (var item in videos1)
                {
                    videosModel.MTCompanyProfileVideosModelsSub.Add(new MTCompanyProfileVideosModel
                    {
                        VideoImagePath = "//s.makinaturkiye.com/VideoThumb/" + item.VideoPicturePath,
                        VideoPath = "//s.makinaturkiye.com/NewVideos/" + item.VideoPath,
                        VideoId = item.VideoId
                    });
                }
                currentPage = i * (int)pageSize - (int)pageSize;
                model.MTStoreImageAndVideosModel.MTCompanyProfileVideosModels.Add(videosModel);
            }

            model.MTStoreImageAndVideosModel.TotalVideoPage = (int)totalPage;
            model.MTStoreImageAndVideosModel.StoreName = store.StoreName;
        }

        public void PrepareCompanyProfileAbout(MTCompanyProfileModel model, Store store)
        {
            if (!string.IsNullOrEmpty(store.StoreProfileHomeDescription))
            {
                model.MTStoreAboutModel.AboutText = store.StoreProfileHomeDescription;
                model.MTStoreAboutModel.IsAboutText = false;
            }

            if (store.StorePicture != null)
                model.MTStoreAboutModel.AboutImagePath = AppSettings.StoreProfilePicture + FileHelper.ImageThumbnailName(store.StorePicture);
            else
                model.MTStoreAboutModel.AboutImagePath = NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.GetStoreImage(store.MainPartyId, store.StoreLogo, "300");
            model.MTStoreAboutModel.StoreName = store.StoreName;
            var storeActivtyType = _storeActivityTypeService.GetStoreActivityTypesByStoreId(store.MainPartyId);

            foreach (var activity in storeActivtyType.ToList())
            {
                model.MTStoreAboutModel.StoreActivity += activity.ActivityType.ActivityName + " ";
            }

            if (store.StoreEmployeesCount > 0)
                model.MTStoreAboutModel.StoreEmployeeCount = _constantService.GetConstantByConstantId((short)store.StoreEmployeesCount).ConstantName;
            if (store.StoreActiveType != null)
                if (store.StoreActiveType > 0)
                {
                    if (store.StoreType != null)
                    {
                        var constant = _constantService.GetConstantByConstantId((short)store.StoreType);
                        if (constant != null)
                        {
                            model.MTStoreAboutModel.StoreType = constant.ConstantName;
                        }
                    }
                }
            model.MTStoreAboutModel.StoreEstablishmentDate = Convert.ToString(store.StoreEstablishmentDate);
            if (store.StoreCapital != null)
                if (store.StoreCapital > 0)
                    model.MTStoreAboutModel.StoreCapital = _constantService.GetConstantByConstantId((short)store.StoreCapital).ConstantName;
            if (store.StoreEndorsement != null)
                if (store.StoreEndorsement > 0)
                    model.MTStoreAboutModel.StoreEndorsement = _constantService.GetConstantByConstantId((short)store.StoreEndorsement).ConstantName;
            var storeInfoNumberShow = _storeNumberShowService.GetStoreInfoNumberShowByStoreMainPartyId(store.MainPartyId);
            if (storeInfoNumberShow != null)
                model.MTStoreAboutModel.StoreInfoNumberShow = storeInfoNumberShow;
            else
            {
                model.MTStoreAboutModel.StoreInfoNumberShow.MersisNoShow = false;
                model.MTStoreAboutModel.StoreInfoNumberShow.TaxNumberShow = false;
                model.MTStoreAboutModel.StoreInfoNumberShow.TradeRegistryNoShow = false;
                model.MTStoreAboutModel.StoreInfoNumberShow.TaxOfficeShow = false;
            }
            model.MTStoreAboutModel.TaxNumber = store.TaxOffice;
            model.MTStoreAboutModel.TaxOffice = store.TaxOffice;
            model.MTStoreAboutModel.TradeRegistrNo = store.TradeRegistrNo;
            model.MTStoreAboutModel.MersisNo = store.MersisNo;
            model.MTStoreAboutModel.StoreAboutShort = store.StoreAbout;
            model.MTStoreAboutModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
        }

        public void PrepareCompanyProfilePopularProducts(MTCompanyProfileModel model, int storeMainPartyId)
        {
            //int totalRecord;
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var popularProducts = _productService.GetSPProductsByStoreMainPartyId(7, 1, storeMainPartyId, mainPartyId);
            foreach (var item in popularProducts)
            {
                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
                MTPopularProductsModel popularProductModel = new MTPopularProductsModel
                {
                    ProductName = item.ProductName,
                    ProductId = item.ProductId,
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    BrandName = item.BrandName,
                    ModelName = item.ModelName,
                    PriceText = item.GetFormattedPrice(),
                    FavoriteProductId = item.FavoriteProductId,
                    HasVideo = item.HasVideo,
                    ProductPriceWithDiscount = item.DiscountType.HasValue && item.DiscountType.Value != 0 ? item.ProductPriceWithDiscount.HasValue ? item.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : "" : ""
                };
                if (item.ProductPriceType != (byte)ProductPriceType.PriceAsk && item.ProductPriceType != (byte)ProductPriceType.PriceDiscuss && !string.IsNullOrEmpty(item.GetCurrencyCssName()))
                {
                    popularProductModel.CurrencyCodeName = item.GetCurrencyCssName();
                }
                if (picture != null)
                    popularProductModel.ProductImagePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375);
                model.MTPopularProductsModels.Add(popularProductModel);
            }
        }

        public MTStoreProfileHeaderModel PrepareStoreProfileHeader(string currentPage, Store store, int memberMainPartyId)
        {
            string currentText = "";
            MTStoreProfileHeaderModel headerModel = new MTStoreProfileHeaderModel();
            if (currentPage == "CompanyProfile")
            {
                headerModel.MTStoreProfileMenuActivePage.HomeActive = "active";
                currentText = "";
            }
            else if (currentPage == "Products")
            {
                headerModel.MTStoreProfileMenuActivePage.ProductsActive = "active";
                currentText = " - Ürünlerimiz";
            }
            else if (currentPage == "AboutAs")
            {
                headerModel.MTStoreProfileMenuActivePage.AboutAsActive = "active";
                currentText = " - Kimdir";
            }
            else if (currentPage == "Branch")
            {
                headerModel.MTStoreProfileMenuActivePage.BranchActive = "active";
                currentText = " - Şubelerimiz";
            }
            else if (currentPage == "Services")
            {
                headerModel.MTStoreProfileMenuActivePage.ServicesActive = "active";
                currentText = " - Servis Ağımız";
            }
            else if (currentPage == "DealerShip")
            {
                headerModel.MTStoreProfileMenuActivePage.DealerShipActive = "active";
                currentText = " - Bayiliklerimiz";
            }
            else if (currentPage == "Dealer")
            {
                headerModel.MTStoreProfileMenuActivePage.DealerActive = "active";
                currentText = " - Bayilerimiz";
            }
            else if (currentPage == "Brand")
            {
                headerModel.MTStoreProfileMenuActivePage.BrandActive = "active";
                currentText = " - Markalarımız";
            }
            else if (currentPage == "Connection")
            {
                headerModel.MTStoreProfileMenuActivePage.ConnectionActive = "active";
                currentText = " - İletişim";
            }
            else if (currentPage == "Videos")
            {
                headerModel.MTStoreProfileMenuActivePage.VideosActive = "active";
                currentText = " - Videolarımız";
            }
            else if (currentPage == "StoreImages")
            {
                headerModel.MTStoreProfileMenuActivePage.StoreImagesActive = "active";
                currentText = " - Görsellerimiz";
            }
            else if (currentPage == "Catolog")
            {
                headerModel.MTStoreProfileMenuActivePage.CatologActive = "active";
                currentText = " - Kataloglarımız";
            }
            else if (currentPage == "New")
            {
                headerModel.MTStoreProfileMenuActivePage.NewActive = "active";
                currentText = " - Haberler";
            }
            else if (currentPage == "StoreVideos")
            {
                headerModel.MTStoreProfileMenuActivePage.StoreVideosActive = "active";
                currentText = " - Tanıtım Videoları";
            }
            headerModel.StoreName = store.StoreName;
            if (!string.IsNullOrEmpty(store.StoreShortName))
            {
                headerModel.StoreShortName = store.StoreShortName + currentText;
            }
            else
            {
                headerModel.StoreShortName = "";
                headerModel.StoreName += currentText;
            }
            headerModel.StoreLogoPath = ImageHelper.GetStoreLogoPath(store.MainPartyId, store.StoreLogo, 300);
            string storeAbout = "";
            if (store.StoreAbout != null)
            {
                storeAbout = Server.HtmlDecode(store.StoreAbout);
                storeAbout = Regex.Replace(storeAbout, @"<(.|\n)*?>", string.Empty);
            }
            headerModel.StoreAbout = storeAbout;
            headerModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
            headerModel.HasFavorite = false;
            headerModel.StoreBanner = ImageHelpers.GetStoreBanner(store.MainPartyId, store.StoreBanner);
            if (AuthenticationUser.Membership != null)
            {
                var favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(AuthenticationUser.Membership.MainPartyId, store.MainPartyId);
                if (favoriteStore != null)
                {
                    headerModel.HasFavorite = true;
                }
            }
            headerModel.MainPartyId = store.MainPartyId;

            PrepareStoreProfileHeaderHasModel(headerModel, store, memberMainPartyId);
            var video = _videoService.GetVideoByStoreMainPartyId(store.MainPartyId).OrderBy(x => x.Order).ThenByDescending(x => x.VideoId).FirstOrDefault();
            if (video != null)
            {
                headerModel.StoreVideo = new MTVideoModel
                {
                    PicturePath = ImageHelper.GetVideoImagePath(video.VideoPicturePath),
                    VideoMinute = video.VideoMinute.HasValue ? video.VideoMinute.Value : (byte)0,
                    VideoSecond = video.VideoSecond.HasValue ? video.VideoSecond.Value : (byte)0,
                    VideoPath = video.VideoPath,
                    VideoTitle = video.VideoTitle,
                    VideoUrl = video.VideoPath
                };
            }
            return headerModel;
        }

        public void PrepareStoreProfileHeaderHasModel(MTStoreProfileHeaderModel model, Store store, int memberMainPartyId)
        {
            if (!string.IsNullOrEmpty(store.GeneralText) || !string.IsNullOrEmpty(store.HistoryText) ||
                !string.IsNullOrEmpty(store.PhilosophyText) || !string.IsNullOrEmpty(store.FounderText))
            {
                model.MTStoreProfileMenuHasModel.HasAbout = true;
            }

            model.MTStoreProfileMenuHasModel.HasDealer = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.Dealer).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasVideos = _videoService.GetSPVideoByMainPartyIdAndCategoryId(memberMainPartyId, 0).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasServices = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.AuthorizedService).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasBranch = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.Branch).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasBrand = _storeBrandService.GetStoreBrandByMainPartyId(store.MainPartyId).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasProducts = _productService.GetProductsByMainPartyId(memberMainPartyId).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasDealerShip = _dealarBrandService.GetDealarBrandsByMainPartyId(store.MainPartyId).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasImages = _pictureService.GetPictureByMainPartyId(store.MainPartyId).Where(x => x.StoreImageType == (byte)StoreImageType.StoreImage).ToList().Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasCatolog = _storeCatologFileService.StoreCatologFilesByStoreMainPartyId(store.MainPartyId).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasNew = _storeNewService.GetStoreNewsByStoreMainPartyId(store.MainPartyId, StoreNewTypeEnum.Normal).Count > 0 ? true : false;
            model.MTStoreProfileMenuHasModel.HasStoreVideos = _videoService.GetVideoByStoreMainPartyId(store.MainPartyId).Count > 0 ? true : false;
        }

        public void PrepareCategories(MTStoreProfileProductsModel model, Store store)
        {
            MTCategoryModel categoryModel = new MTCategoryModel();
            var categories = _categoryService.GetCategoriesByStoreMainPartyId(store.MainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Sector || x.CategoryType == (byte)CategoryType.ProductGroup || x.CategoryType == (byte)CategoryType.Category).ToList();

            foreach (var item in categories)
            {
                string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName, store.StoreUrlName);
                categoryModel.MTCategoryItems.Add(new MTCategoryItem
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryType = item.CategoryType,
                    CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                    CategoryUrl = categoryUrl
                });
            }

            if (model.CategoryId != 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(model.CategoryId).Where(x => x.CategoryType != (byte)CategoryType.Model);
                foreach (var item in topCategories)
                {
                    string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, item.CategoryName, store.StoreUrlName);
                    categoryModel.MTTopCategoryItems.Add(new MTCategoryItem
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                        CategoryUrl = categoryUrl,
                        CategoryType = item.CategoryType
                    });
                }
            }
            if (model.CategoryId > 0)
            {
                var activeCategory = _categoryService.GetCategoryByCategoryId(model.CategoryId);
                categoryModel.ActiveCategory = activeCategory;
            }
            model.MTCategoryModel = categoryModel;
        }

        public void PrepareProductsList(MTStoreProfileProductsModel model, Store store, int PageDimension, int page)
        {
            IList<StoreProfileProductsResult> products = new List<StoreProfileProductsResult>();
            string searchTypeText = GetSearchTypeQueryString();
            byte searchType = 0;
            switch (searchTypeText)
            {
                case "sifir": searchType = 72; break;
                case "ikinciel": searchType = 73; break;
                case "hizmet": searchType = 201; break;
                default: searchType = 0; break;
            }

            List<MTProductsPageProductList> productList = new List<MTProductsPageProductList>();
            int totalRecord;
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            products = _productService.GetSPProductsByStoreMainPartyIdAndCategoryId(out totalRecord, PageDimension, page, store.MainPartyId, model.CategoryId, mainPartyId);
            foreach (var item in products)
            {
                int modelYear = 0;
                if (item.ModelYear != null)
                    modelYear = Convert.ToInt32(item.ModelYear);

                MTProductsPageProductList mtProductList = new MTProductsPageProductList
                {
                    ProductId = item.ProductId,
                    BrandName = item.BrandName,
                    CategoryName = item.CategoryName,
                    MainPicture = item.MainPicture,
                    ModelName = item.ModelName,
                    ProductName = item.ProductName,
                    ProductPrice = item.GetFormattedPrice(),
                    Currency = item.GetCurrencyCssName(),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    productrate = item.productrate,
                    ModelYear = modelYear,
                    BriefDetailText = (item.BriefDetailText != null) ? (item.BriefDetailText + " ") : "",
                    ProductTypeText = (item.ProductTypeText != null) ? (item.ProductTypeText + " ") : "",
                    ProductSalesTypeText = (item.ProductSalesTypeText != null) ? (item.ProductSalesTypeText + " ") : "",
                    ProductStatuText = (item.ProductStatuText != null) ? (item.ProductStatuText + " ") : "",
                    ProductImagePath = ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px500x375),
                    CityName = item.CityName,
                    LocalityName = item.LocalityName,
                    ProductNo = item.ProductNo,
                    FavoriteProductId = item.FavoriteProductId,
                    HasVideo = item.HasVideo,
                    ProductPriceDiscount = item.DiscountType.HasValue && item.DiscountType.Value != 0 ? item.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : ""
                };
                string productDesc = "";
                if (item.ProductDescription != null)
                {
                    productDesc = Server.HtmlDecode(item.ProductDescription);
                    productDesc = Regex.Replace(productDesc, @"<(.|\n)*?>", string.Empty);
                }
                mtProductList.ProductDescription = NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.Truncate(productDesc, 320);

                productList.Add(mtProductList);
            }
            PagingModel<MTProductsPageProductList> productsModel = new PagingModel<MTProductsPageProductList>();
            productsModel.CurrentPage = page;
            productsModel.TotalRecord = totalRecord;
            productsModel.PageDimension = PageDimension;
            productsModel.Source = productList;
            model.MTProductsProductListModel.MTProductsPageProductLists = productsModel;
        }

        public void PrepareJsonLd(MTConnectionModel model, Store store)
        {
            try
            {
                var phone = model.Phones.FirstOrDefault(p => p.PhoneType == (byte)PhoneType.Phone);
                var localBusiness = new Schema.NET.LocalBusiness
                {
                    Address = new Schema.NET.PostalAddress
                    {
                        AddressLocality = model.StoreAddress.City.CityName.CheckNullString() + " " + model.StoreAddress.Locality.LocalityName.CheckNullString(),
                        AddressRegion = "TR",
                        StreetAddress = model.AddressMap.CheckNullString()
                    },
                    Description = model.MTStoreProfileHeaderModel.StoreAbout.CheckNullString(),
                    Name = model.StoreName.CheckNullString(),
                    Telephone = phone == null ? string.Empty : (phone.PhoneCulture + " " + phone.PhoneAreaCode + " " + phone.PhoneNumber),
                    Image = new Uri(ImageHelper.GetStoreLogoPath(store.MainPartyId, store.StoreLogo, 300))
                };
                model.MtJsonLdModel.JsonLdString = localBusiness.ToString();
            }
            catch (Exception e)
            {
                model.MtJsonLdModel.JsonLdString = string.Empty;
            }
        }

        #endregion Utilities

        #region Methods

        public ActionResult StoreFailNew()
        {
            Response.RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + Url.RouteUrl(new
            {
                controller = "store",
                action = "index"
            }));
            return null;
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> CompanyProfileNew(string username, string id, string MainPartyId)
        {
            if (string.IsNullOrEmpty(username))
            {
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                if (store != null)
                {
                    string url = UrlBuilder.GetBrandUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    return RedirectPermanent(url);
                }
                return RedirectPermanent(AppSettings.SiteUrl);
            }
            else
            {
                if (id != null)
                {
                    return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + username);
                }

                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);
                if (store != null)
                {
                    var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    if (!Request.IsLocal && Request.Url.ToString().Contains("https://urun"))
                    {
                        return RedirectPermanent(url);
                    }

                    //Seo
                    //SeoPageType = (byte)PageType.Store;
                    //CreateSeoForStore(store);

                    if (store.ViewCount != null)
                    {
                        store.ViewCount += 1;
                    }
                    else
                    {
                        store.ViewCount = 1;
                    }
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        if (store.SingularViewCount != null)
                        {
                            store.SingularViewCount += 1;
                        }
                        else
                        {
                            store.SingularViewCount = 1;
                        }
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }

                    _storeService.UpdateStore(store);

                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + Request.Url.AbsolutePath;
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    if (memberStore == null)
                    {
                        return RedirectToAction("Index", "Store");
                    }
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);

                    MTCompanyProfileModel model = new MTCompanyProfileModel();

                    MTStoreProfileHeaderModel companyProfileHeaderModel = PrepareStoreProfileHeader("CompanyProfile", store, memberMainPartyId);
                    PrepareCompanyProfilePopularProducts(model, store.MainPartyId);

                    PrepareCertificates(model, store.MainPartyId);

                    model.MTStoreProfileHeaderModel = companyProfileHeaderModel;
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;
                    //store about
                    PrepareCompanyProfileAbout(model, store);
                    //store image and videos
                    PrepareCompanyProfileImageAndVideos(model, store, memberMainPartyId);
                    //store members info and map
                    PrepareCompanyProfileStoreMemberAndMap(model, store, memberMainPartyId);
                    //photos
                    var sliderImages = _pictureService.GetPictureByMainPartyId(store.MainPartyId).Where(x => x.StoreImageType == (byte)StoreImageType.StoreProfileSliderImage).OrderBy(x => x.PictureOrder);
                    foreach (var item in sliderImages.ToList())
                    {
                        model.SliderImages.Add(AppSettings.StoreSliderImageFolder + item.PicturePath.Replace("_slider", "-800x300"));
                    }
                    return await Task.FromResult(View(model));
                }
                else
                {
                    StoreFailNew();
                    return null;
                }
            }
        }

        //private void PrepareCertificates(MTCompanyProfileModel model, int mainPartyId)
        //{
        //    var certificates = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);

        //    foreach (var item in certificates)
        //    {
        //        var pictures = _pictureService.GetPictureByStoreCertificateId(item.StoreCertificateId);
        //        int counter = 1;
        //        foreach (var picture in pictures)
        //        {
        //            string certificateName = item.CertificateName;
        //            if (pictures.Count > 1)
        //                certificateName = certificateName + " - " + counter;
        //            model.Certificates.Add(new MTCertificateItem { ImagePath =AppSettings.StoreCertificateImageFolder+picture.PicturePath.Replace("_certificate", "-500x800"),
        //                ImageFullPath = AppSettings.StoreCertificateImageFolder + picture.PicturePath,
        //               Name = certificateName });
        //        }
        //    }
        //}

        [HttpGet]
        [Compress]
        public async Task<ActionResult> ProductsNew(string username, string CategoryId, string MainPartyId)
        {
            if (string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(MainPartyId))
                {
                    var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store != null)
                    {
                        string url = UrlBuilder.GetStoreProfileProductUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                        return RedirectPermanent(url);
                    }
                }
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
            else
            {
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreProductPage;
                    var request = HttpContext.Request;

                    string url = UrlBuilder.GetStoreProfileProductUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);

                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;

                    Category category = null;

                    if (!string.IsNullOrEmpty(CategoryId))
                    {
                        int categoryId = Convert.ToInt32(CategoryId);
                        category = _categoryService.GetCategoryByCategoryId(categoryId);
                        if (category == null)
                        {
                            if (store != null)
                                return RedirectPermanent(UrlBuilder.GetProductUrlForStoreProfile(store.MainPartyId, store.StoreName, store.StoreUrlName));
                            else
                                return RedirectPermanent(AppSettings.SiteAllCategoryUrl);
                        }
                        if (store == null)
                        {
                            string urlNew = AppSettings.SiteAllCategoryUrl;
                            if (category != null)
                            {
                                if ((category.CategoryType == (byte)CategoryType.Category ||
                                    category.CategoryType == (byte)CategoryType.ProductGroup ||
                                    category.CategoryType == (byte)CategoryType.Sector))
                                    urlNew = UrlBuilder.GetCategoryUrl(categoryId, category.CategoryContentTitle, null, string.Empty);
                            }

                            return RedirectPermanent(urlNew);
                        }

                        url = UrlBuilder.GetStoreProfileProductCategoryUrl(category.CategoryId, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName, store.StoreUrlName);
                    }

                    if (!Request.IsLocal)
                    {
                        if (url != Request.Url.ToString())
                        {
                            return RedirectPermanent(url);
                        }
                    }
                    if (request.Url.AbsolutePath.Any(x => char.IsUpper(x)))
                    {
                        return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath.ToLower());
                    }

                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    MTStoreProfileProductsModel model = new MTStoreProfileProductsModel();
                    model.MTProductsProductListModel.StoreMainPartyId = store.MainPartyId;
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    MTStoreProfileHeaderModel headerModel = PrepareStoreProfileHeader("Products", store, memberMainPartyId);
                    model.MTStoreProfileHeaderModel = headerModel;
                    byte pageDimension = 24;
                    if (Request.QueryString["Gorunum"] != null)
                    {
                        model.MTProductsProductListModel.ViewType = Convert.ToByte(Request.QueryString["Gorunum"]);
                    }
                    else
                        model.MTProductsProductListModel.ViewType = 0;

                    if (!string.IsNullOrEmpty(CategoryId))
                    {
                        model.CategoryId = Convert.ToInt32(CategoryId);
                        model.MTProductsProductListModel.CategoryId = model.CategoryId;
                        //SeoPageType = (byte)PageType.StoreProductCategoryPage;

                        string categoryNameForSeo = category.CategoryName;
                        if (category.CategoryType == (byte)CategoryType.Model || category.CategoryType == (byte)CategoryType.Brand)
                        {
                            var ustCat = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);

                            categoryNameForSeo = ustCat.CategoryName + " " + categoryNameForSeo;
                        }

                        //CreateSeoParameter(SeoModel.SeoProductParemeters.Category, categoryNameForSeo);
                        //CreateSeoParameter(SeoModel.SeoProductParemeters.KategoriBaslik, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName);

                        ViewBag.Categoryh1 = categoryNameForSeo;
                    }
                    //CreateSeoForStore(store);

                    int currentPage = 1;
                    PrepareProductsList(model, store, pageDimension, currentPage);
                    //PrepareCategories(model, store);
                    PrepareFilterModel(model);
                    store.ViewCount += 1;

                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile) && store != null)
                    {
                        store.SingularViewCount += 1;
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }
                    _storeService.UpdateStore(store);

                    string order = GetOrderQueryString();
                    int orderById = 0;
                    switch (order)
                    {
                        case "a-z": orderById = 2; break;
                        case "z-a": orderById = 3; break;
                        case "fiyat-azalan": orderById = 4; break;
                        case "fiyat-artan": orderById = 6; break;
                        default: orderById = 0; break;
                    }
                    var products = model.MTProductsProductListModel.MTProductsPageProductLists.Source.ToList();
                    switch (orderById)
                    {
                        case 0:
                            {
                                products = products.OrderByDescending(x => x.productrate).ToList();
                                break;
                            }
                        case 2:
                            {
                                products = products.OrderBy(x => x.ProductName).ToList();
                                break;
                            }
                        case 3:
                            {
                                products = products.OrderByDescending(x => x.ProductName).ToList();
                                break;
                            }
                        case 4:
                            {
                                products = products.OrderByDescending(x => x.ProductPrice).ToList();
                                break;
                            }
                        case 6:
                            {
                                products = products.OrderBy(x => x.ProductPrice).ToList();
                                break;
                            }
                        default:
                            break;
                    }
                    model.MTProductsProductListModel.MTProductsPageProductLists.Source = products;
                    return await Task.FromResult(View(model));
                }
                else
                {
                    StoreFailNew();
                    return null;
                }
            }
        }

        [HttpGet]
        public ActionResult NoAccessStore(int id)
        {
            var store = _storeService.GetStoreByMainPartyId(id);
            MTNoAccessStoreModel model = new MTNoAccessStoreModel();
            model.StoreName = store.StoreName.Replace("Silindi", "").Replace("Silinen", "");
            model.StoreLogoPath = ImageHelper.GetStoreLogoPath(id, store.StoreLogo, 300);
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductPaging(int page, byte displayType, int storeId, byte pageDimension, int CategoryId)
        {
            IList<StoreProfileProductsResult> products = new List<StoreProfileProductsResult>();

            List<MTProductsPageProductList> productList = new List<MTProductsPageProductList>();
            MTProductsProductListModel model = new MTProductsProductListModel();
            model.CategoryId = CategoryId;
            model.StoreMainPartyId = storeId;
            int totalRecord;
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            products = _productService.GetSPProductsByStoreMainPartyIdAndCategoryId(out totalRecord, (int)pageDimension, page, storeId, CategoryId, mainPartyId);

            foreach (var item in products)
            {
                MTProductsPageProductList mtProductList = new MTProductsPageProductList
                {
                    ProductId = item.ProductId,
                    BrandName = item.BrandName,
                    CategoryName = item.CategoryName,
                    MainPicture = item.MainPicture,
                    ModelName = item.ModelName,
                    ProductName = item.ProductName,
                    ProductPrice = item.GetFormattedPrice(),
                    Currency = item.GetCurrencyCssName(),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    productrate = item.productrate,
                    ModelYear = (item.ModelYear != null) ? Convert.ToInt32(item.ModelYear) : 0,
                    BriefDetailText = (item.BriefDetailText != null) ? (item.BriefDetailText + " ") : "",
                    ProductTypeText = (item.ProductTypeText != null) ? (item.ProductTypeText + " ") : "",
                    ProductSalesTypeText = (item.ProductSalesTypeText != null) ? (item.ProductSalesTypeText + " ") : "",
                    ProductStatuText = (item.ProductStatuText != null) ? (item.ProductStatuText + " ") : "",
                    ProductImagePath = ImageHelper.GetProductImagePath(item.ProductId, item.MainPicture, ProductImageSize.px400x300),
                    CityName = item.CityName,
                    LocalityName = item.LocalityName,
                    ProductNo = item.ProductNo,
                    FavoriteProductId = item.FavoriteProductId,
                    HasVideo = item.HasVideo
                };
                string productDesc = Server.HtmlDecode(item.ProductDescription);
                if (!string.IsNullOrEmpty(productDesc))
                {
                    productDesc = Regex.Replace(productDesc, @"<(.|\n)*?>", string.Empty);
                    mtProductList.ProductDescription = NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.Truncate(productDesc, 320);
                }
                productList.Add(mtProductList);
            }
            model.MTProductsPageProductLists.CurrentPage = page;
            model.MTProductsPageProductLists.TotalRecord = totalRecord;
            model.MTProductsPageProductLists.Source = productList;

            string userControlName = string.Empty;
            var pageType = (DisplayType)displayType;
            switch (pageType)
            {
                case DisplayType.Window:
                    userControlName = "_ProductsProductWindow";
                    break;

                case DisplayType.List:
                    userControlName = "_ProductsProductList";
                    break;

                default:
                    break;
            }

            return View(userControlName, model);
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> DealerNew(string username, string MainPartyId)
        {
            if (username != null || username != "")
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);
                if (store != null)
                {
                    MTStoreDealerModel model = new MTStoreDealerModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Dealer", store, memberMainPartyId);

                    model.StoreDealers = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.Dealer).ToList();
                    foreach (var item in model.StoreDealers.ToList())
                    {
                        var address = _addressService.GetAddressByStoreDealerId(item.StoreDealerId);
                        if (address != null)
                        {
                            model.DealerAddresses.Add(address);
                            model.Phones.AddRange(_phoneService.GetPhonesAddressId(address.AddressId));
                        }
                    }

                    model.MainPartyId = store.MainPartyId;
                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                    }

                    _storeService.UpdateStore(store);

                    //SeoPageType = (byte)PageType.StoreDealerPage;
                    //CreateSeoForStore(store);

                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    foreach (var item in model.DealerAddresses)
                    {
                        model.AdressEdits.Add(new AddressShow
                        {
                            Address = item.GetAddressEdit(),
                            AddressId = item.AddressId
                        });
                    }
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;

                    return await Task.FromResult(View(model));
                }
                else
                {
                    store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store != null)
                    {
                        var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/bayiagimiz";
                        return RedirectPermanent(url);
                    }
                    StoreFailNew();
                    return null;
                }
            }
            else
            {
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "store",
                    action = "index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> AboutUsNew(string username, string MainPartyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    _storeService.CachingGetOrSetOperationEnabled = false;
                    var store = _storeService.GetStoreByStoreUrlName(username);

                    if (store != null)
                    {
                        var request = HttpContext.Request;
                        if (request.Url.AbsolutePath.Contains("Hakkimizda"))
                        {
                            return RedirectPermanent("http://www.makinaturkiye.com/" + request.Url.AbsolutePath.ToLower());
                        }
                        MTStoreProfileAboutUsModel model = new MTStoreProfileAboutUsModel();
                        var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                        int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                        model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("AboutAs", store, memberMainPartyId);

                        //var dealers = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, (byte)DealerType.Bayii).Select(x => x.StoreDealerId);

                        model.MainPartyId = store.MainPartyId;
                        store.ViewCount += 1;
                        if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                        {
                            store.SingularViewCount += 1;
                        }
                        _storeService.UpdateStore(store);
                        //SeoPageType = (byte)PageType.StoreAboutPage;
                        //CreateSeoForStore(store);

                        ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                        model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                        model.MainPartyId = store.MainPartyId;
                        model.GeneralText = store.GeneralText;
                        return await Task.FromResult(View(model));
                    }
                    else
                    {
                        return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash);
                    }
                }
                else
                {
                    var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store != null)
                    {
                        var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/hakkimizda";
                        return RedirectPermanent(url);
                    }
                    StoreFailNew();
                    return null;
                }
            }
            catch (Exception)
            {
                //ExceptionHandler.HandleException(Server.GetLastError());
                return RedirectToAction("HataSayfasi", "Common");
                throw;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> BranchNew(string username, string MainPartyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    _storeService.CachingGetOrSetOperationEnabled = false;
                    var store = _storeService.GetStoreByStoreUrlName(username);

                    if (store != null)
                    {
                        MTStoreProfileBranchModel model = new MTStoreProfileBranchModel();
                        var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                        int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                        model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Branch", store, memberMainPartyId);
                        model.StoreBranchs = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.Branch).ToList();
                        bool branchesAnyAdress = false;
                        foreach (var item in model.StoreBranchs.ToList())
                        {
                            var address = _addressService.GetAddressByStoreDealerId(item.StoreDealerId);
                            if (address != null)
                            {
                                model.BranchAddresses.Add(address);
                                model.Phones.AddRange(_phoneService.GetPhonesAddressId(address.AddressId));
                                branchesAnyAdress = true;
                            }
                        }

                        model.MainPartyId = store.MainPartyId;
                        store.ViewCount += 1;
                        if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                        {
                            store.SingularViewCount += 1;
                        }
                        _storeService.UpdateStore(store);
                        //SeoPageType = (byte)PageType.StoreDepartmentPage;
                        //CreateSeoForStore(store);
                        var request = HttpContext.Request;
                        ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                        foreach (var item in model.BranchAddresses)
                        {
                            model.AdressEdits.Add(new AddressShow
                            {
                                Address = item.GetAddressEdit(),
                                AddressId = item.AddressId
                            });
                        }
                        model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                        model.MainPartyId = store.MainPartyId;

                        return await Task.FromResult(View(model));
                    }
                    else
                    {
                        store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                        if (store != null)
                        {
                            var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/subelerimiz";
                            return RedirectPermanent(url);
                        }
                        Response.RedirectPermanent(Url.RouteUrl(new
                        {
                            controller = "Store",
                            action = "Index"
                        }));
                        return null;
                    }
                }
                else
                {
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            catch (Exception ex)
            {
                //ExceptionHandler.HandleException(ex);
                StoreFail();
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> BrandNew(string username, string MainPartyId)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //seo
                    //SeoPageType = (byte)PageType.StoreBrandPage;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    //others
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    MTBrandModel model = new MTBrandModel();
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Brand", store, memberMainPartyId);
                    var storeBrands = _storeBrandService.GetStoreBrandByMainPartyId(store.MainPartyId);
                    if (storeBrands != null)
                    {
                        model.StoreBrands = storeBrands;
                    }
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;
                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }
                    _storeService.UpdateStore(store);
                    return await Task.FromResult(View(model));
                }
                else
                {
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                if (store != null)
                {
                    var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/Markalarimiz";
                    return RedirectPermanent(url);
                }
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> VideosNew(string username, int? CategoryId, string MainPartyId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreVideoPage;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTStoreProfileVideoModel model = new MTStoreProfileVideoModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Videos", store, memberMainPartyId);

                    model.MainPartyId = store.MainPartyId;
                    model.StoreName = store.StoreName;
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                    }
                    _storeService.UpdateStore(store);

                    int categoryId = 0;
                    if (CategoryId != null) categoryId = Convert.ToInt32(CategoryId);
                    var videoResult = _videoService.GetSPVideoByMainPartyIdAndCategoryId(memberMainPartyId, categoryId);
                    PrepareVideosModel(videoResult, model);

                    var categoryResult = _categoryService.GetSPVideoCategoryByMainPartyId(memberMainPartyId, categoryId);
                    PrepareVideoStoreCategoryModel(categoryResult, model);

                    return await Task.FromResult(View(model));
                }
                else
                {
                    store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store != null)
                    {
                        var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/Videolarimiz";
                        return RedirectPermanent(url);
                    }
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> StoreImagesNew(string username, int? CategoryId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreImages;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTStoreImageModel model = new MTStoreImageModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("StoreImages", store, memberMainPartyId);
                    store.ViewCount += 1;
                    model.MainPartyId = store.MainPartyId;
                    model.StoreName = store.StoreName;
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);

                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                    }
                    _storeService.UpdateStore(store);
                    int categoryId = 0;
                    if (CategoryId != null) categoryId = Convert.ToInt32(CategoryId);
                    var storeImages = _pictureService.GetPictureByMainPartyId(store.MainPartyId).Where(x => x.StoreImageType == (byte)StoreImageType.StoreImage);
                    foreach (var item in storeImages)
                    {
                        string[] image = item.PicturePath.Split('.');
                        string imageName = image[0] + "_th";
                        string newImage = imageName + "." + image[1];
                        model.ImagePath.Add(AppSettings.StoreImageFolder + newImage);
                    }

                    return await Task.FromResult(View(model));
                }
                else
                {
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> StorePromotionVideo(string username, int? CategoryId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreImages;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTStoreVideoModel model = new MTStoreVideoModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("StoreVideos", store, memberMainPartyId);
                    store.ViewCount += 1;
                    model.MainPartyId = store.MainPartyId;
                    model.StoreName = store.StoreName;
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);

                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                    }
                    _storeService.UpdateStore(store);
                    int categoryId = 0;
                    if (CategoryId != null) categoryId = Convert.ToInt32(CategoryId);
                    var storeVideos = _videoService.GetVideoByStoreMainPartyId(store.MainPartyId).OrderBy(x => x.Order).ThenByDescending(x => x.VideoId);
                    foreach (var video in storeVideos)
                    {
                        model.MTVideoModels.Add(new MTVideoModel
                        {
                            PicturePath = ImageHelper.GetVideoImagePath(video.VideoPicturePath),
                            VideoMinute = video.VideoMinute.HasValue ? video.VideoMinute.Value : (byte)0,
                            VideoSecond = video.VideoSecond.HasValue ? video.VideoSecond.Value : (byte)0,
                            VideoPath = video.VideoPath,
                            VideoTitle = video.VideoTitle,
                            VideoUrl = video.VideoPath,
                            VideoId = video.VideoId
                        });
                    }

                    return await Task.FromResult(View(model));
                }
                else
                {
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> AuthorizedServicesNew(string username, string MainPartyId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    MTStoreServicesModel model = new MTStoreServicesModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Services", store, memberMainPartyId);

                    model.StoreServices = _storeDealerService.GetStoreDealersByMainPartyId(store.MainPartyId, DealerTypeEnum.AuthorizedService).ToList();

                    foreach (var item in model.StoreServices.ToList())
                    {
                        var address = _addressService.GetAddressByStoreDealerId(item.StoreDealerId);
                        if (address != null)
                        {
                            model.ServicesAddresses.Add(address);
                            model.Phones.AddRange(_phoneService.GetPhonesAddressId(address.AddressId));
                        }
                    }
                    model.MainPartyId = store.MainPartyId;
                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                        _storeService.UpdateStore(store);
                    }
                    //SeoPageType = (byte)PageType.StoreServiceNetwork;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    foreach (var item in model.ServicesAddresses)
                    {
                        model.AdressEdits.Add(new AddressShow
                        {
                            Address = item.GetAddressEdit(),
                            AddressId = item.AddressId
                        });
                    }
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;

                    return await Task.FromResult(View(model));
                }
                else
                {
                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                if (store != null)
                {
                    var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/ServisAgimiz";
                    return RedirectPermanent(url);
                }
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> DealerShipNew(string username, string MainPartyId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //seo
                    //SeoPageType = (byte)PageType.StoreDealerNetwork;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTDealerShipModel model = new MTDealerShipModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("DealerShip", store, memberMainPartyId);
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;
                    model.DealerBrands = _dealarBrandService.GetDealarBrandsByMainPartyId(store.MainPartyId);

                    store.ViewCount += 1;
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        store.SingularViewCount += 1;
                    }
                    _storeService.UpdateStore(store);

                    return await Task.FromResult(View(model));
                }
                else
                {
                    store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store != null)
                    {
                        var url = UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName) + "/Bayiliklerimiz";
                        return RedirectPermanent(url);
                    }

                    Response.RedirectPermanent(Url.RouteUrl(new
                    {
                        controller = "Store",
                        action = "Index"
                    }));
                    return null;
                }
            }
            else
            {
                Response.RedirectPermanent(Url.RouteUrl(new
                {
                    controller = "Store",
                    action = "Index"
                }));
                return null;
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> ConnectionNew(string username, string MainPartyId)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //seo
                    //SeoPageType = (byte)PageType.StoreConnectionPage;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTConnectionModel model = new MTConnectionModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Connection", store, memberMainPartyId);
                    model.StoreActiveType = Convert.ToByte(store.StoreActiveType);
                    model.MainPartyId = store.MainPartyId;
                    var member = _memberService.GetMemberByMainPartyId(memberMainPartyId);
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

                        model.Phones.Add(new MTStorePhoneModel
                        {
                            PhoneAreaCode = item.PhoneAreaCode,
                            PhoneCulture = item.PhoneCulture,
                            PhoneNumber = item.PhoneNumber,
                            PhoneType = (PhoneType)item.PhoneType.Value,
                            ShowPhone = showPhone
                        });
                    }

                    model.StoreAddress = _addressService.GetFisrtAddressByMainPartyId(store.MainPartyId);
                    if (model.StoreAddress == null)
                        model.StoreAddress = _addressService.GetFisrtAddressByMainPartyId(memberMainPartyId); ;
                    model.StoreName = store.StoreName;
                    if (!string.IsNullOrEmpty(store.StoreWeb))
                    {
                        if (store.StoreWeb == "http://")
                        {
                            model.StoreWebUrl = AppSettings.SiteUrlWithoutLastSlash + UrlBuilder.GetStoreProfileUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                        }
                        else if (store.StoreWeb.Contains("http"))
                        {
                            model.StoreWebUrl = store.StoreWeb;
                        }
                        else
                        {
                            if (store.StoreWeb.Contains("makinaturkiye"))
                            {
                                model.StoreWebUrl = "https://" + store.StoreWeb;
                            }
                            else
                            {
                                model.StoreWebUrl = "http://" + store.StoreWeb;
                            }
                        }
                    }
                    model.AuthorizedNameSurname = member.MemberName + " " + member.MemberSurname;
                    model.AddressMap = model.StoreAddress.GetAddressEdit();
                    if (store.ViewCount != null)
                    {
                        store.ViewCount += 1;
                    }
                    else
                    {
                        store.ViewCount = 1;
                    }
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        if (store.SingularViewCount != null)
                        {
                            store.SingularViewCount += 1;
                        }
                        else
                        {
                            store.SingularViewCount = 1;
                        }
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }
                    _storeService.UpdateStore(store);

                    PrepareJsonLd(model, store);

                    return await Task.FromResult(View(model));
                }
                else
                {
                    StoreFailNew();
                    return null;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(MainPartyId))
                {
                    var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(MainPartyId));
                    if (store == null)
                    {
                        StoreFailNew();
                        return null;
                    }
                    var url = UrlBuilder.GetStoreConnectUrl(store.MainPartyId, store.StoreName, store.StoreUrlName);
                    return RedirectPermanent(url);
                }
                //ExceptionHandler.HandleException(Server.GetLastError());
                return RedirectToAction("HataSayfasi", "Common");
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> CatologNew(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreCatolog;
                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = "https://www.makinaturkiye.com" + request.Url.AbsolutePath;
                    MTCatologModel model = new MTCatologModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("Catolog", store, memberMainPartyId);
                    model.StoreActiveType = store.StoreActiveType.Value;
                    model.MainPartyId = store.MainPartyId;

                    var catologs = _storeCatologFileService.StoreCatologFilesByStoreMainPartyId(store.MainPartyId).OrderBy(x => x.FileOrder).ThenByDescending(x => x.StoreCatologFileId);
                    foreach (var item in catologs)
                    {
                        string filePath = FileUrlHelper.GetStoreCatologUrl(item.FileName, store.MainPartyId);
                        model.MTStoreCatologItems.Add(new MTStoreCatologItem { FileOrder = item.FileOrder, FilePath = filePath, Name = item.Name });
                    }
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        if (store.SingularViewCount != null)
                        {
                            store.SingularViewCount += 1;
                        }
                        else
                        {
                            store.SingularViewCount = 1;
                        }
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }
                    _storeService.UpdateStore(store);
                    return await Task.FromResult(View(model));
                }
                else
                {
                    StoreFailNew();
                    return null;
                }
            }
            else
            {
                //ExceptionHandler.HandleException(Server.GetLastError());
                return RedirectToAction("HataSayfasi", "Common");
            }
        }

        [HttpGet]
        [Compress]
        public async Task<ActionResult> News(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                _storeService.CachingGetOrSetOperationEnabled = false;
                var store = _storeService.GetStoreByStoreUrlName(username);

                if (store != null)
                {
                    //SeoPageType = (byte)PageType.StoreNews;

                    //CreateSeoForStore(store);
                    var request = HttpContext.Request;
                    ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
                    MTNewModel model = new MTNewModel();
                    var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(store.MainPartyId);
                    int memberMainPartyId = Convert.ToInt32(memberStore.MemberMainPartyId);
                    model.MTStoreProfileHeaderModel = PrepareStoreProfileHeader("New", store, memberMainPartyId);
                    model.StoreActiveType = store.StoreActiveType.Value;
                    model.MainPartyId = store.MainPartyId;
                    var storeNews = _storeNewService.GetStoreNewsByStoreMainPartyId(store.MainPartyId, StoreNewTypeEnum.Normal)
                        .Where(x => x.Active == true).OrderByDescending(x => x.UpdateDate);

                    foreach (var item in storeNews)
                    {
                        model.StoreNewItems.Add(new MTStoreNewItemModel
                        {
                            DateString = item.RecordDate.ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                            ImagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px300x300.ToString()),
                            NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title),
                            StoreName = store.StoreName,
                            Title = item.Title
                        });
                    }
                    if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == store.MainPartyId && c.Value == SingularViewCountType.StoreProfile))
                    {
                        if (store.SingularViewCount != null)
                        {
                            store.SingularViewCount += 1;
                        }
                        else
                        {
                            store.SingularViewCount = 1;
                        }
                        SessionSingularViewCountType.SingularViewCountTypes.Add(store.MainPartyId, SingularViewCountType.StoreProfile);
                    }
                    _storeService.UpdateStore(store);

                    return await Task.FromResult(View(model));
                }
                else
                {
                    StoreFailNew();
                    return null;
                }
            }
            else
            {
                //ExceptionHandler.HandleException(Server.GetLastError());
                return RedirectToAction("HataSayfasi", "Common");
            }
        }

        [HttpPost]
        public JsonResult ProductSearch(string name, int storeMainPartyId)
        {
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeMainPartyId);
            var productForStore = _productService.GetProductsByMainPartyId(Convert.ToInt32(memberStore.MemberMainPartyId));
            var productNames = from p in productForStore where p.ProductName.ToLower().Contains(name.ToLower()) select (p.ProductName);
            return Json(productNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductSearchGet(string productName, int storeMainPartyId)
        {
            var memberStore = _memberStoreService.GetMemberStoreByStoreMainPartyId(storeMainPartyId);
            var productForStore = _productService.GetProductsByMainPartyId(Convert.ToInt32(memberStore.MemberMainPartyId));
            var product = (from p in productForStore where p.ProductName.ToLower().Contains(productName.ToLower()) select p).FirstOrDefault();
            if (product != null)
            {
                string productUrl = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName);
                return Json(new { ProductUrl = productUrl }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false);
            }
        }

        [HttpGet]
        public ActionResult RemoveFavoriteStore(int mainPartyId)
        {
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                FavoriteStore favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(AuthenticationUser.Membership.MainPartyId, mainPartyId);
                if (favoriteStore != null)
                {
                    _favoriteStoreService.DeleteFavoriteStore(favoriteStore);
                    return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
                }
            }
            var store = _storeService.GetStoreByMainPartyId(mainPartyId);
            TempData["MessageError"] = store.StoreName + " firmasını favori satıcılıarınıza çıkarmak için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.";
            return RedirectToAction("logon", "MemberShip");
        }

        [HttpGet]
        public ActionResult AddFavoriteStore(int MainPartyId)
        {
            if (AuthenticationUser.Membership.MainPartyId > 0)
            {
                var curFavoriteStore = new FavoriteStore
                {
                    MemberMainPartyId = AuthenticationUser.Membership.MainPartyId,
                    StoreMainPartyId = MainPartyId
                };
                _favoriteStoreService.InsertFavoriteStore(curFavoriteStore);

                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }

            Store party = new Store();
            string ProductUrl;
            party = _storeService.GetStoreByMainPartyId(MainPartyId);

            ProductUrl = string.Format("/{0}/{1}/{2}/{3}", NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.ToUrl("Sirket"), party.MainPartyId, NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.ToUrl(party.StoreName), NeoSistem.MakinaTurkiye.Core.Web.Helpers.Helpers.ToUrl("SirketProfili"));

            TempData["RedirectUrl"] = ProductUrl;
            TempData["MessageError"] = party.StoreName + " firmasını favori satıcılıarınıza eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.";
            return RedirectToAction("logon", "MemberShip");
        }

        [HttpPost]
        public JsonResult StoreStatisticCreate(string storeID)
        {
            int storeId = Convert.ToInt32(storeID);
            string ipAdress = Request.UserHostAddress;
            bool isNullProductStatistic = false;
            DateTime dateNow = DateTime.Now;
            DateTime recordDate = DateTime.Now;
            if (!SessionSingularViewCountType.SingularViewCountTypes.Any(c => c.Key == storeId))
            {
                var storeStatistic1 = new StoreStatistic();
                storeStatistic1.RecordDate = dateNow;
                storeStatistic1.UserIp = ipAdress;
                storeStatistic1.StoreId = storeId;
                storeStatistic1.SingularViewCount = 1;

                _storeStatisticService.InsertStoreStatistic(storeStatistic1);
                return Json(true);
            }
            var storeStatistic = _storeStatisticService.GetStoreStatisticByIpAndStoreIdAndRecordDate(storeId, ipAdress, recordDate);
            if (storeStatistic == null)
            {
                storeStatistic = new StoreStatistic();
                storeStatistic.RecordDate = dateNow;
                isNullProductStatistic = true;
                storeStatistic.UserIp = ipAdress;
                storeStatistic.StoreId = storeId;
                storeStatistic.SingularViewCount = 1;

                var locationHelper = new LocationHelper(ipAdress);
                try
                {
                    var JSONObj = locationHelper.GetLocationFromIp();

                    storeStatistic.UserCity = JSONObj["regionName"].ToString();
                    storeStatistic.UserCountry = JSONObj["country"].ToString();
                }
                catch (Exception ex)
                {
                }
            }

            if (isNullProductStatistic)
                storeStatistic.ViewCount = 1;
            else
                storeStatistic.ViewCount += 1;

            if (isNullProductStatistic)
                _storeStatisticService.InsertStoreStatistic(storeStatistic);
            else
                _storeStatisticService.UpdateStoreStatistic(storeStatistic);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Status301()
        //{
        //    var redirectLocation = this.Request.RequestContext.RouteData.DataTokens["redirectLocation"] as string;
        //    Response.CacheControl = "no-cache";
        //    Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
        //    Response.StatusCode = 301;
        //    Response.RedirectLocation = AppSettings.SiteUrlWithoutLastSlash + redirectLocation;
        //    return new ContentResult();

        //}

        [ChildActionOnly]
        public ActionResult _CategoryProduct(int storeId, string categoryId)
        {
            MTCategoryModel categoryModel = new MTCategoryModel();
            int CategoryId = 0;
            if (!string.IsNullOrEmpty(categoryId))
                CategoryId = Convert.ToInt32(categoryId);
            var store = _storeService.GetStoreByMainPartyId(storeId);
            var categories = _categoryService.GetCategoriesByStoreMainPartyId(store.MainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Sector || x.CategoryType == (byte)CategoryType.ProductGroup || x.CategoryType == (byte)CategoryType.Category).ToList();
            if (CategoryId == 0)
            {
                foreach (var item in categories)
                {
                    string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName, store.StoreUrlName);
                    categoryModel.MTCategoryItems.Add(new MTCategoryItem
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        CategoryType = item.CategoryType,
                        CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                        CategoryUrl = categoryUrl
                    });
                }
            }

            if (CategoryId != 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(CategoryId);
                if (category.CategoryType != (byte)CategoryTypeEnum.ProductGroup && category.CategoryType != (byte)CategoryTypeEnum.Category)
                {
                    var topCategories = _categoryService.GetSPTopCategories(CategoryId).Where(x => x.CategoryType != (byte)CategoryType.Model);

                    foreach (var item in topCategories)
                    {
                        string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, item.CategoryContentTitle, store.StoreUrlName);
                        categoryModel.MTTopCategoryItems.Add(new MTCategoryItem
                        {
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                            CategoryUrl = categoryUrl,
                            CategoryType = item.CategoryType
                        });
                    }
                }
                else
                {
                    if (category.CategoryType == (byte)CategoryTypeEnum.Category)
                    {
                        var topCategories = _categoryService.GetSPTopCategories(CategoryId).Where(x => x.CategoryId != CategoryId);

                        foreach (var item in topCategories)
                        {
                            string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, item.CategoryContentTitle, store.StoreUrlName);
                            categoryModel.MTTopCategoryItems.Add(new MTCategoryItem
                            {
                                CategoryId = item.CategoryId,
                                CategoryName = item.CategoryName,
                                CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                                CategoryUrl = categoryUrl,
                                CategoryType = item.CategoryType
                            });
                        }
                    }
                    var ids = categories.Select(x => x.CategoryId).ToList();

                    var subCategories = _categoryService.GetSPBottomCategories(CategoryId).Where(x => ids.Contains(x.CategoryId));
                    var subCategoriesNew = _categoryService.GetCategoriesByCategoryIds(subCategories.Select(x => x.CategoryId).ToList());
                    foreach (var item in subCategoriesNew)
                    {
                        string categoryUrl = UrlBuilder.GetStoreProfileProductCategoryUrl(item.CategoryId, !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName, store.StoreUrlName);
                        categoryModel.MTTopCategoryItems.Add(new MTCategoryItem
                        {
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            CategoryType = item.CategoryType.Value,
                            CategoryParentId = Convert.ToInt32(item.CategoryParentId),
                            CategoryUrl = categoryUrl
                        });
                    }
                }

                var activeCategory = _categoryService.GetCategoryByCategoryId(CategoryId);
                categoryModel.ActiveCategory = activeCategory;
            }
            return PartialView(categoryModel);
        }

        #endregion Methods
    }
}