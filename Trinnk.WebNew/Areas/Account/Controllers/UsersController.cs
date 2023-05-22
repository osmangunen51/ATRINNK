using Trinnk.Entities.Tables.Members;
using Trinnk.Services.Members;
using Trinnk.Services.Messages;
using Trinnk.Services.Stores;
using Trinnk.Utilities.Controllers;
using Trinnk.Utilities.MailHelpers;
using NeoSistem.Trinnk.Web.Areas.Account.Constants;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Users;
using NeoSistem.Trinnk.Web.Models;
using NeoSistem.Trinnk.Web.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{
    public class UsersController : BaseAccountController
    {
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMemberService _memberService;
        private readonly IMessagesMTService _messageMTService;
        private readonly IStoreService _storeService;


        public UsersController(IMemberStoreService memberStoreService, IMemberService memberService,
            IMessagesMTService messageMTService,
            IStoreService storeService)
        {
            this._memberStoreService = memberStoreService;
            this._memberService = memberService;
            this._messageMTService = messageMTService;
            this._storeService = storeService;

            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
        }

        public ActionResult Index()
        {
            MTUserListModelView model = new MTUserListModelView();
            int mainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(mainPartyId);
            if (memberStore.MemberStoreType != (byte)MemberStoreType.Owner)
            {
                model.IsAllowedToSee = false;

            }
            else
            {
                model.IsAllowedToSee = true;
                int storeMainPartyId = memberStore.StoreMainPartyId.Value;
                var memberMainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(storeMainPartyId).Where(x => x.MemberMainPartyId != mainPartyId).Select(x => x.MemberMainPartyId);
                var members = _memberService.GetMembersByMainPartyIds(memberMainPartyIds.ToList());

                foreach (var item in members)
                {
                    var mainParty = _memberService.GetMainPartyByMainPartyId(item.MainPartyId);
                    model.MTUserItems.Add(new MTUserItemModel { Active = item.Active, Email = item.MemberEmail, MainPartyId = item.MainPartyId, NameSurname = item.MemberName + " " + item.MemberSurname, Password = item.MemberPassword, RecordDate = mainParty.MainPartyRecordDate });
                }
            }

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.Users);

            return View(model);
        }
        public ActionResult Add()
        {
            MTUserFormModelView model = new MTUserFormModelView();
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            if (memberStore.MemberStoreType == (byte)MemberStoreType.Helper)
            {
                return RedirectToAction("Index");
            }
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.Users);
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(MTUserFormModel formModel)
        {
            int memberMainPartyId = AuthenticationUser.Membership.MainPartyId;

            if (ModelState.IsValid && memberMainPartyId != 0)
            {
                var userModel = formModel;

                var mainParty = new MainParty
                {
                    Active = false,
                    MainPartyType = (byte)MainPartyType.FastMember,
                    MainPartyRecordDate = DateTime.Now,
                    MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.MemberName.ToLower() + " " + userModel.MemberSurname.ToLower())
                };
                _memberService.InsertMainParty(mainParty);

                var member = new Member();
                int mainPartyId = mainParty.MainPartyId;
                member.MainPartyId = mainPartyId;
                member.MemberEmail = userModel.MemberEmail;
                member.MemberPassword = userModel.MemberPassword;
                member.MemberName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.MemberName.ToLower());
                member.MemberSurname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(userModel.MemberSurname);
                member.MemberType = (byte)MemberType.Enterprise;
                member.Active = true;
                member.FastMemberShipType = (byte)FastMembershipType.Normal;
                member.Gender = Convert.ToBoolean(userModel.Gender);
                member.MemberEmail = userModel.MemberEmail;
                _memberService.InsertMember(member);


                string memberNo = "##";
                for (int i = 0; i < 7 - mainParty.MainPartyId.ToString().Length; i++)
                {
                    memberNo = memberNo + "0";
                }
                memberNo = memberNo + mainParty.MainPartyId;

                var memberForMemberNo = _memberService.GetMemberByMainPartyId(mainPartyId);
                member.MemberNo = memberNo;
                _memberService.UpdateMember(member);

                var storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId;
                var store = _storeService.GetStoreByMainPartyId(storeMainPartyId.Value);
                var memberStore = new MemberStore();
                memberStore.MemberMainPartyId = mainParty.MainPartyId;
                memberStore.StoreMainPartyId = storeMainPartyId;
                memberStore.MemberStoreType = (byte)MemberStoreType.Helper;
                _memberStoreService.InsertMemberStore(memberStore);
                var mailMessageTemplate = _messageMTService.GetMessagesMTByMessageMTName("kullanicibilgileri");
                string content = mailMessageTemplate.MailContent.Replace("#email#", userModel.MemberEmail).Replace("#sifre#", userModel.MemberPassword).Replace("#firmaadi#", store.StoreName);
                var toMails = new List<string>();
                toMails.Add(userModel.MemberEmail);
                MailHelper mailHelper = new MailHelper();
                mailHelper.Content = content;
                mailHelper.FromMail = mailMessageTemplate.Mail;
                mailHelper.Password = mailMessageTemplate.MailPassword;
                mailHelper.ToMails = toMails;

                mailHelper.Subject = mailMessageTemplate.MessagesMTTitle;
                mailHelper.FromName = mailMessageTemplate.MailSendFromName;
                mailHelper.Send();

                TempData["success"] = true;
                return RedirectToAction("add");
            }
            else
            {
                MTUserFormModelView model = new MTUserFormModelView();
                model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.StoreSettings, (byte)LeftMenuConstants.StoreSettingGeneralInfo.Users);
                return View(model);
            }
        }

    }
}