namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Linq;
    using System.Web.Mvc;

    public class MobileMessageController : BaseController
    {
        //
        // GET: /MessagesMT/

        public ActionResult Index()
        {
            var entities = new MakinaTurkiyeEntities();
            var model = entities.MobileMessages.ToList();

            return View(model);
        }

        public ActionResult Create()
        {

            return View(new NeoSistem.MakinaTurkiye.Management.Models.Entities.MobileMessage());

        }

        [ValidateInput(false)]
        [HttpPost] 
        public ActionResult Create(NeoSistem.MakinaTurkiye.Management.Models.Entities.MobileMessage model,string MessageType)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            var packet = new NeoSistem.MakinaTurkiye.Management.Models.Entities.MobileMessage
            {
                MessageName = model.MessageName,
                MessageContent = model.MessageContent,
                MessageType=Convert.ToByte(MessageType)

            };
            entities.MobileMessages.AddObject(packet);
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditMessage(int id)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            var model = entities.MobileMessages.Where(c => c.ID == id).SingleOrDefault();
            return View(model);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditMessage(int id, NeoSistem.MakinaTurkiye.Management.Models.Entities.MobileMessage model,string MessageType)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            MobileMessage newmodel = entities.MobileMessages.Where(c => c.ID == model.ID).SingleOrDefault();
            newmodel.MessageName = model.MessageName;
            newmodel.MessageContent = model.MessageContent;
            newmodel.MessageType = Convert.ToByte(MessageType);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public string Delete(int id)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            var messages = entities.MessagesMTs.First(x => x.MessagesMTId == id);
            entities.MessagesMTs.DeleteObject(messages);
            entities.SaveChanges();
            return "silindi";
        }

    }
}
