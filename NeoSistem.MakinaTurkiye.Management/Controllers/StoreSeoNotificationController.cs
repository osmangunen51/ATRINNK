using MakinaTurkiye.Services.Stores;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using NeoSistem.MakinaTurkiye.Management.Models.Stores;
using System.Web.Mvc;
using System.Linq;
using System;
using MakinaTurkiye.Entities.Tables.Stores;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using System.Collections.Generic;
using NeoSistem.MakinaTurkiye.Management.Models;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class StoreSeoNotificationController : BaseController
    {
        IStoreSeoNotificationService _storeSeoNotificationService;
        IStoreService _storeService;

        public StoreSeoNotificationController(IStoreSeoNotificationService storeSeoNotificationService, IStoreService storeService)
        {
            _storeSeoNotificationService = storeSeoNotificationService;
            _storeService = storeService;
        }
        public ActionResult Index(int storeMainPartyId)
        {
            var entites = new MakinaTurkiyeEntities();
            var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);
            var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotificationsByStoreMainPartyId(storeMainPartyId).OrderByDescending(x => x.StoreSeoNotificationId);
            List<BaseMemberDescriptionModelItem> baseMemberDescriptions = new List<BaseMemberDescriptionModelItem>();
            foreach (var item in storeSeoNotifications)
            {
                var fromUser = entites.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                var toUser = entites.Users.FirstOrDefault(x => x.UserId == item.ToUserId);
                baseMemberDescriptions.Add(new BaseMemberDescriptionModelItem
                {
                    Description = item.Text,
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId
                });
            }
            StoreSeoNotificationModel model = new StoreSeoNotificationModel();
            model.BaseMemberDescriptionModelItems = baseMemberDescriptions;
            model.StoreName = store.StoreName;
            return View(model);
        }
        public ActionResult Create(int id, int? storeNotId)
        {
            var model = new StoreSeoNotificationFormModel();
            model.StoreMainPartyId = id;
            var entities = new MakinaTurkiyeEntities();
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 21 || g.UserGroupId == 11 select new { u.UserName, u.UserId };
            foreach (var item in users1)
            {
                model.ToUsers.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }
            if (storeNotId.HasValue)
            {
                var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(storeNotId.Value);
                model.PreviousText = storeSeoNotification.Text;
                model.StoreSeoNotificationId = storeNotId.Value;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(StoreSeoNotificationFormModel model, string time)
        {
            if (!string.IsNullOrEmpty(model.Text))
            {
                int year = 0, day = 0, month = 0;
                int hour = 0, minute = 0;
                DateTime lastDate = new DateTime();
                if (string.IsNullOrEmpty(model.RemindDate))
                {
                    model.RemindDate = DateTime.Now.Date.ToString("dd.MM.yyyy");
                }
                else
                {
                    string[] time1 = model.RemindDate.ToString().Split('.');
                    year = Convert.ToInt32(time1[2]);
                    month = Convert.ToInt32(time1[1]);
                    day = Convert.ToInt32(time1[0]);
                }
                if (!string.IsNullOrEmpty(time))
                {
                    hour = Convert.ToInt32(time.Split(':')[0]);
                    minute = Convert.ToInt32(time.Split(':')[1]);
                }
                else
                {
                    hour = DateTime.Now.Hour;
                    minute = DateTime.Now.Minute + 1;
                    if (day != DateTime.Now.Day)
                    {
                        hour = 8;
                        minute = 30;
                    }
                }
                lastDate = new DateTime(year, month, day, hour, minute, 0);
                if (lastDate > DateTime.Now)
                {
                    var storeSeoNotification = new StoreSeoNotification
                    {
                        CreatedDate = DateTime.Now,
                        RemindDate = lastDate,
                        FromUserId = CurrentUserModel.CurrentManagement.UserId,
                        ToUserId = Convert.ToByte(model.ToUserId),
                        Status = 0,
                        StoreMainPartyId = model.StoreMainPartyId,
                        Text = model.Text
                    };
                    _storeSeoNotificationService.InsertStoreSeoNotification(storeSeoNotification);

                    if (model.StoreSeoNotificationId != 0)
                    {
                        var storeSeoNotificationPrev = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(model.StoreSeoNotificationId);
                        storeSeoNotification.RemindDate = null;
                        storeSeoNotification.Status = 1;
                        _storeSeoNotificationService.UpdateStoreSeoNotification(storeSeoNotification);
                    }
                }
                else
                {
                    ModelState.AddModelError("RemindDate", "Tarih şuanki tarihten küçük olamaz");
                }
            }
            else
            {
                ModelState.AddModelError("Text", "Lütfen açıklama giriniz");
            }
            var entities = new MakinaTurkiyeEntities();
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 21 || g.UserGroupId == 11 select new { u.UserName, u.UserId };
            foreach (var item in users1)
            {
                model.ToUsers.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }
            if (model.StoreSeoNotificationId != 0)
            {
                var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(model.StoreSeoNotificationId);
                model.PreviousText = storeSeoNotification.Text;
                model.StoreSeoNotificationId = model.StoreSeoNotificationId;
            }

            return View(model);
        }


        [HttpGet]
        public JsonResult GetCountSeoNotification()
        {
            var entities = new MakinaTurkiyeEntities();
            var userId = CurrentUserModel.CurrentManagement.UserId;
            if (entities.PermissionUsers.Any(x => x.UserId == userId && (x.UserGroupId == 11 || x.UserGroupId == 21)) == true)
            {
                var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationsByDateWithStatus(DateTime.Now, 0, userId);
                return Json(storeSeoNotification.Count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult AllNotification()
        {
            var userId = CurrentUserModel.CurrentManagement.UserId;
            List<BaseMemberDescriptionModelItem> baseMemberDescriptions = new List<BaseMemberDescriptionModelItem>();
            var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotificationsByDateWithStatus(DateTime.Now, 0, userId);
            var entities = new MakinaTurkiyeEntities();
            foreach (var item in storeSeoNotifications)
            {
                var fromUser = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                var toUser = entities.Users.FirstOrDefault(x => x.UserId == item.ToUserId);
                baseMemberDescriptions.Add(new BaseMemberDescriptionModelItem
                {
                    Description = item.Text,
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId
                });
            }
            StoreSeoNotificationModel model = new StoreSeoNotificationModel();
            model.BaseMemberDescriptionModelItems = baseMemberDescriptions;
            return View(model);
        }

        public ActionResult AllSeoNotification()
        {
            int totalRecord;


            FilterModel<BaseMemberDescriptionModelItem> model = new FilterModel<BaseMemberDescriptionModelItem>();
            
            model.Source = PrepareAllStoreSeoNotificationItem(0, DateTime.Now, out totalRecord);
            model.TotalRecord = totalRecord;
            model.PageDimension = 50;
            model.CurrentPage = 1;
            return View(model);
        }
        private List<BaseMemberDescriptionModelItem> PrepareAllStoreSeoNotificationItem(int page, DateTime createdDate, out int totalRecord)
        {
            List<BaseMemberDescriptionModelItem> baseMemberDescriptions = new List<BaseMemberDescriptionModelItem>();
            var entities = new MakinaTurkiyeEntities();
            var userId = CurrentUserModel.CurrentManagement.UserId;

            var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotifications(page, 50, createdDate, out totalRecord);
            foreach (var item in storeSeoNotifications)
            {
                var fromUser = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                var toUser = entities.Users.FirstOrDefault(x => x.UserId == item.ToUserId);
                baseMemberDescriptions.Add(new BaseMemberDescriptionModelItem
                {
                    Description = item.Text,
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId
                });
            }

            return baseMemberDescriptions;

        }
        [HttpPost]
        public PartialViewResult AllSeoNotification(int page, string createdDate)
        {

            FilterModel<BaseMemberDescriptionModelItem> model = new FilterModel<BaseMemberDescriptionModelItem>();
            int totalRecord;
            var createdDateTime = Convert.ToDateTime(createdDate);
            int pageDimension = 50;
            int take = page * pageDimension - pageDimension;
            model.Source = PrepareAllStoreSeoNotificationItem(take, createdDateTime, out totalRecord);
            model.TotalRecord = totalRecord;
            model.PageDimension = pageDimension;
            model.CurrentPage = page;
            return PartialView("_SeoNotificationList", model);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(id);
            _storeSeoNotificationService.DeleteStoreSeoNotification(storeSeoNotification);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}