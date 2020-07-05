namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class UserGroupController : BaseController
    {

        public ActionResult Index()
        {
            PAGEID = PermissionPage.IzinGruplari;

            var userGroup = new Classes.UserGroup();
            var model = userGroup.GetDataSet().Tables[0].AsCollection<UserGroupModel>();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            var userGroup = new Classes.UserGroup();
            var data = userGroup.GetDataSet().Tables[0].AsCollection<UserGroupModel>();
            IEnumerable<UserGroupModel> model;

            if (!string.IsNullOrWhiteSpace(search))
            {
                model = data.Where(c => c.GroupName.Contains(search));
            }
            else
            {
                model = data;
            }

            return View("UserGroupList", model);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniIzinGrubu;

            var model = new UserGroupModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(UserGroupModel model, FormCollection collection)
        {

            var userGroup = new Classes.UserGroup
            {
                GroupName = model.GroupName
            };

            userGroup.Save();

            var permissions = collection["Permission"].Split(',');

            Classes.PermissionGroup permissionGroup = null;

            foreach (var item in permissions)
            {
                if (item.ToInt32() > 0)
                {
                    permissionGroup = new Classes.PermissionGroup
                    {
                        GroupId = userGroup.UserGroupId,
                        PermissionId = item.ToInt32()
                    };
                    permissionGroup.Save();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.IzinGrubuDuzenle;

            try
            {
                var userGroup = new Classes.UserGroup();
                bool hasRecord = userGroup.LoadEntity(id);
                if (hasRecord)
                {

                    var model = new UserGroupModel
                    {
                        GroupName = userGroup.GroupName
                    };

                    var dataPermission = new Data.Permission();
                    model.Permissions = dataPermission.GetItemsByGroupId(id).AsCollection<PermissionModel>();

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
        public ActionResult Edit(int id, UserGroupModel model, FormCollection collection)
        {
            var userGroup = new Classes.UserGroup();
            bool hasRecord = userGroup.LoadEntity(id);

            if (hasRecord)
            {
                userGroup.GroupName = model.GroupName;
                userGroup.UserGroupId = id;

                userGroup.Save();

                var permissionsGroup = new Classes.PermissionGroup();
                permissionsGroup.DeleteByGroupId(userGroup.UserGroupId);

                var permissions = collection["Permission"].Split(',');

                Classes.PermissionGroup permissionGroup = null;

                foreach (var item in permissions)
                {
                    if (item.ToInt32() > 0)
                    {
                        permissionGroup = new Classes.PermissionGroup
                        {
                            GroupId = userGroup.UserGroupId,
                            PermissionId = item.ToInt32()
                        };
                        permissionGroup.Save();
                    }
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var userGroup = new Classes.UserGroup();
            bool hasDelete = userGroup.Delete(id);
            return Json(new { m = hasDelete });
        }
    }
}
