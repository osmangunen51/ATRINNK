using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Users;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Authentication;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class HelpController : BaseController
    {
        // GET: Help
        private int PageSize = 20;
        private IHelpService _helpService;
        private IConstantService _constantService;


        public HelpController(IHelpService helpService, IConstantService constantService)
        {
            this._helpService = helpService;
            this._constantService = constantService;
        }
        public ActionResult Index()
        {
            HelpListModel model = new HelpListModel();
            model.TotalPage = Convert.ToInt32(Math.Ceiling((decimal)(_helpService.GetAllHelp().Count / PageSize)));
            model.CurrentPage = 1;
            var helps = _helpService.GetHelpByForPaging(PageSize, 0);
            using (MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities())
            {
                var constandList = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.CrmYardimKategori).ToList();
                foreach (var help in helps)
                {
                    HelpModel item = new HelpModel { ConstantId=help.ConstantId,Content = help.Content, ID = help.HelpId, RecordDate = help.RecordDate, Subject = help.Subject };
                    if (item.ConstantId > 0)
                    {
                        item.Constant = constandList.FirstOrDefault(x => x.ConstantId == (short)item.ConstantId);
                    }
                    model.HelpModels.Add(item);
                }
            }
            return View(model);
        }

        public ActionResult Show()
        {
            HelpListModel model = new HelpListModel();
            var helps1 = _helpService.GetHelpByForPaging(10000, 0);
            using (MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities())
            {
                var constandList = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.CrmYardimKategori).ToList();
                foreach (var help in helps1)
                {
                    HelpModel item = new HelpModel { ConstantId = help.ConstantId, Content = help.Content, ID = help.HelpId, RecordDate = help.RecordDate, Subject = help.Subject };
                    if (item.ConstantId > 0)
                    {
                        item.Constant = constandList.FirstOrDefault(x => x.ConstantId == (short)item.ConstantId);
                    }
                    model.WHelpModels.Add(item);
                }
            }
            return View(model);
        }

        public ActionResult Add()
        {
            HelpModel model = new HelpModel();
            return View(model);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Add(HelpModel model)
        {
            if (!string.IsNullOrEmpty(model.Subject))
            {
                global::MakinaTurkiye.Entities.Tables.Users.Help help = new global::MakinaTurkiye.Entities.Tables.Users.Help();
                help.Content = model.Content;
                help.RecordDate = DateTime.Now;
                help.ConstantId = model.ConstantId;
                help.Subject = model.Subject;
                _helpService.InsertHelp(help);
                return RedirectToAction("index");
            }
            else
            {
                ModelState.AddModelError("emptySubject", "Lütfen Konuyu Giriniz");
            }

            return View(model);
        }
        public ActionResult EditHelp(int id)
        {
            var help = _helpService.GetHelpByHelpId(id);
            HelpModel model = new HelpModel();
            model.Content = help.Content;
            model.ConstantId = help.ConstantId;
            model.Subject = help.Subject;
            model.ID = help.HelpId;
            return View(model);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditHelp(HelpModel model)
        {
            var help = _helpService.GetHelpByHelpId(model.ID);
            help.Content = model.Content;
            help.Subject = model.Subject;
            help.ConstantId = model.ConstantId;
            _helpService.UpdateHelp(help);
            ViewBag.Message = "Kayıt Başarıyla Güncellenmiştir.";
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetForPaging(int newPage)
        {
            int FromWhere = newPage * PageSize - PageSize;
            var helps = _helpService.GetHelpByForPaging(PageSize, FromWhere);
            HelpListModel model = new HelpListModel();
            model.TotalPage = _helpService.GetAllHelp().Count;
            model.CurrentPage = newPage;
            model.TotalPage = Convert.ToInt32(Math.Ceiling((decimal)(_helpService.GetAllHelp().Count / PageSize)));
            using (MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities())
            {
                var constandList = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.CrmYardimKategori).ToList();
                foreach (var help in helps)
                {
                    HelpModel item = new HelpModel { ConstantId = help.ConstantId, Content = help.Content, ID = help.HelpId, RecordDate = help.RecordDate, Subject = help.Subject };
                    if (item.ConstantId > 0)
                    {
                        item.Constant = constandList.FirstOrDefault(x => x.ConstantId == (short)item.ConstantId);
                    }
                    model.HelpModels.Add(item);
                }
            }
            return PartialView("_HelpDataItem", model);
        }
        [HttpPost]
        public JsonResult Delete(int ID)
        {
            var help = _helpService.GetHelpByHelpId(ID);
            _helpService.DeleteHelp(help);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult SearchByText(string SearchText)
        {

            HelpListModel model = new HelpListModel();
            var helps = _helpService.HelpSearchBySearchText(SearchText);
            model.CurrentPage = 1;
            model.TotalPage = 1;
            foreach (var help in helps)
            {
                HelpModel item = new HelpModel { Content = help.Content, ID = help.HelpId, RecordDate = help.RecordDate, Subject = help.Subject };
                model.HelpModels.Add(item);

            }

            return PartialView("_HelpDataItem", model);

        }

        public ActionResult ErrorCreate(string type)
        {
            WebSiteErrorCreateModel model = new WebSiteErrorCreateModel();
            model.UserId = CurrentUserModel.CurrentManagement.UserId;
            if (!string.IsNullOrEmpty(type))
                model.IsAdvice = true;
            else
                model.IsAdvice = false;
            model.IsSolved = false;
            model.IsFirst = false;

            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProblemType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName);
            model.ErrorTypes.Add(new SelectListItem
            {
                Selected = true,
                Text = "Lütfen Seçiniz",
                Value = ""
            });
            foreach (var item in constants)
            {
                model.ErrorTypes.Add(new SelectListItem
                {
                    Text = item.ConstantName,
                    Value = item.ConstantId.ToString()
                });
            }

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ErrorCreate(WebSiteErrorCreateModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                global::MakinaTurkiye.Entities.Tables.Users.WebSiteError webSiteError = new global::MakinaTurkiye.Entities.Tables.Users.WebSiteError();
                webSiteError.Content = model.Content;
                webSiteError.Title = model.Title;
                webSiteError.UserId = (byte)model.UserId;
                webSiteError.IsAdvice = model.IsAdvice;
                webSiteError.IsSolved = model.IsSolved;
                webSiteError.IsFirst = model.IsFirst;
                webSiteError.RecordDate = DateTime.Now;
                if (!string.IsNullOrEmpty(model.ProblemType))
                    webSiteError.ProblemTypeId = Convert.ToInt32(model.ProblemType);
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var inputType = InputFileName.Split('.');
                    string newFileName = inputType[0].Replace(" ", "_") + "_" + DateTime.Now.ToString().Replace(" ", "_").Replace(".", "_").Replace("/", "_").Replace(":", "_") + "." + inputType[1];
                    var ServerSavePath = Path.Combine(Server.MapPath("~/FilesError/") + newFileName);
                    file.SaveAs(ServerSavePath);
                    webSiteError.FilePath = "~/FilesError/" + newFileName;
                }
                _helpService.InsertWebSitError(webSiteError);
                if (model.IsAdvice)
                {
                    return RedirectToAction("Errors", new { @type = "advice" });
                }

                return RedirectToAction("Errors");
            }
            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProblemType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName);
            model.ErrorTypes.Add(new SelectListItem
            {
                Selected = true,
                Text = "Lütfen Seçiniz",
                Value = ""
            });
            foreach (var item in constants)
            {
                model.ErrorTypes.Add(new SelectListItem
                {
                    Text = item.ConstantName,
                    Value = item.ConstantId.ToString()
                });
            }

            return View(model);
        }

        public ActionResult Errors(string type)
        {

            WebSiteErrorModel model = new WebSiteErrorModel();

            bool isAdvice = false;
            if (!string.IsNullOrEmpty(type))
                isAdvice = true;
            var errors = _helpService.GetWebSiteErrors();
            errors = errors.Where(x => x.IsAdvice == isAdvice && x.IsSolved == false).OrderByDescending(x => x.IsFirst).ThenBy(x => x.WebSiteErrorId).ToList();
            List<WebSiteErrorListItem> errorList = new List<WebSiteErrorListItem>();
            model.ProblemTypes.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            var errorTypes = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProblemType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName).ToList();

            foreach (var item in errorTypes)
            {
                model.ProblemTypes.Add(new SelectListItem { Text = item.ConstantName, Value = item.ConstantId.ToString() });
            }
            var userIds = errors.Select(x => x.UserId).ToList();

            var users = entities.Users.Where(x => userIds.Contains(x.UserId)).ToList();
            model.Users.Add(new SelectListItem { Text = "Tümü", Value = "0" });
            foreach (var item in users)
            {
                model.Users.Add(new SelectListItem { Text = item.UserName, Value = item.UserId.ToString() });
            }



            model.WebSiteErrorList = PrepareWebSiteErrorModel(errors);
            model.Type = type;
            return View(model);
        }
        public List<WebSiteErrorListItem> PrepareWebSiteErrorModel(List<global::MakinaTurkiye.Entities.Tables.Users.WebSiteError> errors)
        {


            var websiteErrorList = new List<WebSiteErrorListItem>();

            foreach (var item in errors)
            {
                var entities1 = new MakinaTurkiyeEntities();
                var user = entities1.Users.FirstOrDefault(x => x.UserId == item.UserId);
                string problemTypeText = "Tanımsız";
                if (item.ProblemTypeId.HasValue)
                {
                    var constnat = _constantService.GetConstantByConstantId((short)item.ProblemTypeId.Value);
                    if (constnat != null)
                    {
                        problemTypeText = constnat.ConstantName;
                    }
                }
                websiteErrorList.Add(new WebSiteErrorListItem
                {
                    Content = item.Content,
                    IsFirst = item.IsFirst,
                    Title = item.Title,
                    UserId = item.UserId,
                    UserName = user.UserName,
                    WebSiteErrorId = item.WebSiteErrorId,
                    ProblemTypeText = problemTypeText,
                    FilePath = item.FilePath,
                    IsSolved = item.IsSolved,
                    RecordDate = item.RecordDate,
                    IsWaiting = item.IsWaiting.HasValue ? item.IsWaiting.Value : false
                });
            }
            return websiteErrorList;
        }
        [HttpPost]
        public PartialViewResult Errors(string userId, string problemType, string type, byte problemSolved)
        {
            WebSiteErrorModel model = new WebSiteErrorModel();
            bool isAdvice = false;
            if (!string.IsNullOrEmpty(type))
                isAdvice = true;
            var errors = _helpService.GetWebSiteErrors();
            errors = errors.Where(x => x.IsAdvice == isAdvice).ToList();
            if (problemSolved < 2)
            {
                errors = errors.Where(x => x.IsSolved == Convert.ToBoolean(problemSolved)).ToList();
            }

            errors = errors.OrderByDescending(x => x.IsFirst).ThenBy(x => x.WebSiteErrorId).ToList();
            if (!string.IsNullOrEmpty(userId) && userId != "0")
            {
                errors = errors.Where(x => x.UserId == Convert.ToInt32(userId)).ToList();

            }
            if (!string.IsNullOrEmpty(problemType) && problemType != "0")
                errors = errors.Where(x => x.ProblemTypeId == Convert.ToInt32(problemType)).ToList();

            List<WebSiteErrorListItem> errorList = new List<WebSiteErrorListItem>();

            var websiteErrorList = PrepareWebSiteErrorModel(errors);
            return PartialView("_WebsiteErrorListItem", websiteErrorList);

        }
        public ActionResult ErrorEdit(int id)
        {
            var websiteErorr = _helpService.GetWebSiteErrorByWebSiteErrorId(id);
            WebSiteErrorCreateModel model = new WebSiteErrorCreateModel();
            model.WebSiteErrorId = websiteErorr.WebSiteErrorId;
            model.Title = websiteErorr.Title;
            model.Content = websiteErorr.Content;
            model.IsAdvice = websiteErorr.IsAdvice;
            model.UserId = websiteErorr.UserId;
            model.IsFirst = websiteErorr.IsFirst;
            model.IsSolved = websiteErorr.IsSolved;
            model.IsWaiting = websiteErorr.IsWaiting.HasValue ? websiteErorr.IsWaiting.Value : false;
            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProblemType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName);
            model.ErrorTypes.Add(new SelectListItem
            {
                Text = "Lütfen Seçiniz",
                Value = ""
            });
            foreach (var item in constants)
            {
                var problemType = new SelectListItem
                {
                    Text = item.ConstantName,
                    Value = item.ConstantId.ToString()
                };
                if (websiteErorr.ProblemTypeId == item.ConstantId)
                    problemType.Selected = true;
                model.ErrorTypes.Add(problemType);
            }
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ErrorEdit(WebSiteErrorCreateModel model)
        {
            var webSiteError = _helpService.GetWebSiteErrorByWebSiteErrorId(model.WebSiteErrorId);
            if (ModelState.IsValid)
            {

                webSiteError.Content = model.Content;
                webSiteError.UserId = (byte)model.UserId;
                webSiteError.Title = model.Title;
                webSiteError.UserId = (byte)model.UserId;
                webSiteError.IsSolved = model.IsSolved;
                webSiteError.IsFirst = model.IsFirst;
                webSiteError.IsWaiting = model.IsWaiting;
                if (!string.IsNullOrEmpty(model.ProblemType))
                    webSiteError.ProblemTypeId = Convert.ToInt32(model.ProblemType);

                _helpService.UpdateWebSitError(webSiteError);
                if (model.IsAdvice)
                {
                    return RedirectToAction("Errors", new { @type = "advice" });
                }
                return RedirectToAction("Errors");
            }

            var constants = _constantService.GetConstantByConstantType(ConstantTypeEnum.ProblemType).OrderBy(x => x.Order).ThenBy(x => x.ConstantName);
            model.ErrorTypes.Add(new SelectListItem
            {
                Text = "Lütfen Seçiniz",
                Value = ""
            });
            foreach (var item in constants)
            {
                var problemType = new SelectListItem
                {
                    Text = item.ConstantName,
                    Value = item.ConstantId.ToString()
                };
                if (webSiteError.ProblemTypeId == item.ConstantId)
                    problemType.Selected = true;
                model.ErrorTypes.Add(problemType);
            }
            return View(model);
        }
        public ActionResult MakeFirst(int id)
        {
            var error = _helpService.GetWebSiteErrorByWebSiteErrorId(id);
            error.IsFirst = true;
            _helpService.UpdateWebSitError(error);
            return RedirectToAction("Errors");
        }
        [HttpPost]
        public JsonResult ErrorDelete(int ID)
        {
            var error = _helpService.GetWebSiteErrorByWebSiteErrorId(ID);
            string filePath = Server.MapPath(error.FilePath);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
            if (fileInfo.Exists) {
                fileInfo.Delete();
            }   
            _helpService.DeleteWebSitError(error);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}