namespace NeoSistem.Trinnk.Management.Controllers
{
    using Models;
    using NeoSistem.Trinnk.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class BankAccountController : BaseController
    {
        public ActionResult Index()
        {
            PAGEID = PermissionPage.HesapListesi;

            var pftypeItems = entities.Accounts.ToList();
            return View(pftypeItems);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.HesapOlustur;

            return View(new Account());
        }

        [HttpPost]
        public ActionResult Create(Account model)
        {
            try
            {
                var curPacketFeatureType = new Account
                {
                    BankName = model.BankName,
                    AccountNo = model.AccountNo,
                    AccountName = model.AccountName,
                    BranchCode = model.BranchCode,
                    BranchName = model.BranchName,
                    IbanNo = model.IbanNo,
                };
                entities.Accounts.AddObject(curPacketFeatureType);
                entities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(byte id)
        {
            try
            {
                var curPacketFeatureType = entities.Accounts.SingleOrDefault(c => c.AccountId == id);
                entities.Accounts.DeleteObject(curPacketFeatureType);
                entities.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
            return Json(true);
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.HesapDuzenle;

            var curActivityType = entities.Accounts.SingleOrDefault(c => c.AccountId == id);
            if (curActivityType != null)
            {
                return View(curActivityType);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, Account model)
        {
            try
            {
                var curPacketFeatureType = entities.Accounts.SingleOrDefault(c => c.AccountId == id);
                curPacketFeatureType.BankName = model.BankName;
                curPacketFeatureType.AccountName = model.AccountName;
                curPacketFeatureType.AccountNo = model.AccountNo;
                curPacketFeatureType.BranchName = model.BranchName;
                curPacketFeatureType.BranchCode = model.BranchCode;
                curPacketFeatureType.IbanNo = model.IbanNo;

                entities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}