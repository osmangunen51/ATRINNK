namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Services.Users;
    using Models;
    using Models.Authentication;
    using System;
    using System.Web.Mvc;

    public class AccountController : Controller
    {
        IUserInformationService _userInformationService;

        public AccountController(IUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            EnterpriseFormsAuthentication.SingOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult Login(AccountModel collection, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Data.User dataUser = new Data.User();
                    Classes.User curUser = dataUser.Login(collection.UserName, collection.UserPass);
                    if (curUser != null)
                    {


                        EnterpriseFormsAuthentication.CreateFormsAuthenticationTicket(curUser.UserName, "Admin");
                        CurrentUserModel.CurrentManagement = curUser;

                        CurrentUserModel.CurrentManagement.Permissions = dataUser.GetPermissions(curUser.UserId).AsCollection<Classes.Permission>();

                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Tüm alanların doldurulması gerekiyor.");
                    return View();
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}