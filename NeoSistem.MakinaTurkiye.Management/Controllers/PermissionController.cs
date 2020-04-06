namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class PermissionController : BaseController
    {
        //
        // GET: /Permission/

        public ActionResult Index()
        {
            PAGEID = PermissionPage.Izinler;

            var permission = new Classes.Permission();
            var model = permission.GetDataSet().Tables[0].AsCollection<PermissionModel>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {

            var permission = new Classes.Permission();
            var data = permission.GetDataSet().Tables[0].AsCollection<PermissionModel>();
            IEnumerable<PermissionModel> model;

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = data.Where(c => c.PermissionName.Contains(search));
            }
            else
            {
                model = data;
            }

            return View("PermissionList", model);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(PermissionModel model)
        {

            return PostView(m =>
            {

                var permission = new Classes.Permission()
                {
                    PermissionGroupName = model.PermissionGroupName,
                    PermissionName = model.PermissionName
                };

                permission.Save();

                return RedirectToAction("Index");

            }, model);
        }

        public ActionResult Edit(int id)
        {

            try
            {
                var permission = new Classes.Permission();
                bool hasRecord = permission.LoadEntity(id);

                if (hasRecord)
                {

                    var model = new PermissionModel
                    {
                        PermissionGroupName = permission.PermissionGroupName,
                        PermissionName = permission.PermissionName
                    };

                    return View(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, PermissionModel model)
        {

            return PostView(m =>
            {
                var permission = new Classes.Permission();
                bool hasRecord = permission.LoadEntity(id);

                if (hasRecord)
                {

                    UpdateClass(model, permission);
                    permission.PermissionId = id;

                    permission.Save();

                    return RedirectToAction("Index");
                }


                return View(model);
            }, model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var permission = new Classes.Permission();
            bool hasDelete = permission.Delete(id);

            return Json(hasDelete);
        }
    }
}
