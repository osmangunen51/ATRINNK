using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.FormatHelpers;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class MemberDescriptionController : BaseController
    {
        #region Fields

        IMemberDescriptionService _memberDescService;
        IMemberStoreService _memberStoreService;
        IPreRegistirationStoreService _preRegistrationStoreService;


        #endregion

        #region Ctor

        public MemberDescriptionController(IMemberDescriptionService memberDesc, IMemberStoreService memberStoreService,
               IPreRegistirationStoreService preRegistrationStoreService)
        {
            this._memberStoreService = memberStoreService;
            this._memberDescService = memberDesc;
            this._preRegistrationStoreService = preRegistrationStoreService;
        }

        #endregion

        #region Methods

        //MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        public ActionResult Index(string page, string order, string type, string opr, int? UserId)
        {
            int totalPages = 0;

            BaseMemberDescriptionModelNew model = new BaseMemberDescriptionModelNew();
            List<BaseMemberDescriptionModelItem> baseList = new List<BaseMemberDescriptionModelItem>();

            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 18 || g.UserGroupId == 16 select new { u.UserName, u.UserId };
            //var users = entities.Users.OrderBy(x => x.UserName).ToList();
            int userId = 0;
            if (UserId != null)
                userId = Convert.ToInt32(UserId);

            model.AutherizedId = userId;
            var users = users1.ToList();
            int PAGE = 1;
            if (string.IsNullOrEmpty(order)) order = "InputDate";
            if (!string.IsNullOrEmpty(page) && page != "0") PAGE = Convert.ToInt32(page);
            int TakeWhere = ((int)PAGE * 100) - 100;
            if (string.IsNullOrEmpty(type))
            {
                type = "all";
            }

            var selectListAll = new SelectListItem { Text = "Tümü", Value = "/MemberDescription/Index?UserId=0&order=" + order + "&type=" + type + "&opr=" + opr + "&page=1" };
            if (model.AutherizedId == 0)
                selectListAll.Selected = true;
            model.Users.Add(selectListAll);
            foreach (var item in users)
            {
                var selectListItem = new SelectListItem { Text = item.UserName, Value = "/MemberDescription/Index?UserId=" + item.UserId.ToString() + "&order=" + order + "&type=" + type + "&opr=" + opr + "&page=1" };
                if (model.AutherizedId == item.UserId)
                    selectListItem.Selected = true;
                model.Users.Add(selectListItem);

            }
            if (order == "InputDate")
            {
                var List = _memberDescService.GetMemberDateDesc(TakeWhere, 100, type, userId, out totalPages);
                foreach (var item in List)
                {
                    BaseMemberDescriptionModelItem baseMember = new BaseMemberDescriptionModelItem();
                    baseMember.Description = item.Description;
                    if (item.Description != null)
                    {
                        baseMember.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                    }
                    baseMember.ID = item.ID;
                    baseMember.InputDate = Convert.ToDateTime(item.Date);
                    baseMember.UserId = item.UserId;
                    baseMember.Color = item.UserColor;
                    baseMember.PortfoyName = item.UserName;
                    baseMember.FromUserName = item.FromUserName;

                    if (item.UpdateDate != null)
                    {
                        baseMember.LastDate = item.UpdateDate.ToDateTime();
                    }
                    else
                    {
                        baseMember.LastDate = DBNull.Value.ToDateTime();
                    }

                    baseMember.Title = item.Title;
                    baseMember.MainPartyId = Convert.ToInt32(item.MainPartyId);
                    baseMember.StoreID = item.StoreMainPartyId;
                    if (item.StoreName != null)
                        baseMember.StoreName = item.StoreName.ToString();
                    else baseMember.StoreName = "";
                    baseMember.MemberMainPartyId = item.MemberMainPartyId;
                    baseMember.MemberName = item.MemberName;
                    baseMember.MemberSurname = item.MemberSurname;
                    baseMember.MemberType = item.MemberType;
                    baseList.Add(baseMember);
                }
            }
            else
            {
                var List = _memberDescService.GetMemberDescCloseDate(TakeWhere, 100, type, userId, out totalPages);
                foreach (var item in List)
                {
                    BaseMemberDescriptionModelItem baseMember = new BaseMemberDescriptionModelItem();
                    baseMember.Description = item.Description;
                    if (item.Description != null)
                    {
                        baseMember.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                    }
                    baseMember.ID = item.ID;
                    baseMember.InputDate = item.Date;
                    baseMember.UserId = item.UserId;
                    if (item.UpdateDate != null)
                    {
                        baseMember.LastDate = item.UpdateDate.ToDateTime();
                    }
                    baseMember.Title = item.Title;
                    baseMember.MainPartyId = Convert.ToInt32(item.MainPartyId);
                    baseMember.PortfoyName = item.UserName;
                    baseMember.FromUserName = item.FromUserName;
                    if (item.StoreName != null)
                        baseMember.StoreName = item.StoreName.ToString();
                    else baseMember.StoreName = "";

                    baseMember.MemberMainPartyId = item.MemberMainPartyId;
                    baseMember.MemberName = item.MemberName;
                    baseMember.MemberSurname = item.MemberSurname;
                    baseMember.MemberType = item.MemberType;
                    baseMember.StoreID = item.StoreMainPartyId;
                    baseMember.Color = item.UserColor;
                    //if (baseList.FirstOrDefault(x => x.MainPartyId == item.MainPartyId) == null)
                    baseList.Add(baseMember);

                }
            }
            model.CurrentPage = page.ToInt32();
            model.TotalPage = Convert.ToInt32(Math.Ceiling((float)totalPages / 100));


            model.TotalCount = totalPages;
            model.Order = order;
            model.Type = type;


            model.BaseMemberDescriptionModelItems = baseList;
            return View(model);
        }

        [HttpPost]
        public ActionResult index(string searchTxt)
        {
            var searchList = entities.SP_GetBaseMemberDescBySearchText(searchTxt);
            List<BaseMemberDescriptionModel> baseList = new List<BaseMemberDescriptionModel>();
            foreach (var item in searchList)
            {
                BaseMemberDescriptionModel memberDesc = new BaseMemberDescriptionModel();
                memberDesc.Description = item.Description;
                memberDesc.MainPartyId = Convert.ToInt32(item.MainPartyId);
                memberDesc.Member = entities.Members.First(x => x.MainPartyId == item.MainPartyId);
                memberDesc.Title = item.Title;

                if (item.UpdateDate != null)
                {
                    memberDesc.LastDate = item.UpdateDate.ToDateTime();
                }

                memberDesc.StoreName = item.StoreName;
                memberDesc.InputDate = Convert.ToDateTime(item.Date);
                memberDesc.DescriptionDegree = item.DescriptionDegree;
                baseList.Add(memberDesc);
            }
            ViewData["title"] = searchTxt + " Arama Sonuçları";
            ViewData["type"] = "all";
            ViewData["Curr"] = 1;
            ViewData["order"] = "InputDate";
            ViewData["Sayfa"] = 0;
            ViewData["Tot"] = baseList.Count;
            return View(baseList);
        }

        public ActionResult noUpdateDate(int? page)
        {
            if (page == null) page = 1;
            int TakeWhere = ((int)page * 100) - 100;

            var List = _memberDescService.GetMemberDescNoUpdateDate(TakeWhere, 100);
            List<BaseMemberDescriptionModel> descList = new List<BaseMemberDescriptionModel>();
            foreach (var item in List)
            {
                BaseMemberDescriptionModel model = new BaseMemberDescriptionModel();
                model.Description = item.Description;
                model.DescriptionDegree = item.DescriptionDegree;
                model.ID = Convert.ToInt32(item.descId);
                if (item.MainPartyId.HasValue)
                    model.MainPartyId = item.MainPartyId.Value;
                model.Member = entities.Members.First(x => x.MainPartyId == item.MainPartyId);
                model.StoreName = "";
                if (item.StoreName != null)
                {
                    model.StoreName = item.StoreName.ToString();
                }
                if (item.Date != null)
                    model.InputDate = item.Date;
                model.Title = item.Title;
                descList.Add(model);
            }
            int totalPages = entities.MemberDescriptions.Where(x => x.UpdateDate == null).ToList().Count;
            ViewData["Sayfa"] = Convert.ToInt32(Math.Ceiling((float)totalPages / 100));
            ViewData["Curr"] = page;
            ViewData["Tot"] = totalPages;
            return View(descList);
        }

        [HttpPost]

        public JsonResult UpdateDate(int ID)
        {
            var memberDesc = entities.BaseMemberDescriptions.First(x => x.ID == ID);
            memberDesc.Date = DateTime.Now;
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(string order, string page, string type)
        {
            int descId = 0;
            if (Request.QueryString["descId"] != null)
            {
                descId = Request.QueryString["descId"].ToInt32();
            }
            short check = Request.QueryString["check"].ToSByte();
            if (order == "InputDate")
            {
                var description = entities.BaseMemberDescriptions.First(x => x.ID == descId);
                description.DescriptionDegree = check;
                entities.SaveChanges();
                var baseDesc = entities.MemberDescriptions.FirstOrDefault(x => x.MainPartyId == description.MainPartyId && x.Title == description.Title);
                if (baseDesc != null)
                {
                    baseDesc.DescriptionDegree = check;
                    entities.SaveChanges();
                }
            }
            else
            {
                var description1 = entities.MemberDescriptions.Where(x => x.descId == descId).SingleOrDefault();
                description1.DescriptionDegree = check;
                entities.SaveChanges();
                var memberDesc = entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == description1.MainPartyId);
                if (memberDesc != null)
                {
                    memberDesc.DescriptionDegree = check;
                    entities.SaveChanges();
                }
            }
            return RedirectToAction("index", "MemberDescription", new { order = order, type = type, page = page });

        }
        [HttpPost]
        public JsonResult getNotification1()
        {
            int totalRecord = 0;
            int maxCount = 10000;
            var List = _memberDescService.GetMemberDescByOnDate(Convert.ToInt32(CurrentUserModel.CurrentManagement.UserId), 0, maxCount, 1, (byte)MemberDescriptionOrder.Defaullt, 0, out totalRecord);

            return Json(totalRecord, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getNotification()
        {
            int totalRecord = 0;
            var List = _memberDescService.GetMemberDescByOnDate(0, 0, 1000, 1, (byte)MemberDescriptionOrder.Defaullt, 0, out totalRecord).Where(x => x.Status == null || x.Status == 0 && x.UserId != 76 && x.UserId != 77 && x.UserId != 75);
            return Json(new { count = totalRecord });
        }

        public ActionResult Notification(string UserId)
        {
            NotificationModelBase model = new NotificationModelBase();
            var memberDescs = new List<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionForStore>();
            int pageDimension = 30;
            int userId = 0;
            int totalRecord = 0;
            if (!string.IsNullOrEmpty(UserId))
            {

                userId = Convert.ToInt32(UserId);


            }
            else
            {
                PAGEID = PermissionPage.AllNotification;
            }
            memberDescs = _memberDescService.GetMemberDescByOnDate(userId, 0, pageDimension, 1, (byte)MemberDescriptionOrder.Defaullt, 0, out totalRecord).ToList();
            PrepareUserTypeModel(model);
            int totalCount = memberDescs.Count;
            var listNotif = PrepareNotificationList(memberDescs, pageDimension);
            FilterModel<NotificationModel> filterModel = new FilterModel<NotificationModel>();
            filterModel.TotalRecord = totalRecord;
            filterModel.CurrentPage = 1;
            filterModel.PageDimension = pageDimension;
            filterModel.Source = listNotif;
            model.Notifications = filterModel;
            var users = entities.Users.Where(x => x.Active == true).OrderBy(x => x.UserName).ToList();
            model.Users.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in users)
            {
                var selectList = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                if (item.UserId.ToString() == UserId)
                {
                    selectList.Selected = true;
                }
                model.Users.Add(selectList);
            }
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();
            var constantList = new List<SelectListItem>();
            constantList.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in constants)
            {
                var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };
                constantList.Add(selectListItem);
            }
            model.Titles = constantList;
            return View(model);
        }
        [HttpPost]
        public PartialViewResult Notification(int userId, int page, int userGroupId, int orderBy, int constantId)
        {
            int pageDimension = 30;
            var memberDescs = new List<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionForStore>();
            int totalRecord = 0;

            memberDescs = _memberDescService.GetMemberDescByOnDate(userId, userGroupId, pageDimension, page, orderBy, constantId, out totalRecord).ToList();
            memberDescs = memberDescs.OrderByDescending(x => x.IsFirst).ThenBy(x => x.UpdateDate).ToList();
            var listNotif = PrepareNotificationList(memberDescs, pageDimension);
            FilterModel<NotificationModel> filterModel = new FilterModel<NotificationModel>();
            filterModel.TotalRecord = totalRecord;
            filterModel.CurrentPage = page;
            filterModel.PageDimension = pageDimension;
            filterModel.Source = listNotif;
            return PartialView("_NotificationList", filterModel);
        }
        public void PrepareUserTypeModel(NotificationModelBase model)
        {
            var userTypes = entities.UserGroups.ToList();
            model.UserTypes.Add(new SelectListItem { Text = "Kullanıcı Tipi", Value = "0" });
            foreach (var item in userTypes)
            {
                model.UserTypes.Add(new SelectListItem { Text = item.GroupName, Value = item.UserGroupId.ToString() });
            }
        }
        public List<NotificationModel> PrepareNotificationList(List<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionForStore> memberDescs, int pageDimension)
        {
            List<NotificationModel> listNotif = new List<NotificationModel>();
            foreach (var item in memberDescs)
            {


                NotificationModel not = new NotificationModel();
                not.MainPartyId = item.MainPartyId.HasValue ? item.MainPartyId.Value : 0;
                if (not.MainPartyId != 0)
                {
                    var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(not.MainPartyId);
                    if (memberStore != null)
                        not.StoreMainPartyId = Convert.ToInt32(memberStore.StoreMainPartyId);
                    else
                        not.StoreMainPartyId = 9999999;
                }
                not.Title = item.Title;
                not.InputDate = item.Date;
                not.LastDate = item.UpdateDate;
                not.ID = Convert.ToInt32(item.descId);
                not.Description = item.Description;
                not.IsImmediate = item.IsImmediate.HasValue ? item.IsImmediate.Value : false;
                if (item.Description != null)
                {
                    not.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                }
                var member = entities.Members.FirstOrDefault(x => x.MainPartyId == item.MainPartyId);
                string memberName = "", memberSurname = "", storeName = "";
                if (member == null)
                {
                    if (item.PreRegistrationStoreId.HasValue)
                    {
                        var preRegistration = _preRegistrationStoreService.GetPreRegistirationStoreByPreRegistrationStoreId(item.PreRegistrationStoreId.Value);
                        if (preRegistration != null)
                        {
                            memberName = preRegistration.MemberName;
                            memberSurname = preRegistration.MemberSurname;
                            storeName = preRegistration.StoreName;
                        }

                    }

                }
                else
                {
                    memberName = member.MemberName;
                    memberSurname = member.MemberSurname;
                    var anyStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == member.MainPartyId);
                    if (anyStore != null)
                    {
                        var aStore = entities.Stores.FirstOrDefault(x => x.MainPartyId == anyStore.StoreMainPartyId);
                        if (aStore != null)
                            storeName = aStore.StoreName;
                    }

                }
                not.MemberName = memberName + " " + memberSurname;

                not.StoreName = storeName;
                if (item.Status != null || item.Status != 0)
                {
                    not.Status = Convert.ToInt32(item.Status);
                }
                var user = entities.Users.FirstOrDefault(x => x.UserId == item.UserId);
                if (user != null)
                    not.SalesPersonName = user.UserName;
                if (item.FromUserId != null)
                {
                    var fromUser = entities.Users.FirstOrDefault(x => x.UserId == item.FromUserId);
                    if (fromUser != null)
                        not.FromUserName = fromUser.UserName;
                }


                if (!string.IsNullOrEmpty(Request.QueryString["UserId"]))
                {
                    bool isFirst = false;
                    if (item.IsFirst == true)
                        isFirst = true;
                    not.IsFirst = isFirst;
                }
                listNotif.Add(not);
            }

            return listNotif;
        }
        public ActionResult onSeen(int ID)
        {
            var memberDescUp = entities.MemberDescriptions.First(x => x.descId == ID);
            memberDescUp.Status = 1;
            entities.SaveChanges();
            return RedirectToAction("Notification");
        }
        public ActionResult FirstProcess(int id, int UserId, int isFirst)
        {
            var memberDesc = entities.MemberDescriptions.FirstOrDefault(x => x.descId == id);
            bool IsFirst = false;
            if (isFirst == 1)
                IsFirst = true;
            memberDesc.IsFirst = IsFirst;
            entities.SaveChanges();
            return Redirect("/MemberDescription/Notification?UserId=" + UserId);
        }
        public ActionResult UpdateDescriptionDate(int id)
        {
            UpdateDateDescriptionModel model = new UpdateDateDescriptionModel();
            model.ID = id;
            var memberDesc = entities.MemberDescriptions.FirstOrDefault(x => x.descId == id);
            model.LastDate = memberDesc.UpdateDate.ToDateTime().ToString("dd.MM.yyyy");

            model.Content = memberDesc.Description;
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateDescriptionDate(UpdateDateDescriptionModel model)
        {

            int year = 0, day = 0, month = 0;
            string[] time1 = model.LastDate.ToString().Split('.');

            year = Convert.ToInt32(time1[2]);
            month = Convert.ToInt32(time1[1]);
            day = Convert.ToInt32(time1[0]);

            int hour = 0, minute = 0;
            if (!string.IsNullOrEmpty(model.Hour))
            {
                hour = Convert.ToInt32(model.Hour.Split(':')[0]);
                minute = Convert.ToInt32(model.Hour.Split(':')[1]);

            }
            else
            {
                hour = DateTime.Now.Hour;
                minute = DateTime.Now.Minute + 1;

            }

            DateTime lastDate = new DateTime(year, month, day, hour, minute, 0);
            if (DateTime.Now > lastDate)
            {

                var memberDesc = entities.MemberDescriptions.FirstOrDefault(x => x.descId == model.ID);
                ModelState.AddModelError("LastDate", "Tarih Şu Anki Tarihten Küçük olamaz");
                model.Content = memberDesc.Description;
                return View(model);
            }
            else
            {
                var memberDesc = entities.MemberDescriptions.FirstOrDefault(x => x.descId == model.ID);
                memberDesc.UpdateDate = lastDate;
                //memberDesc.Description = model.Content;
                var baseMemberDesc = entities.BaseMemberDescriptions.Where(x => x.MainPartyId == memberDesc.MainPartyId).OrderByDescending(x => x.ID).FirstOrDefault();
                baseMemberDesc.Title = memberDesc.Title;
                //baseMemberDesc.Description = memberDesc.Description;
                baseMemberDesc.UpdateDate = lastDate;
                baseMemberDesc.Description = model.Content;
                entities.SaveChanges();

                ViewData["basarili"] = "Tarih Güncellenmiştir";
                model.Content = memberDesc.Description;

                return View(model);

            }

        }

        public ActionResult AllTask()
        {
            int page = 1;
            int pageSize = 100;
            string OrderColumn = "descId";
            byte OrderType = 0;
            var any = CurrentUserModel.CurrentManagement.Permissions.Any(c => c.PermissionId == PermissionPage.AllNotification);
            int UserId = CurrentUserModel.CurrentManagement.UserId;
            if (any == true)
            {
                UserId = 0;
            }
            ViewData["UserId"] = UserId.ToString();

            int TotalRecord;
            DateTime date = Convert.ToDateTime("2001.1.1");

            var list = _memberDescService.SP_GetMemberDescriptionSearch(page, pageSize, UserId, date, OrderColumn, OrderType, 0, "", out TotalRecord);
            foreach (var item in list)
            {
                if (item.Description != null)
                {
                    item.Description = FormatHelper.GetMemberDescriptionText(item.Description);
                }
            }
            FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem> model = new FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem>();

            model.Source = list;
            model.TotalRecord = TotalRecord;
            model.CurrentPage = page;
            model.PageDimension = pageSize;
            model.OrderName = OrderColumn;
            model.Order = OrderType.ToString();

            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where p.UserGroupId == 16 || p.UserGroupId == 18 || p.UserGroupId == 20 select new { u.UserName, u.UserId };

            List<SelectListItem> users = new List<SelectListItem>();
            if (any == true)
            {
                var selectListAll = new SelectListItem { Text = "Tümü", Value = "0" };

                selectListAll.Selected = true;
                users.Add(selectListAll);
                foreach (var item in users1)
                {
                    var selectListItem = new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() };
                    users.Add(selectListItem);

                }

            }


            ViewBag.Users = users;
            List<SelectListItem> constantList = new List<SelectListItem>();
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.StoreDescriptionType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();

            constantList.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in constants)
            {
                var selectListItem = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };
                constantList.Add(selectListItem);
            }
            ViewBag.Constants = constantList;
            ViewData["CurrentUserName"] = CurrentUserModel.CurrentManagement.UserName;
            return View(model);
        }
        [HttpPost]
        public PartialViewResult AllTask(int UserId, string OrderColumn, byte OrderType, string Date, int Page, int ConstantId, string CreatedDate)
        {

            int TotalRecord;
            DateTime date = Convert.ToDateTime("2001.1.1");
            if (!string.IsNullOrEmpty(Date)) date = Convert.ToDateTime(Date);




            var list = _memberDescService.SP_GetMemberDescriptionSearch(Page, 100, UserId, date, OrderColumn, OrderType, ConstantId, CreatedDate, out TotalRecord);

            foreach (var item in list)
            {
                item.Description = FormatHelper.GetMemberDescriptionText(item.Description);
            }
            FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem> model = new FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem>();

            model.Source = list;
            model.TotalRecord = TotalRecord;
            model.CurrentPage = Page;
            model.PageDimension = 100;
            model.OrderName = OrderColumn;
            model.Order = OrderType.ToString();
            var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.GroupName == "Portföy Yöneticisi" select new { u.UserName, u.UserId };
            ViewData["CurrentUserName"] = CurrentUserModel.CurrentManagement.UserName;
            return PartialView("_AllTaskListItem", model);
        }

        public ActionResult NotificationCount()
        {
            var list = _memberDescService.SP_GetMemberDescriptionCount();
            int totalCount = 0;
            var listNew = list.GroupBy(x => x.UpdateDateNew).Select(x => new { totalCount = x.Sum(a => a.TotalCount), date1 = x.Key });
            totalCount = listNew.Count();
            listNew = listNew.OrderBy(x => x.date1).Skip(1 * 50 - 50).Take(50);
            var userIds = list.Select(c => c.UserId).Distinct().ToList();
            List<MemberDescriptionCountModel> collection = new List<MemberDescriptionCountModel>();
            foreach (var item2 in listNew)
            {
                var itemModel = new MemberDescriptionCountModel { TotalCount = item2.totalCount, UpdateDateNew = item2.date1 };
                var users1 = entities.Users.Where(x => userIds.Contains(x.UserId));

                //  var users1 = from u in entities.Users join p in entities.PermissionUsers on u.UserId equals p.UserId join g in entities.UserGroups on p.UserGroupId equals g.UserGroupId where g.UserGroupId == 16 || g.UserGroupId == 18 || g.UserGroupId == 20 select new { u.UserName, u.UserId };
                foreach (var item in users1)
                {
                    itemModel.Usercounts.Add(new Usercount { Count = 0, UserName = item.UserName });
                }
                var userCounts = from l in list join u in users1 on l.UserId equals u.UserId where l.UpdateDateNew == item2.date1 select new { username = u.UserName, l.TotalCount };
                foreach (var usercount in userCounts)
                {
                    var userCheck = itemModel.Usercounts.FirstOrDefault(x => x.UserName == usercount.username);
                    if (userCheck != null)
                        userCheck.Count = usercount.TotalCount;
                }
                collection.Add(itemModel);
            }
            var model = new FilterModel<MemberDescriptionCountModel>
            {
                CurrentPage = 1,
                TotalRecord = totalCount,
                Source = collection,
                PageDimension = 50
            };
            return View(model);
        }
        [HttpPost]
        public PartialViewResult NotificationCount(int CurrentPage)
        {
            var list = _memberDescService.SP_GetMemberDescriptionCount();
            int totalCount = 0;
            var listNew = list.GroupBy(x => x.UpdateDateNew).Select(x => new { totalCount = x.Sum(a => a.TotalCount), date1 = x.Key });
            totalCount = listNew.Count();
            var userIds = list.Select(c => c.UserId).Distinct().ToList();
            listNew = listNew.OrderBy(x => x.date1).Skip(CurrentPage * 50 - 50).Take(50);
            List<MemberDescriptionCountModel> collection = new List<MemberDescriptionCountModel>();
            foreach (var item2 in listNew)
            {
                var itemModel = new MemberDescriptionCountModel { TotalCount = item2.totalCount, UpdateDateNew = item2.date1 };
                var users1 = entities.Users.Where(x => userIds.Contains(x.UserId));

                foreach (var item in users1)
                {
                    itemModel.Usercounts.Add(new Usercount { Count = 0, UserName = item.UserName });
                }
                var userCounts = from l in list join u in users1 on l.UserId equals u.UserId where l.UpdateDateNew == item2.date1 select new { username = u.UserName, l.TotalCount };
                foreach (var usercount in userCounts)
                {
                    var userCheck = itemModel.Usercounts.FirstOrDefault(x => x.UserName == usercount.username);
                    if (userCheck != null)
                        userCheck.Count = usercount.TotalCount;

                }
                collection.Add(itemModel);
            }


            var model = new FilterModel<MemberDescriptionCountModel>
            {
                CurrentPage = CurrentPage,
                TotalRecord = totalCount,
                Source = collection,
                PageDimension = 50
            };

            return PartialView("_NotificationCountList", model);
        }

        public string UpdateDescs()
        {
            _memberDescService.SP_UpdateMemberDescriptions();
            return "Güncelleştirme Başarılı";
        }
        #endregion
    }
}
