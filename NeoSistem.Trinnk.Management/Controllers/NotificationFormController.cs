namespace NeoSistem.Trinnk.Management.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class NotificationFormController : BaseController
    {
        public ActionResult Index()
        {
            PAGEID = PermissionPage.BildirimListesi;

            IList<NotificationFormModel> NotificationForm = new List<NotificationFormModel>();

            var data = (from nf in entities.NotificationForms join m in entities.Members on nf.MainPartyId equals m.MainPartyId select new { m.MemberName, m.MemberSurname, nf.NotificationFormSubject, nf.RecordDate, nf.IsRead, nf.NotificationFormDescription, nf.NotificationFormId });

            foreach (var item in data)
            {
                NotificationForm.Add(new NotificationFormModel
                {
                    MemberName = item.MemberName,
                    MemberSurname = item.MemberSurname,
                    IsRead = item.IsRead.Value,
                    NotificationFormDescription = item.NotificationFormDescription,
                    NotificationFormSubject = item.NotificationFormSubject,
                    RecordDate = item.RecordDate.Value,
                    NotificationFormId = item.NotificationFormId
                });
            }
            return View(NotificationForm);
        }

        public ActionResult View(int id)
        {
            PAGEID = PermissionPage.BildirimGoruntuleme;

            try
            {
                var notificationForm = entities.NotificationForms.SingleOrDefault(c => c.NotificationFormId == id);
                notificationForm.IsRead = true;
                entities.SaveChanges();

                var model = new NotificationFormModel();
                model.IsRead = notificationForm.IsRead.Value;
                model.MemberName = notificationForm.Member.MemberName;
                model.MemberSurname = notificationForm.Member.MemberSurname;
                model.NotificationFormDescription = notificationForm.NotificationFormDescription;
                model.NotificationFormSubject = notificationForm.NotificationFormSubject;
                model.RecordDate = notificationForm.RecordDate.Value;

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}