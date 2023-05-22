namespace NeoSistem.Trinnk.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.Trinnk.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class MobileMessageController : BaseController
    {
        //
        // GET: /MessagesMT/

        public ActionResult Index()
        {
            var entities = new TrinnkEntities();
            var model = entities.MobileMessages.ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new NeoSistem.Trinnk.Management.Models.Entities.MobileMessage());
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(NeoSistem.Trinnk.Management.Models.Entities.MobileMessage model, string MessageType)
        {
            TrinnkEntities entities = new TrinnkEntities();
            var packet = new NeoSistem.Trinnk.Management.Models.Entities.MobileMessage
            {
                MessageName = model.MessageName,
                MessageContent = model.MessageContent,
                MessageType = Convert.ToByte(MessageType)

            };
            entities.MobileMessages.AddObject(packet);
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditMessage(int id)
        {
            TrinnkEntities entities = new TrinnkEntities();
            var model = entities.MobileMessages.Where(c => c.ID == id).SingleOrDefault();
            return View(model);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditMessage(int id, NeoSistem.Trinnk.Management.Models.Entities.MobileMessage model, string MessageType)
        {
            TrinnkEntities entities = new TrinnkEntities();
            MobileMessage newmodel = entities.MobileMessages.Where(c => c.ID == model.ID).SingleOrDefault();
            newmodel.MessageName = model.MessageName;
            newmodel.MessageContent = model.MessageContent;
            newmodel.MessageType = Convert.ToByte(MessageType);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public string Delete(int id)
        {
            TrinnkEntities entities = new TrinnkEntities();
            var messages = entities.MessagesMTs.First(x => x.MessagesMTId == id);
            entities.MessagesMTs.DeleteObject(messages);
            entities.SaveChanges();
            return "silindi";
        }

    }
}
