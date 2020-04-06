namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System.Linq;
    using System.Web.Mvc;

    public class MessagesMTController : BaseController
    { 

        public ActionResult Index()
        {
            var entities = new MakinaTurkiyeEntities();
            var model = entities.MessagesMTs.ToList();


            return View(model);
        }

        public ActionResult Create()
        {

            return View(new NeoSistem.MakinaTurkiye.Management.Models.Entities.MessagesMT());

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(NeoSistem.MakinaTurkiye.Management.Models.Entities.MessagesMT model)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            var packet = new NeoSistem.MakinaTurkiye.Management.Models.Entities.MessagesMT
            {
                MessagesMTName = model.MessagesMTName,
                MessagesMTPropertie = model.MessagesMTPropertie,
                MessagesMTTitle = model.MessagesMTTitle,
                Mail = model.Mail,
                MailPassword = model.MailPassword,
                MailSendFromName = model.MailSendFromName
            };

            entities.MessagesMTs.AddObject(packet);
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditConstants(int id)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            var model = entities.MessagesMTs.Where(c => c.MessagesMTId == id).SingleOrDefault();
            return View(model);

        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditConstants(int id, NeoSistem.MakinaTurkiye.Management.Models.Entities.MessagesMT model)
        {
            MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
            if (model.MessagesMTName == "templatemail")
            {
                MessagesMT newmodel = entities.MessagesMTs.Where(c => c.MessagesMTId == id).SingleOrDefault();
                newmodel.Mail = model.Mail;
                newmodel.MailPassword = model.MailPassword;
                newmodel.MailSendFromName = model.MailSendFromName;
                newmodel.MessagesMTPropertie = model.MessagesMTPropertie;
                newmodel.MessagesMTTitle = model.MessagesMTTitle;
                newmodel.MailContent = model.MailContent;
                entities.SaveChanges();
                var otherMail = entities.MessagesMTs.Where(x => x.MailContent != null && x.MessagesMTName != "templatemail");

                foreach (var item in otherMail)
                {
                    using (var entities1 = new MakinaTurkiyeEntities())
                    {
                        var memberMessage = entities1.MessagesMTs.FirstOrDefault(x => x.MessagesMTId == item.MessagesMTId);
                        memberMessage.MessagesMTPropertie = model.MessagesMTPropertie.Replace("#icerik#", item.MailContent);
                        entities1.SaveChanges();
                    }
                }
                var mailConsant = entities.Constants.Where(x => x.ConstantType == 14 || x.ConstantType == 15);
                foreach (var item in mailConsant.ToList())
                {
                    using (var entities1 = new MakinaTurkiyeEntities())
                    {
                        var constant = entities1.Constants.FirstOrDefault(x => x.ConstantId == item.ConstantId);

                        constant.ContstantPropertie = model.MessagesMTPropertie.Replace("#icerik#", constant.ConstantMailContent);
                        entities1.SaveChanges();
                    }

                }
            }
            else
            {
                MessagesMT newmodel = entities.MessagesMTs.Where(c => c.MessagesMTId == id).SingleOrDefault();
                newmodel.Mail = model.Mail;
                newmodel.MailPassword = model.MailPassword;
                newmodel.MailSendFromName = model.MailSendFromName;
                var messageTemplate = entities.MessagesMTs.FirstOrDefault(c => c.MessagesMTName == "templatemail");
                var content = messageTemplate.MessagesMTPropertie;
                content = content.Replace("#icerik#", model.MailContent);
                newmodel.MessagesMTPropertie = content;
                newmodel.MessagesMTTitle = model.MessagesMTTitle;
                newmodel.MailContent = model.MailContent;
                entities.SaveChanges();

            }
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
