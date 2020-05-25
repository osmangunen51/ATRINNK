using MakinaTurkiye.Entities.Tables.Seos;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Seos;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Videos;
using MakinaTurkiye.Entities.Tables.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Utilities.FormatHelpers;
using MakinaTurkiye.Services.Common;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class CommonController : BaseController
    {
        private readonly ISeoDefinitionService _seoDefinitionService;
        private readonly IStoreNewService _storeNewService;
        private readonly ICategoryService _categoryService;
        private readonly IVideoService _videoService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IAddressService _addressService;

        public CommonController(ISeoDefinitionService seoDefinitionService,
            IStoreNewService storeNewService, ICategoryService categoryService,
            IVideoService videoService, IStoreService storeService,
            IProductService productService, IMemberStoreService memberStoreService,
            IAddressService addressService)
        {
            _seoDefinitionService = seoDefinitionService;
            _storeNewService = storeNewService;
            _categoryService = categoryService;
            _videoService = videoService;
            _storeService = storeService;
            _productService = productService;
            _memberStoreService = memberStoreService;
            _addressService = addressService;
        }

        [ChildActionOnly]
        public ActionResult MetaTag()
        {
            MetaTagModel model = new MetaTagModel();
            PrepareMetaTagModel(model);
            return PartialView(model);
        }

        private void PrepareMetaTagProperty(MetaTagModel model)
        {

        }

        private void PrepareMetaTagModel(MetaTagModel model)
        {
            var seos = _seoDefinitionService.GetAllSeos();
            string controlleName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("controller");
            string areaName = "";
            var dataTokens = this.ControllerContext.ParentActionViewContext.RouteData.DataTokens;

            if (dataTokens.ContainsKey("area"))
            {
                areaName = (string)dataTokens["area"];
                controlleName = areaName + "/" + controlleName;
            }

            controlleName = controlleName.ToLower();

            switch (controlleName)
            {
                case "category": PrepareMetaTagModelForCategory(model, seos); break;
                case "help": PrepareMetaTagModelForHelp(model, seos); break;
                case "home": PrepareMetaTagModelForHome(model, seos); break;
                case "membership": PrepareMetaTagModelForMembership(model, seos); break;
                case "product": PrepareMetaTagModelForProduct(model, seos); break;
                case "productRequest": PrepareMetaTagModelForProductRequest(model, seos); break;
                case "store": PrepareMetaTagModelForStore(model, seos); break;
                case "storenew": PrepareMetaTagModelForStoreNew(model, seos); break;
                case "storeprofilenew": PrepareMetaTagModelForStoreProfileNew(model, seos); break;
                case "videos": PrepareMetaTagModelForVideo(model, seos); break;
                case "common": PrepareMetaTagModelForCommon(model, seos); break;
                case "account/home": PrepareMetaTagModelForAccountHome(model, seos); break;
                case "account/advert": PrepareMetaTagModelForAdvert(model, seos); break;
                case "account/favorite": PrepareMetaTagModelForFavorite(model, seos); break;
                case "account/membertype": PrepareMetaTagModelForMemberType(model, seos); break;
                case "account/message": PrepareMetaTagModelForMessage(model, seos); break;
                case "account/order": PrepareMetaTagModelForOrder(model, seos); break;
                case "account/othersetings": PrepareMetaTagModelForOtherSettings(model, seos); break;
                case "account/personal": PrepareMetaTagModelForPersonel(model, seos); break;
                case "account/profile": PrepareMetaTagModelForAccountProfile(model, seos); break;
                case "account/statistic": PrepareMetaTagModelForAccountStatistic(model, seos); break;
                case "account/store": PrepareMetaTagModelForAccountStore(model, seos); break;
                case "account/storeactivity": PrepareMetaTagModelForAccountStoreActivity(model, seos); break;
                case "account/storenew": PrepareMetaTagModelForAccountStoreNew(model, seos); break;
                case "account/users": PrepareMetaTagModelForAccountUsers(model, seos); break;
                case "account/video": PrepareMetaTagModelForAccountStoreVideos(model, seos); break;
                default:
                    break;
            }
        }

        private void PrepareMetaTagModelForCategory(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.General;

            int categoryId = 0;
            int countryId = 0;
            int cityId = 0;
            int localityId = 0;

            Category category = null;
            switch (actionName)
            {
                case "SearchResults": seoIdNameEnum = SeoIdNameEnum.ProductSearchPage; break;
                case "ProductSearchOneStep": seoIdNameEnum = SeoIdNameEnum.ProductSearchPage; break;
                case "Index2":

                    if (categoryId == 0)
                        categoryId = GetModelIdRoutData();

                    if (categoryId == 0)
                        categoryId = GetBrandIdRouteData();

                    if (categoryId == 0)
                        categoryId = GetSeriesIdRoutData();

                    if (categoryId == 0)
                        categoryId = GetCategoryIdRouteData();

                    //if(categoryId==0)
                    //    categoryId = GetSelectedCategoryIdRouteData();

                    countryId = GetCountryIdQueryString();
                    cityId = GetCityIdQueryString();
                    localityId = GetLocalityIdQueryString();

                    if (categoryId <= 0)
                        seoIdNameEnum = SeoIdNameEnum.SectorAll;
                    else
                    {
                        category = _categoryService.GetCategoryByCategoryId(categoryId);
                        switch (category.CategoryType.Value)
                        {
                            case (byte)CategoryTypeEnum.Sector:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SectorLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SectorCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SectorCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.Sector;

                                break;
                            case (byte)CategoryTypeEnum.ProductGroup:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ProductGrupLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ProductGrupCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ProductGrupCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.ProductGroup;

                                break;
                            case (byte)CategoryTypeEnum.Category:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.Category;

                                break;
                            case (byte)CategoryTypeEnum.Brand:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryBrandLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryBrandCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.CategoryBrandCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.Brand;

                                break;
                            case (byte)CategoryTypeEnum.Model:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ModelLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ModelCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.ModelCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.Model;
                                break;

                            case (byte)CategoryTypeEnum.Series:

                                if (localityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SerieLocality;
                                else if (cityId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SerieCity;
                                else if (countryId > 0)
                                    seoIdNameEnum = SeoIdNameEnum.SerieCountry;
                                else
                                    seoIdNameEnum = SeoIdNameEnum.Serie;

                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default: break;
            }

            var seo = seos.FirstOrDefault(s => s.SeoId == (int)seoIdNameEnum);
            string description = seo.Description;
            string keywords =!string.IsNullOrEmpty(seo.Keywords) ? seo.Keywords : "";
            string title = seo.Title;

            if (seoIdNameEnum == SeoIdNameEnum.ProductSearchPage)
            {
                string searchText = FormatHelper.GetSearchText(GetSearchTextQueryString());

                description = description.Replace("{ArananKelime}", searchText);
                keywords = keywords.Replace("{ArananKelime}", searchText);
                title = title.Replace("{ArananKelime}", searchText);
            }
            else
            {
                bool parameterCheck = false;
                if (category != null)
                {

                    var topCategories = _categoryService.GetSPTopCategories(categoryId);
                    string categoryName = category.CategoryName;
                    string categoryContentTile = "";
                    if (!string.IsNullOrEmpty(category.CategoryContentTitle))
                        categoryContentTile = category.CategoryContentTitle;

                    if (category.CategoryType == (byte)CategoryTypeEnum.Brand || category.CategoryType == (byte)CategoryTypeEnum.Series || category.CategoryType == (byte)CategoryTypeEnum.Model)
                    {
                        var cat = topCategories.LastOrDefault(x => x.CategoryType == (byte)CategoryTypeEnum.Category);
                        int categoryIdRelated = GetCategoryIdRouteData();

                        if (categoryIdRelated != 0 && category.CategoryType == (byte)CategoryTypeEnum.Brand)
                            cat = topCategories.FirstOrDefault(x => x.CategoryId == categoryIdRelated);

                        if (cat != null)
                        {
                            categoryName = cat.CategoryName;
                            if (!string.IsNullOrEmpty(cat.CategoryContentTitle))
                                categoryContentTile = cat.CategoryContentTitle;
                        }
                        else
                        {
                            categoryName = "";
                            categoryContentTile = "";
                        }
                    }

                    description = description.Replace("{Kategori}", categoryName);

                    keywords = keywords.Replace("{Kategori}", categoryName);
                    title = title.Replace("{Kategori}", categoryName);

                    description = description.Replace("{KategoriBaslik}", categoryContentTile);
                    keywords = keywords.Replace("{KategoriBaslik}", categoryContentTile);
                    title = title.Replace("{KategoriBaslik}", categoryContentTile);




                    int i = 0;
                    string fistTopCategoryName = string.Empty;
                    string fistTopCategoryTitle = string.Empty;
                    string topCategoriesNameText = string.Empty;

                    foreach (var item in topCategories)
                    {
                        if (string.IsNullOrWhiteSpace(topCategoriesNameText))
                        {
                            topCategoriesNameText = item.CategoryName.Replace("&", "").Replace("-", "");
                        }
                        else
                        {
                            topCategoriesNameText = topCategoriesNameText + ", " + item.CategoryName.Replace("&", "").Replace("-", "");
                        }
                        i = i + 1;
                        if (i == topCategories.Count - 1)
                        {
                            fistTopCategoryName = item.CategoryName;
                            fistTopCategoryTitle = item.CategoryContentTitle;

                        }
                    }

                    if (category.CategoryType == (byte)CategoryTypeEnum.Category)
                    {
                        description = description.Replace("{IlkUstKategoriBaslik}", string.Empty);
                        description = description.Replace("{IlkUstKategoriBaslik}", string.Empty);
                        title = title.Replace("{IlkUstKategoriBaslik}", string.Empty);

                        description = description.Replace("{IlkUstKategori}", string.Empty);
                        description = description.Replace("{IlkUstKategori}", string.Empty);
                        title = title.Replace("{IlkUstKategori}", string.Empty);
                    }
                    else
                    {
                        description = description.Replace("{IlkUstKategoriBaslik}", fistTopCategoryTitle);
                        description = description.Replace("{IlkUstKategoriBaslik}", fistTopCategoryTitle);
                        title = title.Replace("{IlkUstKategoriBaslik}", fistTopCategoryTitle);

                        description = description.Replace("{IlkUstKategori}", fistTopCategoryName);
                        description = description.Replace("{IlkUstKategori}", fistTopCategoryName);
                        title = title.Replace("{IlkUstKategori}", fistTopCategoryName);
                    }

                    description = description.Replace("{UstKategori}", topCategoriesNameText);
                    keywords = keywords.Replace("{UstKategori}", topCategoriesNameText);
                    title = title.Replace("{UstKategori}", topCategoriesNameText);


                    if (seoIdNameEnum == SeoIdNameEnum.Brand || seoIdNameEnum == SeoIdNameEnum.BrandCity || seoIdNameEnum == SeoIdNameEnum.BrandCountry ||
                        seoIdNameEnum == SeoIdNameEnum.BrandLocality || seoIdNameEnum == SeoIdNameEnum.CategoryBrand || seoIdNameEnum == SeoIdNameEnum.CategoryBrandCity ||
                        seoIdNameEnum == SeoIdNameEnum.CategoryBrandCountry || seoIdNameEnum == SeoIdNameEnum.CategoryBrandLocality)
                    {
                        #region Replace


                        var brand = topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryTypeEnum.Brand);

                        description = description.Replace("{Marka}", brand.CategoryName);
                        keywords = keywords.Replace("{Marka}", brand.CategoryName);
                        title = title.Replace("{Marka}", brand.CategoryName);

                        description = description.Replace("{ModelMarka}", category.CategoryName);
                        keywords = keywords.Replace("{ModelMarka}", category.CategoryName);
                        title = title.Replace("{ModelMarka}", category.CategoryName);

                        #endregion
                    }
                    else if (seoIdNameEnum == SeoIdNameEnum.Model || seoIdNameEnum == SeoIdNameEnum.CategoryCity || seoIdNameEnum == SeoIdNameEnum.CategoryCountry ||
                          seoIdNameEnum == SeoIdNameEnum.CategoryLocality || seoIdNameEnum == SeoIdNameEnum.ModelCity || seoIdNameEnum == SeoIdNameEnum.ModelCountry || seoIdNameEnum == SeoIdNameEnum.ModelLocality)
                    {
                        #region Replace

                        var brand = topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryTypeEnum.Brand);

                        var series = topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryTypeEnum.Series);

                        description = description.Replace("{Seri}", series != null ? series.CategoryName : "");
                        keywords = keywords.Replace("{Seri}", series != null ? series.CategoryName : "");
                        title = title.Replace("{Seri}", series != null ? series.CategoryName : "");

                        if (brand != null)
                        {
                            var topCategory = _categoryService.GetCategoryByCategoryId(brand.CategoryParentId.Value);
                            string brandName = brand.CategoryName;

                            description = description.Replace("{Marka}", brandName);
                            keywords = keywords.Replace("{Marka}", brandName);
                            title = title.Replace("{Marka}", brandName);
                        }
                        else
                        {
                            description = description.Replace("{Marka}", string.Empty);
                            keywords = keywords.Replace("{Marka}", string.Empty);
                            title = title.Replace("{Marka}", string.Empty);
                        }

                        description = description.Replace("{Model}", category.CategoryName);
                        keywords = keywords.Replace("{Model}", category.CategoryName);
                        title = title.Replace("{Model}", category.CategoryName);

                        description = description.Replace("{ModelMarka}", category.CategoryName);
                        keywords = keywords.Replace("{ModelMarka}", category.CategoryName);
                        title = title.Replace("{ModelMarka}", category.CategoryName);

                        var parentCategories = _categoryService.GetCategoriesByCategoryParentId(category.CategoryId);
                        if (parentCategories.Count > 0)
                        {
                            string parentCategoryText = string.Empty;
                            foreach (var item in parentCategories)
                            {
                                if (string.IsNullOrWhiteSpace(parentCategoryText))
                                {
                                    parentCategoryText = item.CategoryName;
                                }
                                else
                                    parentCategoryText = parentCategoryText + ", " + item.CategoryName;
                            }

                            description = description.Replace("{AltKategoriForAktifKategori}", parentCategoryText);
                            keywords = keywords.Replace("{AltKategoriForAktifKategori}", parentCategoryText);
                            title = title.Replace("{AltKategoriForAktifKategori}", parentCategoryText);
                        }

                        #endregion

                    }
                    else if (seoIdNameEnum == SeoIdNameEnum.Serie || seoIdNameEnum == SeoIdNameEnum.SerieCity || seoIdNameEnum == SeoIdNameEnum.SerieCountry ||
                            seoIdNameEnum == SeoIdNameEnum.SerieLocality)
                    {
                        #region Replace

                        var brand = topCategories.FirstOrDefault(c => c.CategoryType == (byte)CategoryTypeEnum.Brand);

                        description = description.Replace("{Marka}", brand.CategoryName);
                        keywords = keywords.Replace("{Marka}", brand.CategoryName);
                        title = title.Replace("{Marka}", brand.CategoryName);

                        description = description.Replace("{Seri}", category.CategoryName);
                        keywords = keywords.Replace("{Seri}", category.CategoryName);
                        title = title.Replace("{Seri}", category.CategoryName);

                        #endregion
                    }
                    else if (seoIdNameEnum == SeoIdNameEnum.ProductGroup || seoIdNameEnum == SeoIdNameEnum.ProductGrupCity || seoIdNameEnum == SeoIdNameEnum.ProductGrupCity ||
                            seoIdNameEnum == SeoIdNameEnum.ProductGrupCountry || seoIdNameEnum == SeoIdNameEnum.ProductGrupLocality)
                    {

                    }
                    else if (seoIdNameEnum == SeoIdNameEnum.Sector || seoIdNameEnum == SeoIdNameEnum.SectorCity || seoIdNameEnum == SeoIdNameEnum.SectorCountry ||
                            seoIdNameEnum == SeoIdNameEnum.SectorLocality)
                    {

                    }

                    if (localityId > 0)
                    {
                        var locality = _addressService.GetLocalityByLocalityId(localityId);
                        if (locality != null)
                        {
                            description = description.Replace("{Ilce}", locality.LocalityName);
                            keywords = keywords.Replace("{Ilce}", locality.LocalityName);
                            title = title.Replace("{Ilce}", locality.LocalityName);
                        }
                        parameterCheck = true;
                    }

                    if (cityId > 0)
                    {
                        var city = _addressService.GetCityByCityId(cityId);
                        if (city != null)
                        {
                            description = description.Replace("{Sehir}", city.CityName);
                            keywords = keywords.Replace("{Sehir}", city.CityName);
                            title = title.Replace("{Sehir}", city.CityName);
                        }
                        parameterCheck = true;
                    }

                    if (countryId > 0)
                    {
                        var country = _addressService.GetCountryByCountryId(countryId);
                        if (country != null)
                        {
                            description = description.Replace("{Ulke}", country.CountryName);
                            keywords = keywords.Replace("{Ulke}", country.CountryName);
                            title = title.Replace("{Ulke}", country.CountryName);
                        }
                        parameterCheck = true;
                    }
                }

                if (parameterCheck == false && category != null)
                {
                    description = !string.IsNullOrEmpty(category.Description) ? category.Description : description;
                    keywords = !string.IsNullOrEmpty(category.Keywords) ? category.Keywords : keywords;
                    title = !string.IsNullOrEmpty(category.Title) ? category.Title : title;
                }
                if (GetPageQueryString() > 1)
                {
                    description+= " - "+ GetPageQueryString();
                    keywords +=  " - " + GetPageQueryString();
                    title += " - " + GetPageQueryString();
                }

                model.Description = description;
                model.Keywords = keywords;
                model.Robots = seo.Robots;
                model.Title = title;
            }
        }

        private void PrepareMetaTagModelForHelp(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = actionName == "Index" ? SeoIdNameEnum.Help : SeoIdNameEnum.HelpCategory;
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            int helpCategoryId = GetCategoryIdRouteData();
            if (helpCategoryId != 0)
            {
                var helpCategory = _categoryService.GetCategoryByCategoryId(helpCategoryId);
                seo.Title = seo.Title.Replace("{Kategori}", helpCategory.CategoryName);
                seo.Keywords = seo.Keywords.Replace("{Kategori}", helpCategory.CategoryName);
                seo.Description = seo.Description.Replace("{Kategori}", helpCategory.CategoryName);

            }

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForHome(MetaTagModel model, IList<Seo> seos)
        {
            var seo = seos.First(s => s.SeoId == (int)SeoIdNameEnum.General);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForCommon(MetaTagModel model, IList<Seo> seos)
        {
            var seo = seos.First(s => s.SeoId == (int)SeoIdNameEnum.Error);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForMembership(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = actionName == "LogOn" ? SeoIdNameEnum.Uyelik : SeoIdNameEnum.General;
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForProduct(MetaTagModel model, IList<Seo> seos)
        {
            var seo = seos.First(s => s.SeoId == (int)SeoIdNameEnum.Product);

            int productId = GetProductIdRoutData();

            var product = _productService.GetProductByProductId(productId);
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(product.MainPartyId.Value);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            if (store != null && !string.IsNullOrWhiteSpace(store.StoreName))
            {
                seo.Description = seo.Description.Replace("{FirmaAdi}", store.StoreName);
                seo.Keywords = seo.Keywords.Replace("{FirmaAdi}", store.StoreName);
                seo.Title = seo.Title.Replace("{FirmaAdi}", store.StoreName);
            }

            seo.Description = seo.Description.Replace("{UrunAdi}", product.ProductName);
            seo.Keywords = seo.Keywords.Replace("{UrunAdi}", product.ProductName);
            seo.Title = seo.Title.Replace("{UrunAdi}", product.ProductName);

            seo.Description = seo.Description.Replace("{Kategori}", product.Category.CategoryName);
            seo.Keywords = seo.Keywords.Replace("{Kategori}", product.Category.CategoryName);
            seo.Title = seo.Title.Replace("{Kategori}", product.Category.CategoryName);

            string brandName = "";
            if (product.Brand != null)
                brandName = product.Brand.CategoryName;
            seo.Description = seo.Description.Replace("{Marka}", brandName);
            seo.Keywords = seo.Keywords.Replace("{Marka}", brandName);
            seo.Title = seo.Title.Replace("{Marka}", brandName);

            if (product.Model != null)
            {
                seo.Description = seo.Description.Replace("{Model}", product.Model.CategoryName);
                seo.Keywords = seo.Keywords.Replace("{Model}", product.Model.CategoryName);
                seo.Title = seo.Title.Replace("{Model}", product.Model.CategoryName);
            }

            if (product.ModelYear.HasValue && product.ModelYear.Value > 0)
            {
                seo.Description = seo.Description.Replace("{ModelYili}", product.ModelYear.ToString());
                seo.Keywords = seo.Keywords.Replace("{ModelYili}", product.ModelYear.ToString());
                seo.Title = seo.Title.Replace("{ModelYili}", product.ModelYear.ToString());
            }

            int lastCategoryId = product.GetLastCategoryId();
            var topCategories = _categoryService.GetSPTopCategories(lastCategoryId);
            string unifiedCategories = topCategories.GetUnifiedCategories();


            seo.Description = seo.Description.Replace("{UstKategori}", unifiedCategories);
            seo.Keywords = seo.Keywords.Replace("{UstKategori}", unifiedCategories);
            seo.Title = seo.Title.Replace("{UstKategori}", unifiedCategories);

            seo.Description = seo.Description.Replace("{UrunTipi}", product.GetProductTypeText());
            seo.Keywords = seo.Keywords.Replace("{UrunTipi}", product.GetProductTypeText());
            seo.Title = seo.Title.Replace("{UrunTipi}", product.GetProductTypeText());

            seo.Description = seo.Description.Replace("{UrunDurumu}", product.GetProductStatuText());
            seo.Keywords = seo.Keywords.Replace("{UrunDurumu}", product.GetProductStatuText());
            seo.Title = seo.Title.Replace("{UrunDurumu}", product.GetProductStatuText());

            seo.Description = seo.Description.Replace("{SatisDetayi}", product.GetProductSalesTypeText());
            seo.Keywords = seo.Keywords.Replace("{SatisDetayi}", product.GetProductSalesTypeText());
            seo.Title = seo.Title.Replace("{SatisDetayi}", product.GetProductSalesTypeText());

            seo.Description = seo.Description.Replace("{KisaDetay}", product.GetBriefDetailText());
            seo.Keywords = seo.Keywords.Replace("{KisaDetay}", product.GetBriefDetailText());
            seo.Title = seo.Title.Replace("{KisaDetay}", product.GetBriefDetailText());

            seo.Description = seo.Description.Replace("{Fiyati}", product.GetFormattedPrice());
            seo.Keywords = seo.Keywords.Replace("{Fiyati}", product.GetFormattedPrice());
            seo.Title = seo.Title.Replace("{Fiyati}", product.GetFormattedPrice());

            if (product.City != null && !string.IsNullOrEmpty(product.City.CityName))
            {
                seo.Description = seo.Description.Replace("{Sehir}", product.City.CityName);
                seo.Keywords = seo.Keywords.Replace("{Sehir}", product.City.CityName);
                seo.Title = seo.Title.Replace("{Sehir}", product.City.CityName);
            }

            if (product.Locality != null && !string.IsNullOrEmpty(product.Locality.LocalityName))
            {
                seo.Description = seo.Description.Replace("{Ilce}", product.Locality.LocalityName);
                seo.Keywords = seo.Keywords.Replace("{Ilce}", product.Locality.LocalityName);
                seo.Title = seo.Title.Replace("{Ilce}", product.Locality.LocalityName);
            }

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForProductRequest(MetaTagModel model, IList<Seo> seos)
        {
            var seo = seos.First(s => s.SeoId == (int)SeoIdNameEnum.ProductRequest);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForStore(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.General;
            int categoryId = GetCategoryIdRouteData();
            seoIdNameEnum = categoryId == 0 ? SeoIdNameEnum.StoreCategoryHome : SeoIdNameEnum.StoreCategory;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);

            string storePageTitle = string.Empty;
            if (categoryId > 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryId);

                if (!string.IsNullOrEmpty(category.StorePageTitle))
                    storePageTitle = category.StorePageTitle;
                else if (!string.IsNullOrEmpty(category.CategoryContentTitle))
                    storePageTitle = category.CategoryContentTitle;
                else
                    storePageTitle = category.CategoryName;
            }

            model.Description = seo.Description.Replace("{Kategori}", storePageTitle);
            if (seo.Keywords != null)
                model.Keywords = seo.Keywords.Replace("{Kategori}", storePageTitle);
            model.Title = seo.Title.Replace("{Kategori}", storePageTitle);

        }

        private void PrepareMetaTagModelForStoreNew(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.General;
            StoreNew storeNew = null;

            if (actionName == "Index")
            {
                byte newType = GetNewTypeRouteData();
                seoIdNameEnum = newType == (byte)StoreNewTypeEnum.Normal ? SeoIdNameEnum.StoreNewHome : SeoIdNameEnum.SuccessStories;

            }
            else if (actionName == "Detail")
            {
                int newId = GetNewIdRouteData();
                storeNew = _storeNewService.GetStoreNewByStoreNewId(newId);
                seoIdNameEnum = storeNew.NewType == (byte)StoreNewTypeEnum.Normal ? SeoIdNameEnum.StoreNewDetail : SeoIdNameEnum.SuccesstoriesDetail;

            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);

            if (storeNew != null)
            {
                seo.Description = seo.Description.Replace("{HaberAdi}", storeNew.Title);
                if (seo.Keywords != null)
                    seo.Keywords = seo.Keywords.Replace("{HaberAdi}", storeNew.Title);
                seo.Title = seo.Title.Replace("{HaberAdi}", storeNew.Title);

                var store = _storeService.GetStoreByMainPartyId(storeNew.StoreMainPartyId);

                seo.Description = seo.Description.Replace("{FirmaAdi}", store.StoreName);
                if (seo.Keywords != null)
                    seo.Keywords = seo.Keywords.Replace("{FirmaAdi}", store.StoreName);
                seo.Title = seo.Title.Replace("{FirmaAdi}", store.StoreName);

            }

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForStoreProfileNew(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.General;
            int categoryId = 0;
            switch (actionName)
            {
                case "News": seoIdNameEnum = SeoIdNameEnum.StoreNews; break;
                case "CatologNew": seoIdNameEnum = SeoIdNameEnum.StoreCatolog; break;
                case "ConnectionNew": seoIdNameEnum = SeoIdNameEnum.StoreConnectionPage; break;
                case "DealerShipNew": seoIdNameEnum = SeoIdNameEnum.StoreDealerNetwork; break;
                case "AuthorizedServicesNew": seoIdNameEnum = SeoIdNameEnum.StoreServiceNetwork; break;
                case "StoreImagesNew": seoIdNameEnum = SeoIdNameEnum.StoreImages; break;
                case "VideosNew": seoIdNameEnum = SeoIdNameEnum.StoreVideoPage; break;
                case "BrandNew": seoIdNameEnum = SeoIdNameEnum.StoreBrandPage; break;
                case "BranchNew": seoIdNameEnum = SeoIdNameEnum.StoreDepartmentPage; break;
                case "AboutUsNew": seoIdNameEnum = SeoIdNameEnum.StoreAboutPage; break;
                case "DealerNew": seoIdNameEnum = SeoIdNameEnum.StoreDealerPage; break;
                case "CompanyProfileNew": seoIdNameEnum = SeoIdNameEnum.Store; break;
                case "StorePromotionVideo": seoIdNameEnum = SeoIdNameEnum.StorePromotionVideo; break;
                case "ProductsNew":
                    seoIdNameEnum = SeoIdNameEnum.Store;
                    categoryId = GetCategoryIdRouteData();
                    seoIdNameEnum = categoryId == 0 ? SeoIdNameEnum.StoreProductPage : SeoIdNameEnum.StoreProductCategoryPage;
                    break;
                default: break;
            }
            string storeUsername = GetStoreUsernameRoutData();
            var store = _storeService.GetStoreByStoreUrlName(storeUsername);
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            var adress = _addressService.GetAddressesByMainPartyId(store.MainPartyId);
            if (string.IsNullOrEmpty(seo.Keywords))
                seo.Keywords = "";
            if (string.IsNullOrEmpty(seo.Description))
                seo.Description = "";
            if (adress.Count > 0)
            {
                if (adress.FirstOrDefault().City != null)
                {
                    string cityName = adress.FirstOrDefault().City.CityName;
                    seo.Description = seo.Description.Replace("{Sehir}", cityName);
                    if (!string.IsNullOrEmpty(seo.Keywords))
                        seo.Keywords = seo.Keywords.Replace("{Sehir}", cityName);
                    seo.Title = seo.Title.Replace("{Sehir}", cityName);
                }
            }
            seo.Description = seo.Description.Replace("{FirmaAdi}", store.StoreName);
            if (!string.IsNullOrEmpty(seo.Keywords))
                seo.Keywords = seo.Keywords.Replace("{FirmaAdi}", store.StoreName);
            seo.Title = seo.Title.Replace("{FirmaAdi}", store.StoreName);

            if (!string.IsNullOrEmpty(store.StoreShortName))
            {
                seo.Description = seo.Description.Replace("{FirmaKısaAdi}", store.StoreShortName);
                seo.Keywords = seo.Keywords.Replace("{FirmaKısaAdi}", store.StoreShortName);
                seo.Title = seo.Title.Replace("{FirmaKısaAdi}", store.StoreShortName);
            }
            seo.Description = seo.Description.Replace("{FirmSeoTitle}", store.SeoTitle);

            seo.Keywords = seo.Keywords.Replace("{FirmSeoTitle}", store.SeoTitle);
            seo.Title = seo.Title.Replace("{FirmSeoTitle}", store.SeoTitle);

            seo.Description = seo.Description.Replace("{FirmSeoDecsription}", store.SeoDescription);

            seo.Keywords = seo.Keywords.Replace("{FirmSeoDecsription}", store.SeoDescription);
            seo.Title = seo.Title.Replace("{FirmSeoDecsription}", store.SeoDescription);

            seo.Description = seo.Description.Replace("{FirmSeoKeyword}", store.SeoKeyword);
            if (!string.IsNullOrEmpty(seo.Keywords))
                seo.Keywords = seo.Keywords.Replace("{FirmSeoKeyword}", store.SeoKeyword);
            seo.Title = seo.Title.Replace("{FirmSeoKeyword}", store.SeoKeyword);

            if (categoryId > 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryId);

                string categoryNameSeoText = category.CategoryName;
                if (category.CategoryType == (byte)CategoryTypeEnum.Model || category.CategoryType == (byte)CategoryTypeEnum.Brand)
                {
                    var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);

                    categoryNameSeoText = categoryParent.CategoryName + " " + categoryNameSeoText;
                }


                seo.Description = seo.Description.Replace("{Kategori}", categoryNameSeoText);
                if (!string.IsNullOrEmpty(seo.Keywords))
                    seo.Keywords = seo.Keywords.Replace("{Kategori}", categoryNameSeoText);
                seo.Title = seo.Title.Replace("{Kategori}", categoryNameSeoText);

                string categoryTitle = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
                seo.Description = seo.Description.Replace("{KategoriBaslik}", categoryTitle);
                if (!string.IsNullOrEmpty(seo.Keywords))
                    seo.Keywords = seo.Keywords.Replace("{KategoriBaslik}", categoryTitle);
                seo.Title = seo.Title.Replace("{KategoriBaslik}", categoryTitle);
            }
            if (actionName == "CompanyProfileNew")
            {
                if (!string.IsNullOrEmpty(store.SeoDescription))
                    seo.Description = store.SeoDescription;
                if (!string.IsNullOrEmpty(store.SeoKeyword))
                    seo.Keywords = store.SeoKeyword;
                if (!string.IsNullOrEmpty(store.SeoTitle))
                    seo.Title = store.SeoTitle;
            }
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForVideo(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.General;

            int categoryId = 0;
            int videoId = 0;
            string searchText = string.Empty;
            string categoryName = string.Empty;
            string categoryTitle = string.Empty;
            string parentCategoryNamesText = string.Empty;

            switch (actionName)
            {
                case "VideoSearch":
                    seoIdNameEnum = SeoIdNameEnum.VideoSearchPage;
                    searchText = GetSearchTextQueryString();
                    break;
                case "VideoItems2":
                    videoId = GetVideoIdRoutData();
                    seoIdNameEnum = SeoIdNameEnum.VideoViewPage;
                    break;
                case "Index":
                    categoryId = GetCategoryIdRouteData();
                    seoIdNameEnum = categoryId == 0 ? SeoIdNameEnum.VideoMainPage : SeoIdNameEnum.VideoCategoryPage;
                    break;
                default: break;
            }

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);

            seo.Description = seo.Description.Replace("{ArananKelime}", searchText);
            seo.Keywords = seo.Keywords.Replace("{ArananKelime}", searchText);
            seo.Title = seo.Title.Replace("{ArananKelime}", searchText);

            if (categoryId > 0)
            {
                var category = _categoryService.GetCategoryByCategoryId(categoryId);
                var parentCategories = _categoryService.GetCategoriesByCategoryParentId(categoryId);

                categoryName = category.CategoryName;

                foreach (var item in parentCategories)
                {
                    parentCategoryNamesText = string.IsNullOrWhiteSpace(parentCategoryNamesText) ?
                                              item.CategoryName : parentCategoryNamesText + ", " + item.CategoryName;
                }

                categoryTitle = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;

                seo.Description = seo.Description.Replace("{Kategori}", categoryName);
                seo.Keywords = seo.Keywords.Replace("{Kategori}", categoryName);
                seo.Title = seo.Title.Replace("{Kategori}", categoryName);


                seo.Description = seo.Description.Replace("{AltKategoriForAktifKategori}", parentCategoryNamesText);
                seo.Keywords = seo.Keywords.Replace("{AltKategoriForAktifKategori}", parentCategoryNamesText);
                seo.Title = seo.Title.Replace("{AltKategoriForAktifKategori}", parentCategoryNamesText);

                seo.Description = seo.Description.Replace("{KategoriBaslik}", categoryTitle);
                seo.Keywords = seo.Keywords.Replace("{KategoriBaslik}", categoryTitle);
                seo.Title = seo.Title.Replace("{KategoriBaslik}", categoryTitle);

            }

            if (videoId > 0)
            {
                var video = _videoService.GetVideoByVideoId(videoId);
                var product = _videoService.GetSPStoreAndProductDetailByProductId(video.ProductId.Value);

                if (!string.IsNullOrEmpty(video.VideoTitle))
                {

                    seo.Description = seo.Description.Replace("{UrunAdi}", video.VideoTitle);
                    seo.Keywords = seo.Keywords.Replace("{UrunAdi}", video.VideoTitle);
                    seo.Title = seo.Title.Replace("{UrunAdi}", video.VideoTitle);
                }
                else
                {
                    seo.Description = seo.Description.Replace("{UrunAdi}", product.ProductName);
                    seo.Keywords = seo.Keywords.Replace("{UrunAdi}", product.ProductName);
                    seo.Title = seo.Title.Replace("{UrunAdi}", product.ProductName);
                }

                seo.Description = seo.Description.Replace("{Kategori}", product.CategoryName);
                seo.Keywords = seo.Keywords.Replace("{Kategori}", product.CategoryName);
                seo.Title = seo.Title.Replace("{Kategori}", product.CategoryName);

                seo.Description = seo.Description.Replace("{Marka}", product.BrandName);
                seo.Keywords = seo.Keywords.Replace("{Marka}", product.BrandName);
                seo.Title = seo.Title.Replace("{Marka}", product.BrandName);

                seo.Description = seo.Description.Replace("{Model}", product.ModelName);
                seo.Keywords = seo.Keywords.Replace("{Model}", product.ModelName);
                seo.Title = seo.Title.Replace("{Model}", product.ModelName);

                seo.Description = seo.Description.Replace("{ModelYili}", product.ModelYear.ToString());
                seo.Keywords = seo.Keywords.Replace("{ModelYili}", product.ModelYear.ToString());
                seo.Title = seo.Title.Replace("{ModelYili}", product.ModelYear.ToString());

                seo.Description = seo.Description.Replace("{FirmaAdi}", product.StoreName);
                seo.Keywords = seo.Keywords.Replace("{FirmaAdi}", product.StoreName);
                seo.Title = seo.Title.Replace("{FirmaAdi}", product.StoreName);

                seo.Description = seo.Description.Replace("{UrunTipi}", product.ProductTypeText);
                seo.Keywords = seo.Keywords.Replace("{UrunTipi}", product.ProductTypeText);
                seo.Title = seo.Title.Replace("{UrunTipi}", product.ProductTypeText);

                seo.Description = seo.Description.Replace("{UrunDurumu}", product.ProductStatuText);
                seo.Keywords = seo.Keywords.Replace("{UrunDurumu}", product.ProductStatuText);
                seo.Title = seo.Title.Replace("{UrunDurumu}", product.ProductStatuText);

                seo.Description = seo.Description.Replace("{SatisDetayi}", product.BriefDetailText);
                seo.Keywords = seo.Keywords.Replace("{SatisDetayi}", product.BriefDetailText);
                seo.Title = seo.Title.Replace("{SatisDetayi}", product.BriefDetailText);

                seo.Description = seo.Description.Replace("{Fiyati}", video.Product.GetFormattedPriceWithCurrency());
                seo.Keywords = seo.Keywords.Replace("{Fiyati}", video.Product.GetFormattedPriceWithCurrency());
                seo.Title = seo.Title.Replace("{Fiyati}", video.Product.GetFormattedPriceWithCurrency());

                if (product.StoreCityName != "" && product.StoreCityName != null)
                {
                    seo.Description = seo.Description.Replace("{Sehir}", product.StoreCityName);
                    seo.Keywords = seo.Keywords.Replace("{Sehir}", product.StoreCityName);
                    seo.Title = seo.Title.Replace("{Sehir}", product.StoreCityName);
                }
                else if (product.MemberCityName != "" && product.MemberCityName != null)
                {
                    seo.Description = seo.Description.Replace("{Sehir}", product.MemberCityName);
                    seo.Keywords = seo.Keywords.Replace("{Sehir}", product.MemberCityName);
                    seo.Title = seo.Title.Replace("{Sehir}", product.MemberCityName);
                }
                if (product.StoreLocalityName != "" && product.StoreLocalityName != null)
                {
                    seo.Description = seo.Description.Replace("{Ilce}", product.StoreLocalityName);
                    seo.Keywords = seo.Keywords.Replace("{Ilce}", product.StoreLocalityName);
                    seo.Title = seo.Title.Replace("{Ilce}", product.StoreLocalityName);

                }
                else if (product.MemberLocalityName != "" && product.MemberLocalityName != null)
                {
                    seo.Description = seo.Description.Replace("{Ilce}", product.MemberLocalityName);
                    seo.Keywords = seo.Keywords.Replace("{Ilce}", product.MemberLocalityName);
                    seo.Title = seo.Title.Replace("{Ilce}", product.MemberLocalityName);
                }

            }

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAdvert(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.AdvertManagment;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);

            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForFavorite(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;

            switch (actionName)
            {
                case "Product":
                    seoIdNameEnum = SeoIdNameEnum.FavoriteProduct;
                    break;
                case "Store":
                    seoIdNameEnum = SeoIdNameEnum.FavoriteStore;
                    break;
                default:
                    break;
            }


            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }
        private void PrepareMetaTagModelForAccountHome(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.AccountHome;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }
        private void PrepareMetaTagModelForMemberType(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.MemberType;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForMessage(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;
            switch (actionName)
            {
                case "Detail":
                    seoIdNameEnum = SeoIdNameEnum.MessageDetail;
                    break;
                case "Index":
                    seoIdNameEnum = SeoIdNameEnum.MessageIndex;
                    break;
                default:
                    seoIdNameEnum = SeoIdNameEnum.MessageIndex;
                    break;
            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }
        private void PrepareMetaTagModelForOrder(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.Order;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForOtherSettings(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.OtherSettings;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }
        private void PrepareMetaTagModelForPersonel(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;
            actionName = actionName.ToLower();
            switch (actionName)
            {
                case "changeaddress":
                    seoIdNameEnum = SeoIdNameEnum.ChangeAddress;
                    break;
                case "changeemail":
                    seoIdNameEnum = SeoIdNameEnum.ChangeEmail;
                    break;
                case "changepassword":
                    seoIdNameEnum = SeoIdNameEnum.ChangePassword;
                    break;
                case "index":
                    seoIdNameEnum = SeoIdNameEnum.Profile;
                    break;
                case "phoneactive":
                    seoIdNameEnum = SeoIdNameEnum.PhoneActive;
                    break;
                case "taxupdate":
                    seoIdNameEnum = SeoIdNameEnum.TaxUpdate;
                    break;
                case "update":
                    seoIdNameEnum = SeoIdNameEnum.PersonelUpdate;
                    break;
                default:
                    seoIdNameEnum = SeoIdNameEnum.Profile;
                    break;
            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountProfile(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;
            actionName = actionName.ToLower();
            switch (actionName)
            {
                case "aboutus":
                    seoIdNameEnum = SeoIdNameEnum.ProfileAboutUs;
                    break;
                default:
                    seoIdNameEnum = SeoIdNameEnum.ProfileIndex;
                    break;
            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }


        private void PrepareMetaTagModelForAccountStatistic(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;
            actionName = actionName.ToLower();
            switch (actionName)
            {
                case "productstatistics":
                    seoIdNameEnum = SeoIdNameEnum.ProductStatistic;
                    break;
                case "statisticstore":
                    seoIdNameEnum = SeoIdNameEnum.StoreStatistics;
                    break;
                default:
                    seoIdNameEnum = SeoIdNameEnum.StatisticIndex;
                    break;
            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountStore(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = 0;
            actionName = actionName.ToLower();
            switch (actionName)
            {
                case "certificate":
                    seoIdNameEnum = SeoIdNameEnum.Certificate;
                    break;
                case "contactsettings":
                    seoIdNameEnum = SeoIdNameEnum.ContactSettings;
                    break;
                case "createcatolog":
                    seoIdNameEnum = SeoIdNameEnum.CreateCatolog;
                    break;
                case "createcertificate":
                    seoIdNameEnum = SeoIdNameEnum.CreateCertificate;
                    break;

                case "createstoresliderımage":
                    seoIdNameEnum = SeoIdNameEnum.CreateStoreSliderImage;
                    break;
                case "mycatologlist":
                    seoIdNameEnum = SeoIdNameEnum.MyCatolog;
                    break;
                case "updateactivity":
                    seoIdNameEnum = SeoIdNameEnum.UpdateActivity;
                    break;
                case "updatebanner":
                    seoIdNameEnum = SeoIdNameEnum.UpdateBanner;
                    break;
                case "updatelogo":
                    seoIdNameEnum = SeoIdNameEnum.UpdateLogo;
                    break;
                case "updatestore":
                    seoIdNameEnum = SeoIdNameEnum.UpdateStore;
                    break;
                default:
                    seoIdNameEnum = SeoIdNameEnum.UpdateStore;
                    break;
            }
            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountStoreActivity(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.StoreActivity;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountStoreNew(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.StoreNewManagment;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountUsers(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.StoreUsersManagment;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        private void PrepareMetaTagModelForAccountStoreVideos(MetaTagModel model, IList<Seo> seos)
        {
            string actionName = this.ControllerContext.ParentActionViewContext.RouteData.GetRequiredString("action");
            SeoIdNameEnum seoIdNameEnum = SeoIdNameEnum.StoreVideosManagment;

            var seo = seos.First(s => s.SeoId == (int)seoIdNameEnum);
            model.Description = seo.Description;
            model.Keywords = seo.Keywords;
            model.Robots = seo.Robots;
            model.Title = seo.Title;
        }

        [ValidateInput(false)]
        public ActionResult HataSayfasi()
        {
            Response.Status = "404 Page Not Found";
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}