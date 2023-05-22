using global::Trinnk.Services.Users;
using Trinnk.Core;
using Trinnk.Services.Common;
using Trinnk.Services.Messages;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.Trinnk.Core.Web.Helpers;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.Authentication;
using NeoSistem.Trinnk.Management.Models.Entities;
using NeoSistem.Trinnk.Management.Models.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Controllers
{
    public class UserController : BaseController
    {
        #region Constants

        private const string STARTCOLUMN = "UserName";
        private const string ORDER = "Asc";
        private const int PAGEDIMENSION = 20;

        #endregion Constants

        private static Data.User dataUser = null;
        private static ICollection<UserModel> collection = null;
        public readonly IUserMailTemplateService _userTemplateService;
        public readonly IConstantService _constantService;
        private readonly IUserInformationService _userInformationService;
        private readonly IUserFileService _userFileService;

        public UserController(IUserMailTemplateService userTemplateService, IConstantService constantService,
            IUserInformationService userInformationService, IUserFileService userFileService)
        {
            this._userTemplateService = userTemplateService;
            this._constantService = constantService;
            this._userInformationService = userInformationService;
            this._userFileService = userFileService;
        }

        #region Methods

        public ActionResult Index()
        {
            PAGEID = PermissionPage.Uyeler;

            int total = 0;
            dataUser = new NeoSistem.Trinnk.Data.User();

            var userIsAdmin = entities.PermissionUsers.FirstOrDefault(x => x.UserGroupId == 7 && x.UserId == CurrentUserModel.CurrentManagement.UserId);
            ViewData["Admin"] = userIsAdmin != null ? true : false;

            collection = dataUser.Search(ref total, PAGEDIMENSION, 1, string.Empty, STARTCOLUMN, ORDER).AsCollection<UserModel>();

            var permissons = CurrentUserModel.CurrentManagement.Permissions.ToList();

            var model = new FilterModel<UserModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int page)
        {
            PAGEID = PermissionPage.Uyeler;

            int total = 0;
            dataUser = new NeoSistem.Trinnk.Data.User();
            var userIsAdmin = entities.PermissionUsers.FirstOrDefault(x => x.UserGroupId == 7 && x.UserId == CurrentUserModel.CurrentManagement.UserId);
            ViewData["Admin"] = userIsAdmin != null ? true : false;
            collection = dataUser.Search(ref total, PAGEDIMENSION, page, string.Empty, STARTCOLUMN, ORDER).AsCollection<UserModel>();

            var model = new FilterModel<UserModel>
            {
                CurrentPage = page,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return PartialView("UserList", model);
        }

        [ValidateInput(false)]
        public ActionResult Create()
        {
            PAGEID = PermissionPage.UyeEkle;
            UserFormModel model = new UserFormModel();
            ViewData["Title"] = "Avansas - Kullanıcı Ekle ";
            var userModel = new UserModel { };
            userModel.MailSmtp = AppSettings.MailHost;
            userModel.SendCode = AppSettings.MailPort;
            var dataPermissionUser = new Data.PermissionUser();
            var userGroups = entities.UserGroups.ToList();
            var userGroupModel = new List<UserGroupModel>();
            foreach (var item in userGroups)
            {
                userGroupModel.Add(new UserGroupModel
                {
                    GroupName = item.GroupName,
                    UserGroupId = item.UserGroupId
                });
            }
            userModel.Groups = userGroupModel;
            model.UserModel = userModel;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UserModel model, UserInformationModel userInformationModel, FormCollection collection)
        {
            var curUser = new User
            {
                UserName = model.UserName,
                UserPass = model.UserPass,
                MailPassword = model.MailPassword,
                MailSmtp = model.MailSmtp,
                SendCode = model.SendCode,
                UserMail = model.UserMail,
                UserColor = model.UserColor,
                Active = true,
                ActiveForDesc = true,
                Name = model.Name,
                Surname = model.Surname,
                CallCenterUrl = model.Surname,
                MemberDescriptionTransferState = model.MemberDescriptionTransferState,
                Signature = model.Signature,
                CreatedDate=DateTime.Now,
                LastLoginDate= DateTime.Now,
                LastActivityDate= DateTime.Now,
            };

            entities.Users.AddObject(curUser);
            int userGroupId = 0;
            if (collection["Permission"] != null)
            {
                var values = collection["Permission"].Split(',');
                var permissionUser = new Classes.PermissionUser();
                foreach (var item in values)
                {
                    if (item.ToInt32() > 0)
                    {
                        permissionUser = new Classes.PermissionUser
                        {
                            UserGroupId = item.ToInt32(),
                            UserId = curUser.UserId,
                        };
                        userGroupId = item.ToInt32();
                        permissionUser.Save();
                    }
                }
            }
            entities.SaveChanges();
            var userInformation = new global::Trinnk.Entities.Tables.Users.UserInformation();
            userInformation.Address = userInformationModel.Adress;
            if (!string.IsNullOrEmpty(userInformationModel.BirthDate))
                userInformation.BirthDate = Convert.ToDateTime(userInformationModel.BirthDate);
            userInformation.DriverLicense = userInformationModel.DriverLicense;
            userInformation.Education = userInformationModel.Education;
            if (!string.IsNullOrEmpty(userInformationModel.EndDate))
                userInformation.EndWorkDate = Convert.ToDateTime(userInformationModel.EndDate);
            userInformation.Gender = Convert.ToBoolean(userInformationModel.Gender);
            userInformation.IdentityNumber = userInformationModel.IdentityNumber;
            userInformation.MarialStatus = userInformationModel.MarialStatus;
            userInformation.NameSurname = userInformationModel.NameSurname;
            userInformation.NumberOfChildren = userInformationModel.NumberOfChildren;
            userInformation.PhoneNumber = userInformationModel.PhoneNumber;
            if (!string.IsNullOrEmpty(userInformationModel.StartWorkDate))
                userInformation.StartWorkDate = Convert.ToDateTime(userInformationModel.StartWorkDate);
            userInformation.UpdateDate = DateTime.Now;
            userInformation.RecordDate = DateTime.Now;
            userInformation.UserId = curUser.UserId;
            userInformation.UserGroupId = userGroupId;
            userInformation.BankAccountNumber = userInformationModel.BankAccountNumber;
            userInformation.SecondPhoneNumber = userInformation.SecondPhoneNumber;
            userInformation.WhoSecondPhoneNumber = userInformation.WhoSecondPhoneNumber;
            _userInformationService.InsertUserInformation(userInformation);
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.UyeDuzenle;
            Classes.User curUser = new Classes.User();
            UserFormModel model = new UserFormModel();
            bool hasRecord = curUser.LoadEntity(id);
            var userModel = new UserModel
            {
                Name = curUser.Name,
                Surname = curUser.Surname,
                UserName = curUser.UserName,
                UserPass = curUser.UserPass,
                MailPassword = curUser.MailPassword,
                MailSmtp = curUser.MailSmtp,
                SendCode = curUser.SendCode,
                UserMail = curUser.UserMail,
                UserColor = curUser.UserColor,
                Active = curUser.Active,
                ActiveForDesc = curUser.ActiveForDesc,
                MemberDescriptionTransferState = curUser.MemberDescriptionTransferState,
                Signature = curUser.Signature,
                CallCenterUrl = curUser.CallCenterUrl,
            };
            if (curUser.MailSmtp == "")
            {
                curUser.MailSmtp = AppSettings.MailHost;
                curUser.SendCode = AppSettings.MailPort;
            }
            var dataPermissionUser = new Data.PermissionUser();
            userModel.Groups = dataPermissionUser.GetItemsByUserId(curUser.UserId).AsCollection<UserGroupModel>();
            model.UserModel = userModel;
            var userInformation = _userInformationService.GetUserInformationByUserId(id);
            var userInformationModel = new UserInformationModel();
            if (userInformation != null)
            {
                userInformationModel.Adress = userInformation.Address;
                userInformationModel.BirthDate = userInformation.BirthDate.HasValue ? userInformation.BirthDate.Value.ToString("dd.MM.yyyy") : "";
                userInformationModel.DriverLicense = userInformation.DriverLicense;
                userInformationModel.Education = userInformation.Education;
                userInformationModel.EndDate = userInformation.EndWorkDate.HasValue ? userInformation.EndWorkDate.Value.ToString("dd.MM.yyyy") : "";
                userInformationModel.Gender = Convert.ToByte(userInformation.Gender);
                userInformationModel.IdentityNumber = userInformation.IdentityNumber;
                userInformationModel.MarialStatus = userInformation.MarialStatus;
                userInformationModel.NameSurname = userInformation.NameSurname;
                userInformationModel.NumberOfChildren = userInformation.NumberOfChildren;
                userInformationModel.PhoneNumber = userInformation.PhoneNumber;
                userInformationModel.StartWorkDate = userInformation.StartWorkDate.ToString("dd.MM.yyyy");
                userInformationModel.BankAccountNumber = userInformation.BankAccountNumber;
                userInformationModel.SecondPhoneNumber = userInformation.SecondPhoneNumber;
                userInformationModel.WhoSecondPhoneNumber = userInformation.WhoSecondPhoneNumber;
            }
            model.UserInformationModel = userInformationModel;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, UserModel model, UserInformationModel userInformationModel, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var curUser = entities.Users.FirstOrDefault(x => x.UserId == id);
                if (curUser != null)
                {
                    curUser.Name = model.Name;
                    curUser.Surname = model.Surname;
                    curUser.UserName = model.UserName;
                    curUser.UserPass = model.UserPass;
                    curUser.UserMail = model.UserMail;
                    curUser.MailSmtp = model.MailSmtp;
                    curUser.MailPassword = model.MailPassword;
                    curUser.SendCode = model.SendCode;
                    curUser.UserColor = model.UserColor;
                    curUser.Active = model.Active;
                    curUser.ActiveForDesc = model.ActiveForDesc;
                    curUser.Signature = model.Signature;
                    curUser.CallCenterUrl = model.CallCenterUrl;
                    curUser.MemberDescriptionTransferState = model.MemberDescriptionTransferState;
                    entities.SaveChanges();
                }

                int userGroupId = 0;
                var curPermissionUser = new Classes.PermissionUser();
                curPermissionUser.DeleteByUserId(curUser.UserId);
                if (!string.IsNullOrEmpty(collection["Permission"]))
                {
                    var values = collection["Permission"].Split(',');

                    var permissionUser = new Classes.PermissionUser();

                    foreach (var item in values)
                    {
                        if (item.ToInt32() > 0)
                        {
                            permissionUser = new Classes.PermissionUser
                            {
                                UserGroupId = item.ToInt32(),
                                UserId = curUser.UserId
                            };

                            userGroupId = item.ToInt32();
                            permissionUser.Save();
                        }
                    }
                }
                var userInformation = _userInformationService.GetUserInformationByUserId(id);
                bool anyUser = true;
                if (userInformation == null)
                {
                    anyUser = false;
                    userInformation = new global::Trinnk.Entities.Tables.Users.UserInformation();
                }
                userInformation.Address = userInformationModel.Adress;
                if (!string.IsNullOrEmpty(userInformationModel.BirthDate))
                    userInformation.BirthDate = Convert.ToDateTime(userInformationModel.BirthDate);
                userInformation.DriverLicense = userInformationModel.DriverLicense;
                userInformation.Education = userInformationModel.Education;
                if (!string.IsNullOrEmpty(userInformationModel.EndDate))
                    userInformation.EndWorkDate = Convert.ToDateTime(userInformationModel.EndDate);
                userInformation.Gender = Convert.ToBoolean(userInformationModel.Gender);
                userInformation.IdentityNumber = userInformationModel.IdentityNumber;
                userInformation.MarialStatus = userInformationModel.MarialStatus;
                userInformation.NameSurname = userInformationModel.NameSurname;
                userInformation.NumberOfChildren = userInformationModel.NumberOfChildren;
                userInformation.PhoneNumber = userInformationModel.PhoneNumber;
                userInformation.SecondPhoneNumber = userInformationModel.SecondPhoneNumber;
                userInformation.WhoSecondPhoneNumber = userInformationModel.WhoSecondPhoneNumber;
                userInformation.BankAccountNumber = userInformationModel.BankAccountNumber;
                if (!string.IsNullOrEmpty(userInformationModel.StartWorkDate))
                    userInformation.StartWorkDate = Convert.ToDateTime(userInformationModel.StartWorkDate);
                userInformation.UpdateDate = DateTime.Now;
                if (!anyUser)
                    userInformation.RecordDate = DateTime.Now;
                userInformation.UserId = curUser.UserId;
                userInformation.UserGroupId = userGroupId;
                if (!anyUser)
                {
                    _userInformationService.InsertUserInformation(userInformation);
                }
                else
                    _userInformationService.UpdateUserInformation(userInformation);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            NeoSistem.Trinnk.Classes.User curUser = new NeoSistem.Trinnk.Classes.User();
            bool hasDeleted = curUser.Delete(id);
            return Json(hasDeleted);
        }

        public ActionResult CreateMailTemplate(int id, string tempId)
        {
            var model = new MailTemplateFormModel();
            if (!string.IsNullOrEmpty(tempId))
            {
                int tempID = Convert.ToInt32(tempId);
                var temp = _userTemplateService.GetUserMailTemplateByUserMailTemplateId(tempID);
                model.MailTemplateId = temp.UserMailTemplateId;
                model.MailContent = temp.MailContent;
                model.SpecialId = temp.SpecialMailId;
                model.Subject = temp.Subject;
                model.UserGroupId = temp.UserGroupId;
            }
            model.UserId = id;
            PrepareMailTemplateForm(model);
            PrepareMailTemplateList(model);
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateMailTemplate(MailTemplateFormModel model)
        {
            if (ModelState.IsValid)
            {
                var userMailTemplate = new global::Trinnk.Entities.Tables.Messages.UserMailTemplate();
                if (model.MailTemplateId != 0)
                    userMailTemplate = _userTemplateService.GetUserMailTemplateByUserMailTemplateId(model.MailTemplateId);

                userMailTemplate.MailContent = model.MailContent;
                userMailTemplate.SpecialMailId = model.SpecialId;
                userMailTemplate.Subject = model.Subject;
                userMailTemplate.UserId = model.UserId;
                userMailTemplate.MailContent = model.MailContent;
                userMailTemplate.UserGroupId = model.UserGroupId;
                if (model.MailTemplateId != 0)
                    _userTemplateService.UpdateUserMailTemplate(userMailTemplate);
                else
                    _userTemplateService.InsertUserMailTemplate(userMailTemplate);
                return RedirectToAction("Index");
            }
            else
            {
                PrepareMailTemplateForm(model);
                PrepareMailTemplateList(model);
                return View(model);
            }
        }

        public void PrepareMailTemplateList(MailTemplateFormModel model)
        {
            var mailTemplateList = _userTemplateService.GetUserMailTemplatesByUserId(model.UserId);
            foreach (var item in mailTemplateList)
            {
                var listItem = new UserMailTemplateListItemModel();
                listItem.Name = _constantService.GetConstantByConstantId((short)item.SpecialMailId).ConstantName;
                listItem.Subject = item.Subject;
                listItem.Id = item.UserMailTemplateId;
                listItem.UserGroupName = entities.UserGroups.FirstOrDefault(x => x.UserGroupId == item.UserGroupId).GroupName;
                model.UserMailTemplateListItemModels.Add(listItem);
            }
        }

        public void PrepareMailTemplateForm(MailTemplateFormModel model)
        {
            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.UserSpecialMailType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName);
            if (model.SpecialId == 0)
                model.SpecialMails.Add(new SelectListItem { Text = "Seçiniz", Selected = true });
            foreach (var item in constants)
            {
                var selectList = new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() };
                if (item.Id == model.SpecialId)
                    selectList.Selected = true;
                model.SpecialMails.Add(selectList);
            }
            var userGroup = entities.UserGroups.ToList();
            if (model.UserGroupId == 0)
                model.UserGroups.Add(new SelectListItem { Text = "Seçiniz", Selected = true });
            foreach (var item in userGroup)
            {
                var selectList = new SelectListItem { Text = item.GroupName, Value = item.UserGroupId.ToString() };
                if (model.UserGroupId == item.UserGroupId)
                    selectList.Selected = true;
                model.UserGroups.Add(selectList);
            }
        }

        [HttpPost]
        public JsonResult DeleteMailTemplate(int id)
        {
            var template = _userTemplateService.GetUserMailTemplateByUserMailTemplateId(id);
            _userTemplateService.DeleteUserMailTemplate(template);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Files(int userId)
        {
            var model = new UserFileModel();

            var fileTypes = new List<SelectListItem>();
            fileTypes.Add(new SelectListItem { Text = "Seçinizi", Value = "0" });
            int counter = 1;
            string[] names = new string[] { "Fotoğraflar", "Nüfus cüzdanı fotokopisi", "İkametgâh Belgesi", "Nüfus Kayıt Örneği", "Aile Durumunu Bildirir Belge", "Askerlik durum belgesi", "Sağlık raporu", "Diploma fotokopisi", "Adli sicil kaydı" };
            foreach (var item in names)
            {
                fileTypes.Add(new SelectListItem { Text = item, Value = counter.ToString() });
                counter++;
            }

            model.FileTypes = fileTypes;
            var userFiles = _userFileService.GetUserFilesByUserId(userId);
            foreach (var item in userFiles)
            {
                model.UserFileItems.Add(new UserFileItemModel
                {
                    FilePath = "/FilesUser/" + item.UserId + "/" + item.FileName,
                    Type = names[item.FileType - 1],
                    UserFileId = item.UserFileId
                });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Files(string userId, byte fileType, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var inputType = InputFileName.Split('.');
                    string oldfile = file.FileName;
                    string uzanti = oldfile.Substring(oldfile.LastIndexOf("."), oldfile.Length - oldfile.LastIndexOf("."));
                    string newFileName = Guid.NewGuid().ToString() + "." + uzanti;
                    string path = Server.MapPath("~/FilesUser/" + userId);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var ServerSavePath = Path.Combine(path + "/" + newFileName);

                    file.SaveAs(ServerSavePath);

                    var userFile = new global::Trinnk.Entities.Tables.Users.UserFile
                    {
                        CreatedDate = DateTime.Now,
                        FileType = fileType,
                        FileName = newFileName,
                        UserId = Convert.ToByte(userId),
                    };
                    _userFileService.InsertUserFile(userFile);
                }
            }
            TempData["success"] = "Başarılı bir şekilde eklenmiştir.";
            return RedirectToAction("Files", new { userId = userId });
        }

        [HttpGet]
        public ActionResult FileDelete(int id)
        {
            var userFile = _userFileService.GetUserFileByUserFileId(id);
            int userId = userFile.UserId;
            var filePath = "/FilesUser/" + userFile.UserId + "/" + userFile.FileName;
            FileHelpers.Delete(filePath);
            _userFileService.DeleteUserFile(userFile);
            return RedirectToAction("Files", new { userId = userId });
        }

        #endregion Methods
    }
}