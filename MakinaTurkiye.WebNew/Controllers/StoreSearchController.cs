namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    using Models;
    using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    [AllowAnonymous]
    public class StoreSearchController : BaseController
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

        //[HttpPost]
        //public JsonResult SearchCategory(string categoryName)
        //{
        //    if (!string.IsNullOrWhiteSpace(categoryName) && categoryName.Length >= 3)
        //    {
        //        IList<Category> categoryItems = null;
        //        List<SelectListItem> categoryList = new List<SelectListItem>();

        //        using (var entities = new MakinaTurkiyeEntities())
        //        {
        //            categoryItems = entities.spCategoryGetCategoryByCategoryName(categoryName).Where(c => c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori).ToList();
        //        }
        //        foreach (var item in categoryItems)
        //        {
        //            string title = "";
        //            if (!string.IsNullOrEmpty(item.StorePageTitle))
        //                title = item.StorePageTitle;
        //            else
        //            {
        //                title = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
        //            }
        //            categoryList.Add(new SelectListItem
        //            {
        //                Text = title,
        //                Value = item.CategoryId.ToString()
        //            });
        //        }
        //        return Json(categoryList, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json("");
        //}

        //public ActionResult CategoryGetUrlName(string categoryName)
        //{
        //    return Json(Helpers.StringHelpers.ToUrl(categoryName), JsonRequestBehavior.AllowGet);
        //}

    }
}