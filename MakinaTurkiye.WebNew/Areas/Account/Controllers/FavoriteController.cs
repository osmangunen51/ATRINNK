using MakinaTurkiye.Services.Catalog;

using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.FavoriteProducts;
using NeoSistem.MakinaTurkiye.Web.Factories;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;

using NeoSistem.EnterpriseEntity.Extensions.Data;

using System.Collections.Generic;
using System.Web.Mvc;
using System;
using MakinaTurkiye.Utilities.Controllers;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class FavoriteController : BaseAccountController
    {
        int TotalRecord = 0;
        byte pageDimension = 5;
        IProductService _productService;
        IProductModelFactory _productModelFactory;


        public FavoriteController(IProductService productService,
            IProductModelFactory productModelFactory)
        {
            this._productService = productService;
            this._productModelFactory = productModelFactory;

            this._productService.CachingGetOrSetOperationEnabled = false;
        }

        public ActionResult Index()
        {
            AccountFavoritePageType pageType = (AccountFavoritePageType)Convert.ToByte(Request.QueryString["PageType"]);
            switch (pageType)
            {
                case AccountFavoritePageType.Product:
                    return RedirectToAction("Product", "Favorite");
                case AccountFavoritePageType.Store:
                    return RedirectToAction("Store", "Favorite");
                default:
                    break;
            }
            return View();
        }

        public ActionResult Product()
        {
            var model = new FavoriteProductViewModel();

            //var dataFavoriteProduct = new Data.FavoriteProduct();
            //var getProduct = new SearchModel<ProductModel>
            //{
            //    CurrentPage = 1,
            //    PageDimension = pageDimension,
            //    TotalRecord = TotalRecord
            //};

            int totalRecord = 0;
            var favoriteProducts = _productService.GetSPFavoriteProductsByMainPartyId(AuthenticationUser.Membership.MainPartyId, 1, 4, out totalRecord);
            List<MTCategoryProductModel> source = new List<MTCategoryProductModel>();
            foreach (var item in favoriteProducts)
            {
                source.Add(_productModelFactory.PrepareProductAreaItemModel(item));
            }
            model.MTCategoryProductModels.CurrentPage = 1;
            model.MTCategoryProductModels.TotalRecord = totalRecord;
            model.MTCategoryProductModels.PageDimension = 20;
            model.MTCategoryProductModels.Source = source;
            model.LeftMenuModel = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyFavorites, (byte)LeftMenuConstants.MyFavorite.MyFavoriteAd);
            return View(model);
        }

        [HttpGet]
        public JsonResult GetFavoriteProducts(int page, int pageDimension)
        {
            ResponseModel<string> res = new ResponseModel<string>();
            int totalRecord = 0;
            var favoriteProducts = _productService.GetSPFavoriteProductsByMainPartyId(AuthenticationUser.Membership.MainPartyId, page, pageDimension, out totalRecord);
            List<MTCategoryProductModel> source = new List<MTCategoryProductModel>();
            string content = "";
            if (favoriteProducts.Count > 0)
            {
                foreach (var item in favoriteProducts)
                {
                    var modelItem = _productModelFactory.PrepareProductAreaItemModel(item);
                    content = content + "<div class='col-md-6 col-lg-4'>" + RenderPartialToString("~/Areas/Account/Views/Shared/_ProductItemBox.cshtml", modelItem) + "</div>";

                }
                res.Result = content;
                res.IsSuccess = true;
            }
            else
            {
                res.IsSuccess = false;
                res.Result = "";
            }
  

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProductPaging(int page, byte displayType)
        {
            var dataFavoriteProduct = new Data.FavoriteProduct();
            var getProduct = new SearchModel<ProductModel>
            {
                CurrentPage = page,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };

            var data = dataFavoriteProduct.GetSearchWebByMainPartyId(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();

            getProduct.Source = data;
            getProduct.TotalRecord = TotalRecord;

            string userControlName = string.Empty;
            DisplayType pageType = (DisplayType)displayType;
            switch (pageType)
            {
                case DisplayType.Window:
                    userControlName = "/Areas/Account/Views/Favorite/ProductWindow.cshtml";
                    break;
                case DisplayType.List:
                    userControlName = "/Areas/Account/Views/Favorite/ProductList.cshtml";
                    break;
                case DisplayType.Text:
                    userControlName = "/Areas/Account/Views/Favorite/ProductText.cshtml";
                    break;
                default:
                    break;
            }

            return View(userControlName, getProduct);
        }

        public ActionResult Store()
        {
            var dataFavoriteStore = new Data.FavoriteStore();
            var getStore = new SearchModel<StoreModel>
            {
                CurrentPage = 1,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };

            var data = dataFavoriteStore.GetSearchWebByMainPartyId(ref TotalRecord, getStore.PageDimension, getStore.CurrentPage, AuthenticationUser.Membership.MainPartyId).AsCollection<StoreModel>();

            getStore.Source = data;
            getStore.TotalRecord = TotalRecord;
            var model = new FavoriteStoreModel
            {
                GetStore = getStore
            };
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyFavorites, (byte)LeftMenuConstants.MyFavorite.MyFavoriteStore);
            return View(model);
        }

        [HttpPost]
        public ActionResult StorePaging(int page, byte displayType)
        {
            var dataFavoriteStore = new Data.FavoriteStore();
            var getStore = new SearchModel<StoreModel>
            {
                CurrentPage = page,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };

            var data = dataFavoriteStore.GetSearchWebByMainPartyId(ref TotalRecord, getStore.PageDimension, getStore.CurrentPage, AuthenticationUser.Membership.MainPartyId).AsCollection<StoreModel>();

            getStore.Source = data;
            getStore.TotalRecord = TotalRecord;

            string userControlName = string.Empty;
            DisplayType pageType = (DisplayType)displayType;
            switch (pageType)
            {
                case DisplayType.Window:
                    userControlName = "/Areas/Account/Views/Favorite/StoreWindow.cshtml";
                    break;
                case DisplayType.List:
                    userControlName = "/Areas/Account/Views/Favorite/StoreList.cshtml";
                    break;
                case DisplayType.Text:
                    userControlName = "/Areas/Account/Views/Favorite/StoreText.cshtml";
                    break;
                default:
                    break;
            }

            return View(userControlName, getStore);
        }

    }
}