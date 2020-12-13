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
    public class StoreSearchController : BaseController
    {
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

        public ActionResult CategoryGetUrlName(string categoryName)
        {
            return Json(Helpers.StringHelpers.ToUrl(categoryName), JsonRequestBehavior.AllowGet);
        }
    }
}