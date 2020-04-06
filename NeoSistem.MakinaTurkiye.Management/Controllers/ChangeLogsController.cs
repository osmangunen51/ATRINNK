using global::MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Services.Common;
using NeoSistem.MakinaTurkiye.Management.Models;
using System;
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

            return View();
        }

        public ActionResult Store(int? page)
        {

            int pageSize = 30;
   
            int skipRows = 0;
            ViewData["page"] = page == null ? 0 : (int)page;
            FilterModel<global::MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory> filterModel = new FilterModel<global::MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory>();
            int totalCount = _storeChangeHistoryService.GetAllStoreChangeHistory().ToList().Count;
            filterModel.TotalRecord = totalCount;
            filterModel.PageDimension = pageSize;
            filterModel.Source = _storeChangeHistoryService.GetAllStoreChangeHistory().OrderByDescending(x => x.StoreChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();
            filterModel.CurrentPage = 1;

            return View(filterModel);

        }

        public ActionResult Phone(int? page)
        {
            int pageSize = 30;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(_phoneChangeHistoryService.GetAllPhoneChangeHistory().ToList().Count / (float)pageSize));

            var phoneChangeHistories = _phoneChangeHistoryService.GetAllPhoneChangeHistory().OrderByDescending(x => x.PhoneChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();


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

        public ActionResult Address(int? page)
        {
            int pageSize = 30;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;

            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(_addressChangeHistoryService.GetAllAddressChangeHistory().ToList().Count / (float)pageSize));

            var addressChangeHistories = _addressChangeHistoryService.GetAllAddressChangeHistory().OrderByDescending(x => x.AddressChangeHistoryId).Skip(skipRows).Take(pageSize).ToList();

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