namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class AddressTypeController : BaseController
    {
        const string STARTCOLUMN = "AddressTypeId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        static ICollection<AddressTypeModel> collection = null;

        public ActionResult Index()
        {
            PAGEID = PermissionPage.AdresTipiListesi;

            int total = 0;
            var curAddressType = new Classes.AddressType();
            collection = curAddressType.GetDataTable().AsCollection<AddressTypeModel>();

            var model = new FilterModel<AddressTypeModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return View(model);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniAdresTipi;

            ViewData["Title"] = "Makina Türkiye - Ürün Grubu Ekle ";
            var model = new AddressTypeModel { };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AddressTypeModel model)
        {

            try
            {
                var curAddressType = new Classes.AddressType
                {
                    AddressTypeName = model.AddressTypeName,
                };
                curAddressType.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var curAddressType = new Classes.AddressType();
            return Json(new { m = curAddressType.Delete(id) });
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.AdresTipiDüzenle;

            ViewData["Title"] = "Makina Türkiye - Ürün Grubu Ekle ";
            var curAddressType = new Classes.AddressType();
            if (curAddressType.LoadEntity(id))
            {
                var model = new AddressTypeModel();
                UpdateClass(curAddressType, model);

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, AddressTypeModel model)
        {

            try
            {
                var curAddressType = new Classes.AddressType();
                if (curAddressType.LoadEntity(id))
                {
                    curAddressType.AddressTypeId = id;
                    curAddressType.AddressTypeName = model.AddressTypeName;

                    curAddressType.Save();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

    }
}