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
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Utilities.FormatHelpers;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class StoreSeoNotificationController : BaseController
    {
        IStoreSeoNotificationService _storeSeoNotificationService;
        IStoreService _storeService;
        IConstantService _constantService;

        public StoreSeoNotificationController(IStoreSeoNotificationService storeSeoNotificationService, IStoreService storeService,
            IConstantService constantService)
        {
            _storeSeoNotificationService = storeSeoNotificationService;
            _storeService = storeService;
            _constantService = constantService;
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
                    Description = FormatHelper.GetMemberDescriptionText(item.Text),
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId,
                    Title = item.ConstantId.HasValue ? _constantService.GetConstantByConstantId(item.ConstantId.Value).ConstantName : ""
                });
            }
            StoreSeoNotificationModel model = new StoreSeoNotificationModel();
            model.BaseMemberDescriptionModelItems = baseMemberDescriptions;
            model.StoreName = store.StoreName;
            return View(model);
        }
        public ActionResult Create(int? id, int? storeNotId)
        {
            var model = new StoreSeoNotificationFormModel();

            var entities = new MakinaTurkiyeEntities();
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where (g.UserGroupId == 21 || g.UserGroupId == 11 || g.UserGroupId == 7 || g.UserGroupId ==18) && u.Active == true select new { u.UserName, u.UserId };
            var storeSeoNotification = new StoreSeoNotification();
            var contstants = _constantService.GetConstantByConstantType(ConstantTypeEnum.SeoDecriptionTitle);
            if (storeNotId.HasValue)
            {
                storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(storeNotId.Value);
                model.PreviousText = storeSeoNotification.Text;
                model.StoreSeoNotificationId = storeNotId.Value;
                model.StoreMainPartyId = storeSeoNotification.StoreMainPartyId;
                if (!storeSeoNotification.ConstantId.HasValue || storeSeoNotification.ConstantId == 0)
                    model.Titles.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });

                foreach (var item in contstants)
                {
                    if (storeSeoNotification.ConstantId == item.ConstantId)
                    {
                        model.Titles.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString(), Selected = true });
                    }
                    else
                    {
                        model.Titles.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() });
                    }

                }
                model.ToUserId = storeSeoNotification.ToUserId;
                model.ConstantId = storeSeoNotification.ConstantId.HasValue ? storeSeoNotification.ConstantId.Value : (short)0;
            }
            else if (id.HasValue)
            {
                model.StoreMainPartyId = id.Value;
                model.Titles.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });
                foreach (var item in contstants)
                {
                    model.Titles.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() });
                }
            }

            foreach (var item in users1.OrderBy(x => x.UserName))
            {
                var element = new SelectListItem
                {
                    Text = item.UserName,
                    Value = item.UserId.ToString()
                };
                if (storeSeoNotification != null && item.UserId == storeSeoNotification.ToUserId)
                {
                    element.Selected = true;
                }

                model.ToUsers.Add(element);
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
                if (model.ConstantId == 0)
                {
                    ModelState.AddModelError("ConstantId", "Lütfen Başlık Seçiniz");
                }
                else
                {
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

                        if (model.StoreSeoNotificationId != 0)
                        {
                            var storeSeoNotificationPrev = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(model.StoreSeoNotificationId);
                            storeSeoNotificationPrev.RemindDate = lastDate;
                            storeSeoNotificationPrev.Status = 0;
                            storeSeoNotificationPrev.ToUserId = Convert.ToByte(model.ToUserId);
                            if (storeSeoNotificationPrev.ConstantId == model.ConstantId)
                            {
                                storeSeoNotificationPrev.Text = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Text + "-" + "<span style='color:#44000d; font-weight:600;'>" + CurrentUserModel.CurrentManagement.UserName + "</span>" + "~" + storeSeoNotificationPrev.Text;
                                _storeSeoNotificationService.UpdateStoreSeoNotification(storeSeoNotificationPrev);
                            }
                            else
                            {
                                string text = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Text + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>";

                                var storeSeoNotification = new StoreSeoNotification
                                {
                                    CreatedDate = DateTime.Now,
                                    RemindDate = lastDate,
                                    FromUserId = CurrentUserModel.CurrentManagement.UserId,
                                    ToUserId = Convert.ToByte(model.ToUserId),
                                    Status = 0,
                                    StoreMainPartyId = model.StoreMainPartyId,
                                    ConstantId = model.ConstantId,
                                    Text = text
                                };
                                _storeSeoNotificationService.InsertStoreSeoNotification(storeSeoNotification);
                            }

                        }
                        else
                        {
                            string text = "<span style='color:#31c854; '>" + DateTime.Now + "</span>-" + model.Text + "<span style='color:#44000d'>" + CurrentUserModel.CurrentManagement.UserName + "</span>";

                            var storeSeoNotification = new StoreSeoNotification
                            {
                                CreatedDate = DateTime.Now,
                                RemindDate = lastDate,
                                FromUserId = CurrentUserModel.CurrentManagement.UserId,
                                ToUserId = Convert.ToByte(model.ToUserId),
                                Status = 0,
                                StoreMainPartyId = model.StoreMainPartyId,
                                ConstantId = model.ConstantId,
                                Text = text
                            };
                            _storeSeoNotificationService.InsertStoreSeoNotification(storeSeoNotification);

                        }



                        return RedirectToAction("Index", new { storeMainPartyId = model.StoreMainPartyId });
                    }
                    else
                    {
                        ModelState.AddModelError("RemindDate", "Tarih şuanki tarihten küçük olamaz");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("Text", "Lütfen açıklama giriniz");
            }
            var entities = new MakinaTurkiyeEntities();
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where (g.UserGroupId == 21 || g.UserGroupId == 11 || g.UserGroupId == 7 || g.UserGroupId == 18) && u.Active == true select new { u.UserName, u.UserId };
            foreach (var item in users1.OrderBy(x => x.UserName))
            {
                model.ToUsers.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }
            if (model.StoreSeoNotificationId != 0)
            {
                var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationByStoreSeoNotificationId(model.StoreSeoNotificationId);
                model.PreviousText = storeSeoNotification.Text;
                model.StoreSeoNotificationId = model.StoreSeoNotificationId;
            }
            var contstants = _constantService.GetConstantByConstantType(ConstantTypeEnum.SeoDecriptionTitle);
            if (model.ConstantId == 0)
                model.Titles.Add(new SelectListItem { Text = "Seçiniz", Value = "0" });
            foreach (var item in contstants)
            {
                if (model.ConstantId == item.ConstantId)
                {
                    model.Titles.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString(), Selected = true });
                }
                else
                {
                    model.Titles.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() });
                }
            }
            return View(model);
        }
        [HttpGet]
        public JsonResult GetCountSeoNotification()
        {
            var entities = new MakinaTurkiyeEntities();
            var userId = CurrentUserModel.CurrentManagement.UserId;
            if (entities.PermissionUsers.Any(x => x.UserId == userId && (x.UserGroupId == 11 || x.UserGroupId == 21 || x.UserGroupId ==7 || x.UserGroupId ==18 )) == true)
            {
                var storeSeoNotification = _storeSeoNotificationService.GetStoreSeoNotificationsByDateWithStatus(DateTime.Now, 0, userId);
                return Json(new { Count = storeSeoNotification.Count }, JsonRequestBehavior.AllowGet);
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
                var store = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                baseMemberDescriptions.Add(new BaseMemberDescriptionModelItem
                {
                    Description = FormatHelper.GetMemberDescriptionText(item.Text),
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId,
                    StoreName = store.StoreName,
                    Title = item.ConstantId.HasValue ? _constantService.GetConstantByConstantId(item.ConstantId.Value).ConstantName : ""
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

            model.Source = PrepareAllStoreSeoNotificationItem(0, null, out totalRecord);
            model.TotalRecord = totalRecord;
            model.PageDimension = 50;
            model.CurrentPage = 1;
            return View(model);
        }
        private List<BaseMemberDescriptionModelItem> PrepareAllStoreSeoNotificationItem(int page, DateTime? createdDate, out int totalRecord)
        {
            List<BaseMemberDescriptionModelItem> baseMemberDescriptions = new List<BaseMemberDescriptionModelItem>();
            var entities = new MakinaTurkiyeEntities();
            var userId = CurrentUserModel.CurrentManagement.UserId;

            var storeSeoNotifications = _storeSeoNotificationService.GetStoreSeoNotifications(page, 50, createdDate, out totalRecord);
            foreach (var item in storeSeoNotifications)
            {
                var fromUser = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                var toUser = entities.Users.FirstOrDefault(x => x.UserId == item.ToUserId);
                var store = _storeService.GetStoreByMainPartyId(item.StoreMainPartyId);
                baseMemberDescriptions.Add(new BaseMemberDescriptionModelItem
                {
                    Description = FormatHelper.GetMemberDescriptionText(item.Text),
                    FromUserName = fromUser.UserName,
                    ToUserName = toUser.UserName,
                    StoreID = item.StoreMainPartyId,
                    StoreName = store.StoreName,
                    LastDate = item.RemindDate,
                    InputDate = item.CreatedDate,
                    ID = item.StoreSeoNotificationId,
                    Title = item.ConstantId.HasValue ? _constantService.GetConstantByConstantId(item.ConstantId.Value).ConstantName : ""

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