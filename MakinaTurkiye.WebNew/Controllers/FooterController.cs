﻿using MakinaTurkiye.Caching;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Content;
using NeoSistem.MakinaTurkiye.Web.Helpers;
using NeoSistem.MakinaTurkiye.Web.Models.Footer;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class FooterController : Controller
    {
        #region Fields

        private readonly IFooterService _footerService;
        private readonly ICacheManager _cacheManager;
        private readonly IConstantService _constantService;



        #endregion

        #region Ctor

        public FooterController(IFooterService footerService, ICacheManager cacheManager, IConstantService constantService)
        {
            this._footerService = footerService;
            this._cacheManager = cacheManager;
            this._constantService = constantService;

        }

        #endregion

        #region Methods

        [ChildActionOnly]
        public ActionResult Content()
        {
            //var sayfa = Request.QueryString["page"];{
            int languageId = HtmlLangHelper.GetLanguageId();
       
            string key = string.Format("makinaturkiye.footer-content-test-"+ languageId);
            var testModel = _cacheManager.Get(key, () =>
            {
                MTFooterModel model = new MTFooterModel();

                var footerParent = _footerService.GetAllFooterParent().Where(x=>x.LanguageId==languageId);
                List<MTFooterParentModel> footerParents = new List<MTFooterParentModel>();

                foreach (var item in footerParent)
                {
                    MTFooterParentModel footerParentItem = new MTFooterParentModel();
                    footerParentItem.FooterParentId = item.FooterParentId;
                    footerParentItem.FooterParentName = item.FooterParentName;
                    foreach (var item1 in item.FooterContents.OrderBy(x => x.DisplayOrder))
                    {
                        MTFooterContentModel footerContentModel = new MTFooterContentModel
                        {
                            FooterContentName = item1.FooterContentName,
                            FooterContentUrl = item1.FooterContentUrl
                        };
                        footerParentItem.FooterContents.Add(footerContentModel);
                    }
                    footerParents.Add(footerParentItem);
                }


                model.FooterParentModels = footerParents;
                return model;
            });

            return PartialView(testModel);
        }

        #endregion

    }
}
