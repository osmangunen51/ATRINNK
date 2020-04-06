namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions;
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using MvcContrib.Pagination;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class CreditCardController : BaseController
    {
        internal static readonly string SESSION_USERID = "CreditCardInstallment";

        public IList<CreditCardInstallment> SessionCreditCardInstallmentItems
        {
            get
            {
                if (Session[SESSION_USERID] == null)
                {
                    Session[SESSION_USERID] = new List<CreditCardInstallment>();
                }
                return Session[SESSION_USERID] as List<CreditCardInstallment>;
            }
            set { Session[SESSION_USERID] = value; }
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.KrediKartiListesi;

            var pftypeItems = entities.CreditCards.ToList();
            return View(pftypeItems);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniKrediKartiTanimi;

            SessionCreditCardInstallmentItems = null;
            return View(new CreditCardViewModel { CreditCardInstallmentItems = SessionCreditCardInstallmentItems });
        }

        [HttpPost]
        public ActionResult Create(CreditCardViewModel model)
        {
            try
            {
                var curCreditCard = new CreditCard
                {
                    CreditCardName = model.CreditCard.CreditCardName,
                    VirtualPosId = model.CreditCard.VirtualPosId,
                    Active=model.CreditCard.Active
                };
                entities.CreditCards.AddObject(curCreditCard);
                entities.SaveChanges();

                foreach (var item in SessionCreditCardInstallmentItems.OrderBy(c => c.CreditCardInstallmentId))
                {
                    var curCreditCardInstallment = new CreditCardInstallment
                    {
                        CreditCardValue = item.CreditCardValue,
                        CreditCardId = curCreditCard.CreditCardId,
                        CreditCardCount = item.CreditCardCount
                    };
                    entities.CreditCardInstallments.AddObject(curCreditCardInstallment);
                }
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
                var curCreditCard = entities.CreditCards.SingleOrDefault(c => c.CreditCardId == id);
                entities.CreditCards.DeleteObject(curCreditCard);
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
            PAGEID = PermissionPage.KrediKartiDuzenle;

            SessionCreditCardInstallmentItems.Clear();

            var model = new CreditCardViewModel();
            var curCreditCard = entities.CreditCards.SingleOrDefault(c => c.CreditCardId == id);

            if (curCreditCard != null)
            {
                model.CreditCard = curCreditCard;
                model.CreditCardInstallmentItems = entities.CreditCardInstallments.Where(c => c.CreditCardId == id).ToList();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, CreditCardViewModel model, string[] VirtualPostInstallment)
        {
            try
            {
                var curCreditCard = entities.CreditCards.SingleOrDefault(c => c.CreditCardId == id);
                curCreditCard.CreditCardName = model.CreditCard.CreditCardName;
                curCreditCard.VirtualPosId = model.CreditCard.VirtualPosId;
                curCreditCard.Active = model.CreditCard.Active;

                entities.SaveChanges();

                if (VirtualPostInstallment != null)
                {
                    for (int i = 0; i < VirtualPostInstallment.Length; i++)
                    {
                        var tempVpi = VirtualPostInstallment.GetValue(i);
                        var tempVpiId = tempVpi.ToString().Split('&').GetValue(0).ToByte();
                        var tempVpiValue = tempVpi.ToString().Split('&').GetValue(1).ToDecimal();

                        var vpi = entities.CreditCardInstallments.SingleOrDefault(c => c.CreditCardInstallmentId == tempVpiId);

                        if (tempVpi.ToString().Split('&').Length == 3)
                        {
                            vpi.CreditCardCount = tempVpi.ToString().Split('&').GetValue(2).ToByte();
                        }
                        else
                        {
                            vpi.CreditCardCount = tempVpi.ToString().Split('&').GetValue(2).ToByte();
                        }

                        vpi.CreditCardValue = tempVpiValue;
                    }
                }
                entities.SaveChanges();


                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult SetVirtualPostInstallment(string value, byte count, byte? vpId)
        {
            if (!vpId.HasValue)
            {
                byte id = (SessionCreditCardInstallmentItems.Count + 1).ToByte();
                SessionCreditCardInstallmentItems.Add(new CreditCardInstallment { CreditCardValue = value.ToDecimal(), CreditCardInstallmentId = id, CreditCardCount = count });
                return View("CreditCardInstallment", SessionCreditCardInstallmentItems);
            }
            else
            {
                var vpi = new CreditCardInstallment { CreditCardValue = value.ToDecimal(), CreditCardId = vpId, CreditCardCount = count };
                entities.CreditCardInstallments.AddObject(vpi);
                entities.SaveChanges();

                var vpiItems = entities.CreditCardInstallments.Where(c => c.CreditCardId == vpId).ToList();
                return View("CreditCardInstallmentForEdit", vpiItems);
            }
        }

        public ActionResult DeleteVirtualPostInstallment(byte value, byte? vpId)
        {
            if (!vpId.HasValue)
            {
                SessionCreditCardInstallmentItems.RemoveAt(value - 1);
                return View("CreditCardInstallment", SessionCreditCardInstallmentItems);
            }
            else
            {
                var vpi = entities.CreditCardInstallments.SingleOrDefault(c => c.CreditCardInstallmentId == value);
                entities.DeleteObject(vpi);
                entities.SaveChanges();

                var vpiItems = entities.CreditCardInstallments.Where(c => c.CreditCardId == vpId.Value).ToList();
                return View("CreditCardInstallment", vpiItems);
            }
        }

        public ActionResult CreditCardLog(int? page)
        {
            PAGEID = PermissionPage.KrediKartıLog;
            var cclog = (from c in entities.CreditCardLogs

                         select c).ToList().OrderByDescending(c => c.Date).AsPagination(page ?? 1, 25);

            ViewData["Sayfa"] = cclog.TotalPages;
            ViewData["Curr"] = cclog.PageNumber;
            ViewData["Tot"] = cclog.TotalItems;

            return View(cclog);

        }
    }
}