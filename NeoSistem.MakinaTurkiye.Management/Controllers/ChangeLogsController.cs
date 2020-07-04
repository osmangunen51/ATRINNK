using global::MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Common;
using NeoSistem.MakinaTurkiye.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class ChangeLogsController : BaseController
    {
        #region Fields

        private readonly IStoreChangeHistoryService _storeChangeHistoryService;
        private readonly IPhoneChangeHistoryService _phoneChangeHistoryService;
        private readonly IAddressChangeHistoryService _addressChangeHistoryService;

        #endregion

        #region Ctor

        public ChangeLogsController(IStoreChangeHistoryService storeChangeHistoryService, IPhoneChangeHistoryService phoneChangeHistoryService, IAddressChangeHistoryService addressChangeHistoryService)
        {
            this._storeChangeHistoryService = storeChangeHistoryService;
            this._phoneChangeHistoryService = phoneChangeHistoryService;
            this._addressChangeHistoryService = addressChangeHistoryService;
        }

        #endregion

        #region Methods

        public ActionResult index()
        {
            int pageSize = 50;
            int totalRecord;
            var result = _storeChangeHistoryService.SP_StoreInfoChange(pageSize,1, out totalRecord);
            FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult> model = new FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult>();
            model.PageDimension = pageSize;
            model.Source = result;
            model.CurrentPage = 1;
            model.TotalRecord = totalRecord;

            return View(model);
        }

        [HttpPost]
        public PartialViewResult Index(int page)
        {
            int pageSize = 50;
            int totalRecord;
            var result = _storeChangeHistoryService.SP_StoreInfoChange(pageSize, page, out totalRecord);
            FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult> model = new FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult>();
            model.PageDimension = pageSize;
            model.Source = result;
            model.CurrentPage = page;
            model.TotalRecord = totalRecord;
            return PartialView("_StoreChangeList", model);
        }

        public ActionResult Store(int? page, int? mainpartyId)
        {

            int pageSize = 30;
   
            int skipRows = 0;
            ViewData["page"] = page == null ? 0 : (int)page;
            
            
            FilterModel<global::MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory> filterModel = new FilterModel<global::MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory>();
            var result = new List<StoreChangeHistory>();
            if (mainpartyId != null)
                result = _storeChangeHistoryService.GetAllStoreChangeHistory().Where(x => x.MainPartyId == mainpartyId).ToList();
            else
                result = _storeChangeHistoryService.GetAllStoreChangeHistory().ToList();

            int totalCount = result.ToList().Count;
            filterModel.TotalRecord = totalCount;
            filterModel.PageDimension = pageSize;
            filterModel.Source = result.OrderByDescending(x => x.StoreChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();
            filterModel.CurrentPage = 1;

            return View(filterModel);

        }

        public ActionResult Phone(int? page, int? mainPartyId)
        {
            int pageSize = 30;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            var result =new List<global::MakinaTurkiye.Entities.Tables.Common.PhoneChangeHistory>();
            if (mainPartyId != null)
                result = _phoneChangeHistoryService.GetAllPhoneChangeHistory().Where(x => x.MainPartyId == mainPartyId).ToList();
            else
                result = _phoneChangeHistoryService.GetAllPhoneChangeHistory().ToList();
            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(result.ToList().Count / (float)pageSize));
            var phoneChangeHistories = result.OrderByDescending(x => x.PhoneChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();
            return View(phoneChangeHistories);

        }

        public ActionResult storedetail(int storeChangeHistoryId)
        {
            var storeChange = _storeChangeHistoryService.GetStoreChangeHistoryByStoreChangeHistoryId(storeChangeHistoryId);

            return View(storeChange);
        }

        public ActionResult phonedetail(int phoneChangeHistoryId)
        {
            var phoneChange = _phoneChangeHistoryService.GetPhoneChangeHistoryByPhoneChangeHistoryId(phoneChangeHistoryId);
            return View(phoneChange);
        }

        public ActionResult Address(int? page, int? mainPartyId)
        {
            int pageSize = 30;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            var result = new List<global::MakinaTurkiye.Entities.Tables.Common.AddressChangeHistory>();

            if (mainPartyId != null)
            {
                result = _addressChangeHistoryService.GetAllAddressChangeHistory().Where(x => x.MainPartyId == mainPartyId).ToList();
            }
            else
                result = _addressChangeHistoryService.GetAllAddressChangeHistory().ToList();

            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(result.Count / (float)pageSize));

            var addressChangeHistories =result.OrderByDescending(x => x.AddressChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();

            return View(addressChangeHistories);
        }

        [HttpPost]
        public JsonResult addressChangeHistoryDelete(int id)
        {
            var addressChangeHistory = _addressChangeHistoryService.GetAddressChangeHistory(id);
            _addressChangeHistoryService.DeleteAddressChangeHistory(addressChangeHistory);
            return Json(true);
        }

        [HttpPost]
        public JsonResult phoneChangeHistoryDelete(int id)
        {
            var phoneChangeHistory = _phoneChangeHistoryService.GetPhoneChangeHistoryByPhoneChangeHistoryId(id);
            _phoneChangeHistoryService.DeletePhoneChangeHistory(phoneChangeHistory);
            return Json(true);
        }

        [HttpPost]
        public ActionResult storeChangeHistoryDelete(int id)
        {
            var storeChangeHistory = _storeChangeHistoryService.GetStoreChangeHistoryByStoreChangeHistoryId(id);
            _storeChangeHistoryService.DeleteStoreChangeHistory(storeChangeHistory);
            return Json(true);
        }

        #endregion
    }
}