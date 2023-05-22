using Trinnk.Services.Content;
using NeoSistem.Trinnk.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    public class FooterController : BaseController
    {
        #region Fields

        private readonly IFooterService _footerService;

        #endregion

        #region Ctor

        public FooterController(IFooterService footerService)
        {
            this._footerService = footerService;
        }

        #endregion

        #region Methods

        public ActionResult FooterParents(int? page)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            var footerParents = _footerService.GetAllFooterParent().OrderBy(x => x.DisplayOrder).ToList().Skip(skipRows).Take(pageSize);
            List<FooterParentModel> footerParentList = new List<FooterParentModel>();
            foreach (var footerItem in footerParents)
            {
                footerParentList.Add(new FooterParentModel { DisplayOrder = footerItem.DisplayOrder, FooterParentName = footerItem.FooterParentName, FooterParentId = footerItem.FooterParentId });
            }

            return View(footerParentList);
        }

        public ActionResult FooterParentAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FooterParentAdd(FooterParentModel model)
        {
            var footerParent = new global::Trinnk.Entities.Tables.Content.FooterParent();
            footerParent.FooterParentName = model.FooterParentName;
            footerParent.DisplayOrder = model.DisplayOrder;
            _footerService.InsertFooterParent(footerParent);
            return RedirectToAction("FooterParents");
        }

        public ActionResult FooterParentUpdate(int id)
        {
            var footerParent = _footerService.GetFooterParentByFooterParentId(id);
            FooterParentModel footerParentModel = new FooterParentModel();
            footerParentModel.DisplayOrder = footerParent.DisplayOrder;
            footerParentModel.FooterParentName = footerParent.FooterParentName;
            footerParentModel.FooterParentId = footerParent.FooterParentId;
            return View(footerParentModel);
        }

        [HttpPost]
        public ActionResult FooterParentUpdate(FooterParentModel model)
        {
            var footerParent = _footerService.GetFooterParentByFooterParentId(model.FooterParentId);
            footerParent.FooterParentName = model.FooterParentName;
            footerParent.DisplayOrder = model.DisplayOrder;
            _footerService.UpdateFooterParent(footerParent);
            return RedirectToAction("FooterParents");
        }

        [HttpPost]
        public JsonResult FooterParentDelete(int id)
        {
            var footerContents = _footerService.GetFooterContentsByFooterParentId(id);
            foreach (var item in footerContents)
            {
                _footerService.DeleteFooterContent(item);
            }
            var footerParent = _footerService.GetFooterParentByFooterParentId(id);
            _footerService.DeleteFooterParent(footerParent);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FooterContents(int? page, int? id)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            IEnumerable<global::Trinnk.Entities.Tables.Content.FooterContent> footerContents;
            if (id == null)
                footerContents = _footerService.GetAllFooterContent().OrderBy(m => m.FooterParent.DisplayOrder).Skip(skipRows).Take(pageSize);
            else
                footerContents = _footerService.GetFooterContentsByFooterParentId(Convert.ToInt32(id)).OrderBy(m => m.FooterParent.DisplayOrder).Skip(skipRows).Take(pageSize);
            List<FooterContentModel> footerContentList = new List<FooterContentModel>();
            foreach (var item in footerContents)
            {
                footerContentList.Add(new FooterContentModel { DisplayOrder = item.DisplayOrder, FooterContentId = item.FooterContentId, FooterContentName = item.FooterContentName, FooterContentUrl = item.FooterContentUrl, FooterParentId = item.FooterParentId.ToString(), FooterParentName = item.FooterParent.FooterParentName });
            }
            return View(footerContentList);
        }

        public ActionResult FooterContentAdd()
        {
            var footerParents = _footerService.GetAllFooterParent().OrderBy(x => x.DisplayOrder).ThenBy(x => x.FooterParentName);
            FooterContentModel footerContenModel = new FooterContentModel();
            foreach (var item in footerParents)
            {
                SelectListItem item1 = new SelectListItem();
                item1.Value = item.FooterParentId.ToString();
                item1.Text = item.FooterParentName;
                footerContenModel.FooterParentItems.Add(item1);
            }
            return View(footerContenModel);
        }

        [HttpPost]
        public ActionResult FooterContentAdd(FooterContentModel model, string footerParentId)
        {
            var footerContent = new global::Trinnk.Entities.Tables.Content.FooterContent();
            footerContent.DisplayOrder = model.DisplayOrder;
            footerContent.FooterContentName = model.FooterContentName;
            footerContent.FooterContentUrl = model.FooterContentUrl;
            footerContent.FooterParentId = Convert.ToInt32(footerParentId);
            _footerService.InsertFooterContent(footerContent);
            return RedirectToAction("FooterContentAdd");
        }

        public ActionResult FooterContentUpdate(int id)
        {
            var footerContent = _footerService.GetFooterContentByFooterContentId(id);
            FooterContentModel footerContentModel = new FooterContentModel();
            footerContentModel.FooterContentName = footerContent.FooterContentName;
            footerContentModel.FooterContentId = footerContent.FooterContentId;
            footerContentModel.FooterContentUrl = footerContent.FooterContentUrl;
            var footerParents = _footerService.GetAllFooterParent().OrderBy(x => x.DisplayOrder).ThenBy(x => x.FooterParentName);
            foreach (var item in footerParents)
            {
                SelectListItem item1 = new SelectListItem();
                item1.Value = item.FooterParentId.ToString();
                item1.Text = item.FooterParentName;
                if (item.FooterParentId == footerContent.FooterParentId)
                    item1.Selected = true;
                footerContentModel.FooterParentItems.Add(item1);
            }
            footerContentModel.DisplayOrder = footerContent.DisplayOrder;
            return View(footerContentModel);
        }

        [HttpPost]
        public ActionResult FooterContentUpdate(FooterContentModel model, string footerParentId)
        {
            var footerContent = _footerService.GetFooterContentByFooterContentId(model.FooterContentId);
            footerContent.DisplayOrder = model.DisplayOrder;
            footerContent.FooterContentName = model.FooterContentName;
            footerContent.FooterContentUrl = model.FooterContentUrl;
            footerContent.FooterParentId = Convert.ToInt32(footerParentId);
            _footerService.UpdateFooterContent(footerContent);
            return RedirectToAction("FooterContentUpdate", "Footer", new { id = model.FooterContentId });
        }

        [HttpPost]
        public JsonResult FooterContentDelete(int id)
        {
            var footerContent = _footerService.GetFooterContentByFooterContentId(id);
            _footerService.DeleteFooterContent(footerContent);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}