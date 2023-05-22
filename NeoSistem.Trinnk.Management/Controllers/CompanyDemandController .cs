using Trinnk.Services.Members;
using Trinnk.Services.Stores;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.Trinnk.Management.Models;
using NeoSistem.Trinnk.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace NeoSistem.Trinnk.Management.Controllers
{

    public class CompanyDemandController : BaseController
    {
        #region Fields

        private readonly IMemberService _memberService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;

        #endregion

        #region Ctor

        public CompanyDemandController(IMemberService memberService, IMemberStoreService memberStoreService, IStoreService storeService)
        {
            this._memberService = memberService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
        }

        #endregion

        #region Methods

        public ActionResult Index(int? page, int? status, int? id, string pagetype)
        {
            var entities = new TrinnkEntities();


            if (status != null)
            {

                int ID = Convert.ToInt32(id);
                var demandUp = entities.CompanyDemandMemberships.First(x => x.CompanyDemandMembershipId == ID);
                demandUp.Status = Convert.ToInt32(status);
                entities.SaveChanges();
                ViewData["success"] = true;
            }

            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            List<CompanyDemandMembership> messageList = new List<CompanyDemandMembership>();
            if (pagetype == "DemandsForPacket")
            {

                ViewData["pageNumbers"] = Convert.ToInt32(entities.CompanyDemandMemberships.Where(c => c.isDemandForPacket == true).ToList().Count / pageSize);

            }
            else
            {
                ViewData["pageNumbers"] = Convert.ToInt32(entities.CompanyDemandMemberships.ToList().Count / pageSize);
                messageList = entities.CompanyDemandMemberships.OrderByDescending(x => x.CompanyDemandMembershipId).ThenBy(x => x.DemandDate).Skip(skipRows).Take(pageSize).ToList();
            }
            return View(messageList);

        }

        [HttpPost]
        public JsonResult DeleteDemand(int id)
        {
            var demand = entities.CompanyDemandMemberships.SingleOrDefault(x => x.CompanyDemandMembershipId == id);
            entities.CompanyDemandMemberships.DeleteObject(demand);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DemandForPacket(int? page, int? status, int? id, string pagetype)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            if (status != null)
            {

                int ID = Convert.ToInt32(id);
                var demandUp = entities.CompanyDemandMemberships.First(x => x.CompanyDemandMembershipId == ID);
                demandUp.Status = Convert.ToInt32(status);
                entities.SaveChanges();
                ViewData["success"] = true;
            }
            var demandsList = entities.CompanyDemandMemberships.Where(x => x.isDemandForPacket == true).OrderByDescending(x => x.CompanyDemandMembershipId).ThenBy(x => x.DemandDate).Skip(skipRows).Take(pageSize).ToList();
            var packetForDemands = new List<DemandsForPacketModel>();
            foreach (var item in demandsList.ToList())
            {
                DemandsForPacketModel itemDemand = new DemandsForPacketModel();
                itemDemand.PacketForDemandModelId = item.CompanyDemandMembershipId;
                itemDemand.NameSurname = item.NameSurname;
                itemDemand.Phone = item.Phone;
                itemDemand.Email = item.Email;
                itemDemand.DemandDate = item.DemandDate;
                itemDemand.Status = Convert.ToInt32(item.Status);

                string memberNo = item.Statement.ToString().Split(' ')[0];
                int memberMainPartyId = _memberService.GetMemberByMemberNo(memberNo).MainPartyId;
                itemDemand.MemberMainPartyId = memberMainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
                int storeMainPartyId = Convert.ToInt32(memberStore.StoreMainPartyId);
                var store = _storeService.GetStoreByMainPartyId(storeMainPartyId);
                itemDemand.WebUrl = store.StoreWeb;
                itemDemand.StoreName = item.CompanyName;
                itemDemand.StoreMainPartyId = storeMainPartyId;
                packetForDemands.Add(itemDemand);
            }
            return View(packetForDemands);

        }

        #endregion
    }
}