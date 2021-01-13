using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;


namespace NeoSistem.MakinaTurkiye.Web.Controllers
{

    [AllowAnonymous]
    public class StoreSearchController : Controller
    {
        //public ActionResult Index()
        //{
        //  ViewData["topMenuFagfa"] = "active";

        //  SeoPageType = (byte)PageType.General;

        //  var model = new StoreSearchViewModel();
        //  using (var entities = new MakinaTurkiyeEntities())
        //  {
        //    var categoryLeftMenu = entities.Categories.Where(c => c.CategoryParentId == null&&c.MainCategoryType==(byte)MainCategoryType.Ana_Kategori).AsEnumerable();
        //    model.Categories = categoryLeftMenu.ToList();
        //  }
        //  return View(model);
        //}

        //public ActionResult GetCategory(int id, byte categoryType)
        //{
        //    IEnumerable<Category> model = null;
        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        model = entities.Categories.Where(c => c.CategoryParentId == id && c.CategoryType == categoryType && c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori).ToList();
        //    }
        //    return View("CategoryItems", model);
        //}

        //public ActionResult GetMainCategory()
        //{
        //    IEnumerable<Category> model = null;
        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        model = entities.Categories.Where(c => c.CategoryParentId == 1).ToList();
        //    }
        //    return View("CategoryMain", model);
        //}

        //public ActionResult CategoryParent(int categoryId, int CategoryGroupType)
        //{
        //    ViewData["topMenuFagfa"] = "active";
        //    var curCategory = new Classes.Category();
        //    CategoryModel currentModel = null;

        //    bool hasRecord = curCategory.LoadEntity(categoryId);
        //    if (hasRecord)
        //    {
        //        currentModel = new CategoryModel
        //        {
        //            CategoryName = curCategory.CategoryName,
        //            CategoryId = curCategory.CategoryId,
        //            CategoryParentId = curCategory.CategoryParentId
        //        };
        //    }

        //    var dataCategory = new Data.Category();
        //    var viewModel = new StoreSearchViewModel();

        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        var model = entities.Categories.Where(c => c.CategoryParentId == categoryId).AsEnumerable();
        //        var ids = (from c in model select c.CategoryId);

        //        viewModel.Categories = model.ToList();
        //        viewModel.Category = currentModel;
        //        viewModel.CategoryParentCategoryItems = (from c in entities.Categories where ids.Contains(c.CategoryParentId.Value) select c).ToList();
        //    }
        //    return View(viewModel);
        //}

        //public ActionResult GetParentCategory(int id)
        //{
        //    IEnumerable<Category> model;
        //    using (var entities = new MakinaTurkiyeEntities())
        //    {
        //        model = entities.Categories.Where(c => c.CategoryParentId == id).ToList();
        //    }
        //    return View("CategoryParentItems", model);
        //}

        [HttpPost]
        public JsonResult SearchCategory(string categoryName)
        {
            if (!string.IsNullOrWhiteSpace(categoryName) && categoryName.Length >= 3)
            {
                IList<NeoSistem.MakinaTurkiye.Web.Models.Entities.Category> categoryItems = null;
                List<SelectListItem> categoryList = new List<SelectListItem>();

                using (MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities entities = new MakinaTurkiye.Web.Models.Entities.MakinaTurkiyeEntities())
                {
                    byte MainCategory = (byte)MainCategoryTypeEnum.MainCategory;
                    categoryItems = entities.spCategoryGetCategoryByCategoryName(categoryName).Where(c => c.MainCategoryType == MainCategory).ToList<NeoSistem.MakinaTurkiye.Web.Models.Entities.Category>();
                }

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

        //public ActionResult CategoryGetUrlName(string categoryName)
        //{
        //    return Json(Helpers.StringHelpers.ToUrl(categoryName), JsonRequestBehavior.AllowGet);
        //}
    }
}