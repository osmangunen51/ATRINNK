namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class DictionaryController : BaseController
    {
        public ActionResult Index(int? page, int? status, int? id)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            ViewData["pageNumbers"] = Convert.ToInt32(entities.CompanyDemandMemberships.ToList().Count / pageSize);
            var messageList = entities.Dictionaries.OrderByDescending(x => x.ID).Skip(skipRows).Take(pageSize).ToList();
            return View(messageList);
        }

        [HttpPost]
        public JsonResult DeleteDictinoary(int id)
        {
            var dic = entities.Dictionaries.SingleOrDefault(x => x.ID == id);
            entities.Dictionaries.DeleteObject(dic);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {

            return View(new Dictionary());
        }

        [HttpPost]
        public ActionResult Create(Dictionary model)
        {
            if (ModelState.IsValid)
            {
                entities.Dictionaries.AddObject(model);
                entities.SaveChanges();
            }
            return RedirectToAction("index");
        }

        public ActionResult update(int id)
        {
            var updateDic = entities.Dictionaries.First(x => x.ID == id);

            return View(updateDic);

        }

        [HttpPost]
        public ActionResult update(Dictionary model)
        {
            if (ModelState.IsValid)
            {
                var updateDic = entities.Dictionaries.First(x => x.ID == model.ID);
                updateDic.DicDescription = model.DicDescription;
                updateDic.DicShortName = model.DicShortName;
                entities.SaveChanges();
            }
            return RedirectToAction("index");

        }

    }
}