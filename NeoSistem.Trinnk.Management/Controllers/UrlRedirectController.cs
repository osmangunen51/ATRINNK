using Trinnk.Entities.Tables.Common;
using Trinnk.Services.Common;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.UrlRedirectModels;
using System;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    public class UrlRedirectController : BaseController
    {
        IUrlRedirectService _urlRedirectService;


        private const int PAGE_DIMENSION = 50;

        public UrlRedirectController(IUrlRedirectService urlRedirectService)
        {
            this._urlRedirectService = urlRedirectService;
        }
        //GET: UrlRedirect
        public ActionResult Index()
        {
            int skip = 0;
            int take = PAGE_DIMENSION;
            int totalCount;
            var urlRedirects = _urlRedirectService.GetUrlRedirects(skip, take, out totalCount);
            FilterModel<UrlRedirect> model = new FilterModel<UrlRedirect>();
            model.Source = urlRedirects;
            model.TotalRecord = totalCount;
            model.PageDimension = take;
            model.CurrentPage = 1;
            return View(model);
        }
        [HttpPost]
        public PartialViewResult Index(int page)
        {


            int take = PAGE_DIMENSION;
            int skip = (page * take) - take;
            int totalCount;
            var urlRedirects = _urlRedirectService.GetUrlRedirects(skip, take, out totalCount);
            FilterModel<UrlRedirect> model = new FilterModel<UrlRedirect>();
            model.Source = urlRedirects;
            model.TotalRecord = totalCount;
            model.PageDimension = take;
            model.CurrentPage = page;
            return PartialView("_UrlRedirectList", model);

        }

        public ActionResult Create(int? urlRedirectId)
        {
            UrlRedirectItem model = new UrlRedirectItem();
            if (urlRedirectId.HasValue)
            {
                var urlRedirect = _urlRedirectService.GetUrlRedirectByUrlRedirectId(urlRedirectId.Value);
                model.UrlRedirectId = urlRedirect.UrlRedirectId;
                model.NewUrl = urlRedirect.NewUrl;
                model.OldUrl = urlRedirect.OldUrl;

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(UrlRedirectItem model)
        {
            if (model.UrlRedirectId != 0)
            {

            }
            else
            {
                var urlRedirect = new UrlRedirect();
                urlRedirect.NewUrl = model.NewUrl;
                urlRedirect.OldUrl = model.OldUrl;
                urlRedirect.CreatedDate = DateTime.Now;
                _urlRedirectService.InsertUrlRedirect(urlRedirect);

            }
            TempData["Message"] = "Başarılı";
            return RedirectToAction("Create");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var urlRediect = _urlRedirectService.GetUrlRedirectByUrlRedirectId(id);
            if (urlRediect != null)
            {
                _urlRedirectService.DeleteUrlRedirect(urlRediect);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}