namespace NeoSistem.Trinnk.Management.Controllers
{
    using Models;
    using NeoSistem.Trinnk.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class VirtualPosController : BaseController
    {
        public ActionResult Index()
        {
            PAGEID = PermissionPage.SanalPosListesi;

            var virtualPos = entities.VirtualPos.ToList();
            return View(virtualPos);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniSanalPos;

            return View(new VirtualPos());
        }

        [HttpPost]
        public ActionResult Create(VirtualPos model)
        {
            try
            {
                var curVirtualPos = new VirtualPos
                {
                    VirtualPostName = model.VirtualPostName,
                    VirtualPosActive = model.VirtualPosActive,
                    VirtualPosApiPass = model.VirtualPosApiPass,
                    VirtualPosApiUrl = model.VirtualPosApiUrl,
                    VirtualPosApiUserName = model.VirtualPosApiUserName,
                    VirtualPosClientId = model.VirtualPosClientId,
                    VirtualPosPostUrl = model.VirtualPosPostUrl,
                    VirtualPosStoreKey = model.VirtualPosStoreKey,
                    VirtualPosStoreType = model.VirtualPosStoreType,
                };
                entities.VirtualPos.AddObject(curVirtualPos);
                entities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(byte id)
        {
            try
            {
                var curVirtualPos = entities.VirtualPos.SingleOrDefault(c => c.VirtualPosId == id);
                entities.VirtualPos.DeleteObject(curVirtualPos);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
            return Json(true);
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.SanalPosDuzenle;

            var curVirtualPos = entities.VirtualPos.SingleOrDefault(c => c.VirtualPosId == id);

            if (curVirtualPos != null)
            {
                return View(curVirtualPos);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, VirtualPos model)
        {
            try
            {
                var curPacketFeatureType = entities.VirtualPos.SingleOrDefault(c => c.VirtualPosId == id);
                curPacketFeatureType.VirtualPostName = model.VirtualPostName;
                curPacketFeatureType.VirtualPosActive = model.VirtualPosActive;
                curPacketFeatureType.VirtualPosApiPass = model.VirtualPosApiPass;
                curPacketFeatureType.VirtualPosApiUrl = model.VirtualPosApiUrl;
                curPacketFeatureType.VirtualPosApiUserName = model.VirtualPosApiUserName;
                curPacketFeatureType.VirtualPosClientId = model.VirtualPosClientId;
                curPacketFeatureType.VirtualPosPostUrl = model.VirtualPosPostUrl;
                curPacketFeatureType.VirtualPosStoreKey = model.VirtualPosStoreKey;
                curPacketFeatureType.VirtualPosStoreType = model.VirtualPosStoreType;

                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}