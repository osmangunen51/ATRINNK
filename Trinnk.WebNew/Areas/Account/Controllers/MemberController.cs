﻿using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using Trinnk.Utilities.Controllers;
using NeoSistem.Trinnk.Web.Models;
using NeoSistem.Trinnk.Web.Models.Authentication;



using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{
    public class MemberController : BaseAccountController
    {
        #region Fields

        private readonly IMemberService _memberService;
        private readonly IStoreService _storeService;

        #endregion

        #region Ctor

        public MemberController(IMemberService memberService, IStoreService storeService)
        {
            this._memberService = memberService;
            this._storeService = storeService;

            this._memberService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
        }

        #endregion
        public ActionResult Index()
        {
            int mainPartyId = AuthenticationUser.Membership.MainPartyId;
            var storeModel = new StoreModel();

            //var storeMainParty = entities.MemberStores.FirstOrDefault(c => c.MemberMainPartyId == mainPartyId);

            var curMember = _memberService.GetMemberByMainPartyId(mainPartyId);
            storeModel.MemberItem = curMember;

            if (curMember.MemberType == (byte)MemberType.Enterprise)
            {
                var store =
                storeModel.Store = _storeService.GetStoreByMainPartyId(mainPartyId);
            }

            return View(storeModel);
        }


    }
}