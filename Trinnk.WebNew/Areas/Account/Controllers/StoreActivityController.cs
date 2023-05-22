using Trinnk.Entities.Tables.Stores;
using Trinnk.Services.Catalog;
using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using Trinnk.Utilities.Controllers;
using NeoSistem.Trinnk.Web.Areas.Account.Constants;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Stores;
using NeoSistem.Trinnk.Web.Models.Authentication;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{
    public class StoreActivityController : BaseAccountController
    {
        ICategoryService _categoryService;
        IStoreService _storeService;
        IMemberStoreService _memberStoreService;
        IStoreActivityCategoryService _storeActivityCategoryService;

        public StoreActivityController(ICategoryService categoryService,
            IStoreService storeService,
            IMemberStoreService memberStoreService,
             IStoreActivityCategoryService storeActivityCategoryService)
        {
            this._categoryService = categoryService;
            this._storeService = storeService;
            this._memberStoreService = memberStoreService;
            this._storeActivityCategoryService = storeActivityCategoryService;

            this._categoryService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
        }
        // GET: Account/StoreActivity
        public ActionResult Index()
        {
            var sectoreCategories = _categoryService.GetMainCategories();
            MTStoreActivityModel model = new MTStoreActivityModel();
            var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId.Value;
            var storeActivities = _storeActivityCategoryService.GetStoreActivityCategoriesByMainPartyId(storeMainPartyId);

            foreach (var item in sectoreCategories)
            {
                var itemCategory = new SelectListItem
                {
                    Value = item.CategoryId.ToString(),
                    Text = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                };
                if (storeActivities.FirstOrDefault(x => x.CategoryId == item.CategoryId) != null)
                {
                    itemCategory.Selected = true;
                }
                model.Categories.Add(itemCategory);
            }
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingOtherInfo.ActivityType);
            foreach (var item in storeActivities)
            {
                model.StoreActivityCategories.Add(item.StoreActivityCategoryId, !string.IsNullOrEmpty(item.Category.CategoryContentTitle) ? item.Category.CategoryContentTitle : item.Category.CategoryName);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(int[] subcategory)
        {
            var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId.Value;
            var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);
            if (subcategory != null && subcategory.Length > 0)
            {
                foreach (var item in subcategory)
                {
                    if (!store.StoreActivityCategories.Any(x => x.CategoryId == item))
                    {
                        var storeActivityCategory = new StoreActivityCategory
                        {
                            CategoryId = item,
                            MainPartyId = storeMainPartyId,
                        };
                        _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);

                    }
                }
                TempData["SuccessMessage"] = "Faaliyet alanlarınız kayıt edilmiştir.";
            }
            else
            {
                TempData["ErrorMessage"] = "Lütfen kategori seçiniz";
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult Delete(int storeActivityCategoryId)
        {
            var storeActivity = _storeActivityCategoryService.GetStoreActivityCategoryByStoreActivityCategoryId(storeActivityCategoryId);
            _storeActivityCategoryService.DeleteStoreActivityCategory(storeActivity);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}