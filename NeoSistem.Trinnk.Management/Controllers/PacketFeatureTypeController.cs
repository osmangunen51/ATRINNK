namespace NeoSistem.Trinnk.Management.Controllers
{
    using Models;
    using NeoSistem.Trinnk.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class PacketFeatureTypeController : BaseController
    {
        public ActionResult Index()
        {
            PAGEID = PermissionPage.PaketOzellikleri;

            var pftypeItems = entities.PacketFeatureTypes.ToList();
            return View(pftypeItems);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniPaketOzelligi;

            return View(new PacketFeatureType());
        }

        [HttpPost]
        public ActionResult Create(PacketFeatureType model)
        {
            try
            {
                var curPacketFeatureType = new PacketFeatureType
                {
                    PacketFeatureTypeName = model.PacketFeatureTypeName,
                    PacketFeatureTypeDesc = model.PacketFeatureTypeDesc,
                    PacketFeatureTypeOrder = model.PacketFeatureTypeOrder
                };
                entities.PacketFeatureTypes.AddObject(curPacketFeatureType);
                entities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var curPacketFeatureType = entities.PacketFeatureTypes.SingleOrDefault(c => c.PacketFeatureTypeId == id);
                var packetFeatures = entities.PacketFeatures.Where(x => x.PacketFeatureTypeId == id);
                foreach (var packetFeature in packetFeatures)
                {
                    entities.PacketFeatures.DeleteObject(packetFeature);
                }
                entities.SaveChanges();
                entities.PacketFeatureTypes.DeleteObject(curPacketFeatureType);
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
            PAGEID = PermissionPage.PaketOzelligiDuzenle;

            var curActivityType = entities.PacketFeatureTypes.SingleOrDefault(c => c.PacketFeatureTypeId == id);
            if (curActivityType != null)
            {
                return View(curActivityType);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, PacketFeatureType model)
        {
            try
            {
                var curPacketFeatureType = entities.PacketFeatureTypes.SingleOrDefault(c => c.PacketFeatureTypeId == id);
                curPacketFeatureType.PacketFeatureTypeName = model.PacketFeatureTypeName;
                curPacketFeatureType.PacketFeatureTypeDesc = model.PacketFeatureTypeDesc;
                curPacketFeatureType.PacketFeatureTypeOrder = model.PacketFeatureTypeOrder;

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