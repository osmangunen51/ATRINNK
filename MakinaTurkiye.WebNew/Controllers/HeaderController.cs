using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Utilities.HttpHelpers;
using MakinaTurkiye.Utilities.Mvc;
using NeoSistem.MakinaTurkiye.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    [Compress]
    public class HeaderController : BaseController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly ICategoryPlaceChoiceService _categoryPlaceChoiceService;

        #endregion

        #region Ctor

        public HeaderController(ICategoryService categoryService,ICategoryPlaceChoiceService categoryPlaceChoiceService)
        {
            this._categoryService = categoryService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;
        }

        #endregion

        #region Methods

        [ChildActionOnly]
        public ActionResult _HeaderTopMenu()
        {
            var helpCategories = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.ForStore, false).ToList();
            var helpCategoryForMember = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct((byte)HelpCategoryPlace.ForMember, false);

            helpCategories.AddRange(helpCategoryForMember);

            MTHeaderTopMenuModel model = new MTHeaderTopMenuModel();

            foreach (var item in helpCategories)
            {

                string url = UrlBuilder.GetHelpCategoryUrl(item.CategoryId, item.Category.CategoryName);
                model.HeaderTopMenuForHelp.Add(new MTHeaderTopMenuItem
                {
                    HelpCategoryName = item.Category.CategoryName,
                    HelpCategoryType = item.CategoryPlaceType,
                    HelpCategoryId = item.CategoryId,
                    Url = url
                });
            }

            return PartialView(model);
        }


        #endregion
    }
}
