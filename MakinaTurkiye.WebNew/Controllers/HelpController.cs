using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NeoSistem.MakinaTurkiye.Web.Models;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Services.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.Help;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using MakinaTurkiye.Utilities.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class HelpController : BaseController
    {
        #region Fields

        private ICategoryService _categoryService;

        #endregion

        #region Ctor

        public HelpController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        #endregion


        #region Methods

        [Compress]
        public ActionResult Index()
        {
            var request = HttpContext.Request;
            ViewBag.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;
            return View();
        }

        public ActionResult Menu()
        {
            List<MTHelpMenuModel> model = new List<MTHelpMenuModel>();
            var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                model.Add(new MTHelpMenuModel { CategoryId = item.CategoryId, CategoryName = item.CategoryName,
                    CategoryParentId = item.CategoryParentId.GetValueOrDefault(),
                    HelpUrl = url });
            }
            return View(model);

        }
        public ActionResult MenuSub(int categoryId)
        {
            if (categoryId <= 0)
            {
                return RedirectToAction("Index");
            }

            var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
            var currentMenuModel = helpCategories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (currentMenuModel == null)
            {
                return RedirectToAction("Index");
            }

            List<MTHelpMenuModel> menuModels = new List<MTHelpMenuModel>();
            foreach (var item in helpCategories)
            {
                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                menuModels.Add(new MTHelpMenuModel { CategoryId = item.CategoryId, CategoryName = item.CategoryName,
                                                    CategoryParentId = item.CategoryParentId.GetValueOrDefault(), HelpUrl = url });
            }

            MTHelpTopModel model = new MTHelpTopModel();
            model.MenuItemModels = menuModels;
            model.CurrentMenuModel = menuModels.FirstOrDefault(c => c.CategoryId == categoryId);

            return View(model);
        }

        [Compress]
        public ActionResult YardimDetay(int categoryId)
        {
            if (categoryId <= 0)
            {
                return RedirectToAction("Index");
            }

            var category = _categoryService.GetCategoryByCategoryId(categoryId);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var request = HttpContext.Request;
            string helpUrl = UrlBuilder.GetHelpCategoryUrl(category.CategoryId, category.CategoryName);
            string absUrl=request.Url.AbsolutePath;

            //#if !DEBUG
            //      absUrl=request.Url.AbsoluteUri;
            //#endif
            //if (absUrl != helpUrl)
            //{
            //    return RedirectPermanent(helpUrl);
            //}

            var model = new MTHelpDetailModel();
            try
            {


                var helpCategories = _categoryService.GetCategoriesByMainCategoryType(MainCategoryTypeEnum.Help);
                var subHelpCategories = helpCategories.Where(c => c.CategoryParentId == categoryId);
                foreach (var item in subHelpCategories)
                {
                    string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.CategoryName);
                    model.SubMenuItemModels.Add(new MTHelpMenuModel { CategoryId = item.CategoryId, CategoryName = item.CategoryName,
                                                        CategoryParentId = item.CategoryParentId.GetValueOrDefault(), HelpUrl = url });
                }

                model.CategoryId = category.CategoryId;
                model.CategoryName = category.CategoryName;
                model.Content = category.Content;
                model.Canonical = AppSettings.SiteUrlWithoutLastSlash + request.Url.AbsolutePath;

                //SeoPageType = (byte)PageType.HelpCategory;
            }
            catch
            {
                return Redirect("/yardim");
            }
            return View(model);
        }

        #endregion

    }
}
