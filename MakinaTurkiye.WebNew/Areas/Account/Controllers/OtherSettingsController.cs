using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

using MakinaTurkiye.Utilities.FileHelpers;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;

using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Catologs;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.OtherSettings;

using NeoSistem.EnterpriseEntity.Extensions.Data;
using MakinaTurkiye.Utilities.Controllers;
using MakinaTurkiye.Entities.Tables.Catalog;
using NeoSistem.MakinaTurkiye.Web.Helpers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class OtherSettingsController : BaseAccountController
    {
        private readonly IConstantService _constantService;
        private readonly IProductService _productService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly ICategoryService _categoryService;

        int TotalRecord = 0;
        byte pageDimension = 50;

        public OtherSettingsController(IConstantService constantService, IStoreService storeService,
                                       IProductService productService, IMemberStoreService memberStoreService, ICategoryService categoryService)
        {
            this._constantService = constantService;
            this._productService = productService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._categoryService = categoryService;

            this._constantService.CachingGetOrSetOperationEnabled=false;
            this._productService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;

        }
        public ActionResult Index()
        {
            var model =new OtherSettingsProductModel();
            model.MTProductItems = PrepareProductModel(1, Request.QueryString["pagetype"].ToString());
            ViewData["ProductPriceTypes"] = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProductPriceType);

            return View(model);
        }
        public SearchModel<MTProductItem> PrepareProductModel(int pageIndex, string pageType , string productName="", string productNo="", string categoryName="",string brandName="",string modelName ="", bool search=false)
        {
            var getProduct = new SearchModel<MTProductItem>
            {
                CurrentPage = pageIndex,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };
            var source = new List<MTProductItem>();
            var skip = pageIndex * pageDimension - pageDimension;
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();
            var products = new List<global::MakinaTurkiye.Entities.Tables.Catalog.Product>();
            if(search==true)
            {
                products =_productService.GetAllProductsByMainPartyIds(mainPartyIds,true).ToList();
            }
            else
            {
                products = _productService.GetAllProductsByMainPartyIds(mainPartyIds, false).ToList();
            }


            //if (pageType == "5")
            //{
            //    products = (from a in products
            //                where ((a.ProductPriceType == (byte)ProductPriceType.Price || a.ProductPriceType == null))
            //                orderby a.productrate
            //                descending
            //                select a).ToList();
            //}
            
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
            if (!string.IsNullOrEmpty(modelName))
            {
                products = products.Where(x => x.Model.CategoryName.ToLower().Contains(modelName.ToLower())).ToList();
            }
            foreach (var item in products.OrderByDescending(x => x.Sort).ThenBy(x => x.ProductName).Skip(skip).Take(pageDimension))
            {
                if(item.ModelId.HasValue)
                    item.Model =  _categoryService.GetCategoryByCategoryId(item.ModelId.Value);
                if (item.BrandId.HasValue)
                    item.Brand = _categoryService.GetCategoryByCategoryId(item.BrandId.Value);

                source.Add(new MTProductItem
                {
                    BrandName = item.Brand != null ? item.Brand.CategoryName : "",
                    BriefDetail = item.GetBriefDetailText(),
                    CategoryName = item.Category.CategoryName,
                    City = item.City.CityName,
                    Locality = item.Locality.LocalityName,
                    ModelName = item.Model != null ? item.Model.CategoryName : "",
                    ModelYear = item.ModelYear.HasValue == true ? item.ModelYear.Value.ToString() : "0",
                    ProductActive = item.ProductActive.Value,
                    ProductActiveType = item.ProductActiveType.Value,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductNo = item.ProductNo,
                    ProductStatusText = item.GetProductStatuText(),
                    ProductTypeText = item.GetProductTypeText(),
                    SalesTypeText = item.GetProductSalesTypeText(),
                    ProductPrice=item.GetFormattedPrice(),
                    ProductPriceDecimal = item.ProductPrice,
                    ProductPriceType = item.ProductPriceType != null ? item.ProductPriceType.Value : (byte)0,
                    ViewCount = item.ViewCount.Value,
                    CurrencyId = item.CurrencyId.HasValue == true ? item.CurrencyId.Value : (byte)0,
                    CurrencyCssText = item.GetCurrencyCssName(),
                    Doping = item.Doping,
                    ProductPriceWithDiscount=  item.DiscountType.HasValue && item.DiscountType.Value != 0 ? item.ProductPriceWithDiscount.Value.GetMoneyFormattedDecimalToString() : "",
                    ProductPriceWithDiscountDecimal = item.DiscountType.HasValue && item.DiscountType.Value != 0 ? item.ProductPriceWithDiscount.Value : 0,
                    Sort = item.Sort.HasValue ? item.Sort.Value : 0
                });
            }
            getProduct.Source = source;
            getProduct.PageDimension = pageDimension;
            getProduct.TotalRecord = products.Count;
            return getProduct;

        }
        [HttpGet]
        public string ExportProducts()
        {
            ICollection<ProductModel> data = new ProductModel[] { };

            var dataProduct = new Data.Product();
            data = dataProduct.GetSearchWebByProductActiveTypeNew(ref TotalRecord, 2000, 1, 7, AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();
            List<MTProductExcelItem> list = new List<MTProductExcelItem>();

            FileHelper fileHelper = new FileHelper();
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.Membership.MainPartyId);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
            string filename = UrlBuilder.ToUrl(store.StoreName) + "_urun-listesi";
            foreach (var item in data)
            {
                MTProductExcelItem product = new MTProductExcelItem();
                product.Marka = item.BrandName;
                product.Model = item.ModelName;
                product.UrunAdi = item.ProductName;
                product.UrunNo = item.ProductNo;
                product.KategoriAdi = item.CategoryName;
                list.Add(product);

            }


            fileHelper.ExportExcel<MTProductExcelItem>(list, filename);

            return "Tamamlandı";
        }
        [HttpPost]
        public JsonResult PriceUpdate1(string ProductId, string PriceNumber)
        {
            int ProductID = Convert.ToInt32(ProductId);
            decimal Price = Convert.ToDecimal(PriceNumber);
            var product = _productService.GetProductByProductId(ProductID);
            if(product.DiscountType.HasValue && product.DiscountType.Value != 0)
            {
                product.ProductPriceWithDiscount = Price;
                product.DiscountType = 2;
                product.DiscountAmount = product.ProductPrice - Price;
                _productService.UpdateProduct(product);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Price > 0)
                {
                    product.ProductPrice = Price;

                    product.ProductPriceType = (byte)ProductPriceType.Price;
                    _productService.UpdateProduct(product);
                    return Json(true, JsonRequestBehavior.AllowGet);

                }

            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AdvertPagingfor(int page, byte displayType, byte advertListType, byte ProductActiveType,string productNo, string productName, string categoryName, string brandName,string modelName)
        {

            var model = new OtherSettingsProductModel();
            model.MTProductItems = PrepareProductModel(page,"5",productName, productNo,categoryName, brandName, modelName,true);

            //var dataProduct = new Data.Product();
            //var getProduct = new SearchModel<ProductModel>
            //{
            //    CurrentPage = page,
            //    PageDimension = 100,
            //    TotalRecord = TotalRecord
            //};

            //var data = dataProduct.GetSearchWebByProductActiveType(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, (byte)advertListType, AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();

            //getProduct.Source = data;
            //getProduct.TotalRecord = TotalRecord;

            //string userControlName = string.Empty;
            //var pageType = (DisplayType)displayType;
            //switch (pageType)
            //{
            //    case DisplayType.Window:
            //        userControlName = "/Areas/Account/Views/Statistic/ProductOnebyOne.cshtml";
            //        break;
            //    case DisplayType.List:
            //        userControlName = "/Areas/Account/Views/Statistic/.cshtml";
            //        break;
            //    case DisplayType.Text:
            //        userControlName = "/Areas/Account/Views/Statistic/ProductOnebyOne.cshtml";
            //        break;
            //    default:
            //        break;
            //}

           return View(model.MTProductItems);
        }
        [HttpPost]
        public ActionResult AdvertSortList(string page, string categoryName, string productNo, string brandName, string productName)
        {
            var getProduct = PrepareProductModel(Convert.ToInt32(page), "4", productName, productNo, categoryName, brandName);
            return View(getProduct);
        }
    }
}