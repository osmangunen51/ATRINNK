namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Core;
    using global::MakinaTurkiye.Services.Users;
    using Microsoft.AspNetCore.SignalR;
    using Models;
    using Models.Authentication;
    using NeoSistem.MakinaTurkiye.Management.ActionFilters;
    using NeoSistem.MakinaTurkiye.Management.Extentions;
    using NeoSistem.MakinaTurkiye.Management.Hubs;
    using System;
    using System.Web.Mvc;

    public class AccountController : SystemBaseController
    {
        IUserInformationService _userInformationService;
        //private readonly IHubContext<Hub<NeoSistem.MakinaTurkiye.Management.Hubs.System>> _hubContext;


        //public AccountController(IUserInformationService userInformationService, IHubContext<Hub<System>> hubContext)
        //{
        //    _userInformationService = userInformationService;
        //    _hubContext = hubContext;
        //}

        //public void SendMessageAsync(string message, int UserID)
        //{
        //    _hubContext.Clients.All.SendAsync(message, UserID);
        //}


        public AccountController(IUserInformationService userInformationService)
        {
            _userInformationService = userInformationService;
        }


        [CompressFilter]
        [WhitespaceFilter]

        public ActionResult Login()
        {
            NeoSistem.MakinaTurkiye.Management.Models.AccountModel model = new AccountModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            EnterpriseFormsAuthentication.SingOut();
            return RedirectToAction("Login", "Account");
        }


        
        [CompressFilter]
        [WhitespaceFilter]
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
                        //if (curUser.UserName=="YÖNETİCİ")
                        //{
                        //    this.SendMessageAsync("Günaydın Gençler", 0);
                        //}
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