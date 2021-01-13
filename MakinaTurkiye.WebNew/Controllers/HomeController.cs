using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.ImageHelpers;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Content;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Home;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using MakinaTurkiye.Utilities.Mvc;
using MakinaTurkiye.Caching;
using System.Text;
using System.Web.UI;
using System.IO;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]

    public class HomeController : BaseController
    {

        #region Utilities

        private void PrepareProductRecomandation(MTHomeModel model)
        {
            MTMayLikeProductModel modelItem = new MTMayLikeProductModel();
            if (Request.Cookies["ProductVisited"] != null)
            {
                string data = Request.Cookies["ProductVisited"].Value;

                if (data.Length > 3)
                {
                    IProductService _productService = EngineContext.Current.Resolve<IProductService>();

                    string[] splittedProductds = data.Split(',');
                    List<MTProductItemRecomandation> list = new List<MTProductItemRecomandation>();

                    foreach (var item in splittedProductds)
                    {
                        var product = _productService.GetProductByProductId(Convert.ToInt32(item));
                        list.Add(new MTProductItemRecomandation { ProductId = product.ProductId, BrandId = product.BrandId, CategoryId = product.CategoryId.Value, ModelId = product.ModelId });

                    }
                    var brandSame = list.GroupBy(x => x.BrandId).Select(x => new { total = x.Count(), brandId = x.Key });
                    var mostBrandIds = brandSame.OrderByDescending(x => x.total).Select(x => x.brandId).Skip(0).Take(5);
                    string brandIds = string.Join(",", mostBrandIds);

                    var modelSame = list.GroupBy(x => x.ModelId).Select(x => new { total = x.Count(), modelId = x.Key });
                    var mostModelIds = modelSame.OrderByDescending(x => x.total).Select(x => x.modelId).Skip(0).Take(5);
                    string modelIds = string.Join(",", mostModelIds);

                    var categorySame = list.GroupBy(x => x.CategoryId).Select(x => new { total = x.Count(), categoryId = x.Key });
                    var mostCategoryIds = categorySame.OrderByDescending(x => x.total).Select(x => x.categoryId);
                    string categoryIds = string.Join(",", mostCategoryIds);

                    var results = _productService.GetSPProductRecomandation(categoryIds, modelIds, brandIds);
                    results = results.OrderByDescending(x => x.TotalCount).Skip(0).Take(10).ToList();

                    foreach (var item in results)
                    {

                        modelItem.Products.Add(new MTMayLikeProductItem
                        {
                            BrandName = item.CategoryName,
                            SmallPicturePah = ImageHelper.GetProductImagePath(item.ProductId, item.ProductPicturePath, ProductImageSize.px200x150),
                            ProductName = item.ProductName,
                            ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName)

                        });
                    }
                    model.MTMayLikeProductModel = modelItem;
                }

            }

        }

        private void PrepareHomeSelectedCategoriesProducts(MTHomeModel model)
        {
            ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
            IProductService productService = EngineContext.Current.Resolve<IProductService>();
            IPictureService pictreService = EngineContext.Current.Resolve<IPictureService>();
            ICategoryPlaceChoiceService categoryPlaceChoiceService = EngineContext.Current.Resolve<ICategoryPlaceChoiceService>();

            var categories = categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)CategoryPlaceType.HomeChoicesed, true);
            var products = productService.GetProductsForChoiced();
            var subCategories = categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)CategoryPlaceType.HomeCenter, true);
            //List<int> ProducCatListe = categories.Select(x => (int)x.CategoryId).ToList();
            //var ProductList = productService.GetProductsByCategoryIdList(ProducCatListe).ToList();
            foreach (var categoryItem in categories)
            {

                model.HomeProductsRelatedCategoryModel.Categories.Add(new MTHomeCategoryModel
                {
                    CategoryName = categoryItem.Category.CategoryName,
                    CategoryId = categoryItem.CategoryId,
                    CategoryIcon = AppSettings.CategoryIconImageFolder + categoryItem.Category.CategoryIcon
                });

                var categoryProducts = products.Where(x => x.CategoryTreeName.Contains(categoryItem.CategoryId.ToString()));
                List<int> Liste = categoryProducts.Select(x => (int)x.CategoryId).ToList();
                var CategoryList = categoryService.GetCategoriesByCategoryIds(Liste).ToList();
                foreach (var item in categoryProducts)
                {
                    var category = CategoryList.FirstOrDefault(x => x.CategoryId == Convert.ToInt32(item.CategoryId));
                    string categoryName = category.CategoryName;
                    string categoryNameUrl1 = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;

                    var productPicture = pictreService.GetFirstPictureByProductId(item.ProductId);
                    MTHomeProductRelatedItem product = new MTHomeProductRelatedItem();
                    product.CategoryId = categoryItem.CategoryId;
                    product.ProductName = item.ProductName;
                    product.PicturePath = ImageHelper.GetProductImagePath(item.ProductId, productPicture.PicturePath, ProductImageSize.px200x150);
                    string productPrice = item.GetFormattedPrice();
                    if (productPrice != "Fiyat Sorunuz")
                    {
                        product.ProductPrice = productPrice;
                    }
                    product.ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName);
                    product.SameCategoryUrl = UrlBuilder.GetCategoryUrl(Convert.ToInt32(item.CategoryId), categoryNameUrl1, null, string.Empty);
                    product.CurrencyCss = item.GetCurrencyCssName();
                    product.CategoryName = categoryName;
                    if (item.BrandId.HasValue)
                    {
                        var brand = categoryService.GetCategoryByCategoryId(item.BrandId.Value);
                        if (brand != null)
                            product.BrandName = brand.CategoryName;
                    }
                    if (item.Model != null) product.ModelName = item.Model.CategoryName;
                    model.HomeProductsRelatedCategoryModel.Products.Add(product);
                }
            }
            foreach (var item in subCategories)
            {
                var topCategories = categoryService.GetSPTopCategories(item.CategoryId).ToList();
                string categoryNameUrl = !string.IsNullOrEmpty(item.Category.CategoryContentTitle) ? item.Category.CategoryContentTitle : item.Category.CategoryName;

                foreach (var id in topCategories)
                {
                    var cat = categories.FirstOrDefault(x => x.CategoryId == id.CategoryId);
                    if (cat != null)
                    {
                        string catUrl = "";

                        string categoryNameUrl1 = !string.IsNullOrEmpty(cat.Category.CategoryContentTitle) ? cat.Category.CategoryContentTitle : cat.Category.CategoryName;

                        if (cat.Category.CategoryType == (byte)CategoryType.Category || cat.Category.CategoryType == (byte)CategoryType.Sector || cat.Category.CategoryType == (byte)CategoryType.ProductGroup)
                        {
                            catUrl = UrlBuilder.GetCategoryUrl(cat.CategoryId, categoryNameUrl1, null, string.Empty);
                        }
                        model.HomeProductsRelatedCategoryModel.Categories.First(x => x.CategoryId == id.CategoryId).SubCategoryModels.Add(new MTHomeCategoryModel
                        {
                            CategoryName = item.Category.CategoryName,
                            CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty)
                        });
                    }
                }
            }

        }

        private void PrepareHomeLeftCategories(MTHomeModel model)
        {
            ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
            ICategoryPlaceChoiceService categoryPlaceChoiceService = EngineContext.Current.Resolve<ICategoryPlaceChoiceService>();

            var leftCategories = categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)CategoryPlaceType.HomeLeftSide, true);
            foreach (var categoryItem in leftCategories)
            {
                string url = "";

                if (categoryItem.Category.CategoryType == (byte)CategoryType.Model)
                {
                    var brand = categoryService.GetCategoryByCategoryId(Convert.ToInt32(categoryItem.Category.CategoryParentId));
                    var categoryUst = categoryService.GetCategoryByCategoryId(Convert.ToInt32(brand.CategoryParentId));
                    url = UrlBuilder.GetModelUrl(categoryItem.CategoryId, categoryItem.Category.CategoryName, brand.CategoryName, categoryUst.CategoryName, categoryUst.CategoryId);
                }
                else
                {
                    url = UrlBuilder.GetCategoryUrl(categoryItem.CategoryId, categoryItem.Category.CategoryName, null, string.Empty);
                }
                model.HomeLeftCategoriesModel.HomeLeftChoicedCategories.Add(new MTHomeCategoryModel { CategoryName = categoryItem.Category.CategoryName, CategoryUrl = url });

            }
            var mainSectors = categoryService.GetMainCategories();
            foreach (var sectorItem in mainSectors)
            {
                string url = UrlBuilder.GetCategoryUrl(sectorItem.CategoryId, sectorItem.CategoryName, null, string.Empty);
                model.HomeLeftCategoriesModel.HomeLeftAllSectors.Add(new MTHomeCategoryModel { CategoryName = sectorItem.CategoryName, CategoryUrl = url });
            }

        }

        private void PreparePopularStoreModel(MTHomeModel model)
        {
            IStoreService _storeService = EngineContext.Current.Resolve<IStoreService>();
            IProductService _productService = EngineContext.Current.Resolve<IProductService>();
            var stores = _storeService.GetHomeStores(pageSize: 6);
            foreach (var item in stores)
            {
                //int totalRecordTemp;//Kullanilmayacak
                var homeStoreModel = new MTHomeStoreModel
                {
                    MainPartyId = item.MainPartyId,
                    StoreAbout = StringHelper.Truncate(item.StoreAbout, 50),
                    StoreName = StringHelper.Truncate(item.StoreName, 45),
                    StoreLogo = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 100),
                    StoreUrl = UrlBuilder.GetStoreProfileUrl(item.MainPartyId, item.StoreName, item.StoreUrlName)
                };
                var topProducts = _productService.GetSPProductsByStoreMainPartyId(3, 1, item.MainPartyId);
                foreach (var x in topProducts)
                {
                    homeStoreModel.TopProductPictures.Add(new StoreProductPictureModel
                    {
                        PicturePath = ImageHelper.GetProductImagePath(x.ProductId, x.MainPicture, ProductImageSize.x160x120),
                        PictureName = x.ProductName,
                        ProductUrl = UrlBuilder.GetProductUrl(x.ProductId, x.ProductName),
                    });
                }
                model.StoreModels.Add(homeStoreModel);
            }

        }

        private void PrepareSliderBannerMoldes(MTHomeModel model)
        {

            IBannerService _bannerService = EngineContext.Current.Resolve<IBannerService>();
            var banners = _bannerService.GetBannersByBannerType(13).OrderBy(x => x.BannerOrder).GroupBy(x => x.BannerOrder);
            int index = 0;
            foreach (var item in banners)
            {
                var bannersSpecial = _bannerService.GetBannersByBannerType(13).Where(x => x.BannerOrder == item.Key);
                var bannerMobile = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Mobile);
                var bannerTablet = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Tablet);
                var bannerPc = bannersSpecial.FirstOrDefault(x => x.BannerImageType == (byte)BannerImageType.Pc);

                var bannerItemModel = new MTHomeBannerModel
                {
                    Index = index,
                    Url = bannerPc.BannerLink,
                    ImageTag = bannerPc.BannerAltTag
                };
                bannerItemModel.PicturePathPc = ImageHelper.GetBannerImagePath(bannerPc.BannerResource);
                if (bannerTablet != null)
                    bannerItemModel.PicturePathTablet = ImageHelper.GetBannerImagePath(bannerTablet.BannerResource);
                if (bannerMobile != null)
                    bannerItemModel.PicturePathMobile = ImageHelper.GetBannerImagePath(bannerMobile.BannerResource);

                model.SliderBannerMoldes.Add(bannerItemModel);
                index++;
            }
        }

        private void PreparePopularVideoModels(MTHomeModel model)
        {
            IVideoService _videoService = EngineContext.Current.Resolve<IVideoService>();
            var popularVideos = _videoService.GetSPPopularVideos();
            foreach (var item in popularVideos)
            {
                model.PopularVideoModels.Add(new MTHomeVideoModel
                {
                    BrandName = item.BrandName,
                    ModelName = item.ModelName,
                    ProductName = item.ProductName,
                    VideoUrl = UrlBuilder.GetVideoUrl(item.VideoId, item.ProductName),
                    PicturePath = ImageHelper.GetVideoImagePath(item.VideoPicturePath)
                });
            }
        }
        //private void PrepareProductShowCaseModels(MTHomeModel model)
        //{
        //    IProductService _productService = EngineContext.Current.Resolve<IProductService>();
        //    ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
        //    IPictureService _pictureService = EngineContext.Current.Resolve<IPictureService>();

        //    var products = _productService.GetProductsByShowCase();

        //    foreach (var item in products)
        //    {
        //        var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
        //        model.ShowCaseProducts.Add(new MTHomeAdModel
        //        {
        //            ProductName = item.ProductName,
        //            TruncatedProductName = StringHelper.Truncate(item.ProductName, 80),
        //            ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
        //            PicturePath = ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px200x150)
        //        });
        //    }
        //}


        private void PreparePopularAdModels(MTHomeModel model)
        {
            IProductService _productService = EngineContext.Current.Resolve<IProductService>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var popularProducts = _productService.GetSPPopularProducts();
            List<int> Liste = popularProducts.Select(x => (int)x.CategoryId).ToList();
            var CategoryList = _categoryService.GetCategoriesByCategoryIds(Liste).ToList();
            foreach (var item in popularProducts)
            {
                var category = CategoryList.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                string categoryNameForUrl = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                model.PopularAdModels.Add(new MTHomeAdModel
                {
                    CategoryName = item.CategoryName,
                    TruncatedCategoryName = StringHelper.Truncate(item.CategoryName, 30),
                    ProductName = item.ProductName,
                    TruncatedProductName = StringHelper.Truncate(item.ProductName, 80),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SimilarUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameForUrl, null, string.Empty),
                    PicturePath = ImageHelper.GetProductImagePath(item.ProductId, item.ProductPicturePath, ProductImageSize.x160x120)
                });
            }
        }

        private void PrepareNewsAdModels(MTHomeModel model)
        {
            IProductService _productService = EngineContext.Current.Resolve<IProductService>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var popularProducts = _productService.GetSPNewProducts();
            List<int> Liste = popularProducts.Select(x => (int)x.CategoryId).ToList();
            var CategoryList = _categoryService.GetCategoriesByCategoryIds(Liste).ToList();
            foreach (var item in popularProducts)
            {
                var category = CategoryList.FirstOrDefault(x => x.CategoryId == item.CategoryId);
                string categoryNameForUrl = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                model.NewsAdModels.Add(new MTHomeAdModel
                {
                    CategoryName = item.CategoryName,
                    TruncatedCategoryName = StringHelper.Truncate(item.CategoryName, 30),
                    ProductName = item.ProductName,
                    TruncatedProductName = StringHelper.Truncate(item.ProductName, 80),
                    ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                    SimilarUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameForUrl, null, string.Empty),
                    PicturePath = ImageHelper.GetProductImagePath(item.ProductId, item.ProductPicturePath, ProductImageSize.x160x120),
                    CurrencyName=item.CurrencyName,
                    ProductPriceType=item.ProductPriceType,
                    ProductPrice = item.ProductPriceType == (byte)ProductPriceType.Price ? item.ProductPrice.GetFormattedPrice((byte)item.ProductPriceType) : "",
                    CurrencyCssName = item.CurrencyName.GetCurrencyCssName(),
                });
            }
        }



        private void PrepareCategoryModels(MTHomeModel model)
        {
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var mainCategories = _categoryService.GetMainCategories();
            foreach (var item in mainCategories)
            {
                MTHomeCategoryModel mainCategoryModel = new MTHomeCategoryModel();
                mainCategoryModel.CategoryName = item.CategoryName;
                string categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                mainCategoryModel.CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);
                mainCategoryModel.ProductCount = item.ProductCount.Value;

                var subCategories = _categoryService.GetCategoriesByCategoryParentId(item.CategoryId);
                foreach (var subItem in subCategories)
                {
                    string categoryNameUrl1 = !string.IsNullOrEmpty(subItem.CategoryContentTitle) ? subItem.CategoryContentTitle : subItem.CategoryName;

                    mainCategoryModel.SubCategoryModels.Add(new MTHomeCategoryModel
                    {
                        CategoryName = subItem.CategoryName,
                        ProductCount = subItem.ProductCount.Value,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryNameUrl1, null, string.Empty)
                    });
                }
                model.CategoryModels.Add(mainCategoryModel);
            }
        }

        private void PrepareHomeCategoryProductModels(List<MTAllSelectedProductModel> modelList, int skip = 0, int take = 1)
        {
            IBaseMenuService _baseMenuService = EngineContext.Current.Resolve<IBaseMenuService>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            IProductService _productService = EngineContext.Current.Resolve<IProductService>(); ;
            IPictureService _pictureService = EngineContext.Current.Resolve<IPictureService>();
            IFavoriteProductService _favoriteProductService = EngineContext.Current.Resolve<IFavoriteProductService>();
            IProductHomePageService _productHomePageService = EngineContext.Current.Resolve<IProductHomePageService>();
            byte index = 0;
            var baseMenus = _baseMenuService.GetAllBaseMenus(skip, take);
            foreach (var baseMenu in baseMenus)
            {
                MTAllSelectedProductModel model = new MTAllSelectedProductModel();
                var categories = _baseMenuService.GetBaseMenuCategoriesByBaseMenuId(baseMenu.BaseMenuId);

                List<MTHomeAdModel> listAllProduct = new List<MTHomeAdModel>();

                int mainPartyId = AuthenticationUser.Membership.MainPartyId;
                foreach (var item in categories)
                {

                    model.CategoryModel.Add(new MTHomeCategoryModel
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.Category.CategoryName
                    });

                    var productHomePages = _productHomePageService.GetProductHomePagesByCategoryId(item.CategoryId);
                    if (productHomePages.Count > 0)
                    {
                        var productIds = productHomePages.Select(x => x.ProductId).ToList();
                        var products = _productService.GetProductsByProductIds(productIds).Where(x => x.ProductActive == true);

                        foreach (var product in products)
                        {
                            bool addedFavorite = false;

                            if (mainPartyId != 0)
                            {
                                var favoriteProduct = _favoriteProductService.GetFavoriteProductByMainPartyIdWithProductId(mainPartyId, product.ProductId);
                                if (favoriteProduct != null)
                                {
                                    addedFavorite = true;
                                }
                            }
                            var productPicture = _pictureService.GetFirstPictureByProductId(product.ProductId);

                            var productItemModel = new MTHomeAdModel
                            {
                                ProductId = product.ProductId,
                                CategoryName = item.Category.CategoryName,
                                PicturePath = ImageHelper.GetProductImagePath(product.ProductId, productPicture.PicturePath, ProductImageSize.px200x150),
                                ProductName = product.ProductName,
                                ProductUrl = UrlBuilder.GetProductUrl(product.ProductId, product.ProductName),
                                TruncatedProductName = StringHelper.Truncate(product.ProductName, 50, true),
                                HasVideo = product.HasVideo,
                                IsFavoriteProduct = addedFavorite,
                                ProductPrice = product.ProductPriceType == (byte)ProductPriceType.Price ? product.GetFormattedPrice() : "",
                                CurrencyCssName = product.GetCurrencyCssName(),
                                Index = index++

                            };
                            if (product.BrandId.HasValue)
                            {
                                var brand = _categoryService.GetCategoryByCategoryId(product.BrandId.Value);
                                productItemModel.BrandName = brand != null ? brand.CategoryName : "";
                            }
                            listAllProduct.Add(productItemModel);
                            model.Products.Add(productItemModel);
                        }
                    }

                }

                if (listAllProduct.Count > 0)
                    AddRandomHomeAdModel(model, listAllProduct);

                model.Index = skip;
                model.BackgrounCss = baseMenu.BackgroundCss;
                model.TabBackgroundCss = baseMenu.TabBackgroundCss;

                modelList.Add(model);
            }
            //var categories = baseMenu.BaseMenuCategories.OrderBy(x => x.Category.BaseMenuOrder).ToList();

        }

        private void AddRandomHomeAdModel(MTAllSelectedProductModel model, List<MTHomeAdModel> products)
        {
            Random rnd = new Random();
            int randomProductCount = Convert.ToInt32(products.Count / 2) + 1;

            List<MTHomeAdModel> productsTemp = new List<MTHomeAdModel>();
            productsTemp.AddRange(products);

            int counter = products.Count - 1;

            for (int i = 0; i < randomProductCount; i++)
            {
                int random = rnd.Next(0, counter);
                if (productsTemp.ElementAt(random) != null)
                {
                    var productElement = productsTemp.ElementAt(random);
                    MTHomeAdModel productItem = new MTHomeAdModel();
                    productItem.ProductId = productElement.ProductId;
                    productItem.ProductUrl = productElement.ProductUrl;
                    productItem.ProductName = productElement.ProductName;
                    productItem.PicturePath = productElement.PicturePath;
                    productItem.CategoryName = "";
                    productItem.BrandName = productElement.BrandName;
                    productItem.HasVideo = productElement.HasVideo;
                    productItem.TruncatedProductName = productElement.TruncatedProductName;
                    productItem.IsFavoriteProduct = productElement.IsFavoriteProduct;
                    productItem.CurrencyCssName = productElement.CurrencyCssName;
                    productItem.ProductPrice = productElement.ProductPrice;
                    model.Products.Add(productItem);
                    productsTemp.RemoveAt(random);
                    counter--;
                }
            }
        }

        private void PrepareSuccessStories(MTHomeModel model)
        {
            IStoreNewService _storNewService = EngineContext.Current.Resolve<IStoreNewService>();
            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();

            var storeNews = _storNewService.GetStoreNewsTop(10, (byte)StoreNewType.SuccessStories);
            List<MTStoreNewItem> storeSuccessList = new List<MTStoreNewItem>();
            foreach (var item in storeNews)
            {
                var store = storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                string imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px100x100.ToString());
                if (string.IsNullOrEmpty(imagePath))
                {
                    imagePath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 100);
                }
                storeSuccessList.Add(new MTStoreNewItem { Date = item.RecordDate.ToString("dd MMM, yyyy", CultureInfo.InvariantCulture), ImagePath = imagePath, NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title), Title = item.Title });
            }
            //model.SuccessStories = storeSuccessList;
        }

        private void PrepareNewsModel(MTHomeModel model)
        {
            IStoreNewService _storNewService = EngineContext.Current.Resolve<IStoreNewService>();
            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();
            var storeNews = _storNewService.GetStoreNewsTop(10, (byte)StoreNewType.Normal);
            List<MTStoreNewItem> storeNewList = new List<MTStoreNewItem>();
            foreach (var item in storeNews)
            {
                var store = storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                string imagePath = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px100x100.ToString());
                if (string.IsNullOrEmpty(imagePath))
                {
                    imagePath = ImageHelper.GetStoreLogoParh(store.MainPartyId, store.StoreLogo, 100);
                }
                storeNewList.Add(new MTStoreNewItem { Date = item.RecordDate.ToString("dd MMM, yyyy", CultureInfo.InvariantCulture), ImagePath = imagePath, NewUrl = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title), Title = item.Title });
            }
            model.StoreNewItems = storeNewList;
        }

        #endregion


        private readonly ICacheManager _cacheManager;
        private readonly IConstantService _constantService;

        public HomeController(ICacheManager cacheManager, IConstantService constantService)
        {
            this._cacheManager = cacheManager;
            this._constantService = constantService;
        }

        #region Methods

        [Compress]
        public async Task<ActionResult> Index()
        {
            //SeoPageType = (byte)PageType.General;
            string key = string.Format("makinaturkiye.home-pages-test");
            var testModel = _cacheManager.Get(key, () =>
            {
                MTHomeModel model = new MTHomeModel();
                PrepareCategoryModels(model);
                if (!Request.Browser.IsMobileDevice)
                {
                    PrepareSliderBannerMoldes(model);
                }

                PreparePopularStoreModel(model);

                var constant = _constantService.GetConstantByConstantId(235);
                model.ConstantTitle = constant.ConstantTitle;
                model.ConstantProperty = constant.ContstantPropertie;

                //PrepareHomeCategoryProductModels(model.MTAllSelectedProduct,0, 1);

                //PreparePopularVideoModels(model);
                //  PrepareProductShowCaseModels(model);
                if (!Request.Browser.IsMobileDevice)
                {
                    PrepareNewsAdModels(model);
                }
                //PrepareHomeLeftCategories(model);
                var modelSelected = new List<MTAllSelectedProductModel>();
                //PrepareHomeCategoryProductModels(modelSelected, 0, 1);
                model.MTAllSelectedProduct = modelSelected;
                PrepareNewsModel(model);
                return model;
            });

            //PrepareProductRecomandation(model);

            //PrepareSuccessStories(model);
            return await Task.FromResult(View(testModel));
        }


        public void PrepareHomeSectorItems(MTHomeModel model)
        {
            var categoryService =EngineContext.Current.Resolve<ICategoryService>();
            var sectorCategories = categoryService.GetMainCategories().Where(x=>x.HomeImagePath!=null);
            foreach (var item in sectorCategories)
            {

                model.HomeSectorItems.Add(new MTHomeSectorItem
                {
                    CategoryContentTitle = item.CategoryContentTitle,
                    CategoryName = item.CategoryName,
                    CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, item.CategoryName, null, string.Empty),
                    ImagePath = ImageHelper.GetHomeSectorImagePath(item.HomeImagePath)
                });
            }
        }

        [HttpGet]
        public JsonResult ProductSeletected(int skip)
        {
            string key = string.Format("makinaturkiye.home-pages-selected-product-{0}",skip);
            ICacheManager _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            var testModel = _cacheManager.Get(key, () =>
            {
                var model = new List<MTAllSelectedProductModel>();
                ResponseModel<string> resModel = new ResponseModel<string>();
                PrepareHomeCategoryProductModels(model, skip, 1);
                if (model.Count > 0)
                {
                    resModel.Result = RenderPartialToString("~/Views/Home/_SelectedProductCategoryAjax.ascx", model);
                    resModel.IsSuccess = true;
                }
                else
                {
                    resModel.IsSuccess = false;

                }
                return resModel;
            });
            return Json(testModel, JsonRequestBehavior.AllowGet);
        }
        protected static string RenderPartialToString(string controlName, object viewData)
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

        public ActionResult _MainMenu()
        {
            List<MTHomeCategoryModel> model = new List<MTHomeCategoryModel>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var mainCategories = _categoryService.GetMainCategories();
            foreach (var item in mainCategories)
            {
                MTHomeCategoryModel mainCategoryModel = new MTHomeCategoryModel();
                mainCategoryModel.CategoryName = item.CategoryName;
                string categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                mainCategoryModel.CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);
                mainCategoryModel.ProductCount = item.ProductCount.Value;

                var subCategories = _categoryService.GetCategoriesByCategoryParentIdAsync(item.CategoryId);
                foreach (var subItem in subCategories.Result)
                {
                    string categoryNameUrl1 = !string.IsNullOrEmpty(subItem.CategoryContentTitle) ? subItem.CategoryContentTitle : subItem.CategoryName;

                    mainCategoryModel.SubCategoryModels.Add(new MTHomeCategoryModel
                    {
                        CategoryName = subItem.CategoryName,
                        ProductCount = subItem.ProductCount.Value,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryNameUrl1, null, string.Empty)
                    });
                }

                model.Add(mainCategoryModel);
            }
            return PartialView(model);
        }

        public ActionResult _HeaderBaseMenu()
        {
            IBaseMenuService _baseMenuService = EngineContext.Current.Resolve<IBaseMenuService>();

            var baseMenus = _baseMenuService.GetAllBaseMenu();

            List<MTBaseMenuModel> model = new List<MTBaseMenuModel>();

            foreach (var item in baseMenus)
            {
                MTBaseMenuModel menuModel = new MTBaseMenuModel();

                var baseMenuCategories = _baseMenuService.GetBaseMenuCategoriesByBaseMenuId(item.BaseMenuId);

                foreach (var category in baseMenuCategories)
                {
                    string urlCategoryname = category.Category.CategoryName;

                    if (!string.IsNullOrEmpty(category.Category.CategoryContentTitle))
                        urlCategoryname = category.Category.CategoryContentTitle;

                    var categoryModel = new MTHomeCategoryModel
                    {
                        CategoryName = category.Category.CategoryName,
                        CategoryId = category.Category.CategoryId,
                        CategoryIcon =ImageHelper.GetCategoryIconPath(category.Category.CategoryIcon),
                        CategoryUrl = UrlBuilder.GetCategoryUrl(category.Category.CategoryId, urlCategoryname, null, string.Empty)
                    };

                    menuModel.CategoryModels.Add(categoryModel);
                }

                menuModel.BaseMenuName = item.BaseMenuName;
                menuModel.BaseMenuId = item.BaseMenuId;

                model.Add(menuModel);
            }

            return View(model);
        }
        [AllowSameSite]
        public PartialViewResult GetSubMenu(int id)
        {
            IBaseMenuService _baseMenuService = EngineContext.Current.Resolve<IBaseMenuService>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            MTBaseSubMenuModel model = new MTBaseSubMenuModel();
            var baseMenu = _baseMenuService.GetBaseMenuByBaseMenuId(id);
            if (baseMenu == null)
            {
                var categories = _categoryService.GetCategoriesByCategoryParentId(id);
                foreach (var item in categories)
                {
                    model.CategoryModels.Add(new MTHomeCategoryModel
                    {
                        CategoryName = item.CategoryName,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName, null, null)
                    });
                }
            }
            else
            {
                var baseMenuCategories = _baseMenuService.GetBaseMenuCategoriesByBaseMenuId(baseMenu.BaseMenuId);
                foreach (var category in baseMenuCategories)
                {
                    string urlCategoryname = category.Category.CategoryName;
                    if (!string.IsNullOrEmpty(category.Category.CategoryContentTitle))
                        urlCategoryname = category.Category.CategoryContentTitle;
                    var categoryModel = new MTHomeCategoryModel
                    {
                        CategoryName = category.Category.CategoryName,
                        CategoryId = category.Category.CategoryId,
                        CategoryUrlName = category.Category.CategoryContentTitle,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(category.Category.CategoryId, urlCategoryname, null, string.Empty),


                    };
                    var subCategories = _categoryService.GetCategoriesByCategoryParentId(category.CategoryId);
                    foreach (var subItem in subCategories.OrderBy(x => x.CategoryOrder).ThenBy(x => x.CategoryName))
                    {
                        string categoryUrlName = subItem.CategoryName;
                        if (!string.IsNullOrEmpty(subItem.CategoryContentTitle))
                            categoryUrlName = subItem.CategoryContentTitle;
                        var subCategory = new MTHomeCategoryModel
                        {
                            CategoryName = subItem.CategoryName,
                            ProductCount = subItem.ProductCount.Value,
                            CategoryUrlName = subItem.CategoryContentTitle,
                            CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryUrlName, null, string.Empty)
                        };
                        var subSubCategories = _categoryService.GetCategoriesByCategoryParentId(subItem.CategoryId);
                        foreach (var subSubItem in subSubCategories)
                        {
                            categoryUrlName = subSubItem.CategoryName;
                            if (!string.IsNullOrEmpty(subSubItem.CategoryContentTitle))
                                categoryUrlName = subSubItem.CategoryContentTitle;
                            var subSubCategory = new MTHomeCategoryModel
                            {
                                CategoryName = subSubItem.CategoryName,
                                CategoryUrlName = subItem.CategoryContentTitle,
                                ProductCount = subSubItem.ProductCount.Value,
                                CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryUrlName, null, string.Empty)
                            };
                            subCategory.SubCategoryModels.Add(subSubCategory);

                        }
                        categoryModel.SubCategoryModels.Add(subCategory);
                    }

                    model.CategoryModels.Add(categoryModel);
                }

                foreach (var item in baseMenu.BaseMenuImages)
                {
                    string url = "";
                    if (!string.IsNullOrEmpty(item.Url))
                        url = item.Url;
                    model.ImageModels.Add(url, "https://s.makinaturkiye.com/" + AppSettings.BaseMenuImageFolder + item.MenuImagePath);

                }
            }

            return PartialView("_HeaderSubMenu", model);
        }

        public ActionResult _HeaderMainMenu()
        {
            List<MTHomeCategoryModel> model = new List<MTHomeCategoryModel>();
            ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var mainCategories = _categoryService.GetMainCategories();
            foreach (var item in mainCategories)
            {
                MTHomeCategoryModel mainCategoryModel = new MTHomeCategoryModel();
                mainCategoryModel.CategoryName = item.CategoryName;
                string urlCategoryname = item.CategoryName;
                if (!string.IsNullOrEmpty(item.CategoryContentTitle))
                    urlCategoryname = item.CategoryContentTitle;
                mainCategoryModel.CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, urlCategoryname, null, string.Empty);
                mainCategoryModel.ProductCount = item.ProductCount.Value;

                var subCategories = _categoryService.GetCategoriesByCategoryParentId(item.CategoryId);
                foreach (var subItem in subCategories)
                {
                    string categoryUrlName = subItem.CategoryName;
                    if (!string.IsNullOrEmpty(subItem.CategoryContentTitle))
                        categoryUrlName = subItem.CategoryContentTitle;
                    mainCategoryModel.SubCategoryModels.Add(new MTHomeCategoryModel
                    {
                        CategoryName = subItem.CategoryName,
                        ProductCount = subItem.ProductCount.Value,
                        CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryUrlName, null, string.Empty)
                    });
                }

                model.Add(mainCategoryModel);
            }
            return PartialView(model);
        }



        [HttpGet]
        public ActionResult GetProductRecomandation()
        {
            MTMayLikeProductModel modelItem = new MTMayLikeProductModel();
            if (Request.Cookies["ProductVisited"] != null)
            {
                string data = Request.Cookies["ProductVisited"].Value;

                if (data.Length > 3)
                {
                    IProductService _productService = EngineContext.Current.Resolve<IProductService>();

                    string[] splittedProductds = data.Split(',');
                    List<MTProductItemRecomandation> list = new List<MTProductItemRecomandation>();

                    foreach (var item in splittedProductds)
                    {
                        var product = _productService.GetProductByProductId(Convert.ToInt32(item));
                        list.Add(new MTProductItemRecomandation { ProductId = product.ProductId, BrandId = product.BrandId, CategoryId = product.CategoryId.Value, ModelId = product.ModelId });

                    }
                    var brandSame = list.GroupBy(x => x.BrandId).Select(x => new { total = x.Count(), brandId = x.Key });
                    var mostBrandIds = brandSame.OrderByDescending(x => x.total).Select(x => x.brandId).Skip(0).Take(5);
                    string brandIds = string.Join(",", mostBrandIds);

                    var modelSame = list.GroupBy(x => x.ModelId).Select(x => new { total = x.Count(), modelId = x.Key });
                    var mostModelIds = modelSame.OrderByDescending(x => x.total).Select(x => x.modelId).Skip(0).Take(5);
                    string modelIds = string.Join(",", mostModelIds);

                    var categorySame = list.GroupBy(x => x.CategoryId).Select(x => new { total = x.Count(), categoryId = x.Key });
                    var mostCategoryIds = categorySame.OrderByDescending(x => x.total).Select(x => x.categoryId);
                    string categoryIds = string.Join(",", mostCategoryIds);

                    var results = _productService.GetSPProductRecomandation(categoryIds, modelIds, brandIds);
                    results = results.OrderByDescending(x => x.TotalCount).Skip(0).Take(10).ToList();

                    foreach (var item in results)
                    {

                        modelItem.Products.Add(new MTMayLikeProductItem
                        {
                            BrandName = item.CategoryName,
                            SmallPicturePah = ImageHelper.GetProductImagePath(item.ProductId, item.ProductPicturePath, ProductImageSize.px200x150),
                            ProductName = item.ProductName,
                            ProductUrl = UrlBuilder.GetProductUrl(item.ProductId, item.ProductName),
                            ModelName = item.ModelName,
                            ProductNo = item.ProductNo,
                        });
                    }

                }

            }
            return PartialView("_ProductMayLike", modelItem);
        }

        #endregion


        #region errorPages

        [HttpPost]
        public ActionResult CallYouMethod(CallYouModel model)
        {

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult PageNotFound()
        {
            Response.RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + "/Error");
            return null;
        }

        public ActionResult CodingError()
        {
            Response.RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + "/Error");
            return null;
        }

        [ValidateInput(false)]
        public ActionResult Error()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true; // this line made it work
            return View();
        }

        #endregion


        #region Old Code

        //[HttpGet]
        //public PartialViewResult _HeaderMainMenuAjax()
        //{
        //    List<MTHomeCategoryModel> model = new List<MTHomeCategoryModel>();
        //    ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
        //    var mainCategories = _categoryService.GetMainCategories();
        //    foreach (var item in mainCategories)
        //    {
        //        MTHomeCategoryModel mainCategoryModel = new MTHomeCategoryModel();
        //        mainCategoryModel.CategoryName = item.CategoryName;
        //        string urlCategoryname = item.CategoryName;
        //        if (!string.IsNullOrEmpty(item.CategoryContentTitle))
        //            urlCategoryname = item.CategoryContentTitle;
        //        mainCategoryModel.CategoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, urlCategoryname, null, string.Empty);
        //        mainCategoryModel.ProductCount = item.ProductCount.Value;

        //        var subCategories = _categoryService.GetCategoriesByCategoryParentId(item.CategoryId);
        //        foreach (var subItem in subCategories)
        //        {
        //            string categoryUrlName = subItem.CategoryName;
        //            if (!string.IsNullOrEmpty(subItem.CategoryContentTitle))
        //                categoryUrlName = subItem.CategoryContentTitle;
        //            mainCategoryModel.SubCategoryModels.Add(new MTHomeCategoryModel
        //            {
        //                CategoryName = subItem.CategoryName,
        //                ProductCount = subItem.ProductCount.Value,
        //                CategoryUrl = UrlBuilder.GetCategoryUrl(subItem.CategoryId, categoryUrlName, null, string.Empty)
        //            });
        //        }

        //        model.Add(mainCategoryModel);
        //    }
        //    return PartialView(model);
        //}


        //private readonly MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();


        [HttpGet]
        public ActionResult StoreLogin()
        {
            //burda login girişi gösterilecek.
            return View();
        }

        //public ActionResult aktar(int id)
        //{
        //    var entities = new MakinaTurkiyeEntities();
        //    Member member = null;s
        //    int? memberid =
        //        entities.MemberStores.Where(c => c.StoreMainPartyId == id).FirstOrDefault().MemberMainPartyId;
        //    member = entities.Members.SingleOrDefault(c => c.MainPartyId == memberid);
        //    //AuthenticationUser.Membership = member;
        //    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, member.MainPartyId.ToString()) }, "LoginCookie");



        //    var ctx = Request.GetOwinContext();
        //    var authManager = ctx.Authentication;
        //    authManager.SignIn(identity);
        //    return RedirectToAction("Advert/Advert", "Account");
        //}





        public ActionResult MessageMemberShip()
        {
            return View();
        }


        public ActionResult Help()
        {
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Yardım", "/Yardim", Navigation.TargetType._self));
            LoadNavigation(alMenu);

            return View();
        }

        public ActionResult SiteMap()
        {
            IList<Navigation> alMenu = new List<Navigation>();
            alMenu.Add(new Navigation("Ana Sayfa", "/", Navigation.TargetType._self));
            alMenu.Add(new Navigation("Yardım", "/SiteHaritasi", Navigation.TargetType._self));
            LoadNavigation(alMenu);

            return View();
        }

        public ActionResult NoScriptPage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FindStoreForName(string searchText, byte SearchType)
        {
            if (!string.IsNullOrWhiteSpace(searchText) && searchText.Length > 1 && SearchType == 3)
            {
                var dataStore = new Data.Store();
                return
                    Json(dataStore.StoreGetBySearchText(searchText)
                        .AsCollection<StoreModel2>()
                        .OrderBy(c => c.StoreName));
            }
            return Json("");
        }


        public ActionResult TermsOfUse()
        {
            return View();
        }


        [HttpPost]
        public JsonResult IsAccess(byte page)
        {
            Thread.Sleep(2000);
            int value = Session["ProcessCount"] == null ? 0 : Convert.ToInt32(Session["ProcessCount"]);
            bool access = PacketAuthenticationModel.IsAccess((PacketPage)page, value);

            return Json(access);
        }



        #endregion


        public ActionResult TestCs()
        {

            return View();
        }
    }
}