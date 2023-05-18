using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Videos;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Entities.Tables.Videos;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Utilities.Mvc;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Videos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    [Compress]
    public partial class VideosController : BaseController
    {
        #region Constants

        private const int SHOWED_PAGE_LENGTH = 21;
        private const string PAGE_INDEX_QUERY_STRING_KEY = "page";
        private const string SEARCH_TEXT_QUERY_STRING_KEY = "SearchText";
        private const string CATEGORY_ID_QUERY_STRING_KEY = "CategoryId";

        #endregion

        #region Fields

        private readonly IVideoService _videoService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public VideosController(IVideoService videoService, ICategoryService categoryService, IStoreService storeService,
            IProductService productService, ICacheManager cacheManager)
        {
            _videoService = videoService;
            _categoryService = categoryService;
            _storeService = storeService;
            _productService = productService;
            _cacheManager = cacheManager;

            _videoService.CachingGetOrSetOperationEnabled = false;
        }

        #endregion

        #region Utilities

        //private void PrepareSeo(int categoryId)
        //{
        //    if (categoryId > 0)
        //    {
        //        var category = _categoryService.GetCategoryByCategoryId(categoryId);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Category, category.CategoryName);
        //        var seoParentCategory = _categoryService.GetCategoriesByCategoryParentId(categoryId);
        //        if (seoParentCategory != null)
        //        {
        //            string parentText = "";
        //            foreach (var item in seoParentCategory)
        //            {
        //                if (string.IsNullOrWhiteSpace(parentText))
        //                {
        //                    parentText = item.CategoryName;
        //                }
        //                else
        //                    parentText = parentText + ", " + item.CategoryName;
        //            }
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.AltKategoriForAktifKategori, parentText);
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.KategoriBaslik,
        //            !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName);
        //        }
        //    }
        //}

        //public void PrepareVideoDetailSeo(ProductAndStoreDetailResult product, Video video)
        //{
        //if (!string.IsNullOrEmpty(video.VideoTitle))
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.ProductName, video.VideoTitle);
        //else
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.ProductName, product.ProductName);

        //CreateSeoParameter(SeoModel.SeoProductParemeters.Category, product.CategoryName);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.Brand, product.BrandName);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.Model, product.ModelName);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.ModelYear, product.ModelYear.ToString());
        //CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, product.StoreName);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductType, product.ProductTypeText);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductStatu, product.ProductStatuText);

        //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductSalesType, product.ProductSalesTypeText);
        //CreateSeoParameter(SeoModel.SeoProductParemeters.ProductSalesType, product.BriefDetailText);

        //CreateSeoParameter(SeoModel.SeoProductParemeters.Price, ViewData["dov"] != null ? ViewData["dov"].ToString() : "-");

        //if (product.StoreCityName != "" && product.StoreCityName != null)
        //{
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, product.StoreCityName);
        //}
        //else if (product.MemberCityName != "" && product.MemberCityName != null)
        //{
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, product.MemberCityName);
        //}
        //if (product.StoreLocalityName != "" && product.StoreLocalityName != null)
        //{
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, product.StoreLocalityName);
        //}
        //else if (product.MemberLocalityName != "" && product.MemberLocalityName != null)
        //{
        //    CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, product.MemberLocalityName);
        //}
        //}

        private void PrepareNavigation(MTVideoViewModel model)
        {
            string navigation = string.Empty;
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Videolar", "/videolar", Navigation.TargetType._self));
            var topCategories = model.VideoCategoryModel.VideoTopCategoryItemModels;

            foreach (var item in topCategories)
            {
                string Title = item.CategoryName + " Videoları";
                var Target = Navigation.TargetType._self;
                string Url = UrlBuilder.GetVideoCategoryUrl(item.CategoryId, item.CategoryName);
                var NavigationItm = new Navigation(Title, Url, Target);
                alMenu.Add(NavigationItm);
            }
            model.Navigation = LoadNavigationV2(alMenu);
        }

        private void PreparePopularVideoModel(MTVideoViewModel model)
        {
            var popularVideos = _videoService.GetSPPopularVideos();
            IList<MTPopularVideoModel> popularVideoModels = new List<MTPopularVideoModel>();
            foreach (var item in popularVideos)
            {

                var videoItem = new MTPopularVideoModel
                {
                    CategoryName = item.CategoryName,
                    PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                    ProductName = item.ProductName,
                    SingularViewCount = item.SingularViewCount,
                    TruncatetStoreName = StringHelper.Truncate(item.StoreName, 255),
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                };

                popularVideoModels.Add(videoItem);
            }
            model.PopularVideoModels = popularVideoModels;
        }


        private void PrepareVideoCategoryModel(int categoryParentId, MTVideoViewModel model)
        {
            var videoCategories = _videoService.GetSPVideoCategoryByCategoryParentIdNew(categoryParentId);
            IList<MTVideoCategoryItemModel> videoCategoryItemModels = new List<MTVideoCategoryItemModel>();
            foreach (var item in videoCategories)
            {
                videoCategoryItemModels.Add(new MTVideoCategoryItemModel
                {
                    CategoryName = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName,
                    //CategoryName = item.CategoryName,
                    CategoryUrl = UrlBuilder.GetVideoCategoryUrl(item.CategoryId, item.CategoryContentTitle)
                });
            }
            model.VideoCategoryModel.VideoCategoryItemModels = videoCategoryItemModels.OrderBy(q => q.CategoryName).ToList();

            if (categoryParentId > 0)
            {
                var topCategories = _categoryService.GetSPTopCategories(categoryParentId);
                if (topCategories.Count > 0)
                {
                    var lastCategory = topCategories.LastOrDefault();
                    model.VideoCategoryModel.SelectedCategoryId = categoryParentId;
                    model.VideoCategoryModel.SelectedCategoryName = !string.IsNullOrEmpty(lastCategory.CategoryContentTitle) ? lastCategory.CategoryContentTitle : lastCategory.CategoryName;

                    foreach (var item in topCategories)
                    {
                        model.VideoCategoryModel.VideoTopCategoryItemModels.Add(new MTVideoCategoryItemModel
                        {
                            CategoryName = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName,
                            //CategoryName = item.CategoryName,
                            CategoryType = item.CategoryType,
                            CategoryId = item.CategoryId,
                            CategoryParentId = item.CategoryParentId
                        });

                    }
                }

            }
        }

        private void PreparePagingModel(int categoryId, int totalItemCount, MTVideoViewModel model)
        {
            //paging
            if (categoryId > 0)
            {
                int pageIndex = GetCurrentPageQueryString();
                if (totalItemCount > SHOWED_PAGE_LENGTH)
                {
                    model.VideoPagingModel.CurrentPage = pageIndex;
                    if (totalItemCount % SHOWED_PAGE_LENGTH == 0)
                    {
                        model.VideoPagingModel.TotalPageCount = totalItemCount / SHOWED_PAGE_LENGTH;
                    }
                    else
                    {
                        model.VideoPagingModel.TotalPageCount = (totalItemCount / SHOWED_PAGE_LENGTH) + 1;
                    }


                    int firstPage = model.VideoPagingModel.CurrentPage >= 5 ? model.VideoPagingModel.CurrentPage - 4 : 1;
                    int lastPage = firstPage + 8;
                    if (lastPage >= model.VideoPagingModel.TotalPageCount)
                    {
                        lastPage = model.VideoPagingModel.TotalPageCount;
                    }
                    model.VideoPagingModel.FirstPage = firstPage;
                    model.VideoPagingModel.LastPage = lastPage;
                }
            }
        }

        private void PreparePagingModel(int categoryId, int totalItemCount, MTVideoSearchViewModel model)
        {
            //paging
            if (categoryId > 0)
            {
                int pageIndex = GetCurrentPageQueryString();
                if (totalItemCount > SHOWED_PAGE_LENGTH)
                {
                    model.VideoPagingModel.CurrentPage = pageIndex;
                    if (totalItemCount % SHOWED_PAGE_LENGTH == 0)
                    {
                        model.VideoPagingModel.TotalPageCount = totalItemCount / SHOWED_PAGE_LENGTH;
                    }
                    else
                    {
                        model.VideoPagingModel.TotalPageCount = (totalItemCount / SHOWED_PAGE_LENGTH) + 1;
                    }


                    int firstPage = model.VideoPagingModel.CurrentPage >= 5 ? model.VideoPagingModel.CurrentPage - 4 : 1;
                    int lastPage = firstPage + 8;
                    if (lastPage >= model.VideoPagingModel.TotalPageCount)
                    {
                        lastPage = model.VideoPagingModel.TotalPageCount;
                    }
                    model.VideoPagingModel.FirstPage = firstPage;
                    model.VideoPagingModel.LastPage = lastPage;
                }
            }
        }

        private void PrepareVideoModel(int categoryId, out int videoCountByCategoryId, MTVideoViewModel model)
        {
            videoCountByCategoryId = 0;
            IList<MTVideoModel> videoModels = new List<MTVideoModel>();
            if (categoryId <= 0)
            {
                var showOnShowCaseVideos = _videoService.GetSPShowOnShowcaseVideos();
                foreach (var item in showOnShowCaseVideos)
                {

                    videoModels.Add(new MTVideoModel
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
                    });
                }
            }
            else
            {
                int pageIndex = GetCurrentPageQueryString();
                var videos = _videoService.GetSPVideoByCategoryId(categoryId, pageIndex, SHOWED_PAGE_LENGTH);
                videoCountByCategoryId = videos.TotalCount;

                foreach (var item in videos)
                {
                    videoModels.Add(new MTVideoModel
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
                    });
                }

                //if (videos.Count < 21)
                //{
                //    var videosCategoryParent = _videoService.GetVideoResultsWithPageSizeCompletion(videos, categoryId, 21);
                //    foreach (var item in videosCategoryParent)
                //    {
                //        videoModels.Add(new MTVideoModel
                //        {
                //            PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                //            ProductUrl = UrlBuilder.ProductUrl(item.ProductId, item.ProductName),
                //            ShortDescription = StringHelper.Truncate(string.Format("{0}-{1}", item.BrandName, item.ModelName), 25),
                //            SingularViewCount = item.SingularViewCount,
                //            StoreName = item.StoreName,
                //            StoreUrl = UrlBuilder.GetStoreProfileUrl(item.StoreId, item.StoreName),
                //            //TruncateProductName = item.ProductName.Length > 35 ? StringHelper.Truncate(item.ProductName, 35) + "..." : item.ProductName,
                //            TruncateProductName = item.ProductName,
                //            TruncateStoreName = item.StoreName.Length > 35 ? StringHelper.Truncate(item.StoreName, 35) + "..." : item.StoreName,
                //            VideoMinute = item.Minute.HasValue ? item.Minute.Value : (byte)1,
                //            VideoSecond = item.Second.HasValue ? item.Second.Value : (byte)1,
                //            VideoRecordDate = string.Format("{0:dd/MM/yyyy}", item.VideoRecordDate),
                //            VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                //        });
                //    }
                //}
            }
            var residualBill = 21 - videoModels.Count;
            if (residualBill > 6 && categoryId > 0)
            {

                List<MTVideoModel> similarVideos = new List<MTVideoModel>();
                var category = _categoryService.GetCategoryByCategoryId(categoryId);
                if (category != null)
                {
                    PrepareOtherVideoModels(videoModels.Count, categoryId, Convert.ToInt32(category.CategoryParentId), 21, similarVideos, 1);
                    model.SimilarVideos = similarVideos;
                }

            }
            model.VideoModels = videoModels;
        }

        public void PrepareOtherVideos(MTVideoItemViewModel videoItemModel, int categoryId, Video video)
        {

            //if (Session["automaticVideoStatus"] != null)
            //    videoItemModel.MTOtherVideosModel.VideoAutomaticStatus = (bool)Session["automaticVideoStatus"];

            var otherVideos = _videoService.GetSPOtherVideoByCategoryIdAndSelectedCategoryId(categoryId, 11, 0)
                              .Where(x => x.VideoId != video.VideoId).ToList();
            foreach (var item in otherVideos)
            {

                videoItemModel.MTOtherVideosModel.MTVideoModels.Add(new MTVideoModel
                {
                    PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    ShortDescription = StringHelper.Truncate(string.Format("{0}-{1}", item.BrandName, item.ModelName), 25),
                    SingularViewCount = item.SingularViewCount,
                    StoreName = item.StoreName,
                    StoreUrl = UrlBuilder.GetStoreProfileUrl(item.StoreId, item.StoreName, item.StoreUrlName),
                    TruncateProductName = item.ProductName,
                    TruncateStoreName = item.StoreName.Length > 35 ? StringHelper.Truncate(item.StoreName, 35) + "..." : item.StoreName,
                    VideoMinute = item.Minute.HasValue ? item.Minute.Value : (byte)1,
                    VideoSecond = item.Second.HasValue ? item.Second.Value : (byte)1,
                    VideoRecordDate = string.Format("{0:dd/MM/yyyy}", item.VideoRecordDate),
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                });


            }
            int pageOtherDimension = 10;

            if (otherVideos.Count < 10)
            {
                PrepareOtherVideoModels(otherVideos.Count, categoryId, Convert.ToInt32(video.Product.Category.CategoryParentId), pageOtherDimension, videoItemModel.MTOtherVideosModel.MTVideoModels, 0);


            }
        }

        public void PrepareStoreAndProductDetail(MTVideoItemViewModel videoItemModel, Video video, ProductAndStoreDetailResult product)
        {

            videoItemModel.MTStoreAndProductDetailModel.ProductUrl = UrlBuilder.GetProductUrl(Convert.ToInt32(video.ProductId), product.ProductName);
            videoItemModel.MTStoreAndProductDetailModel.VideoRecordDate = video.VideoRecordDate.ToString();

            videoItemModel.MTStoreAndProductDetailModel.CurrencyCss = video.Product.GetCurrency();
            videoItemModel.MTStoreAndProductDetailModel.ProductDescription = video.Product.ProductDescription;
            videoItemModel.MTStoreAndProductDetailModel.ProductId = video.ProductId.Value;
            videoItemModel.MTStoreAndProductDetailModel.ProductName = product.ProductName;
            videoItemModel.MTStoreAndProductDetailModel.ProductPrice = video.Product.GetFormattedPriceWithCurrency();

            if (!string.IsNullOrEmpty(video.VideoTitle))
            {
                videoItemModel.MTStoreAndProductDetailModel.VideoTitle = video.VideoTitle;
            }
            else
            {
                videoItemModel.MTStoreAndProductDetailModel.VideoTitle = product.ProductName;
            }
            if (video.Order != null)
            {
                videoItemModel.MTStoreAndProductDetailModel.VideoTitle = $"{videoItemModel.MTStoreAndProductDetailModel.VideoTitle} {video.Order.ToString()}";
            }


            videoItemModel.MTStoreAndProductDetailModel.ProductStatus = video.Product.GetProductStatuText();

            if (video.Product.Brand != null)
                videoItemModel.MTStoreAndProductDetailModel.BrandName = product.CategoryName;
            if (video.Product.Model != null)
                videoItemModel.MTStoreAndProductDetailModel.ModelName = product.ModelName;

            videoItemModel.MTStoreAndProductDetailModel.ProductTypeText = video.Product.GetProductTypeText();
            videoItemModel.MTStoreAndProductDetailModel.StoreLogo = ImageHelper.GetStoreLogoPath(Convert.ToInt32(product.StoreMainPartyId), product.StoreLogo, 100);
            videoItemModel.MTStoreAndProductDetailModel.VideoSingularViewCount = Convert.ToInt64(video.SingularViewCount);
            videoItemModel.MTStoreAndProductDetailModel.StoreName = product.StoreName;
            videoItemModel.MTStoreAndProductDetailModel.StoreUrl = UrlBuilder.GetStoreProfileUrl(Convert.ToInt32(product.StoreMainPartyId), product.StoreName, product.StoreUrlName);
            videoItemModel.MTStoreAndProductDetailModel.StoreVideosPageUrl = UrlBuilder.GetStoreVideoUrl(Convert.ToInt32(product.StoreMainPartyId), product.StoreName, product.StoreUrlName);
            videoItemModel.MTStoreAndProductDetailModel.StoreProductsUrl = UrlBuilder.GetStoreProfileProductUrl(Convert.ToInt32(product.StoreMainPartyId), product.StoreName, product.StoreUrlName);

        }

        public void PrepareStoreModelForVideoSearch(Store store, MTVideoSearchViewModel model)
        {
            if (store != null)
            {
                model.Store.StoreName = store.StoreName;
                model.Store.StoreVideosPageUrl = UrlBuilder.GetStoreVideoUrl(Convert.ToInt32(store.MainPartyId), store.StoreName, store.StoreUrlName);
                model.Store.StoreLogo = ImageHelper.GetStoreLogoPath(Convert.ToInt32(store.MainPartyId), store.StoreLogo, 300);
                model.Store.StoreAbout = store.StoreAbout;

            }
        }

        public void PrepareVideoSearchVideoModel(IPagedList<VideoResult> videos, MTVideoSearchViewModel model)
        {
            if (videos.ToList().Count > 0)
            {
                foreach (var item in videos.ToList())
                {
                    var videoModel = new MTVideoModel
                    {
                        PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                        ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                        ShortDescription = StringHelper.Truncate(string.Format("{0}-{1}", item.BrandName, item.ModelName), 25),
                        SingularViewCount = item.SingularViewCount,
                        StoreName = item.StoreName,
                        StoreUrl = UrlBuilder.GetStoreProfileUrl(item.StoreId, item.StoreName, item.StoreUrlName),
                        TruncateProductName = (item.ProductName.Length > 65) ? StringHelper.Truncate(item.ProductName, 65) + "..." : item.ProductName,
                        TruncateStoreName = item.StoreName.Length > 35 ? StringHelper.Truncate(item.StoreName, 35) + "..." : item.StoreName,
                        VideoMinute = item.Minute.HasValue ? item.Minute.Value : (byte)1,
                        VideoSecond = item.Second.HasValue ? item.Second.Value : (byte)1,
                        VideoRecordDate = string.Format("{0:dd/MM/yyyy}", item.VideoRecordDate),
                        VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)

                    };
                    model.VideoModels.Add(videoModel);
                }

            }

        }

        public void PrepareVideoSearchCategoryModel(IList<VideoCategoryResult> videoCategories, MTVideoSearchViewModel model)
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

                foreach (var item in topCategories)
                {
                    string categoryUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);
                    categoryUrl = QueryStringBuilder.RemoveQueryString(categoryUrl, SEARCH_TEXT_QUERY_STRING_KEY);
                    categoryUrl = QueryStringBuilder.ModifyQueryString(categoryUrl, CATEGORY_ID_QUERY_STRING_KEY + "=" + item.CategoryId, null);
                    categoryUrl = QueryStringBuilder.ModifyQueryString(categoryUrl, SEARCH_TEXT_QUERY_STRING_KEY + "=" + searchText, null);
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
                string categoryUrl = QueryStringBuilder.RemoveQueryString(this.Request.Url.ToString(), PAGE_INDEX_QUERY_STRING_KEY);
                categoryUrl = QueryStringBuilder.RemoveQueryString(categoryUrl, SEARCH_TEXT_QUERY_STRING_KEY);
                categoryUrl = QueryStringBuilder.ModifyQueryString(categoryUrl, CATEGORY_ID_QUERY_STRING_KEY + "=" + item.CategoryId, null);
                categoryUrl = QueryStringBuilder.ModifyQueryString(categoryUrl, SEARCH_TEXT_QUERY_STRING_KEY + "=" + searchText, null);
                var categoryItemModel = new MTVideoCategoryItemModel
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    CategoryUrl = categoryUrl
                };
                model.VideoCategoryModel.VideoCategoryItemModels.Add(categoryItemModel);
            }

        }

        private string GetNavigation(IList<TopCategoryResult> TopCategoryItems)
        {
            string navigation = string.Empty;
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Videolar", AppSettings.VideoUrlBase, Navigation.TargetType._self));
            if (TopCategoryItems != null)
            {
                foreach (var item in TopCategoryItems)
                {
                    if (item.CategoryParentId == null)
                    {
                        alMenu.Add(new Navigation(item.CategoryName + " Videoları", UrlBuilder.GetVideoCategoryUrl(item.CategoryId, item.CategoryName), Navigation.TargetType._self));
                    }
                    else if (item.CategoryType != (byte)CategoryType.ProductGroup)
                    {
                        alMenu.Add(new Navigation(item.CategoryName + " Videoları", UrlBuilder.GetVideoCategoryUrl(item.CategoryId, item.CategoryName), Navigation.TargetType._self));
                    }
                }
            }
            navigation = LoadNavigationV3(alMenu);
            return navigation;
        }

        private void PrepareOtherVideoModels(int totalRecord, int categoryId, int categoryParentId, int pageDimension, List<MTVideoModel> videoModels, int type)
        {
            int residualBill = 0;
            int row = 0;

            if (totalRecord < pageDimension)
            {
                residualBill = pageDimension - totalRecord;
                var category1 = _categoryService.GetCategoryByCategoryId(categoryId);

                var otherVideosTemp = _videoService.GetSPOtherVideoByCategoryIdAndSelectedCategoryId(categoryParentId, pageDimension, categoryId).ToList();
                //if (type == 0)
                //{
                //    otherVideosTemp = otherVideosTemp.Where(v => !videoSeen.Contains(v.VideoId)).ToList();
                //}

                foreach (var item in otherVideosTemp)
                {

                    MTVideoModel model = new MTVideoModel
                    {
                        PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath),
                        ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                        ShortDescription = StringHelper.Truncate(string.Format("{0}-{1}", item.BrandName, item.ModelName), 25),
                        SingularViewCount = item.SingularViewCount,
                        StoreName = item.StoreName,
                        StoreUrl = UrlBuilder.GetStoreProfileUrl(item.StoreId, item.StoreName, item.StoreUrlName),
                        TruncateProductName = item.ProductName,
                        TruncateStoreName = item.StoreName.Length > 35 ? StringHelper.Truncate(item.StoreName, 35) + "..." : item.StoreName,
                        VideoMinute = item.Minute.HasValue ? item.Minute.Value : (byte)1,
                        VideoSecond = item.Second.HasValue ? item.Second.Value : (byte)1,
                        VideoRecordDate = string.Format("{0:dd/MM/yyyy}", item.VideoRecordDate),
                        VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName)
                    };
                    videoModels.Add(model);
                    row++;
                    if (row == residualBill)
                        break;
                }


            }
            if (videoModels.Count < pageDimension && categoryParentId > 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryParentId);
                if (category != null)
                {
                    totalRecord = totalRecord + row;
                    if (category.CategoryParentId != null && category.CategoryParentId != 0)
                    {
                        PrepareOtherVideoModels(totalRecord, category.CategoryId, category.CategoryParentId.Value, pageDimension, videoModels, type);
                    }
                }
            }
        }

        #endregion

        #region Methods

        [HttpGet]
        [OutputCache(Duration = 3600, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> Index()
        {
            int categoryId = GetCategoryIdRouteData();

            int videoCountByCategoryId = 0;
            var request = HttpContext.Request;
            ViewBag.Canonical = AppSettings.VideoUrlBase + request.Url.AbsolutePath;

            //if (categoryId == 0)
            //{
            //    SeoPageType = (byte)PageType.VideoMainPage;
            //}
            //else
            //{
            //    //var category = _categoryService.GetCategoryByCategoryId(categoryId);
            //    //string url = UrlBuilder.GetVideoCategoryUrl(category.CategoryId, category.CategoryName);

            //    //string requestUrl = request.IsLocal ? request.Url.AbsolutePath : request.Url.AbsoluteUri;
            //    //bool urlCheck = requestUrl == url;
            //    //if (!urlCheck)
            //    //{
            //    //    return RedirectPermanent(url);
            //    //}
            //    SeoPageType = (byte)PageType.VideoCategoryPage;
            //}

            if (categoryId != 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryId);
                if (category == null)
                {
                    return RedirectPermanent(AppSettings.VideoUrlBase);
                }
                else
                {
                    var EskiUrl = this.Request.Url.ToString();
                    var YeniUrl = UrlBuilder.GetVideoCategoryUrl(category.CategoryId, category.CategoryContentTitle);
                    if (EskiUrl != YeniUrl)
                    {
                        YeniUrl = $"{YeniUrl}";
                        return RedirectPermanent(YeniUrl);
                    }
                }
            }
            else
            {
                // Vanlıya Alırken Kaldrılacak  Unutma Not Osman

                //if (!request.IsLocal && Request.Url.ToString().Contains("videolar"))
                //{
                //    return RedirectPermanent(AppSettings.VideoUrlBase);
                //}
            }



            string key = string.Format("video-pages-test");
            var testModel = _cacheManager.Get(key, () =>
            {
                var model = new MTVideoViewModel();

                //popular videos
                PreparePopularVideoModel(model);
                //video category
                PrepareVideoCategoryModel(categoryId, model);
                //videos
                ////static değer atama yaptık
                //ajaxVideoModel = model;

                PrepareVideoModel(categoryId, out videoCountByCategoryId, model);
                //paging
                PreparePagingModel(categoryId, videoCountByCategoryId, model);
                //navigation
                PrepareNavigation(model);

                //similar
                //seo parameter
                //PrepareSeo(categoryId);

                return model;

            });

            return await Task.FromResult(View(testModel));
        }



        public ActionResult VideoItems2(int VideoId)
        {

            var request = HttpContext.Request;
            ViewBag.Canonical = AppSettings.VideoUrlBase + request.Url.AbsolutePath;

            _videoService.CachingGetOrSetOperationEnabled = false;
            var video = _videoService.GetVideoByVideoId(VideoId);
            if (video == null)
                return RedirectPermanent(AppSettings.VideoUrlBase);
            var productvideos = _videoService.GetVideosByProductId((int)video.ProductId);
            if (productvideos.Count > 1)
            {
                video.Order = (byte)productvideos.ToList().FindIndex(x => x.VideoId == video.VideoId);
                video.Order++;
            }



            var product = _videoService.GetSPStoreAndProductDetailByProductId(video.ProductId.Value);
            if (product == null)
                return RedirectPermanent(AppSettings.VideoUrlBase);



            //string requestUrl = request.IsLocal ? request.Url.AbsolutePath : request.Url.AbsoluteUri;
            //bool urlCheck = requestUrl == url;
            //if (!urlCheck)
            //{
            //    return RedirectPermanent(url);
            //}

            var url = Request.Url.AbsolutePath;

            if (!Request.IsLocal)
            {
                url = Request.Url.AbsoluteUri;
            }

            var link = UrlBuilder.GetVideoUrl(video.VideoId, product.ProductName);

            if (!Request.IsLocal)
            {
                link = link + Request.Url.Query;
            }
            if (url != link)
                return RedirectPermanent(link);

            if (product.ProductActive == false)
            {
                url = UrlBuilder.GetVideoCategoryUrl(product.CategoryId, product.CategoryName);
                return RedirectPermanent(url);
            }



            //SeoPageType = (byte)PageType.VideoViewPage;
            MTVideoItemViewModel videoItemModel = new MTVideoItemViewModel();
            int categoryId = product.CategoryId;
            videoItemModel.VideoPath = video.VideoPath;
            videoItemModel.VideoId = video.VideoId;
            videoItemModel.MTVideoCategoryItemModel.CategoryId = categoryId;

            var category = _categoryService.GetCategoryByCategoryId(categoryId);
            if (category != null)
            {
                var dataVideos = _videoService.GetSPVideoCategoryByCategoryParentIdNew(category.CategoryParentId.Value);

            }
            else
            {
                return RedirectPermanent(AppSettings.VideoUrlBase);
            }

            ViewData["topMenuVideo"] = "active";

            //prepare
            PrepareStoreAndProductDetail(videoItemModel, video, product);
            PrepareOtherVideos(videoItemModel, categoryId, video);

            //PrepareVideoDetailSeo(product, video);


            video.SingularViewCount += 1;
            _videoService.UpdateVideo(video);

            //navigation
            var topCategories = _categoryService.GetSPTopCategories(categoryId);
            ViewData["ViewNavigation"] = this.GetNavigation(topCategories);

            return View("VideoItems2", videoItemModel);


        }

        [HttpPost]
        public string SetVideoAutomaticStatus(bool status)
        {
            //Session["automaticVideoStatus"] = status;
            return "";
        }

        [HttpGet]
        public ActionResult VideoSearch()
        {

            string searchText = GetSearchTextQueryString();
            if (string.IsNullOrEmpty(searchText))
            {
                return RedirectToActionPermanent("Error", "Home");

            }

            //SeoPageType = (byte)PageType.VideoSearchPage;
            //CreateSeoParameter(SeoModel.SeoProductParemeters.ArananKelime, searchText);//kontrol et
            var model = new MTVideoSearchViewModel();
            //var store = _storeService.GetStoreForVideoSearch(searchText);
            //PrepareStoreModelForVideoSearch(store, model);
            if (searchText.Length >= 3)
            {
                model.SearchText = searchText;
                //PrepareSearchVideoCategoryModel(model);
                //PrepareSearchNavigation(model);
                int categoryId = GetCategoryIdQueryString();
                int pageIndex = GetPageQueryString();
                int pageSize = GetPageSizeQueryString();
                var videos = _videoService.GetSpVideosBySearchText(searchText, categoryId, pageSize, pageIndex);
                var videoCategories = _categoryService.GetSPVideoCategoryForSearchVideo(searchText, categoryId);

                PrepareVideoSearchVideoModel(videos, model);
                PrepareVideoSearchCategoryModel(videoCategories, model);
                PreparePagingModel(categoryId, videos.TotalCount, model);


            }
            return View(model);

        }

        //public ActionResult VideoPaging(int page, byte pageDimension, int categoryId)
        //{
        //    MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();

        //    int videoid = 0;
        //    if (this.RouteData.Values["VideoId"] != null)
        //    {
        //        int videoID = this.RouteData.Values["VideoId"].ToInt32();
        //        videoid = videoID;
        //        var video = entities.Videos.Where(c => c.VideoId == videoID).SingleOrDefault();
        //        ViewData["video"] = video;
        //        var product = entities.spProductDetailInfoByProductID(video.ProductId).SingleOrDefault();
        //        ViewData["makinaentities"] = product;
        //        ViewData["groupname"] = NeoSistem.MakinaTurkiye.Web.Helpers.StringHelpers.ToUrl(entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName);
        //        var model2 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.Currency.CurrencyName).SingleOrDefault();
        //        var model3 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.ProductPrice).SingleOrDefault();

        //        if (model3 != null)
        //        {
        //            decimal sayı = (decimal)model3;
        //            string tutar = sayı.ToString("0.00");
        //            if (tutar == "0,00")
        //            {

        //                ViewData["dov"] = "Sorunuz";
        //            }
        //            else
        //            {
        //                ViewData["dov"] = tutar + " " + model2.ToString();
        //            }
        //        }
        //    }

        //    ViewData["CategoryName"] = entities.Categories.SingleOrDefault(c => c.CategoryId == categoryId).CategoryName;
        //    ViewData["CategoryId"] = categoryId;

        //    var dataVideo = new Data.Video();
        //    var videoModel = new SearchModel<VideoModel>
        //    {
        //        CurrentPage = page,
        //        PageDimension = pageDimension,
        //        TotalRecord = TotalRecord
        //    };

        //    var data = dataVideo.VideoSearch(ref TotalRecord, videoModel.PageDimension, videoModel.CurrentPage, categoryId).AsCollection<VideoModel>();

        //    videoModel.Source = data;
        //    videoModel.TotalRecord = TotalRecord;
        //    if (this.RouteData.Values["VideoId"] == null)
        //    {
        //        int videoID = data.First().VideoId;
        //        var video = entities.Videos.Where(c => c.VideoId == videoID).SingleOrDefault();
        //        ViewData["video"] = video;
        //        var product = entities.spProductDetailInfoByProductID(video.ProductId).SingleOrDefault();
        //        ViewData["makinaentities"] = product;
        //        var model2 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.Currency.CurrencyName).SingleOrDefault();
        //        var model3 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.ProductPrice).SingleOrDefault();

        //        if (model3 != null)
        //        {
        //            decimal sayı = (decimal)model3;
        //            string tutar = sayı.ToString("0.00");
        //            if (tutar == "0,00")
        //            {

        //                ViewData["dov"] = "Sorunuz";
        //            }
        //            else
        //            {
        //                ViewData["dov"] = tutar + " " + model2.ToString();
        //            }
        //        }


        //    }
        //    return View("VideoList", videoModel);
        //}

        //[HttpPost]
        //public ActionResult SetSingularViewCount(int VideoId)
        //{
        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        var video = entities.Videos.SingleOrDefault(c => c.VideoId == VideoId);
        //        video.SingularViewCount += 1;
        //        entities.SaveChanges();
        //    }
        //    //SessionSingularViewCountType.SingularViewCountTypes.Add(VideoId, SingularViewCountType.Video);
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

        //public string GetNavigation(IList<TopCategory> TopCategoryItems, string videoName = null)
        //{

        //    IList<Navigation> alMenu = new List<Navigation>();
        //    alMenu.Add(new Navigation("Videolar", AppSettings.VideoUrlBase, Navigation.TargetType._self));
        //    if (TopCategoryItems != null)
        //    {
        //        foreach (var item in TopCategoryItems)
        //        {
        //            if (item.CategoryParentId == null)
        //            {
        //                alMenu.Add(new Navigation(item.CategoryName + " Videoları", UrlBuilder.GetVideoCategoryUrl(item.CategoryId,item.CategoryName), Navigation.TargetType._self));
        //            }
        //            else if (item.CategoryType != (byte)CategoryType.ProductGroup)
        //            {
        //                alMenu.Add(new Navigation(item.CategoryName + " Videoları", UrlBuilder.GetVideoCategoryUrl(item.CategoryId, item.CategoryName), Navigation.TargetType._self));
        //            }
        //        }
        //    }

        //    if (!string.IsNullOrEmpty(videoName))
        //    {
        //        alMenu.Add(new Navigation(videoName, string.Empty, Navigation.TargetType._self));
        //    }
        //   var navigation = LoadNavigationV3(alMenu);
        //  return navigation;
        //}


        //[HttpPost]
        //public JsonResult VideoKategoriVerileri()
        //{
        //    return Json(ajaxVideoModel);
        //}

        //int TotalRecord = 0;
        //byte PageDimension = 12;

        //public ActionResult VideoItems(int VideoId)
        //{
        //    try
        //    {

        //        SeoPageType = (byte)PageType.VideoViewPage;
        //        ViewData["makinaentities"] = new ProductDetailInfo();
        //        MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        //        var video = entities.Videos.SingleOrDefault(x => x.VideoId == VideoId);
        //        ViewData["video"] = new Video();
        //        ViewData["video"] = video;
        //        var product = entities.spProductDetailInfoByProductID(video.ProductId).SingleOrDefault();
        //        ViewData["makinaentities"] = product;
        //        int categoryId = product.CategoryId.ToInt32();
        //        ViewData["CategoryId"] = categoryId;
        //        ViewData["storeMainPartyId"] = product.StoreMainPartyId.Value;
        //        ViewData["storeLogo"] = product.StoreLogo;
        //        ViewData["groupname"] = NeoSistem.MakinaTurkiye.Web.Helpers.StringHelpers.ToUrl(entities.Categories.Where(c => c.CategoryId == product.ProductGroupId).SingleOrDefault().CategoryName);
        //        var model2 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.Currency.CurrencyName).SingleOrDefault();
        //        var model3 = (from c in entities.Products
        //                      where c.ProductId == video.ProductId
        //                      select c.ProductPrice).SingleOrDefault();

        //        if (model3 != null)
        //        {
        //            decimal sayı = (decimal)model3;
        //            string tutar = sayı.ToString("0.00");
        //            //if (tutar == "0,00")
        //            //{

        //            //    ViewData["dov"] = "Sorunuz";
        //            //}
        //            //else
        //            //{
        //            //    ViewData["dov"] = tutar + " " + model2.ToString();
        //            //}
        //            if (tutar == "0,00")
        //            {

        //                ViewData["dov"] = "00,00";
        //                ViewData["currencyType"] = "TL";
        //            }
        //            else
        //            {
        //                ViewData["dov"] = tutar;
        //                ViewData["currencyType"] = model2.ToString();
        //            }
        //        }
        //        ViewData["topMenuVideo"] = "active";
        //        var model = new VideoModel();

        //        var dataVideo = new Data.Video();
        //        var videoModel = new SearchModel<VideoModel>
        //        {
        //            CurrentPage = 1,
        //            PageDimension = 10
        //        };
        //        ViewData["videoAutomaticStatus"] = false;
        //        if (Session["automaticVideoStatus"] != null)
        //            ViewData["videoAutomaticStatus"] = (bool)Session["automaticVideoStatus"];

        //        var data = dataVideo.VideoSearch(ref TotalRecord, videoModel.PageDimension, videoModel.CurrentPage, categoryId).AsCollection<VideoModel>();
        //        //if (data.Count < 10)
        //        //{
        //        //    int PageSize = data.Count;
        //        //    int _categoryId = _videoService.GetVideoByCategoryParentId(categoryId);
        //        //    var dataCompletion = dataVideo.VideoSearch(ref TotalRecord, videoModel.PageDimension, videoModel.CurrentPage, _categoryId).AsCollection<VideoModel>();
        //        //    foreach (var setData in dataCompletion)
        //        //    {
        //        //        bool hasGetVideo = false;
        //        //        foreach (var getData in data)
        //        //        {
        //        //            if (PageSize <= 10)
        //        //            {
        //        //                if (setData.VideoId != getData.VideoId)
        //        //                {
        //        //                    hasGetVideo = true;
        //        //                }
        //        //                else
        //        //                {
        //        //                    hasGetVideo = false;
        //        //                    break;
        //        //                }
        //        //            }
        //        //        }
        //        //        if (hasGetVideo)
        //        //        {
        //        //            PageSize++;
        //        //            data.Add(setData);
        //        //        }
        //        //    }
        //        //}
        //        videoModel.TotalRecord = TotalRecord;
        //        videoModel.Source = data.Where(v => v.VideoId != VideoId).ToList();
        //        model.SearchModel = videoModel;

        //        //ViewData["CategoryName"] = entities.Categories.SingleOrDefault(c => c.CategoryId == categoryId).CategoryName;

        //        #region SingularViewCount
        //        video.SingularViewCount += 1;
        //        entities.SaveChanges();
        //        //SessionSingularViewCountType.SingularViewCountTypes.Add(VideoId, SingularViewCountType.Video);
        //        #endregion

        //        #region SeoView
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ProductName, product.ProductName);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Category, product.CategoryName);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Brand, product.BrandName);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Model, product.ModelName);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ModelYear, product.ModelYear.ToString());
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.FirmName, product.StoreName);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ProductType, product.ProductTypeText);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ProductStatu, product.ProductStatuText);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ProductSalesType, product.ProductSalesTypeText);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.ProductSalesType, product.BriefDetailText);
        //        CreateSeoParameter(SeoModel.SeoProductParemeters.Price, ViewData["dov"] != null ? ViewData["dov"].ToString() : "-");
        //        if (product.StoreCityName != "" && product.StoreCityName != null)
        //        {
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, product.StoreCityName);
        //        }
        //        else if (product.MemberCityName != "" && product.MemberCityName != null)
        //        {
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.Sehir, product.MemberCityName);
        //        }
        //        if (product.StoreLocalityName != "" && product.StoreLocalityName != null)
        //        {
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, product.StoreLocalityName);
        //        }
        //        else if (product.MemberLocalityName != "" && product.MemberLocalityName != null)
        //        {
        //            CreateSeoParameter(SeoModel.SeoProductParemeters.Ilce, product.MemberLocalityName);
        //        }
        //        #endregion

        //        //navigation
        //        var topCategories = entities.spCategoryTopCategoriesByCategoryId(categoryId).ToList();
        //        this.GetNavigation(topCategories, video.Product.ProductName);

        //        return View(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionHandler.HandleException(ex);
        //        Response.RedirectPermanent(Url.RouteUrl(new
        //        {
        //            controller = "Videos",
        //            action = "Index"
        //        }));
        //        return null;
        //    }
        //}

        //yeni yapılıyor..

        //public ActionResult WrongUrlCategory(int CategoryId)
        //{
        //    var category = _categoryService.GetCategoryByCategoryId(CategoryId);
        //    var url = UrlBuilder.GetCategoryUrl(CategoryId, !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName, null, string.Empty);
        //    return RedirectPermanent(url);
        //}

        //public string VideoDeleteIfNot()
        //{
        //    var entities = new MakinaTurkiyeEntities();
        //    var videos = entities.Videos.ToList();
        //    string[] dizi = new string[2];
        //    foreach (var item in videos)
        //    {
        //        string fileBase = "/UserFiles/NewVideos/" + item.VideoPath+".mp4";
        //        var absolutePath = HttpContext.Server.MapPath(fileBase);
        //        if (!System.IO.File.Exists(absolutePath))
        //        {
        //            entities.Videos.DeleteObject(item);
        //            entities.SaveChanges();
        //            dizi[0] = "deleted";
        //            Response.Write("delted");
        //        }
        //        else
        //        {

        //            Response.Write("not delete");
        //        }
        //    }

        //    return dizi[0];
        //}

        public ActionResult Redirect301()
        {
            return RedirectPermanent(AppSettings.VideoUrlBase);
        }
        #endregion
    }
}